using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PozyczkoPrzypominajka.Models;
using PozyczkoPrzypominajkaV2.Data;
using PozyczkoPrzypominajkaV2.Models.ViewModels;

namespace PozyczkoPrzypominajkaV2.Pages.Loans
{
	public class IndexModel : PageModel
	{
		private readonly ApplicationDbContext _context;
		private readonly UserManager<AppUser> _userManager;

		public IndexModel(ApplicationDbContext context, UserManager<AppUser> userManager)
		{
			_context = context;
			_userManager = userManager;
		}

		public IList<LoanViewModel> LoansGivenUnpaid { get; set; }
		public IList<LoanViewModel> LoansTakenUnpaid { get; set; }

		public async Task OnGetAsync()
		{
			var currentUser = await _userManager.GetUserAsync(User);
			LoansGivenUnpaid = await GetLoansGivenUnpaid(currentUser);
			LoansTakenUnpaid = await GetLoansTakenUnpaid(currentUser);
		}

		private async Task<IList<LoanViewModel>> GetLoansTakenUnpaid(AppUser currentUser)
		{
			var ret = await _context.Loans
				.Where(l =>
					l.Receiver.Id == currentUser.Id
					&& l.Status != StatusEnum.Paid)
				.Select(l =>
					new LoanViewModel
					{
						LoanID = l.LoanID,
						Date = l.Date,
						Giver = l.Giver.ToString(),
						Receiver = l.Receiver.ToString(),
						Amount = l.Amount,
						RepaymentDate = l.RepaymentDate,
						RepaymentAmount = l.RepaymentAmount,
						Interest = l.Interest,
						Status = l.Status.ToString(),
					})
				.OrderByDescending(l => l.Status)
					.ThenByDescending(l => l.RepaymentDate)
				.AsNoTracking()
				.ToListAsync();

			return ret;

		}

		private async Task<IList<LoanViewModel>> GetLoansGivenUnpaid(AppUser currentUser)
		{
			var ret = await _context.Loans
				.Where(l => 
					l.Giver.Id == currentUser.Id
					&& l.Status != StatusEnum.Paid)
				.Select(l =>
					new LoanViewModel
					{
						LoanID = l.LoanID,
						Date = l.Date,
						Giver = l.Giver.ToString(),
						Receiver = l.Receiver.ToString(),
						Amount = l.Amount,
						RepaymentDate = l.RepaymentDate,
						RepaymentAmount = l.RepaymentAmount,
						Interest = l.Interest,
						Status = l.Status.ToString(),
					})
				.OrderByDescending(l => l.Status)
					.ThenByDescending(l => l.RepaymentDate)
				.AsNoTracking()
				.ToListAsync();

			return ret;
		}
	}
}
