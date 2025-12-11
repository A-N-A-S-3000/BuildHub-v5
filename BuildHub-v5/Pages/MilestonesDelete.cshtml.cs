using BuildHubV2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BuildHub_v5.Pages
{
    public class MilestoneDeleteModel : PageModel
    {
        private readonly BuildHubV2.Data.ApplicationDbContext _context;

        public MilestoneDeleteModel(BuildHubV2.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Milestones Milestone { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var milestone = await _context.Milestones
                .FromSqlRaw("SELECT * FROM Milestones WHERE Id = {0}", id)
                .FirstOrDefaultAsync();

            if (milestone == null)
            {
                return NotFound();
            }
            else
            {
                Milestone = milestone;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var milestone = await _context.Milestones
                .FromSqlRaw("SELECT * FROM Milestones WHERE Id = {0}", id)
                .FirstOrDefaultAsync();

            if (milestone != null)
            {
                Milestone = milestone;
                await _context.Database.ExecuteSqlRawAsync("DELETE FROM Milestones WHERE Id = {0}", id);
            }

            return RedirectToPage("./Index");
        }
    }
}
