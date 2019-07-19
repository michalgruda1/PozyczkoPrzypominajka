using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PozyczkoPrzypominajka.Models;
using PozyczkoPrzypominajkaV2.Data;

namespace PozyczkoPrzypominajkaV2.Pages.Loans
{
	public class CreateModel : PageModel
	{
		public CreateModel(ApplicationDbContext context, UserManager<AppUser> userManager)
		{
			_context = context;
			_userManager = userManager;
		}

		private readonly ApplicationDbContext _context;
		private readonly UserManager<AppUser> _userManager;

		[BindProperty] public Loan Loan { get; set; } = new Loan();
		public SelectList Receivers { get; set; } = null;

		public async Task<IActionResult> OnGetAsync()
		{
			var currentUser = await _userManager.GetUserAsync(User);
			var Receivers = await InitReceivers(currentUser);
			Loan.Giver = currentUser;
			Loan.Date = DateTime.Now;
			Loan.RepaymentDate = DateTime.Now + TimeSpan.FromDays(7);

			return Page();
		}		

		public async Task<IActionResult> OnPostAsync()
		{
			if (!ModelState.IsValid)
			{
				return Page();
			}

			_context.Loans.Add(Loan);
			await _context.SaveChangesAsync();

			return RedirectToPage("./Index");
		}

		private async Task<SelectList> InitReceivers(AppUser currentUser)
		{
			var receivers = await _context.Users
				.Where(u => u.Id != currentUser.Id)
				.Select(u =>
					new
					{
						id = u.Id,
						user = u.ToString()
					})
				.AsNoTracking()
				.ToListAsync();

			var ret = new SelectList(receivers, "id", "user");

			return ret;
		}

	}
}