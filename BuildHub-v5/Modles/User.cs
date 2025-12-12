using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BuildHubV2.Models
{
    public class User
    {
        public int Id { get; set; }

        [NotMapped]
        public string UserName { get; set; }

        public string Email { get; set; }
        
        public string Password { get; set; }

        public string UserType { get; set; }
        
        public DateTime CreatedAt { get; set; }

        public ICollection<Company> Companies { get; set; }
    }
}
