using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PozyczkoPrzypominajka.Models;
using PozyczkoPrzypominajkaV2.Data;

namespace PozyczkoPrzypominajkaV2.Pages.Loans
{
    public class IndexModel : PageModel
    {
        private readonly PozyczkoPrzypominajkaV2.Data.ApplicationDbContext _context;

        public IndexModel(PozyczkoPrzypominajkaV2.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Loan> Loan { get;set; }

        public async Task OnGetAsync()
        {
            Loan = await _context.Loans.ToListAsync();
        }
    }
}
