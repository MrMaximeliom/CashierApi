namespace CashierApi.Models
{
    public class Brand
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string? Description { get; set; } 

        public string LogoPath { get; set; }   

        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }

        // Navigation properties
        public List<Product> Products { get; set; } 

    }
}
