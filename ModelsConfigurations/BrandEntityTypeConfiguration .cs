using CashierApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CashierApi.ModelsConfigurations
{
    public class BrandEntityTypeConfiguration : IEntityTypeConfiguration<Brand>
    {
        public void Configure(EntityTypeBuilder<Brand> builder)
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

            // configure Description properties
            builder 
                .Property(x => x.Description)
                .HasMaxLength(200); 

            // configure ImagePath properties
            builder
                .Property(x => x.LogoPath)
                .HasMaxLength(300);



        }
    }
}
