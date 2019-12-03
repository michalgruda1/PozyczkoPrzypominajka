using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PozyczkoPrzypominajka.Models;
using PozyczkoPrzypominajkaV2.Data;
using PozyczkoPrzypominajkaV2.Models.Loan;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PozyczkoPrzypominajkaV2.Pages.Loans
{
	public class IndexModel : PageModel
	{
		private readonly ApplicationDbContext context;
		private readonly UserManager<AppUser> userManager;

		public IndexModel(ApplicationDbContext context, UserManager<AppUser> userManager)
		{
			this.context = context;
			this.userManager = userManager;
		}

		public IList<LoanViewModel> LoansGivenUnpaid { get; set; } = new List<LoanViewModel>();
		public IList<LoanViewModel> LoansTakenUnpaid { get; set; } = new List<LoanViewModel>();

		public async Task OnGetAsync()
		{
			var currentUser = await userManager.GetUserAsync(User);
			LoansGivenUnpaid = await GetLoansGivenUnpaid(currentUser);
			LoansTakenUnpaid = await GetLoansTakenUnpaid(currentUser);
		}

		private async Task<IList<LoanViewModel>> GetLoansTakenUnpaid(AppUser currentUser)
		{
			var loans = await context.Loans
				.Where(l =>
					l.Receiver.Id == currentUser.Id // Aktualny user jest biorącym pożyczkę
					&& l.Status == StatusEnum.Unpaid)
				.AsNoTracking()
				.ToListAsync();

			loans.ForEach(l =>
			{
				l.Receiver = userManager.FindByIdAsync(l.ReceiverID).Result ?? throw new NullReferenceException();
				l.Giver = userManager.FindByIdAsync(l.GiverID).Result ?? throw new NullReferenceException();
			}
			);

			var loansVM = new List<LoanViewModel>();

			foreach (var loan in loans)
			{
				var giver = new SelectListItem(text: loan.Giver.ToString(), value: loan.GiverID, selected: true);
				var giverList = new List<SelectListItem>();
				giverList.Add(giver);

				var receiver = new SelectListItem(text: loan.Receiver.ToString(), value: loan.ReceiverID, selected: true);
				var receiverList = new List<SelectListItem>();
				receiverList.Add(receiver);

				// TODO tyle roboty na selecty? Może da się to uprościć?

				loansVM.Add(
					new LoanViewModel()
					{
						LoanId = loan.LoanID ?? throw new ArgumentNullException(), // To się nie powinno wydarzyć
						GiverList = giverList,
						ReceiverList = receiverList,
						DisbursementDate = loan.Date,
						Amount = loan.Amount,
						RepaymentDate = loan.RepaymentDate,
						RepaymentAmount = loan.RepaymentAmount,
						Interest = loan.Interest,
						Status = loan.Status
					});
			};

			loansVM.OrderByDescending(l => l.Status)
				.ThenByDescending(l => l.RepaymentDate);

			return loansVM;
		}

		private async Task<IList<LoanViewModel>> GetLoansGivenUnpaid(AppUser currentUser)
		{
			var loans = await context.Loans
				.Where(l =>
					l.Giver.Id == currentUser.Id // Dający to aktualny użytkownik
					&& l.Status == StatusEnum.Unpaid)
				.AsNoTracking()
				.ToListAsync();

			loans.ForEach(l =>
			{
				l.Receiver = userManager.FindByIdAsync(l.ReceiverID).Result ?? throw new NullReferenceException();
				l.Giver = userManager.FindByIdAsync(l.GiverID).Result ?? throw new NullReferenceException();
			}
			);

			var loansVM = new List<LoanViewModel>();

			foreach (var loan in loans)
			{
				var giver = new SelectListItem(text: loan.Giver.ToString(), value: loan.GiverID, selected: true);
				var giverList = new List<SelectListItem>();
				giverList.Add(giver);

				var receiver = new SelectListItem(text: loan.Receiver.ToString(), value: loan.ReceiverID, selected: true);
				var receiverList = new List<SelectListItem>();
				receiverList.Add(receiver);

				// TODO tyle roboty na selecty? Może da się to uprościć?

				loansVM.Add(
					new LoanViewModel()
					{
						LoanId = loan.LoanID ?? throw new ArgumentNullException(), // To się nie powinno wydarzyć
						GiverList = giverList,
						ReceiverList = receiverList,
						DisbursementDate = loan.Date,
						Amount = loan.Amount,
						RepaymentDate = loan.RepaymentDate,
						RepaymentAmount = loan.RepaymentAmount,
						Interest = loan.Interest,
						Status = loan.Status
					});
			};

			loansVM.OrderByDescending(l => l.Status)
				.ThenByDescending(l => l.RepaymentDate);

			return loansVM;
		}
	}
}
