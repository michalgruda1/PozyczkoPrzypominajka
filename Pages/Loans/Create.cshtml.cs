using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PozyczkoPrzypominajka.Models;
using PozyczkoPrzypominajkaV2.Data;
using PozyczkoPrzypominajkaV2.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

		[BindProperty]
		public Loan Loan { get; set; }

		[BindProperty]
		public LoanViewModel LoanView { get; set; }

		[BindProperty]
		public Guid? Receiver { get; set; }

		[BindProperty]
		public List<SelectListItem> Receivers { get; set; }

		public async Task<IActionResult> OnGetAsync()
		{
			var currentUser = await userManager.GetUserAsync(User);
			
			return Page();
		}

		public async Task<IActionResult> OnPostAsync()
		{
			if (!ModelState.IsValid)
			{
				return Page();
			}
			var currentUser = await userManager.GetUserAsync(User);
			Loan.GiverID = currentUser.Id;
			context.Loans.Add(Loan);
			await context.SaveChangesAsync();

			return RedirectToPage("./Index");
		}

		private async Task<List<SelectListItem>> InitReceivers(AppUser currentUser, string selected)
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

			var ret = new List<SelectListItem>();

			receivers.ForEach(user =>
			{
				if (selected != null && user.id == selected)
				{
					ret.Add(new SelectListItem(user.user, user.id, selected: true));
				}
				else
				{
					ret.Add(new SelectListItem(user.user, user.id, selected: false));
				}
			});

			return ret;
		}
	}
}