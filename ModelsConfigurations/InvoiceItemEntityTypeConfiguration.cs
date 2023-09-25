using CashierApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CashierApi.ModelsConfigurations
{
    public class InvoiceItemEntityTypeConfiguration : IEntityTypeConfiguration<InvoiceItem>
    {
        public void Configure(EntityTypeBuilder<InvoiceItem> builder)
        {
            // configure Id properties
            builder
                .Property(x => x.Id)
                .IsRequired()
                .ValueGeneratedOnAdd();

            // configure Name properties
            builder
                .Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(200);


            // configure Count properties
            builder
                .Property(x => x.Count)
                .IsRequired();




        }
    }
}
