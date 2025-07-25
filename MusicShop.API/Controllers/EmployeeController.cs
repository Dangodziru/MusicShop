using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using MusicShop.Domain.Entities;

[ApiController]
[Route("api/[controller]")]
public class EmployeeController : ControllerBase
{
    const string dbPath = "C:\\Users\\Goida\\AppData\\Roaming\\DBeaverData\\workspace6\\.metadata\\sample-database-sqlite-1\\Chinook.db";
    const string connectionString = $"Data Source={dbPath};Version=3;";

    [HttpGet("All")]
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
                    employees.Add(MapEmployeeFromReader(reader));
                }
            }
        }
        return employees;
    }

    [HttpGet("SearchById/{employeeId}")]
    public ActionResult<Employee> Get(long employeeId)
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
                        return Ok(MapEmployeeFromReader(reader));
                    }
                }
            }
        }
        return NotFound();
    }

    [HttpGet("Search")]
    public List<Employee> Search([FromQuery] string term)
    {
        var employees = new List<Employee>();

        using (var connection = new SQLiteConnection(connectionString))
        {
            connection.Open();
            const string sql = @"
                SELECT * FROM Employee 
                WHERE 
                    FirstName LIKE @searchTerm OR
                    LastName LIKE @searchTerm OR
                    Email LIKE @searchTerm";

            using (var cmd = new SQLiteCommand(sql, connection))
            {
                cmd.Parameters.AddWithValue("@searchTerm", $"%{term}%");

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        employees.Add(MapEmployeeFromReader(reader));
                    }
                }
            }
        }
        return employees;
    }

    [HttpPost]
    public ActionResult<long> Create([FromBody] Employee employee)
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
                AddEmployeeParameters(cmd, employee);
                var newId = (long)cmd.ExecuteScalar();
                return CreatedAtAction(nameof(Get), new { employeeId = newId }, newId);
            }
        }
    }

    [HttpDelete("{employeeId}")]
    public IActionResult Delete(long employeeId)
    {
        using (var connection = new SQLiteConnection(connectionString))
        {
            connection.Open();
            const string sql = "DELETE FROM Employee WHERE EmployeeId = @employeeId";

            using (var cmd = new SQLiteCommand(sql, connection))
            {
                cmd.Parameters.AddWithValue("@employeeId", employeeId);
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0 ? NoContent() : NotFound();
            }
        }
    }

    [HttpPut("{employeeId}")]
    public IActionResult Update(long employeeId, [FromBody] Employee employee)
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
                AddEmployeeParameters(cmd, employee);

                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0 ? NoContent() : NotFound();
            }
        }
    }

    // Вспомогательные методы
    private Employee MapEmployeeFromReader(SQLiteDataReader reader)
    {
        return new Employee
        {
            EmployeeId = reader.GetInt64("EmployeeId"),
            LastName = reader.IsDBNull("LastName") ? null : reader.GetString("LastName"),
            FirstName = reader.IsDBNull("FirstName") ? null : reader.GetString("FirstName"),
            Title = reader.IsDBNull("Title") ? null : reader.GetString("Title"),
            ReportsTo = reader.IsDBNull("ReportsTo") ? (long?)null : reader.GetInt64("ReportsTo"),
            BirthDate = reader.IsDBNull("BirthDate") ? (DateTime?)null : reader.GetDateTime("BirthDate"),
            HireDate = reader.IsDBNull("HireDate") ? (DateTime?)null : reader.GetDateTime("HireDate"),
            Address = reader.IsDBNull("Address") ? null : reader.GetString("Address"),
            City = reader.IsDBNull("City") ? null : reader.GetString("City"),
            State = reader.IsDBNull("State") ? null : reader.GetString("State"),
            Country = reader.IsDBNull("Country") ? null : reader.GetString("Country"),
            PostalCode = reader.IsDBNull("PostalCode") ? null : reader.GetString("PostalCode"),
            Phone = reader.IsDBNull("Phone") ? null : reader.GetString("Phone"),
            Fax = reader.IsDBNull("Fax") ? null : reader.GetString("Fax"),
            Email = reader.IsDBNull("Email") ? null : reader.GetString("Email")
        };
    }

    private void AddEmployeeParameters(SQLiteCommand cmd, Employee employee)
    {
        cmd.Parameters.AddWithValue("@LastName", employee.LastName ?? (object)DBNull.Value);
        cmd.Parameters.AddWithValue("@FirstName", employee.FirstName ?? (object)DBNull.Value);
        cmd.Parameters.AddWithValue("@Title", employee.Title ?? (object)DBNull.Value);
        cmd.Parameters.AddWithValue("@ReportsTo", employee.ReportsTo ?? (object)DBNull.Value);
        cmd.Parameters.AddWithValue("@BirthDate", employee.BirthDate ?? (object)DBNull.Value);
        cmd.Parameters.AddWithValue("@HireDate", employee.HireDate ?? (object)DBNull.Value);
        cmd.Parameters.AddWithValue("@Address", employee.Address ?? (object)DBNull.Value);
        cmd.Parameters.AddWithValue("@City", employee.City ?? (object)DBNull.Value);
        cmd.Parameters.AddWithValue("@State", employee.State ?? (object)DBNull.Value);
        cmd.Parameters.AddWithValue("@Country", employee.Country ?? (object)DBNull.Value);
        cmd.Parameters.AddWithValue("@PostalCode", employee.PostalCode ?? (object)DBNull.Value);
        cmd.Parameters.AddWithValue("@Phone", employee.Phone ?? (object)DBNull.Value);
        cmd.Parameters.AddWithValue("@Fax", employee.Fax ?? (object)DBNull.Value);
        cmd.Parameters.AddWithValue("@Email", employee.Email ?? (object)DBNull.Value);
    }
}