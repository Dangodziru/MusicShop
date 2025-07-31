using MusicShop.Domain.Entities;

namespace MusicShop.Domain
{
    public interface ICustomerRepositoty
    {
        bool DeleteCustomer(long customerId);
        Customer? Get(long customerId);
        List<Customer> GetAll();
        long? InsertCustomer(string firstName, string lastName, string company, string address, string city, string state, string country, string postalCode, string phone, string fax, string email, long? supportRepId);
        List<Customer> Search(string searchTerm);
        bool UpdateCustomer(long customerId, string firstName, string lastName, string company, string address, string city, string state, string country, string postalCode, string phone, string fax, string email, long? supportRepId);
        bool CustomerIsExist(string email);
    }
}