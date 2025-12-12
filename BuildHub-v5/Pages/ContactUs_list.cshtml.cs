using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BuildHubV2.Data;
using BuildHub_v5.Modles;

namespace BuildHub_v5.Pages
{
    public class ContactUs_listModel : PageModel
    {
        private readonly BuildHubV2.Data.ApplicationDbContext _context;

        public ContactUs_listModel(BuildHubV2.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<ContactUs> ContactUs { get;set; } = default!;

        public async Task OnGetAsync()
        {
            ContactUs = await _context.ContactUs
                .Include(c => c.User).ToListAsync();
        }
    }
}
