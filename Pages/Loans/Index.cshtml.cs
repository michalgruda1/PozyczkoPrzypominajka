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

		public IndexModel(ApplicationDbContext context, UserManager<AppUser> userManager)
		{
			this.context = context;
			this.userManager = userManager;
		}

		public IList<LoanViewModel> LoansGivenUnpaid { get; set; } = new List<LoanViewModel>();
		public IList<LoanViewModel> LoansTakenUnpaid { get; set; } = new List<LoanViewModel>();

		public async Task OnGetAsync()
		{
			var me = await userManager.GetUserAsync(User);
			var loanUtilities = new LoanUtilities(context, userManager);

			LoansGivenUnpaid = await loanUtilities.GetLoansGivenUnpaidAsync(me);
			LoansTakenUnpaid = await loanUtilities.GetLoansTakenUnpaidAsync(me);
		}
	}
}
