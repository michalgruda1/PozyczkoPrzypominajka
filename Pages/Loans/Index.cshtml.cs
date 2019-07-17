﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
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

		public IList<LoanViewModel> Loans { get; set; }

		public async Task OnGetAsync()
		{
			var currentUser = await _userManager.GetUserAsync(User);

			Loans = await _context.Loans
				.Where(l => l.Giver.Id == currentUser.Id || l.Receiver.Id == currentUser.Id)
				.Select(l => new LoanViewModel() {
					LoanID = l.LoanID,
					Date = l.Date,
					Giver = l.Giver.Imie + " " + l.Giver.Nazwisko,
					Receiver = l.Receiver.Imie + " " + l.Receiver.Nazwisko,
					Amount = l.Amount,
					RepaymentDate = l.RepaymentDate,
					RepaymentAmount = l.RepaymentAmount,
					Interest = l.Interest
				})
				.AsNoTracking()
				.ToListAsync();
				
		}
	}
}
