﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PozyczkoPrzypominajka.Models;
using PozyczkoPrzypominajkaV2.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PozyczkoPrzypominajkaV2.Models
{
	public class LoanUtilities
	{
		private readonly ApplicationDbContext context;
		private readonly UserManager<AppUser> userManager;

		public LoanUtilities(ApplicationDbContext context, UserManager<AppUser> userManager)
		{
			this.context = context;
			this.userManager = userManager;
		}

		public IQueryable<AppUser> GetPossibleLoanReceiversForUser(AppUser user)
		{
			// wszyscy prócz zalogowanego
			return context.Users.Where(u => u.Id != user.Id);
		}

		public async System.Threading.Tasks.Task<IList<LoanViewModel>> GetLoansTakenUnpaidAsync(AppUser user)
		{
			var loans = context.Loans
				.Where(l =>
					l.Receiver.Id == user.Id // Aktualny user jest biorącym pożyczkę
					&& l.Status == StatusEnum.Unpaid)
				.AsNoTracking()
				.ToList();

			// TODO Wyjmij logikę tworzenia listy z SelectListItem do zewnętrznej klasy / metody

			var loansVM = new List<LoanViewModel>();

			foreach (var loan in loans)
			{
				loan.Giver = await userManager.FindByIdAsync(loan.GiverID) ?? throw new NullReferenceException();
				var giver = new SelectListItem(text: loan.Giver.ToString(), value: loan.GiverID, selected: true);
				var giverList = new List<SelectListItem>();
				giverList.Add(giver);

				loan.Receiver = await userManager.FindByIdAsync(loan.ReceiverID) ?? throw new NullReferenceException();
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

		public async System.Threading.Tasks.Task<IList<LoanViewModel>> GetLoansGivenUnpaidAsync(AppUser user)
		{
			var loans = context.Loans
				.Where(l =>
					l.Giver.Id == user.Id // Dający to aktualny użytkownik
					&& l.Status == StatusEnum.Unpaid)
				.AsNoTracking()
				.ToList();

			// TODO Wyjmij logikę tworzenia listy z SelectListItem do zewnętrznej klasy / metody

			var loansVM = new List<LoanViewModel>();

			foreach (var loan in loans)
			{
				loan.Giver = await userManager.FindByIdAsync(loan.GiverID) ?? throw new NullReferenceException();
				var giver = new SelectListItem(text: loan.Giver.ToString(), value: loan.GiverID, selected: true);
				var giverList = new List<SelectListItem>();
				giverList.Add(giver);

				loan.Receiver = await userManager.FindByIdAsync(loan.ReceiverID) ?? throw new NullReferenceException();
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

		public IQueryable<Loan> GetLoansForUser(AppUser user)
		{
			return context.Loans
				.Where(l =>
					l.GiverID == user.Id
					|| l.ReceiverID == user.Id)
				.AsNoTracking();
		}
	}
}
