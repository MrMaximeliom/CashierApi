using CashierApi.Models;

namespace CashierApi.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IBaseRepository<User> Users { get; }

        IBaseRepository<Invoice> Invoices { get; }

        IBaseRepository<InvoiceItem> InvoiceItems { get; }


        IBaseRepository<Brand> Brands { get; }

        IBaseRepository<Product> Products { get; }
        IBaseRepository<Company> Companies { get; }


        int Complete();
    }
}
