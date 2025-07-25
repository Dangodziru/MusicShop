using Microsoft.AspNetCore.Mvc;
using MusicShop.API;
using System.Collections.Generic;
using System.Data.SQLite;
using MusicShop.Domain.Entities;

[ApiController]
[Route("api/[controller]")]
public class CustomerController(ILogger<CustomerController> logger) : ControllerBase
{
    const string dbPath = "C:\\Users\\Goida\\AppData\\Roaming\\DBeaverData\\workspace6\\.metadata\\sample-database-sqlite-1\\Chinook.db";
    const string connectionString = $"Data Source={dbPath};Version=3;";

    [HttpGet("All")]
    public List<Customer> GetAll()
    {
        var customers = new List<Customer>();

        using (var connection = new SQLiteConnection(connectionString))
        {
            connection.Open();
            string sql = "SELECT * FROM Customer";

            using (var cmd = new SQLiteCommand(sql, connection))
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    customers.Add(new Customer
                    {
                        CustomerId = (long)reader["CustomerId"],
                        FirstName = reader["FirstName"] as string,
                        LastName = reader["LastName"] as string,
                        Company = reader["Company"] as string,
                        Address = reader["Address"] as string,
                        City = reader["City"] as string,
                        State = reader["State"] as string,
                        Country = reader["Country"] as string,
                        PostalCode = reader["PostalCode"] as string,
                        Phone = reader["Phone"] as string,
                        Fax = reader["Fax"] as string,
                        Email = reader["Email"] as string,
                        SupportRepId = reader["SupportRepId"] as long?
                    });
                }
            }
        }
        return customers;
    }

    [HttpGet("SearchById")]
    public Customer? Get(long customerId)
    {
        using (var connection = new SQLiteConnection(connectionString))
        {
            connection.Open();
            string sql = "SELECT * FROM Customer WHERE CustomerId = @customerId";

            using (var cmd = new SQLiteCommand(sql, connection))
            {
                cmd.Parameters.AddWithValue("@customerId", customerId);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Customer
                        {
                            CustomerId = (long)reader["CustomerId"],
                            FirstName = reader["FirstName"] as string,
                            LastName = reader["LastName"] as string,
                            Company = reader["Company"] as string,
                            Address = reader["Address"] as string,
                            City = reader["City"] as string,
                            State = reader["State"] as string,
                            Country = reader["Country"] as string,
                            PostalCode = reader["PostalCode"] as string,
                            Phone = reader["Phone"] as string,
                            Fax = reader["Fax"] as string,
                            Email = reader["Email"] as string,
                            SupportRepId = reader["SupportRepId"] as long?
                        };
                    }
                }
            }
        }
        return null;
    }

    [HttpGet("Search")]
    public List<Customer> Search(string searchTerm)
    {
        var customers = new List<Customer>();

        using (var connection = new SQLiteConnection(connectionString))
        {
            connection.Open();
            string sql = @"
                SELECT * FROM Customer 
                WHERE FirstName LIKE '%' || @searchTerm || '%' 
                   OR LastName LIKE '%' || @searchTerm || '%' 
                   OR Email LIKE '%' || @searchTerm || '%'";

            using (var cmd = new SQLiteCommand(sql, connection))
            {
                cmd.Parameters.AddWithValue("@searchTerm", searchTerm);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        customers.Add(new Customer
                        {
                            CustomerId = (long)reader["CustomerId"],
                            FirstName = reader["FirstName"] as string,
                            LastName = reader["LastName"] as string,
                            Company = reader["Company"] as string,
                            Address = reader["Address"] as string,
                            City = reader["City"] as string,
                            State = reader["State"] as string,
                            Country = reader["Country"] as string,
                            PostalCode = reader["PostalCode"] as string,
                            Phone = reader["Phone"] as string,
                            Fax = reader["Fax"] as string,
                            Email = reader["Email"] as string,
                            SupportRepId = reader["SupportRepId"] as long?
                        });
                    }
                }
            }
        }
        return customers;
    }

    [HttpPost("InsertCustomer")]
    public long? InsertCustomer(
        string firstName, string lastName, string company,
        string address, string city, string state, string country,
        string postalCode, string phone, string fax, string email, long? supportRepId)
    {
        using (var connection = new SQLiteConnection(connectionString))
        {
            connection.Open();
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

            using (var cmd = new SQLiteCommand(sql, connection))
            {
                cmd.Parameters.AddWithValue("@FirstName", firstName);
                cmd.Parameters.AddWithValue("@LastName", lastName);
                cmd.Parameters.AddWithValue("@Company", company ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Address", address ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@City", city ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@State", state ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Country", country ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@PostalCode", postalCode ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Phone", phone ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Fax", fax ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Email", email ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@SupportRepId", supportRepId ?? (object)DBNull.Value);

                return (long?)cmd.ExecuteScalar();
            }
        }
    }

    [HttpDelete("DeleteCustomer")]
    public IActionResult DeleteCustomer(long customerId)
    {
        using (var connection = new SQLiteConnection(connectionString))
        {
            connection.Open();
            string sql = "DELETE FROM Customer WHERE CustomerId = @customerId";

            using (var cmd = new SQLiteCommand(sql, connection))
            {
                cmd.Parameters.AddWithValue("@customerId", customerId);
                int rowsAffected = cmd.ExecuteNonQuery();

                return rowsAffected > 0
                    ? Ok($"Customer {customerId} deleted successfully")
                    : NotFound($"Customer with ID {customerId} not found");
            }
        }
    }

    [HttpPost("UpdateCustomer")]
    public IActionResult UpdateCustomer(
        long customerId, string firstName, string lastName, string company,
        string address, string city, string state, string country,
        string postalCode, string phone, string fax, string email, long? supportRepId)
    {
        using (var connection = new SQLiteConnection(connectionString))
        {
            connection.Open();
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

            using (var cmd = new SQLiteCommand(sql, connection))
            {
                cmd.Parameters.AddWithValue("@CustomerId", customerId);
                cmd.Parameters.AddWithValue("@FirstName", firstName);
                cmd.Parameters.AddWithValue("@LastName", lastName);
                cmd.Parameters.AddWithValue("@Company", company ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Address", address ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@City", city ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@State", state ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Country", country ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@PostalCode", postalCode ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Phone", phone ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Fax", fax ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Email", email ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@SupportRepId", supportRepId ?? (object)DBNull.Value);

                int rowsAffected = cmd.ExecuteNonQuery();

                return rowsAffected > 0
                    ? Ok($"Customer {customerId} updated successfully")
                    : NotFound($"Customer with ID {customerId} not found");
            }
        }
    }
}