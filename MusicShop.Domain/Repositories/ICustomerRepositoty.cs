using MusicShop.Domain.Entities;

namespace MusicShop.Domain
{
    public interface ICustomerRepositoty
    {
        Task<bool> DeleteCustomer(long customerId);
        Task<Customer?> Get(long customerId);
        Task<IEnumerable<Customer>> GetAll();
        Task<long?> InsertCustomer(
            string firstName, string lastName, string company,
            string address, string city, string state, string country,
            string postalCode, string phone, string fax, string email, long? supportRepId);
        Task<IEnumerable<Customer>> Search(string searchTerm);
        Task<bool> UpdateCustomer(
            long customerId, string firstName, string lastName, string company,
            string address, string city, string state, string country,
            string postalCode, string phone, string fax, string email, long? supportRepId);
        Task<bool> CustomerIsExist(string email);
    }
}