namespace CashierApi.Models
{
    public class InvoiceItem
    {
        public int Id { get; set; } 

        public string Name { get; set; }    

        public int Count { get; set; }      

        public double? Discount { get; set; }    

        public double? TotalPrice { get; set; }

        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }


        // Relation properties
        public int InvoiceId { get; set; }  

        public int ProductId { get; set; }      

        // Navigation properties
        public Invoice Invoice { get; set; }

        public Product Product { get; set; }

    }
}
