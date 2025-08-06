using Dapper;
using MusicShop.Domain.Entities;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using MusicShop.Domain;

namespace MusicShop.Data.Dapper
{
    public class GenreDapperRepository : IGenreRepository
    {
        protected const string dbPath = "C:\\Users\\Goida\\AppData\\Roaming\\DBeaverData\\workspace6\\.metadata\\sample-database-sqlite-1\\Chinook.db";
        protected const string connectionString = $"Data Source={dbPath};Version=3;";

        public List<Genre> GetAll()
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                return connection.Query<Genre>("SELECT * FROM Genre").ToList();
            }
        }

        public Genre? Get(long genreId)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                return connection.QueryFirstOrDefault<Genre>(
                    "SELECT * FROM Genre WHERE GenreId = @genreId",
                    new { genreId }
                );
            }
        }

        public List<Genre> Search(string term)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                return connection.Query<Genre>(
                    "SELECT * FROM Genre WHERE Name LIKE @searchTerm",
                    new { searchTerm = $"%{term}%" }
                ).ToList();
            }
        }

        public long? Insert(Genre genre)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                return connection.ExecuteScalar<long?>(
                    "INSERT INTO Genre (Name) VALUES (@Name); SELECT last_insert_rowid();",
                    new { genre.Name }
                );
            }
        }

        public bool Update(Genre genre)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                int affectedRows = connection.Execute(
                    "UPDATE Genre SET Name = @Name WHERE GenreId = @GenreId",
                    new { genre.GenreId, genre.Name }
                );
                return affectedRows > 0;
            }
        }

        public bool Delete(long genreId)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                int affectedRows = connection.Execute(
                    "DELETE FROM Genre WHERE GenreId = @genreId",
                    new { genreId }
                );
                return affectedRows > 0;
            }
        }
    }
}