using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PozyczkoPrzypominajka.Models;
using PozyczkoPrzypominajkaV2.Data;
using PozyczkoPrzypominajkaV2.Models;
using PozyczkoPrzypominajkaV2.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PozyczkoPrzypominajkaV2.Pages.Loans
{
	public class CreateModel : PageModel
	{
		[BindProperty]
		public LoanEditModel LoanEM { get; set; } = new LoanEditModel() { };
		public LoanViewModel LoanVM { get; set; } = new LoanViewModel() { };

		private readonly ApplicationDbContext context;
		private readonly UserManager<AppUser> userManager;
		private readonly IEnvironment environment;

		public CreateModel(ApplicationDbContext context, UserManager<AppUser> userManager, IEnvironment environment)
		{
			this.context = context;
			this.userManager = userManager;
			this.environment = environment;
		}

		public async Task<IActionResult> OnGetAsync()
		{
			var me = await userManager.GetUserAsync(User);

			LoanVM.Amount = 0;
			LoanVM.DisbursementDate = environment.Now();

			LoanVM.GiverList = new List<SelectListItem>();
			LoanVM.GiverList.Add(new SelectListItem(text: me.ToString(), value: me.Id, selected: true));

			var loanUtilities = new LoanUtilities(context, userManager);
			var receivers = loanUtilities.GetPossibleLoanReceiversForUser(me);

			LoanVM.ReceiverList = await receivers.AsNoTracking()
				.Select(u => new SelectListItem()
				{
					Text = u.ToString(),
					Value = u.Id
				})
				.ToListAsync();

			LoanVM.Amount = 0;

			return Page();
		}

		public async Task<IActionResult> OnPostAsync()
		{
			if (!ModelState.IsValid)
			{
				return Page();
			}
			else
			{
				// TODO zmień identyfikatory przesyłane POST-em z Guid na emaile (bezpieczeństwo danych)
				// TODO wymuś na modelu, by udzielającym był aktualny użytkownik
				var giver = await userManager.FindByIdAsync(LoanEM.GiverId);
				var receiver = await userManager.FindByIdAsync(LoanEM.ReceiverId);

				var newLoan = new Loan(
					loanID: null,
					date: LoanEM.DisbursementDate,
					giverID: giver.Id.ToString(),
					giver: giver,
					receiverID: receiver.Id,
					receiver: receiver,
					amount: LoanEM.Amount,
					repaymentDate: LoanEM.RepaymentDate,
					repaymentAmount: LoanEM.RepaymentAmount,
					status: StatusEnum.Unpaid,
					notifications: null);

				context.Loans.Add(newLoan);
				await context.SaveChangesAsync();
			}

			return RedirectToPage("./Index");
		}
	}
}