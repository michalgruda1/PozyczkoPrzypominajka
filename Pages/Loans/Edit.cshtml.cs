using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PozyczkoPrzypominajka.Models;
using PozyczkoPrzypominajkaV2.Data;
using PozyczkoPrzypominajkaV2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PozyczkoPrzypominajkaV2.Pages.Loans
{
	public class EditModel : PageModel
	{
		private readonly ApplicationDbContext context;
		private readonly UserManager<AppUser> userManager;

		[BindProperty]
		public LoanEditModel LoanEM { get; set; } = new LoanEditModel() { };
		public LoanViewModel LoanVM { get; set; } = new LoanViewModel() { };

		public EditModel(ApplicationDbContext context, UserManager<AppUser> userManager)
		{
			this.context = context;
			this.userManager = userManager;
		}

		public async Task<IActionResult> OnGetAsync(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var me = await userManager.GetUserAsync(User);
			var loanUtilities = new LoanUtilities(context, userManager);

			// Albowiem nie możesz edytować nie swoich pożyczek
			var loans = loanUtilities.GetLoansForUser(me);
			var loan = loans
				.Where(l => l.LoanID == id)
				.FirstOrDefault();
			if (loan == null)
			{
				return NotFound();
			}

			LoanVM.LoanId = loan.LoanID;
			LoanVM.DisbursementDate = loan.Date;
			LoanVM.GiverList = new List<SelectListItem>()
			{
				new SelectListItem()
				{
					Text = me.ToString(),
					Value = me.Id,
					Selected = true
				}
			};
			LoanVM.ReceiverList = await loanUtilities.GetPossibleLoanReceiversForUser(me)
				.Select(u => new SelectListItem()
				{
					Text = u.ToString(),
					Value = u.Id,
					Selected = loan.ReceiverID == u.Id
				})
				.ToListAsync();
			LoanVM.DisbursementDate = loan.Date;
			LoanVM.Amount = loan.Amount;
			LoanVM.RepaymentDate = loan.RepaymentDate;
			LoanVM.RepaymentAmount = loan.RepaymentAmount;
			LoanVM.Interest = loan.Interest;
			LoanVM.Status = loan.Status;

			return Page();
		}

		public async Task<IActionResult> OnPostAsync()
		{
			if (!ModelState.IsValid)
			{
				return Page();
			}

			var me = await userManager.GetUserAsync(User);
			var loanUtilities = new LoanUtilities(context, userManager);

			// Albowiem możesz edytować tylko pożyczkę, której udzieliłeś
			var loans = loanUtilities.GetLoansForUser(me);
			var loan = loans
				.Where(l => l.LoanID == LoanEM.LoanId)
				.FirstOrDefault();
			if (loan == null)
			{
				return NotFound();
			}

			// Albowiem nie możesz zmieniać udzielającego w pożyczce
			var isGiverUnchanged = await loans
				.Where(l => l.LoanID == LoanEM.LoanId)
				.Select(l => l.GiverID == me.Id)
				.AnyAsync();
			if (!isGiverUnchanged)
			{
				return NotFound();
			}

			var loanEdited = new Loan(
				loanID: LoanEM.LoanId,
				date: LoanEM.DisbursementDate,
				giverID: LoanEM.GiverId,
				giver: await userManager.FindByIdAsync(LoanEM.GiverId),
				receiverID: LoanEM.ReceiverId,
				receiver: await userManager.FindByIdAsync(LoanEM.ReceiverId),
				amount: LoanEM.Amount,
				repaymentDate: LoanEM.RepaymentDate,
				repaymentAmount: LoanEM.RepaymentAmount,
				
				// Albowiem status nie może zmienić się przy edycji
				status: loan.Status,
				notifications: null
				);

			context.Attach(loanEdited).State = EntityState.Modified;

			try
			{
				await context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!LoanExists(LoanEM.LoanId))
				{
					return NotFound();
				}
				else
				{
					throw;
				}
			}

			return RedirectToPage("./Index");
		}

		private bool LoanExists(int? id)
		{
			if (!id.HasValue) throw new ArgumentNullException();

			return context.Loans.Any(e => e.LoanID == id);
		}
	}
}
