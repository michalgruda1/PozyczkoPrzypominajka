using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PozyczkoPrzypominajka.Models;
using PozyczkoPrzypominajkaV2.Data;
using PozyczkoPrzypominajkaV2.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PozyczkoPrzypominajkaV2.Pages.Loans
{
	public class DetailsModel : PageModel
  {
    private readonly ApplicationDbContext context;
    private readonly UserManager<AppUser> userManager;

    public DetailsModel(ApplicationDbContext context, UserManager<AppUser> userManager)
    {
      this.context = context;
      this.userManager = userManager;
    }

    public LoanViewModel Loan { get; set; }

    public async Task<IActionResult> OnGetAsync(int? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var loan = await context.Loans.Include(l => l.Giver).Include(l => l.Receiver).FirstOrDefaultAsync(l => l.LoanID == id);

      if (loan == null)
      {
        return NotFound();
      }

      if (loan.GiverID != userManager.GetUserId(User))
      {
        return NotFound();
      }

      Loan = new LoanViewModel()
      {
        LoanId = loan.LoanID,
        DisbursementDate = loan.Date,
        GiverList = new List<SelectListItem>() { new SelectListItem(text: loan.Giver.ToString(), value: loan.Giver.Id, selected: true, disabled: true) },
        ReceiverList = new List<SelectListItem>() { new SelectListItem(text: loan.Receiver.ToString(), value: loan.Receiver.Id, selected: true, disabled: true) },
        Amount = loan.Amount,
        RepaymentAmount = loan.RepaymentAmount,
        RepaymentDate = loan.RepaymentDate,
        Interest = loan.Interest,
        Status = loan.Status
      };

      return Page();
    }
  }
}
