using CashierApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CashierApi.ModelsConfigurations
{
    public class CompanyEntityTypeConfiguration : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
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

            // configure Email properties
            builder
                .Property(x => x.Email)
                .HasMaxLength(300);

            // configure PhoneNumber properties
            builder
                .Property(x => x.PhoneNumber)
                .HasMaxLength(30);



        }
    }
}
