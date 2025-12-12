using BuildHubV2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BuildHub_v5.Pages
{
    public class MilestonesCreateModel : PageModel
    {
        private readonly BuildHubV2.Data.ApplicationDbContext _context;

        public MilestonesCreateModel(BuildHubV2.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Milestones Milestone { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Milestones.Add(Milestone);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Milestones");
        }
    }
}
