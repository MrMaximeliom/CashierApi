using CashierApi.Models;
using CashierApi.ModelsConfigurations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CashierApi
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // change default identity tables names
            modelBuilder.Entity<User>().ToTable("users");
            modelBuilder.Entity<IdentityRole>().ToTable("roles");
            modelBuilder.Entity<IdentityUserRole<string>>().ToTable("userRoles");
            modelBuilder.Entity<IdentityUserClaim<string>>().ToTable("userClaims");
            modelBuilder.Entity<IdentityUserLogin<string>>().ToTable("userLogins");
            modelBuilder.Entity<IdentityRoleClaim<string>>().ToTable("roleClaims");
            modelBuilder.Entity<IdentityUserToken<string>>().ToTable("userTokens");

            // Configure User and Invoice relation
            modelBuilder.Entity<Invoice>()
                .HasOne(p => p.User)
                .WithMany(p => p.Invoices)
                .HasForeignKey(p => p.UserId);

            // Configure Invoice and InvoiceItem relation
            modelBuilder.Entity<InvoiceItem>()
                .HasOne(p => p.Invoice)
                .WithMany(p => p.InvoiceItems)
                .HasForeignKey(p => p.InvoiceId);

            // Configure Brand and Product relation
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Brand)
                .WithMany(p => p.Products)
                .HasForeignKey(p => p.BrandId);

            // Configure Product and InvoiceItem relation
            modelBuilder.Entity<InvoiceItem>()
                .HasOne(p => p.Product)
                .WithMany(p => p.InvoiceItems)
                .HasForeignKey(p => p.ProductId);

            // Configure User and Company relation
            modelBuilder.Entity<User>()
                .HasOne(p => p.Company)
                .WithMany(p => p.Employers)
                .HasForeignKey(p => p.CompanyId);

            // Configure User properties
            new UserEntityTypeConfiguration().Configure(modelBuilder.Entity<User>());

            // Configure Product properties
            new ProductEntityTypeConfiguration().Configure(modelBuilder.Entity<Product>());

            // Configure Invoice properties
            new InvoiceEntityTypeConfiguration().Configure(modelBuilder.Entity<Invoice>());

            // Configure InvoiceItem properties
            new InvoiceItemEntityTypeConfiguration().Configure(modelBuilder.Entity<InvoiceItem>());

            // Configure Brand properties
            new BrandEntityTypeConfiguration().Configure(modelBuilder.Entity<Brand>());

            // Configure Company properties
            new CompanyEntityTypeConfiguration().Configure(modelBuilder.Entity<Company>());
        }

        // Define User model
        public DbSet<User> Users { get; set; }

        // Define Product model
        public DbSet<Product> Products { get; set; }

        // Define Invoice model
        public DbSet<Invoice> Invoices { get; set; }

        // Define InvoiceItem model
        public DbSet<InvoiceItem> InvoiceItems { get; set; }

        // Define Brand model
        public DbSet<Brand> Brands { get; set; }

        // Define Company model
        public DbSet<Company> Companies { get; set; }

    }
}
