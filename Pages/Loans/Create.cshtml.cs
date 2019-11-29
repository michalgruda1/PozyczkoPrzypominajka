using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PozyczkoPrzypominajka.Models;
using PozyczkoPrzypominajkaV2.Data;
using PozyczkoPrzypominajkaV2.Models.Loan;
using PozyczkoPrzypominajkaV2.Services;
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
			var receivers = context.Users.Select(u => u.Id != me.Id);

			LoanVM.Amount = 0;
			LoanVM.DisbursementDate = environment.Now();
			LoanVM.GiverList = await context.Users.AsNoTracking()
				.Where(u => u.Id == me.Id) // tylko zalogowany user jest na liście 
				.Select(u => new SelectListItem()
				{
					Text = u.ToString(),
					Value = u.Id,
					Selected = u.Id == me.Id, // zalogowany user jest wybrany
				})
				.ToListAsync();
			LoanVM.ReceiverList = await context.Users.AsNoTracking()
				.Where(u => u.Id != me.Id) // wszyscy prócz zalogowanego są na liście
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