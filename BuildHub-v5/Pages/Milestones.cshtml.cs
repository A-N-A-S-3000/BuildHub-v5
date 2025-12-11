using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BuildHubV2.Data;
using BuildHubV2.Models;

namespace BuildHub_v5.Pages
{
    public class MilestonesModel : PageModel
    {
        private readonly BuildHubV2.Data.ApplicationDbContext _context;

        public MilestonesModel(BuildHubV2.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Milestones> Milestones { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Milestones = await _context.Milestones.ToListAsync();
        }
    }
}
