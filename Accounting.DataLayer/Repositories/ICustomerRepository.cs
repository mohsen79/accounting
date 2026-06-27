using Accounting.DataLayer.Models;

namespace Accounting.DataLayer.Repositories;

public interface ICustomerRepository
{
    List<Customer> GetAllCustomers();
    Customer GetCustomer(int customerId);
    bool InserCustomer(Customer customer);
    bool UpdateCustomer(Customer customer);
    bool DeleteCustomer(Customer customer);
    bool DeleteCustomer(int customerId);
    void Save();
}
