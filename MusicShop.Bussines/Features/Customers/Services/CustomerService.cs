using MusicShop.Domain;
using MusicShop.Domain.Entities;
using MusicShop.Bussines.Features.Customers.Search;
using MusicShop.Bussines.Features.Customers.Update;
using MusicShop.Bussines.Features.Customers.Get;
using MusicShop.Bussines.Features.Customers.Delete;
using MusicShop.Bussines.Features.Customers.Insert;

namespace MusicShop.Bussines.Features.Customers.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepositoty customerRepository;

        public CustomerService(ICustomerRepositoty customerRepository)
        {
            this.customerRepository = customerRepository;
        }

        public  Task<IEnumerable<Customer>> GetAll()
        {
            return  customerRepository.GetAll();
        }

        public  Task<Customer?> Get(GetRequest request)
        {
            return  customerRepository.Get(request.CustomerId);
        }

        public  Task<IEnumerable<Customer>> Search(SearchRequest request)
        {
            return  customerRepository.Search(request.SearchTerm);
        }

        public async Task<long?> Insert(InsertRequest request)
        {
            if (await customerRepository.CustomerIsExist(request.Email))
            {
                return null;
            }

            return await customerRepository.InsertCustomer(
                request.FirstName,
                request.LastName,
                request.Company,
                request.Address,
                request.City,
                request.State,
                request.Country,
                request.PostalCode,
                request.Phone,
                request.Fax,
                request.Email,
                request.SupportRepId);
        }

        public async Task<bool> Update(UpdateRequest request)
        {
            var existing = await customerRepository.Get(request.CustomerId);
            if (existing == null)
            {
                return false;
            }

            if (!string.Equals(existing.Email, request.Email, System.StringComparison.OrdinalIgnoreCase)
                && await customerRepository.CustomerIsExist(request.Email))
            {
                return false;
            }

            return await customerRepository.UpdateCustomer(
                request.CustomerId,
                request.FirstName,
                request.LastName,
                request.Company,
                request.Address,
                request.City,
                request.State,
                request.Country,
                request.PostalCode,
                request.Phone,
                request.Fax,
                request.Email,
                request.SupportRepId);
        }

        public  Task<bool> Delete(DeleteRequest request)
        {
            return  customerRepository.DeleteCustomer(request.CustomerId);
        }
    }

}
