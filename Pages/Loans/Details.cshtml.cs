using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PozyczkoPrzypominajkaV2.Models.Loan;
using System.Threading.Tasks;

namespace PozyczkoPrzypominajkaV2.Pages.Loans
{
  public class DetailsModel : PageModel
  {
    private readonly PozyczkoPrzypominajkaV2.Data.ApplicationDbContext _context;

    public DetailsModel(PozyczkoPrzypominajkaV2.Data.ApplicationDbContext context)
    {
      _context = context;
    }

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
  }
}
