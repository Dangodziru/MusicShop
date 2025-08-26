using Dapper;
using Microsoft.Extensions.Configuration;
using MusicShop.Domain;
using MusicShop.Domain.Entities;
using System.Data.SQLite;


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
            using var connection = new SQLiteConnection(connectionString);
            return await connection.QueryAsync<Customer>("SELECT * FROM Customer");
        }

        public async Task<Customer?> Get(long customerId)
        {
            using var connection = new SQLiteConnection(connectionString);
            return await connection.QueryFirstOrDefaultAsync<Customer>(
                "SELECT * FROM Customer WHERE CustomerId = @customerId",
                new { customerId }
            );
        }

        public async Task<IEnumerable<Customer>> Search(string searchTerm)
        {
            using var connection = new SQLiteConnection(connectionString);
            string termPattern = $"%{searchTerm}%";
            string sql = @"
                SELECT * FROM Customer 
                WHERE FirstName LIKE @termPattern 
                   OR LastName LIKE @termPattern 
                   OR Email LIKE @termPattern";

            return await connection.QueryAsync<Customer>(sql, new { termPattern });
        }

        public async Task<long?> InsertCustomer(
            string firstName, string lastName, string company,
            string address, string city, string state, string country,
            string postalCode, string phone, string fax, string email, long? supportRepId)
        {
            using var connection = new SQLiteConnection(connectionString);
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

            return await connection.ExecuteScalarAsync<long?>(sql, new
            {
                firstName,
                lastName,
                company,
                address,
                city,
                state,
                country,
                postalCode,
                phone,
                fax,
                email,
                supportRepId
            });
        }

        public async Task<bool> DeleteCustomer(long customerId)
        {
            using var connection = new SQLiteConnection(connectionString);
            int rowsAffected = await connection.ExecuteAsync(
                "DELETE FROM Customer WHERE CustomerId = @customerId",
                new { customerId }
            );
            return rowsAffected > 0;
        }

        public async Task<bool> UpdateCustomer(
            long customerId, string firstName, string lastName, string company,
            string address, string city, string state, string country,
            string postalCode, string phone, string fax, string email, long? supportRepId)
        {
            using var connection = new SQLiteConnection(connectionString);
            string sql = @"
                UPDATE Customer 
                SET 
                    FirstName = @firstName,
                    LastName = @lastName,
                    Company = @company,
                    Address = @address,
                    City = @city,
                    State = @state,
                    Country = @country,
                    PostalCode = @postalCode,
                    Phone = @phone,
                    Fax = @fax,
                    Email = @email,
                    SupportRepId = @supportRepId
                WHERE CustomerId = @customerId";

            int rowsAffected = await connection.ExecuteAsync(sql, new
            {
                customerId,
                firstName,
                lastName,
                company,
                address,
                city,
                state,
                country,
                postalCode,
                phone,
                fax,
                email,
                supportRepId
            });
            return rowsAffected > 0;
        }

        public async Task<bool> CustomerIsExist(string email)
        {
            using var connection = new SQLiteConnection(connectionString);
            var existing = await connection.QueryFirstOrDefaultAsync<Customer>(
                "SELECT 1 FROM Customer WHERE LOWER(Email) = LOWER(@email)",
                new { email }
            );
            return existing != null;
        }
    }
}