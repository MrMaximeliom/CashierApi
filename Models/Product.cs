namespace CashierApi.Models
{
    public class Product
    {
        public int Id { get; set; } 

        public string Name { get; set; } 
        
        public string Barcode { get; set; } 

        public string? Description { get; set; } 

        public double Price { get; set; } 

        public string? ImagePath { get; set; }  

        public DateTime? CreatedAt { get; set; }  = DateTime.UtcNow;
        
        public DateTime? UpdatedAt { get; set; }      

        // Relation fields
        public int BrandId { get; set; }    

        // Navigation properties
        public Brand Brand { get; set; }

        public List<InvoiceItem> InvoiceItems { get; set; } 
    }
}
