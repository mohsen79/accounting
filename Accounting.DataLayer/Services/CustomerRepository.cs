using Accounting.DataLayer.Models;
using Accounting.DataLayer.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Accounting.DataLayer.Services;

public class CustomerRepository : ICustomerRepository
{
    AccountingDbContext db = new AccountingDbContext();

    public bool DeleteCustomer(Customer customer)
    {
        try
        {
            db.Entry(customer).State = EntityState.Deleted;
            return true;
        }
        catch
        {
            return false;
        }
    }

    public bool DeleteCustomer(int customerId)
    {
        Customer customer = GetCustomer(customerId);
        DeleteCustomer(customer);
        return true;
    }

    public List<Customer> GetAllCustomers()
    {
        return db.Customers.ToList();
    }

    public Customer GetCustomer(int customerId)
    {
        return db.Customers.Find(customerId);
    }

    public bool InserCustomer(Customer customer)
    {
        try
        {
            db.Customers.Add(customer);
            return true;
        }
        catch
        {
            return false;
        }
    }

    public void Save()
    {
        db.SaveChanges();
    }

    public bool UpdateCustomer(Customer customer)
    {
        try
        {
            db.Entry(customer).State = EntityState.Modified;
            return true;
        }
        catch
        {
            return false;
        }
    }
}
