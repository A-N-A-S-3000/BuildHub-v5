using BuildHubV2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BuildHubV2.Data;
namespace BuildHub_v5.Pages
{
    public class CompaniesModel : PageModel
    {

        private readonly ApplicationDbContext _context;

        public CompaniesModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Company> CompanyList { get; set; }
        public void OnGet()
        {
            CompanyList = _context.Companies
                .OrderByDescending(c => c.CreatedAt)
                .ToList();
        }

        public IActionResult OnGetDelete(int id)
        {
            var c = _context.Companies.
                FirstOrDefault(x => x.Id == id);

            if (c != null)
            {
                _context.Companies.Remove(c);
                _context.SaveChanges();
            }
            return RedirectToPage("Companies");
        }
    }
}
