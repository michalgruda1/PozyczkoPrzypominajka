using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PozyczkoPrzypominajkaV2.Models.Loan;
using System.Threading.Tasks;

namespace PozyczkoPrzypominajkaV2.Pages.Loans
{
  public class DeleteModel : PageModel
  {
    private readonly PozyczkoPrzypominajkaV2.Data.ApplicationDbContext _context;

    public DeleteModel(PozyczkoPrzypominajkaV2.Data.ApplicationDbContext context)
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

    public async Task<IActionResult> OnPostAsync(int? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      Loan = await _context.Loans.FindAsync(id);

      if (Loan != null)
      {
        _context.Loans.Remove(Loan);
        await _context.SaveChangesAsync();
      }

      return RedirectToPage("./Index");
    }
  }
}
