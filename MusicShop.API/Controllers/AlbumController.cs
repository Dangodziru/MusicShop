using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MusicShop.Data;
using MusicShop.Domain.Entities;
using System.Collections.Generic;
using System.Data.SQLite;

namespace MusicShop.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlbumController : ControllerBase
    {
        const string dbPath = "C:\\Users\\Goida\\AppData\\Roaming\\DBeaverData\\workspace6\\.metadata\\sample-database-sqlite-1\\Chinook.db";
        const string connectionString = $"Data Source={dbPath};Version=3;";
        AlbumRepository albumRepository;
        public AlbumController()
        {
            albumRepository = new AlbumRepository();
        }

        [HttpGet("All")]
        public List<Album> GetAll()
        {
            var albums =  albumRepository.GetAll();
            return albums;
        }

        [HttpGet("SearchById")]
        public Album? Get(long albumId)
        {
            var album = albumRepository.Get(albumId);
            return album;
        }

        [HttpGet("Search")]
        public List<Album> Search(string titleSearch)
        {
            var albums = albumRepository.Search(titleSearch);
            return albums;
        }

        [HttpPost("InsertAlbum")]
        public long? InsertAlbum(string title, long artistId)
        {
            long? newId;

            // Создание подключения
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string sql = "INSERT INTO Album(title, ArtistId) VALUES(@title,@artistId); SELECT last_insert_rowid();";


                using (var cmd = new SQLiteCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@title", title);
                    cmd.Parameters.AddWithValue("@artistId", artistId);

                    newId = (long)cmd.ExecuteScalar();
                }
            }
            return newId;

        }
        
        [HttpDelete("DeleteAlbum")]
        public IActionResult DeleteAlbum(long albumId)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string sql = "DELETE FROM Album WHERE AlbumId = @albumId";

                using (var cmd = new SQLiteCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@albumId", albumId);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        return Ok($"Album {albumId} deleted successfully");
                    }
                    else
                    {
                        return NotFound($"Album with ID {albumId} not found");
                    }
                }
            }
        }
        
        [HttpPost("UpdateAlbum")]
        public IActionResult UpdateAlbum(long albumId, string title, long artistId)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string sql = @"
            UPDATE Album 
            SET Title = @title, ArtistId = @artistId 
            WHERE AlbumId = @albumId";

                using (var cmd = new SQLiteCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@title", title);
                    cmd.Parameters.AddWithValue("@artistId", artistId);
                    cmd.Parameters.AddWithValue("@albumId", albumId);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        return Ok($"Album {albumId} updated successfully");
                    }
                    else
                    {
                        return NotFound($"Album with ID {albumId} not found");
                    }
                }
            }
        }
    }
}