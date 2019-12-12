using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PozyczkoPrzypominajka.Models;
using PozyczkoPrzypominajkaV2.Data;
using PozyczkoPrzypominajkaV2.Models;
using System.Threading.Tasks;

namespace PozyczkoPrzypominajkaV2.Pages.Loans
{
  public class DeleteModel : PageModel
  {
    private readonly ApplicationDbContext context;
    private readonly UserManager<AppUser> userManager;

    public DeleteModel(ApplicationDbContext context, UserManager<AppUser> userManager)
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

      Loan = await context.Loans.FirstOrDefaultAsync(m => m.LoanID == id);

      if (Loan == null)
      {
        return NotFound();
      }

      if (Loan.GiverID != userManager.GetUserId(User))
      {
        // Albowiem nie możesz kasować pożyczki jeśli nie jesteś pożyczkodawcą
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

      Loan = await context.Loans.FindAsync(id);

      // Albowiem nie możesz kasować pożyczki jeśli nie jesteś pożyczkodawcą
      var isMine = Loan.GiverID == userManager.GetUserId(User);

      if (Loan != null && isMine)
      {
        context.Loans.Remove(Loan);
        await context.SaveChangesAsync();
      }

      return RedirectToPage("./Index");
    }
  }
}
