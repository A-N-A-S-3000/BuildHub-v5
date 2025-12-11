using BuildHubV2.Data;
using BuildHubV2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BuildHub_v5.Pages
{
    public class AddComapneyModel : PageModel
    {

        private readonly ApplicationDbContext _context;

        public AddComapneyModel(ApplicationDbContext context)
        {
            _context = context;
        }
        public void OnGet()
        {
        }

        public IActionResult OnPost(int id, string name, string type, string tier, int userId)
        {
            var c = new Company
            {
                Name = name,
                Type = type,
                Tier = string.IsNullOrEmpty(tier) ? null : tier,
                UserId = userId,
                CreatedAt = DateTime.Now
            };

            _context.Companies.Add(c);

            _context.SaveChanges();

            return RedirectToPage("/Companies");
        }
    }
}
