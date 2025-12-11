using BuildHubV2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BuildHubV2.Data;
namespace BuildHub_v5.Pages
{
    public class EditCompaneyModel : PageModel
    {

        private readonly ApplicationDbContext _context;

        public EditCompaneyModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Company> CompanyList { get; set; }

        [BindProperty]
        public Company companyvalue { get; set; }
        public IActionResult OnGet(int id)
        {
            companyvalue = _context.Companies.Find(id);

            if (companyvalue == null)
            {
                return NotFound();
            }

            return Page();


        }

        public IActionResult OnPost(int id, string name, string type, string tier, int userId)
        {
            var c = _context.Companies
                    .FirstOrDefault(x => x.Id == id);

            if (c == null)
                return NotFound();

            c.Name = name;
            c.Type = type;
            c.Tier = string.IsNullOrEmpty(tier) ? null : tier;
            c.UserId = userId;

            _context.SaveChanges();

            return RedirectToPage("/Companies");
        }
    }
}
