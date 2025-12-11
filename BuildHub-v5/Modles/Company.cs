namespace BuildHubV2.Models
{
    public class Company
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Type { get; set; }   

        public string? Tier { get; set; }  

        public DateTime CreatedAt { get; set; }

        
        public int UserId { get; set; }

        
        public User User { get; set; }
    }
}
