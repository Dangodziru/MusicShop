using Microsoft.AspNetCore.Mvc;
using MusicShop.API;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using MusicShop.Domain.Entities;

[ApiController]
[Route("api/[controller]")]
public class InvoiceController : ControllerBase
{
    const string dbPath = "C:\\Users\\Goida\\AppData\\Roaming\\DBeaverData\\workspace6\\.metadata\\sample-database-sqlite-1\\Chinook.db";
    const string connectionString = $"Data Source={dbPath};Version=3;";

    [HttpGet("All")]
    public List<Invoice> GetAll()
    {
        var invoices = new List<Invoice>();

        using (var connection = new SQLiteConnection(connectionString))
        {
            connection.Open();
            const string sql = "SELECT * FROM Invoice";

            using (var cmd = new SQLiteCommand(sql, connection))
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    invoices.Add(MapInvoiceFromReader(reader));
                }
            }
        }
        return invoices;
    }

    [HttpGet("SearchById/{invoiceId}")]
    public ActionResult<Invoice> Get(long invoiceId)
    {
        using (var connection = new SQLiteConnection(connectionString))
        {
            connection.Open();
            const string sql = "SELECT * FROM Invoice WHERE InvoiceId = @invoiceId";

            using (var cmd = new SQLiteCommand(sql, connection))
            {
                cmd.Parameters.AddWithValue("@invoiceId", invoiceId);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return Ok(MapInvoiceFromReader(reader));
                    }
                }
            }
        }
        return NotFound();
    }

    [HttpGet("SearchByCustomer")]
    public List<Invoice> GetByCustomer([FromQuery] long customerId)
    {
        var invoices = new List<Invoice>();

        using (var connection = new SQLiteConnection(connectionString))
        {
            connection.Open();
            const string sql = "SELECT * FROM Invoice WHERE CustomerId = @customerId";

            using (var cmd = new SQLiteCommand(sql, connection))
            {
                cmd.Parameters.AddWithValue("@customerId", customerId);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        invoices.Add(MapInvoiceFromReader(reader));
                    }
                }
            }
        }
        return invoices;
    }

    [HttpPost]
    public ActionResult<long> Create([FromBody] Invoice invoice)
    {
        using (var connection = new SQLiteConnection(connectionString))
        {
            connection.Open();
            const string sql = @"
                INSERT INTO Invoice (
                    CustomerId, InvoiceDate, BillingAddress, BillingCity,
                    BillingState, BillingCountry, BillingPostalCode, Total
                ) 
                VALUES (
                    @CustomerId, @InvoiceDate, @BillingAddress, @BillingCity,
                    @BillingState, @BillingCountry, @BillingPostalCode, @Total
                );
                SELECT last_insert_rowid();";

            using (var cmd = new SQLiteCommand(sql, connection))
            {
                AddInvoiceParameters(cmd, invoice);
                var newId = (long)cmd.ExecuteScalar();
                return CreatedAtAction(nameof(Get), new { invoiceId = newId }, newId);
            }
        }
    }

    [HttpPut("{invoiceId}")]
    public IActionResult Update(long invoiceId, [FromBody] Invoice invoice)
    {
        using (var connection = new SQLiteConnection(connectionString))
        {
            connection.Open();
            const string sql = @"
                UPDATE Invoice 
                SET 
                    CustomerId = @CustomerId,
                    InvoiceDate = @InvoiceDate,
                    BillingAddress = @BillingAddress,
                    BillingCity = @BillingCity,
                    BillingState = @BillingState,
                    BillingCountry = @BillingCountry,
                    BillingPostalCode = @BillingPostalCode,
                    Total = @Total
                WHERE InvoiceId = @InvoiceId";

            using (var cmd = new SQLiteCommand(sql, connection))
            {
                cmd.Parameters.AddWithValue("@InvoiceId", invoiceId);
                AddInvoiceParameters(cmd, invoice);

                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0 ? NoContent() : NotFound();
            }
        }
    }

    [HttpDelete("{invoiceId}")]
    public IActionResult Delete(long invoiceId)
    {
        using (var connection = new SQLiteConnection(connectionString))
        {
            connection.Open();
            const string sql = "DELETE FROM Invoice WHERE InvoiceId = @invoiceId";

            using (var cmd = new SQLiteCommand(sql, connection))
            {
                cmd.Parameters.AddWithValue("@invoiceId", invoiceId);
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0 ? NoContent() : NotFound();
            }
        }
    }

    // Вспомогательные методы
    private Invoice MapInvoiceFromReader(SQLiteDataReader reader)
    {
        return new Invoice
        {
            InvoiceId = reader.GetInt64("InvoiceId"),
            CustomerId = reader.GetInt64("CustomerId"),
            InvoiceDate = reader.GetDateTime("InvoiceDate"),
            BillingAddress = reader.IsDBNull("BillingAddress") ? null : reader.GetString("BillingAddress"),
            BillingCity = reader.IsDBNull("BillingCity") ? null : reader.GetString("BillingCity"),
            BillingState = reader.IsDBNull("BillingState") ? null : reader.GetString("BillingState"),
            BillingCountry = reader.IsDBNull("BillingCountry") ? null : reader.GetString("BillingCountry"),
            BillingPostalCode = reader.IsDBNull("BillingPostalCode") ? null : reader.GetString("BillingPostalCode"),
            Total = reader.GetDecimal("Total")
        };
    }

    private void AddInvoiceParameters(SQLiteCommand cmd, Invoice invoice)
    {
        cmd.Parameters.AddWithValue("@CustomerId", invoice.CustomerId);
        cmd.Parameters.AddWithValue("@InvoiceDate", invoice.InvoiceDate);
        cmd.Parameters.AddWithValue("@BillingAddress", invoice.BillingAddress ?? (object)DBNull.Value);
        cmd.Parameters.AddWithValue("@BillingCity", invoice.BillingCity ?? (object)DBNull.Value);
        cmd.Parameters.AddWithValue("@BillingState", invoice.BillingState ?? (object)DBNull.Value);
        cmd.Parameters.AddWithValue("@BillingCountry", invoice.BillingCountry ?? (object)DBNull.Value);
        cmd.Parameters.AddWithValue("@BillingPostalCode", invoice.BillingPostalCode ?? (object)DBNull.Value);
        cmd.Parameters.AddWithValue("@Total", invoice.Total);
    }
}