using CashierApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CashierApi.ModelsConfigurations
{
    public class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            // configure Id properties
            builder
                .Property(x => x.Id)
                .IsRequired()
                .ValueGeneratedOnAdd();

            // configure FirstName properties
            builder
                .Property(x => x.FirstName)
                .IsRequired()
                .HasMaxLength(100);

            // configure LastName properties
            builder 
                .Property(x => x.LastName)
                .IsRequired()
                .HasMaxLength(100); 

            // configure Email properties
            builder
                .Property(x => x.Email)
                .IsRequired()
                .HasMaxLength(200);

            // configure PhoneNumber properties
            builder
                .Property(x => x.PhoneNumber)
                .HasMaxLength(30);

            // configure Username properties
            builder
                .Property(x => x.UserName)
                .IsRequired(false)
                .HasMaxLength(30);

        }
    }
}
