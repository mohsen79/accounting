using Accounting.DataLayer.Repositories;
using Accounting.DataLayer.Services;

namespace Accounting.DataLayer.Models;

public class UnitOfWork : IDisposable
{
    AccountingDbContext db = new AccountingDbContext();
    private ICustomerRepository _customerRepository;
    public ICustomerRepository CustomerRepository
    {
        get
        {
            if (_customerRepository == null)
            {
                _customerRepository = new CustomerRepository(db);
            }

            return _customerRepository;
        }
    }

    public void Save()
    {
        db.SaveChanges();
    }

    public void Dispose()
    {
        db.Dispose();
    }
}
