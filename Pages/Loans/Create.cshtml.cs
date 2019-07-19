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
			this.context = context;
			this.userManager = userManager;
		}

		private readonly ApplicationDbContext context;
		private readonly UserManager<AppUser> userManager;

		[BindProperty] public Loan Loan { get; set; } = new Loan();
		[BindProperty] public SelectList Receivers { get; set; } = new SelectList();

		public async Task<IActionResult> OnGetAsync()
		{
			var currentUser = await userManager.GetUserAsync(User);
			Receivers = await InitReceivers(currentUser, userManager);
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

			Loan.Receiver = await userManager.FindByIdAsync((string)Receivers.SelectedValue);
			context.Loans.Add(Loan);
			await context.SaveChangesAsync();

			return RedirectToPage("./Index");
		}

		private async Task<SelectList> InitReceivers(AppUser currentUser, UserManager<AppUser> userManager)
		{
			var receivers = await context.Users
				.Where(u =>
					u.Id != currentUser.Id)
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