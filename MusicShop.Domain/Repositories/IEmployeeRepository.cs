using MusicShop.Domain.Entities;

namespace MusicShop.Domain
{
    public interface IEmployeeRepository
    {
        Task<bool> DeleteEmployee(long employeeId);
        Task<Employee?> Get(long employeeId);
        Task<IEnumerable<Employee>> GetAll();
        Task<long?> InsertEmployee(
            string lastName, string firstName, string title, long? reportsTo,
            DateTime? birthDate, DateTime? hireDate, string address, string city,
            string state, string country, string postalCode, string phone,
            string fax, string email);
        Task<IEnumerable<Employee>> Search(string searchTerm);
        Task<bool> UpdateEmployee(
            long employeeId, string lastName, string firstName, string title, long? reportsTo,
            DateTime? birthDate, DateTime? hireDate, string address, string city,
            string state, string country, string postalCode, string phone,
            string fax, string email);
    }
}