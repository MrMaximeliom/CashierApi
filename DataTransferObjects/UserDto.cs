using CashierApi.Models;

namespace CashierApi.DataTransferObjects
{
    public record UserDto
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }


        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public string? ImagePath { get; set; }

        public IFormFile ImageFile { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public DateTime UpdatedAt { get; set; }

        // Relation properties

        public int? CompanyId { get; set; }

        // Navigation properties
        public Company? Company { get; set; }
        public List<Invoice>? Invoices { get; set; }

        public List<RefreshToken>? RefreshTokens { get; set; }
    }
}
