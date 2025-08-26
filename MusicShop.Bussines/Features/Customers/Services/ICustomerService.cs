using MusicShop.Bussines.Features.Customers.Delete;
using MusicShop.Bussines.Features.Customers.Get;
using MusicShop.Bussines.Features.Customers.Insert;
using MusicShop.Bussines.Features.Customers.Search;
using MusicShop.Bussines.Features.Customers.Update;
using MusicShop.Domain.Entities;

namespace MusicShop.Bussines.Features.Customers.Services
{
    public interface ICustomerService
    {
        Task<bool> Delete(DeleteRequest request);
        Task<Customer?> Get(GetRequest request);
        Task<IEnumerable<Customer>> GetAll();
        Task<long?> Insert(InsertRequest request);
        Task<IEnumerable<Customer>> Search(SearchRequest request);
        Task<bool> Update(UpdateRequest request);
    }
}