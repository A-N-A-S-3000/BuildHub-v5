using Microsoft.AspNetCore.Identity;

namespace BuildHubV2.Models
{
    public class User: IdentityUser
    {
        public int Id { get; set; }

        public string Email { get; set; }
        public string UserType { get; set; }
        public DateTime CreatedAt { get; set; }

        
        public ICollection<Company> Companies { get; set; }
    }
}
