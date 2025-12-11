using BuildHubV2.Models;
using Microsoft.EntityFrameworkCore;


namespace BuildHubV2.Data
{
    public class ApplicationDbContext : DbContext

    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<BuildHubV2.Models.Milestones> Milestones { get; set; } = default!;
    }
}
