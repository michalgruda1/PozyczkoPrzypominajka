using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PozyczkoPrzypominajka.Models;
using PozyczkoPrzypominajkaV2.Data;
using PozyczkoPrzypominajkaV2.Models.Loan;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PozyczkoPrzypominajkaV2.Pages.Loans
{
	public class IndexModel : PageModel
	{
		private readonly ApplicationDbContext context;
		private readonly UserManager<AppUser> userManager;
		private readonly LoanUtilities loanUtilities;

		public IndexModel(ApplicationDbContext context, UserManager<AppUser> userManager, LoanUtilities loanUtilities)
		{
			this.context = context;
			this.userManager = userManager;
			this.loanUtilities = loanUtilities;
		}

		public IList<LoanViewModel> LoansGivenUnpaid { get; set; } = new List<LoanViewModel>();
		public IList<LoanViewModel> LoansTakenUnpaid { get; set; } = new List<LoanViewModel>();

		public async Task OnGetAsync()
		{
			var me = await userManager.GetUserAsync(User);
			LoansGivenUnpaid = loanUtilities.GetLoansGivenUnpaid(me);
			LoansTakenUnpaid = loanUtilities.GetLoansTakenUnpaid(me);
		}
	}
}
