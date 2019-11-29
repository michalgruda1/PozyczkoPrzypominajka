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
			var receivers = await context.Users
				.AllAsync(u => u.Id != currentUser.Id);

			var loans = await context.Loans
				.Where(l =>
					l.Receiver.Id == currentUser.Id // Aktualny user jest biorącym pożyczkę
					&& l.Status != StatusEnum.Unpaid)
				.AsNoTracking()
				.ToListAsync();

			var loansVM = new List<LoanViewModel>();

			foreach (var loan in loans)
			{
				var giverTuple = (text: loan.Giver.ToString(), value: loan.GiverID);
				var giverAsList = new List<Tuple<string, string>>();
				giverAsList.Add(giverTuple.ToTuple());
				var giver = new SelectList(giverAsList, loan.GiverID);

				var receiverTuple = (text: loan.Giver.ToString(), value: loan.GiverID);
				var receiverAsList = new List<Tuple<string, string>>();
				receiverAsList.Add(receiverTuple.ToTuple());
				var receiver = new SelectList(receiverAsList, loan.GiverID);

				// TODO tyle roboty na selecty? Może da się to uprościć?

				loansVM.Add(
					new LoanViewModel()
					{
						LoanId = loan.LoanID ?? throw new ArgumentNullException(), // To się nie powinno wydarzyć
						GiverList = giver.ToList(),
						ReceiverList = receiver.ToList(),
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
			var receivers = await context.Users
				.AllAsync(u => u.Id != currentUser.Id);

			var loans = await context.Loans
				.Where(l =>
					l.Giver.Id == currentUser.Id // Dający to aktualny użytkownik
					&& l.Status != StatusEnum.Unpaid)
				.AsNoTracking()
				.ToListAsync();

			var loansVM = new List<LoanViewModel>();

			foreach (var loan in loans)
			{
				var giverTuple = (text: loan.Giver.ToString(), value: loan.GiverID);
				var giverAsList = new List<Tuple<string, string>>();
				giverAsList.Add(giverTuple.ToTuple());
				var giver = new SelectList(giverAsList, loan.GiverID);

				var receiverTuple = (text: loan.Giver.ToString(), value: loan.GiverID);
				var receiverAsList = new List<Tuple<string, string>>();
				receiverAsList.Add(receiverTuple.ToTuple());
				var receiver = new SelectList(receiverAsList, loan.GiverID);

				// TODO tyle roboty na selecty? Może da się to uprościć?

				loansVM.Add(
					new LoanViewModel()
					{
						LoanId = loan.LoanID ?? throw new ArgumentNullException(), // To się nie powinno wydarzyć
						GiverList = giver.ToList(),
						ReceiverList = receiver.ToList(),
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
