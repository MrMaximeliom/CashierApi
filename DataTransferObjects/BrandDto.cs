using CashierApi.Models;

namespace CashierApi.DataTransferObjects
{
    public class BrandDto
    {
        public int Id { get; set; }     

        public string Name { get; set; }

        public string? Description { get; set; }

        public string? LogoPath { get; set; }
        public IFormFile? LogoFile { get; set; }


        public DateTime? CreatedDate { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedDate { get; set; }

        // Navigation properties
        public List<Product>? Products { get; set; }
    }
}
