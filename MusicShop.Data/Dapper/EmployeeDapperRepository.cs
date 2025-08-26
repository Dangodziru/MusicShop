using Dapper;
using Microsoft.Extensions.Configuration;
using MusicShop.Domain;
using MusicShop.Domain.Entities;
using System.Data;
using System.Data.SQLite;
using System.Threading.Tasks;

namespace MusicShop.Data.Dapper
{
    public class EmployeeDapperRepository : IEmployeeRepository
    {
        protected readonly string connectionString;

        public EmployeeDapperRepository(IConfiguration config)
        {
            connectionString = config.GetConnectionString("MusicShop")!;
        }

        public async Task<IEnumerable<Employee>> GetAll()
        {
            using var connection = new SQLiteConnection(connectionString);
            return await connection.QueryAsync<Employee>("SELECT * FROM Employee");
        }

        public async Task<Employee?> Get(long employeeId)
        {
            using var connection = new SQLiteConnection(connectionString);
            return await connection.QueryFirstOrDefaultAsync<Employee>(
                "SELECT * FROM Employee WHERE EmployeeId = @employeeId",
                new { employeeId }
            );
        }

        public async Task<IEnumerable<Employee>> Search(string searchTerm)
        {
            using var connection = new SQLiteConnection(connectionString);
            string termPattern = $"%{searchTerm}%";
            string sql = @"
                SELECT * FROM Employee 
                WHERE 
                    FirstName LIKE @termPattern OR
                    LastName LIKE @termPattern OR
                    Email LIKE @termPattern";

            return await connection.QueryAsync<Employee>(sql, new { termPattern });
        }

        public async Task<long?> InsertEmployee(
            string lastName, string firstName, string title, long? reportsTo,
            DateTime? birthDate, DateTime? hireDate, string address, string city,
            string state, string country, string postalCode, string phone,
            string fax, string email)
        {
            using var connection = new SQLiteConnection(connectionString);
            string sql = @"
                INSERT INTO Employee (
                    LastName, FirstName, Title, ReportsTo, BirthDate,
                    HireDate, Address, City, State, Country,
                    PostalCode, Phone, Fax, Email
                ) 
                VALUES (
                    @LastName, @FirstName, @Title, @ReportsTo, @BirthDate,
                    @HireDate, @Address, @City, @State, @Country,
                    @PostalCode, @Phone, @Fax, @Email
                );
                SELECT last_insert_rowid();";

            return await connection.ExecuteScalarAsync<long?>(sql, new
            {
                LastName = lastName,
                FirstName = firstName,
                Title = title,
                ReportsTo = reportsTo,
                BirthDate = birthDate,
                HireDate = hireDate,
                Address = address,
                City = city,
                State = state,
                Country = country,
                PostalCode = postalCode,
                Phone = phone,
                Fax = fax,
                Email = email
            });
        }

        public async Task<bool> DeleteEmployee(long employeeId)
        {
            using var connection = new SQLiteConnection(connectionString);
            int rowsAffected = await connection.ExecuteAsync(
                "DELETE FROM Employee WHERE EmployeeId = @employeeId",
                new { employeeId }
            );
            return rowsAffected > 0;
        }

        public async Task<bool> UpdateEmployee(
            long employeeId, string lastName, string firstName, string title, long? reportsTo,
            DateTime? birthDate, DateTime? hireDate, string address, string city,
            string state, string country, string postalCode, string phone,
            string fax, string email)
        {
            using var connection = new SQLiteConnection(connectionString);
            string sql = @"
                UPDATE Employee 
                SET 
                    LastName = @LastName,
                    FirstName = @FirstName,
                    Title = @Title,
                    ReportsTo = @ReportsTo,
                    BirthDate = @BirthDate,
                    HireDate = @HireDate,
                    Address = @Address,
                    City = @City,
                    State = @State,
                    Country = @Country,
                    PostalCode = @PostalCode,
                    Phone = @Phone,
                    Fax = @Fax,
                    Email = @Email
                WHERE EmployeeId = @EmployeeId";

            int rowsAffected = await connection.ExecuteAsync(sql, new
            {
                EmployeeId = employeeId,
                LastName = lastName,
                FirstName = firstName,
                Title = title,
                ReportsTo = reportsTo,
                BirthDate = birthDate,
                HireDate = hireDate,
                Address = address,
                City = city,
                State = state,
                Country = country,
                PostalCode = postalCode,
                Phone = phone,
                Fax = fax,
                Email = email
            });
            return rowsAffected > 0;
        }
    }
}