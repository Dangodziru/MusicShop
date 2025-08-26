using MusicShop.Bussines.Features.Employes.Delete;
using MusicShop.Bussines.Features.Employes.Get;
using MusicShop.Bussines.Features.Employes.Insert;
using MusicShop.Bussines.Features.Employes.Search;
using MusicShop.Bussines.Features.Employes.Update;
using MusicShop.Domain;
using MusicShop.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MusicShop.Bussines.Features.Employees.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            this.employeeRepository = employeeRepository;
        }

        public  Task<IEnumerable<Employee>> GetAll()
        {
            return  employeeRepository.GetAll();
        }

        public  Task<Employee?> Get(EmployeeGetRequest request)
        {
            return  employeeRepository.Get(request.EmployeeId);
        }

        public  Task<IEnumerable<Employee>> Search(EmployeeSearchRequest request)
        {
            return  employeeRepository.Search(request.SearchTerm);
        }

        public async Task<long?> Insert(EmployeeInsertRequest request)
        {
            return await employeeRepository.InsertEmployee(
                request.LastName, request.FirstName, request.Title,
                request.ReportsTo, request.BirthDate, request.HireDate,
                request.Address, request.City, request.State, request.Country,
                request.PostalCode, request.Phone, request.Fax, request.Email);
        }

        public async Task<bool> Update(EmployeeUpdateRequest request)
        {
            if (await employeeRepository.UpdateEmployee(request.EmployeeId,
                request.LastName, request.FirstName, request.Title,
                request.ReportsTo, request.BirthDate, request.HireDate,
                request.Address, request.City, request.State, request.Country,
                request.PostalCode, request.Phone, request.Fax, request.Email) == true)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public  Task<bool> Delete(EmployeeDeleteRequest request)
        {
            return  employeeRepository.DeleteEmployee(request.EmployeeId);
        }

    }
}