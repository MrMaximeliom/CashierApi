using CashierApi.Models;

namespace CashierApi.DataTransferObjects
{
    public record InvoiceDto
    {
        public int Id { get; set; }

        public string Number { get; set; }

        public double VAT { get; set; } = 0.15;

        public double DeliveryPrice { get; set; }

        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }

        // Relation properties
        public string UserId { get; set; }


        // Navigation properties
        public List<InvoiceItemDto>? InvoiceItems { get; set; }

        public User? User { get; set; }
    }
}
