using BuildHubV2.Models;
using System.ComponentModel.DataAnnotations;

namespace BuildHub_v5.Modles
{
    public class ContactUs
    {
        [Key]
        public int Id { get; set; }

        // optional (can be null)
        public int? UserId { get; set; }

        [Required]
        [MaxLength(150)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [MaxLength(255)]
        public string Email { get; set; } = string.Empty;

        [Required]
        [MaxLength(200)]
        public string Subject { get; set; } = string.Empty;

        [Required]
        public string Message { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Navigation (optional)
        public User? User { get; set; }
    }
}
