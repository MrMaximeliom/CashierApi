using CashierApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CashierApi.ModelsConfigurations
{
    public class ProductEntityTypeConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
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
                .HasMaxLength(100);

            // configure Barcode properties
            builder
                .Property(x => x.Barcode)
                  .IsRequired()
                .HasMaxLength(400);

            // configure Price properties
            builder
                .Property(x => x.Price)
                  .IsRequired();

            // configure Description properties
            builder 
                .Property(x => x.Description)
                .HasMaxLength(200); 

            // configure ImagePath properties
            builder
                .Property(x => x.ImagePath)
                .HasMaxLength(300);



        }
    }
}
