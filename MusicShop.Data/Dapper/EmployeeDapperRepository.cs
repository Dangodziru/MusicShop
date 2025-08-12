using Dapper;
using Microsoft.Extensions.Configuration;
using MusicShop.Domain;
using MusicShop.Domain.Entities;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
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
            using (var connection = new SQLiteConnection(connectionString))
            {
                await connection.OpenAsync();
                return await connection.QueryAsync<Employee>("SELECT * FROM Employee");
            }
        }

        public async Task<Employee?> Get(long employeeId)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                await connection.OpenAsync();
                return await connection.QueryFirstOrDefaultAsync<Employee>(
                    "SELECT * FROM Employee WHERE EmployeeId = @employeeId",
                    new { employeeId }
                );
            }
        }

        public async Task<IEnumerable<Employee>> Search(string searchTerm)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                await connection.OpenAsync();
                const string sql = @"
                    SELECT * FROM Employee 
                    WHERE 
                        FirstName LIKE '%' || @searchTerm || '%' OR
                        LastName LIKE '%' || @searchTerm || '%' OR
                        Email LIKE '%' || @searchTerm || '%'";

                return await connection.QueryAsync<Employee>(sql, new { searchTerm });
            }
        }

        public async Task<long?> InsertEmployee(
            string lastName, string firstName, string title, long? reportsTo,
            DateTime? birthDate, DateTime? hireDate, string address, string city,
            string state, string country, string postalCode, string phone,
            string fax, string email)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@LastName", lastName, DbType.String);
            parameters.Add("@FirstName", firstName, DbType.String);
            parameters.Add("@Title", title, DbType.String);
            parameters.Add("@ReportsTo", reportsTo, DbType.Int64);
            parameters.Add("@BirthDate", birthDate, DbType.DateTime);
            parameters.Add("@HireDate", hireDate, DbType.DateTime);
            parameters.Add("@Address", address, DbType.String);
            parameters.Add("@City", city, DbType.String);
            parameters.Add("@State", state, DbType.String);
            parameters.Add("@Country", country, DbType.String);
            parameters.Add("@PostalCode", postalCode, DbType.String);
            parameters.Add("@Phone", phone, DbType.String);
            parameters.Add("@Fax", fax, DbType.String);
            parameters.Add("@Email", email, DbType.String);

            using (var connection = new SQLiteConnection(connectionString))
            {
                await connection.OpenAsync();
                const string sql = @"
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

                return await connection.ExecuteScalarAsync<long?>(sql, parameters);
            }
        }

        public async Task<bool> DeleteEmployee(long employeeId)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                await connection.OpenAsync();
                int rowsAffected = await connection.ExecuteAsync(
                    "DELETE FROM Employee WHERE EmployeeId = @employeeId",
                    new { employeeId }
                );
                return rowsAffected > 0;
            }
        }

        public async Task<bool> UpdateEmployee(
            long employeeId, string lastName, string firstName, string title, long? reportsTo,
            DateTime? birthDate, DateTime? hireDate, string address, string city,
            string state, string country, string postalCode, string phone,
            string fax, string email)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@EmployeeId", employeeId);
            parameters.Add("@LastName", lastName, DbType.String);
            parameters.Add("@FirstName", firstName, DbType.String);
            parameters.Add("@Title", title, DbType.String);
            parameters.Add("@ReportsTo", reportsTo, DbType.Int64);
            parameters.Add("@BirthDate", birthDate, DbType.DateTime);
            parameters.Add("@HireDate", hireDate, DbType.DateTime);
            parameters.Add("@Address", address, DbType.String);
            parameters.Add("@City", city, DbType.String);
            parameters.Add("@State", state, DbType.String);
            parameters.Add("@Country", country, DbType.String);
            parameters.Add("@PostalCode", postalCode, DbType.String);
            parameters.Add("@Phone", phone, DbType.String);
            parameters.Add("@Fax", fax, DbType.String);
            parameters.Add("@Email", email, DbType.String);

            using (var connection = new SQLiteConnection(connectionString))
            {
                await connection.OpenAsync();
                const string sql = @"
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

                int rowsAffected = await connection.ExecuteAsync(sql, parameters);
                return rowsAffected > 0;
            }
        }
    }
}