using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        private readonly PozyczkoPrzypominajkaV2.Data.ApplicationDbContext _context;

        public EditModel(PozyczkoPrzypominajkaV2.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Loan Loan { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Loan = await _context.Loans.FirstOrDefaultAsync(m => m.LoanID == id);

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

            _context.Attach(Loan).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
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
            return _context.Loans.Any(e => e.LoanID == id);
        }
    }
}
