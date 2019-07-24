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
	public class EditModel : PageModel
	{
		private readonly ApplicationDbContext context;
		private readonly UserManager<AppUser> userManager;

		public EditModel(ApplicationDbContext context, UserManager<AppUser> userManager)
		{
			this.context = context;
			this.userManager = userManager;
		}

		[BindProperty]
		public Loan Loan { get; set; }

		public async Task<IActionResult> OnGetAsync(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			Loan = await context.Loans
					.Include(l => l.Giver)
					.Include(l => l.Receiver)
					.FirstOrDefaultAsync(l => l.LoanID == id);

			if (Loan == null)
			{
				return NotFound();
			}

			return Page();
		}

		public async Task<IActionResult> OnPostAsync()
		{
			if (!ModelState.IsValid)
			{
				return Page();
			}

			context.Attach(Loan).State = EntityState.Modified;

			try
			{
				await context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!LoanExists(Loan.LoanID))
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

		private bool LoanExists(int id)
		{
			return context.Loans.Any(e => e.LoanID == id);
		}
	}
}
