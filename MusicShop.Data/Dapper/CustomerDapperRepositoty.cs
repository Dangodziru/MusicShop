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
    public class CustomerDapperRepository : ICustomerRepositoty
    {
        protected readonly string connectionString;

        public CustomerDapperRepository(IConfiguration config)
        {
            connectionString = config.GetConnectionString("MusicShop")!;
        }

        public async Task<IEnumerable<Customer>> GetAll()
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                await connection.OpenAsync();
                return await connection.QueryAsync<Customer>("SELECT * FROM Customer");
            }
        }

        public async Task<Customer?> Get(long customerId)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                await connection.OpenAsync();
                return await connection.QueryFirstOrDefaultAsync<Customer>(
                    "SELECT * FROM Customer WHERE CustomerId = @customerId",
                    new { customerId }
                );
            }
        }

        public async Task<IEnumerable<Customer>> Search(string searchTerm)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                await connection.OpenAsync();
                string sql = @"
                    SELECT * FROM Customer 
                    WHERE FirstName LIKE '%' || @searchTerm || '%' 
                       OR LastName LIKE '%' || @searchTerm || '%' 
                       OR Email LIKE '%' || @searchTerm || '%'";

                return await connection.QueryAsync<Customer>(sql, new { searchTerm });
            }
        }

        public async Task<long?> InsertCustomer(
            string firstName, string lastName, string company,
            string address, string city, string state, string country,
            string postalCode, string phone, string fax, string email, long? supportRepId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@FirstName", firstName);
            parameters.Add("@LastName", lastName);
            parameters.Add("@Company", company, DbType.String);
            parameters.Add("@Address", address, DbType.String);
            parameters.Add("@City", city, DbType.String);
            parameters.Add("@State", state, DbType.String);
            parameters.Add("@Country", country, DbType.String);
            parameters.Add("@PostalCode", postalCode, DbType.String);
            parameters.Add("@Phone", phone, DbType.String);
            parameters.Add("@Fax", fax, DbType.String);
            parameters.Add("@Email", email, DbType.String);
            parameters.Add("@SupportRepId", supportRepId, DbType.Int64);

            using (var connection = new SQLiteConnection(connectionString))
            {
                await connection.OpenAsync();
                string sql = @"
                    INSERT INTO Customer (
                        FirstName, LastName, Company, Address, City, 
                        State, Country, PostalCode, Phone, Fax, Email, SupportRepId
                    ) 
                    VALUES (
                        @FirstName, @LastName, @Company, @Address, @City,
                        @State, @Country, @PostalCode, @Phone, @Fax, @Email, @SupportRepId
                    );
                    SELECT last_insert_rowid();";

                return await connection.ExecuteScalarAsync<long?>(sql, parameters);
            }
        }

        public async Task<bool> DeleteCustomer(long customerId)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                await connection.OpenAsync();
                int rowsAffected = await connection.ExecuteAsync(
                    "DELETE FROM Customer WHERE CustomerId = @customerId",
                    new { customerId }
                );
                return rowsAffected > 0;
            }
        }

        public async Task<bool> UpdateCustomer(
            long customerId, string firstName, string lastName, string company,
            string address, string city, string state, string country,
            string postalCode, string phone, string fax, string email, long? supportRepId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@CustomerId", customerId);
            parameters.Add("@FirstName", firstName);
            parameters.Add("@LastName", lastName);
            parameters.Add("@Company", company, DbType.String);
            parameters.Add("@Address", address, DbType.String);
            parameters.Add("@City", city, DbType.String);
            parameters.Add("@State", state, DbType.String);
            parameters.Add("@Country", country, DbType.String);
            parameters.Add("@PostalCode", postalCode, DbType.String);
            parameters.Add("@Phone", phone, DbType.String);
            parameters.Add("@Fax", fax, DbType.String);
            parameters.Add("@Email", email, DbType.String);
            parameters.Add("@SupportRepId", supportRepId, DbType.Int64);

            using (var connection = new SQLiteConnection(connectionString))
            {
                await connection.OpenAsync();
                string sql = @"
                    UPDATE Customer 
                    SET 
                        FirstName = @FirstName,
                        LastName = @LastName,
                        Company = @Company,
                        Address = @Address,
                        City = @City,
                        State = @State,
                        Country = @Country,
                        PostalCode = @PostalCode,
                        Phone = @Phone,
                        Fax = @Fax,
                        Email = @Email,
                        SupportRepId = @SupportRepId
                    WHERE CustomerId = @CustomerId";

                int rowsAffected = await connection.ExecuteAsync(sql, parameters);
                return rowsAffected > 0;
            }
        }

        public async Task<bool> CustomerIsExist(string email)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                await connection.OpenAsync();
                var existing = await connection.QueryFirstOrDefaultAsync<Customer>(
                    "SELECT 1 FROM Customer WHERE Email = @email",
                    new { email }
                );
                return existing != null;
            }
        }
    }
}