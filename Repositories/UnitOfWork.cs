using CashierApi;
using CashierApi.Interfaces;
using CashierApi.Models;
using CashierApi.Repositories;

namespace CashierApi.Interfaces
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public IBaseRepository<User> Users { get; private set; }

        public IBaseRepository<Invoice> Invoices { get; private set; }

        public IBaseRepository<InvoiceItem> InvoiceItems { get; private set; }

        public IBaseRepository<Brand> Brands { get; private set; }

        public IBaseRepository<Product> Products { get; private set; }
        public IBaseRepository<Company> Companies { get; private set; }






        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Users = new BaseRepository<User>(_context);
            Invoices = new BaseRepository<Invoice>(_context);
            InvoiceItems = new BaseRepository<InvoiceItem>(_context);
            Products = new BaseRepository<Product>(_context);
            Brands = new BaseRepository<Brand>(_context);
            Companies = new BaseRepository<Company>(_context);
           



        }

        public int Complete()
        {
            return _context.SaveChanges();
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
