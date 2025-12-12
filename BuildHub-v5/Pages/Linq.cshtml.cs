using BuildHubV2.Data;
using BuildHubV2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BuildHub_v5.Pages
{
    public class LinqModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public LinqModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Company> FilteredCompanies { get; set; } = new List<Company>();
        public IList<Company> OrderedCompanies { get; set; } = new List<Company>();
        public IList<IGrouping<string, Company>> GroupedCompanies { get; set; } = new List<IGrouping<string, Company>>();
        public IList<UserCompanyDto> UserCompanies { get; set; } = new List<UserCompanyDto>();

        public async Task OnGetAsync()
        {
            // Where: Filter companies created in the last year
            FilteredCompanies = await _context.Companies
                .Where(c => c.CreatedAt >= DateTime.Now.AddYears(-1))
                .ToListAsync();

            // OrderBy: Order companies by name
            OrderedCompanies = await _context.Companies
                .OrderBy(c => c.Name)
                .ToListAsync();

            // GroupBy: Group companies by Type
            // Note: GroupBy is often done in memory for complex types or specific EF Core versions if not supported directly in SQL translation
            var allCompanies = await _context.Companies.ToListAsync();
            GroupedCompanies = allCompanies
                .GroupBy(c => c.Type)
                .ToList();

            // Join: Query involving multiple tables (Users and Companies)
            UserCompanies = await _context.Companies
                .Include(c => c.User)
                .Select(c => new UserCompanyDto
                {
                    CompanyName = c.Name,
                    UserName = c.User.Email // Assuming Email is used as name or identifier
                })
                .ToListAsync();
        }

        public class UserCompanyDto
        {
            public string CompanyName { get; set; }
            public string UserName { get; set; }
        }
    }
}
