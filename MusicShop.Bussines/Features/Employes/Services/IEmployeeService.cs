using MusicShop.Bussines.Features.Employes.Delete;
using MusicShop.Bussines.Features.Employes.Get;
using MusicShop.Bussines.Features.Employes.Insert;
using MusicShop.Bussines.Features.Employes.Search;
using MusicShop.Bussines.Features.Employes.Update;
using MusicShop.Domain.Entities;

namespace MusicShop.Bussines.Features.Employees.Services
{
    public interface IEmployeeService
    {
        Task<bool> Delete(EmployeeDeleteRequest request);
        Task<Employee?> Get(EmployeeGetRequest request);
        Task<IEnumerable<Employee>> GetAll();
        Task<long?> Insert(EmployeeInsertRequest request);
        Task<IEnumerable<Employee>> Search(EmployeeSearchRequest request);
        Task<bool> Update(EmployeeUpdateRequest request);
    }
}