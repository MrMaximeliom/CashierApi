using Microsoft.AspNetCore.Identity;

namespace CashierApi.Models
{
    public class User:IdentityUser
    {
        public string FirstName {  get; set; }  

        public string LastName { get; set; }

        public string? ImagePath { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public DateTime UpdatedAt { get; set; } 

        // Relation properties

        public int? CompanyId { get; set; } 

        // Navigation properties
        public Company? Company { get; set; }    
        public List<Invoice> Invoices { get; set; }

        public List<RefreshToken> RefreshTokens { get; set; }

    }
}
