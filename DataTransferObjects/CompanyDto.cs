using CashierApi.Models;

namespace CashierApi.DataTransferObjects
{
    public class CompanyDto
    {
        public int Id { get; set; } 
        public string Name { get; set; }

        public string? Description { get; set; }

        public string? Email { get; set; }

        public string? PhoneNumber { get; set; }

        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }

        // Navigation properties
        public List<User> Employers { get; set; }
    }
}
