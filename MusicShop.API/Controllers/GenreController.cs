using Microsoft.AspNetCore.Mvc;
using MusicShop.API;
using System.Collections.Generic;
using System.Data.SQLite;
using MusicShop.Domain.Entities;

[ApiController]
[Route("api/[controller]")]
public class GenreController : ControllerBase
{
    const string dbPath = "C:\\Users\\Goida\\AppData\\Roaming\\DBeaverData\\workspace6\\.metadata\\sample-database-sqlite-1\\Chinook.db";
    const string connectionString = $"Data Source={dbPath};Version=3;";

    [HttpGet("All")]
    public List<Genre> GetAll()
    {
        var genres = new List<Genre>();

        using (var connection = new SQLiteConnection(connectionString))
        {
            connection.Open();
            const string sql = "SELECT * FROM Genre";

            using (var cmd = new SQLiteCommand(sql, connection))
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    genres.Add(new Genre
                    {
                        GenreId = (long)reader["GenreId"],
                        Name = (string)reader["Name"]
                    });
                }
            }
        }
        return genres;
    }

    [HttpGet("SearchById/{genreId}")]
    public ActionResult<Genre> Get(long genreId)
    {
        using (var connection = new SQLiteConnection(connectionString))
        {
            connection.Open();
            const string sql = "SELECT * FROM Genre WHERE GenreId = @genreId";

            using (var cmd = new SQLiteCommand(sql, connection))
            {
                cmd.Parameters.AddWithValue("@genreId", genreId);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return Ok(new Genre
                        {
                            GenreId = (long)reader["GenreId"],
                            Name = (string)reader["Name"]
                        });
                    }
                }
            }
        }
        return NotFound();
    }

    [HttpGet("Search")]
    public List<Genre> Search([FromQuery] string term)
    {
        var genres = new List<Genre>();

        using (var connection = new SQLiteConnection(connectionString))
        {
            connection.Open();
            const string sql = "SELECT * FROM Genre WHERE Name LIKE @searchTerm";

            using (var cmd = new SQLiteCommand(sql, connection))
            {
                cmd.Parameters.AddWithValue("@searchTerm", $"%{term}%");

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        genres.Add(new Genre
                        {
                            GenreId = (long)reader["GenreId"],
                            Name = (string)reader["Name"]
                        });
                    }
                }
            }
        }
        return genres;
    }

    [HttpPost]
    public ActionResult<long> Create([FromBody] Genre genre)
    {
        using (var connection = new SQLiteConnection(connectionString))
        {
            connection.Open();
            const string sql = @"
                INSERT INTO Genre (Name)
                VALUES (@Name);
                SELECT last_insert_rowid();";

            using (var cmd = new SQLiteCommand(sql, connection))
            {
                cmd.Parameters.AddWithValue("@Name", genre.Name);
                var newId = (long)cmd.ExecuteScalar();
                return CreatedAtAction(nameof(Get), new { genreId = newId }, newId);
            }
        }
    }

    [HttpPut("{genreId}")]
    public IActionResult Update(long genreId, [FromBody] Genre genre)
    {
        using (var connection = new SQLiteConnection(connectionString))
        {
            connection.Open();
            const string sql = @"
                UPDATE Genre 
                SET Name = @Name
                WHERE GenreId = @GenreId";

            using (var cmd = new SQLiteCommand(sql, connection))
            {
                cmd.Parameters.AddWithValue("@GenreId", genreId);
                cmd.Parameters.AddWithValue("@Name", genre.Name);

                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0 ? NoContent() : NotFound();
            }
        }
    }

    [HttpDelete("{genreId}")]
    public IActionResult Delete(long genreId)
    {
        using (var connection = new SQLiteConnection(connectionString))
        {
            connection.Open();
            const string sql = "DELETE FROM Genre WHERE GenreId = @genreId";

            using (var cmd = new SQLiteCommand(sql, connection))
            {
                cmd.Parameters.AddWithValue("@genreId", genreId);
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0 ? NoContent() : NotFound();
            }
        }
    }
}