using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PozyczkoPrzypominajka.Models;
using PozyczkoPrzypominajkaV2.Data;
using PozyczkoPrzypominajkaV2.Models.Loan;
using PozyczkoPrzypominajkaV2.Services;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PozyczkoPrzypominajkaV2.Pages.Loans
{
	public class CreateModel : PageModel
	{
		public CreateModel(ApplicationDbContext context, UserManager<AppUser> userManager, IEnvironment environment)
		{
			this.context = context;
			this.userManager = userManager;
			this.environment = environment;
		}

		private readonly ApplicationDbContext context;
		private readonly UserManager<AppUser> userManager;
		private readonly IEnvironment environment;

		public LoanViewModel LoanVM { get; set; }

		[BindProperty]
		public LoanInputModel LoanIM { get; set; }

		public async Task<IActionResult> OnGetAsync()
		{
			var me = await userManager.GetUserAsync(User);
			LoanVM.Amount = 0;
			LoanVM.Date = environment.Now();
			LoanVM.GiverId = me.Id;
			LoanVM.GiverName = me.ToString();
			LoanVM.Amount = 0;

			var receivers = await context.Users
					.AsNoTracking()
					.Select(u => new Tuple<string, string>(u.Id, u.ToString()))
					.ToListAsync();

			LoanVM.Init(receivers);

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
				var giver = await userManager.FindByIdAsync(LoanIM.GiverID);
				var receiver = await userManager.FindByIdAsync(LoanIM.ReceiverID);

				var newLoan = new Loan(
					loanID: null,
					date: LoanIM.DisbursementDate,
					giverID: giver.Id.ToString(),
					giver: giver,
					receiverID: receiver.Id,
					receiver: receiver,
					amount: LoanIM.Amount,
					repaymentDate: LoanIM.RepaymentDate,
					repaymentAmount: LoanIM.RepaymentAmount,
					status: LoanIM.Status,
					notifications: null);

				context.Loans.Add(newLoan);
				await context.SaveChangesAsync();
			}

			return RedirectToPage("./Index");
		}
	}
}