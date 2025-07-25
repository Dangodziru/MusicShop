using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MusicShop.API;
using MusicShop.API.Controllers;
using System.Collections.Generic;
using System.Data.SQLite;
using MusicShop.Domain.Entities;

[ApiController]
[Route("api/[controller]")]
public class ArtistController(ILogger<ArtistController> logger) : ControllerBase
{
    const string dbPath = "C:\\Users\\Goida\\AppData\\Roaming\\DBeaverData\\workspace6\\.metadata\\sample-database-sqlite-1\\Chinook.db";
    const string connectionString = $"Data Source={dbPath};Version=3;";

    [HttpGet("All")]
    public List<Artist> GetAll()
    {

        var list = new List<Artist>();

        // Создание подключения
        using (var connection = new SQLiteConnection(connectionString))
        {
            connection.Open();

            string sql = "SELECT * FROM Artist";

            using (var cmd = new SQLiteCommand(sql, connection))
            using (SQLiteDataReader reader = cmd.ExecuteReader())
            {

                while (reader.Read())
                {

                    list.Add(new Artist { ArtistId = (long)reader["ArtistId"], Name = (string)reader["Name"] });

                }
            }

        }


        return list;

    }
    
    [HttpGet("SearchById")]
    public Artist? Get(long artistId)
    {

        Artist? artist = null;


        // Создание подключения
        using (var connection = new SQLiteConnection(connectionString))
        {
            connection.Open();

            string sql = "SELECT * FROM Artist WHERE ArtistId =" + artistId;

            using (var cmd = new SQLiteCommand(sql, connection))
            using (SQLiteDataReader reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {

                    artist = new Artist { ArtistId = (long)reader["ArtistId"], Name = (string)reader["Name"] };

                }
            }

        }

        return artist;

    }

    [HttpGet("Search")]
    public List<Artist> Search(string titleSearch)
    {

        var list = new List<Artist>();

        // Создание подключения
        using (var connection = new SQLiteConnection(connectionString))
        {
            connection.Open();

            string sql = "SELECT * FROM Artist WHERE Name LIKE '%" + titleSearch + "%'";

            using (var cmd = new SQLiteCommand(sql, connection))
            using (SQLiteDataReader reader = cmd.ExecuteReader())
            {

                while (reader.Read())
                {

                    list.Add(new Artist { ArtistId = (long)reader["ArtistId"], Name = (string)reader["Name"] });

                }
            }

        }

        return list;
    }

    [HttpPost("InsertArtist")]
    public long? InsertArtist(string name)
    {
        long? newId;

        // Создание подключения
        using (var connection = new SQLiteConnection(connectionString))
        {
            connection.Open();

            string sql = "INSERT INTO Artist(Name, ArtistId) VALUES(@Name,@artistId); SELECT last_insert_rowid();";


            using (var cmd = new SQLiteCommand(sql, connection))
            {
                cmd.Parameters.AddWithValue("@Name", name);

                newId = (long)cmd.ExecuteScalar();
            }
        }
        return newId;

    }
   
    [HttpDelete("DeleteArtist")]
    public IActionResult DeleteArtist(long artistId)
    {
        using (var connection = new SQLiteConnection(connectionString))
        {
            connection.Open();
            string sql = "DELETE FROM Artist WHERE ArtistId = @artistId";

            using (var cmd = new SQLiteCommand(sql, connection))
            {
                cmd.Parameters.AddWithValue("@artistId", artistId);

                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    return Ok($"Artist {artistId} deleted successfully");
                }
                else
                {
                    return NotFound($"Artist with ID {artistId} not found");
                }
            }
        }
    }
   
    [HttpPost("UpdateAlbum")]
    public IActionResult UpdateAlbum(long artistId, string name)
    {
        using (var connection = new SQLiteConnection(connectionString))
        {
            connection.Open();
            string sql = @"
            UPDATE Artist 
            SET Name = @name, ArtistId = @artistId 
            WHERE ArtistId = @artistId";

            using (var cmd = new SQLiteCommand(sql, connection))
            {
                cmd.Parameters.AddWithValue("@title", name);
                cmd.Parameters.AddWithValue("@artistId", artistId);

                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    return Ok($"Artist {artistId} updated successfully");
                }
                else
                {
                    return NotFound($"Artist with ID {artistId} not found");
                }
            }
        }
    }
}   
    

