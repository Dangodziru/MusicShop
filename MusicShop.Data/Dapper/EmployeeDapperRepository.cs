using MusicShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicShop.Domain;

namespace MusicShop.Data.Dapper
{
    public class EmployeeDapperRepository : IEmployeeRepository
    {
        const string dbPath = "C:\\Users\\Goida\\AppData\\Roaming\\DBeaverData\\workspace6\\.metadata\\sample-database-sqlite-1\\Chinook.db";
        const string connectionString = $"Data Source={dbPath};Version=3;";

        public List<Employee> GetAll()
        {
            var employees = new List<Employee>();

            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                const string sql = "SELECT * FROM Employee";

                using (var cmd = new SQLiteCommand(sql, connection))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        employees.Add(new Employee
                        {
                            EmployeeId = (long)reader["EmployeeId"],
                            LastName = reader["LastName"] as string,
                            FirstName = reader["FirstName"] as string,
                            Title = reader["Title"] as string,
                            ReportsTo = reader["ReportsTo"] as long?,
                            BirthDate = reader["BirthDate"] as DateTime?,
                            HireDate = reader["HireDate"] as DateTime?,
                            Address = reader["Address"] as string,
                            City = reader["City"] as string,
                            State = reader["State"] as string,
                            Country = reader["Country"] as string,
                            PostalCode = reader["PostalCode"] as string,
                            Phone = reader["Phone"] as string,
                            Fax = reader["Fax"] as string,
                            Email = reader["Email"] as string
                        });
                    }
                }
            }
            return employees;
        }

        public Employee? Get(long employeeId)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                const string sql = "SELECT * FROM Employee WHERE EmployeeId = @employeeId";

                using (var cmd = new SQLiteCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@employeeId", employeeId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Employee
                            {
                                EmployeeId = (long)reader["EmployeeId"],
                                LastName = reader["LastName"] as string,
                                FirstName = reader["FirstName"] as string,
                                Title = reader["Title"] as string,
                                ReportsTo = reader["ReportsTo"] as long?,
                                BirthDate = reader["BirthDate"] as DateTime?,
                                HireDate = reader["HireDate"] as DateTime?,
                                Address = reader["Address"] as string,
                                City = reader["City"] as string,
                                State = reader["State"] as string,
                                Country = reader["Country"] as string,
                                PostalCode = reader["PostalCode"] as string,
                                Phone = reader["Phone"] as string,
                                Fax = reader["Fax"] as string,
                                Email = reader["Email"] as string
                            };
                        }
                    }
                }
            }
            return null;
        }

        public List<Employee> Search(string searchTerm)
        {
            var employees = new List<Employee>();

            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                const string sql = @"
                SELECT * FROM Employee 
                WHERE 
                    FirstName LIKE '%' || @searchTerm || '%' OR
                    LastName LIKE '%' || @searchTerm || '%' OR
                    Email LIKE '%' || @searchTerm || '%'";

                using (var cmd = new SQLiteCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@searchTerm", searchTerm);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            employees.Add(new Employee
                            {
                                EmployeeId = (long)reader["EmployeeId"],
                                LastName = reader["LastName"] as string,
                                FirstName = reader["FirstName"] as string,
                                Title = reader["Title"] as string,
                                ReportsTo = reader["ReportsTo"] as long?,
                                BirthDate = reader["BirthDate"] as DateTime?,
                                HireDate = reader["HireDate"] as DateTime?,
                                Address = reader["Address"] as string,
                                City = reader["City"] as string,
                                State = reader["State"] as string,
                                Country = reader["Country"] as string,
                                PostalCode = reader["PostalCode"] as string,
                                Phone = reader["Phone"] as string,
                                Fax = reader["Fax"] as string,
                                Email = reader["Email"] as string
                            });
                        }
                    }
                }
            }
            return employees;
        }

        public long? InsertEmployee(
            string lastName, string firstName, string title, long? reportsTo,
            DateTime? birthDate, DateTime? hireDate, string address, string city,
            string state, string country, string postalCode, string phone,
            string fax, string email)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
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

                using (var cmd = new SQLiteCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@LastName", lastName ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@FirstName", firstName ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Title", title ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@ReportsTo", reportsTo ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@BirthDate", birthDate ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@HireDate", hireDate ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Address", address ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@City", city ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@State", state ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Country", country ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@PostalCode", postalCode ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Phone", phone ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Fax", fax ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Email", email ?? (object)DBNull.Value);

                    return (long?)cmd.ExecuteScalar();
                }
            }
        }

        public bool DeleteEmployee(long employeeId)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                const string sql = "DELETE FROM Employee WHERE EmployeeId = @employeeId";

                using (var cmd = new SQLiteCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@employeeId", employeeId);
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }

        public bool UpdateEmployee(
            long employeeId, string lastName, string firstName, string title, long? reportsTo,
            DateTime? birthDate, DateTime? hireDate, string address, string city,
            string state, string country, string postalCode, string phone,
            string fax, string email)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
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

                using (var cmd = new SQLiteCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@EmployeeId", employeeId);
                    cmd.Parameters.AddWithValue("@LastName", lastName ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@FirstName", firstName ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Title", title ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@ReportsTo", reportsTo ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@BirthDate", birthDate ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@HireDate", hireDate ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Address", address ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@City", city ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@State", state ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Country", country ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@PostalCode", postalCode ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Phone", phone ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Fax", fax ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Email", email ?? (object)DBNull.Value);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }
    }
}