using MusicShop.Domain.Entities;

namespace MusicShop.Domain
{
    public interface IEmployeeRepository
    {
        bool DeleteEmployee(long employeeId);
        Employee? Get(long employeeId);
        List<Employee> GetAll();
        long? InsertEmployee(string lastName, string firstName, string title, long? reportsTo, DateTime? birthDate, DateTime? hireDate, string address, string city, string state, string country, string postalCode, string phone, string fax, string email);
        List<Employee> Search(string searchTerm);
        bool UpdateEmployee(long employeeId, string lastName, string firstName, string title, long? reportsTo, DateTime? birthDate, DateTime? hireDate, string address, string city, string state, string country, string postalCode, string phone, string fax, string email);
    }
}