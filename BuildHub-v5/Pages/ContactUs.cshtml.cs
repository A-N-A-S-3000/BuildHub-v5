using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BuildHubV2.Data;
using BuildHub_v5.Modles;

namespace BuildHub_v5.Pages
{
    public class ContactUsModel : PageModel
    {
        private readonly BuildHubV2.Data.ApplicationDbContext _context;

        public ContactUsModel(BuildHubV2.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return Page();
        }

        [BindProperty]
        public ContactUs ContactUs { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.ContactUs.Add(ContactUs);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
