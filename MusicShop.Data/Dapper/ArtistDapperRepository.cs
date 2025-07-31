using Dapper;
using MusicShop.Domain.Entities;
using System.Data.SQLite;
using System.Linq;

namespace MusicShop.Data.Dapper
{
    public class ArtistDapperRepository : IArtistRepository
    {
        protected const string dbPath = "C:\\Users\\Goida\\AppData\\Roaming\\DBeaverData\\workspace6\\.metadata\\sample-database-sqlite-1\\Chinook.db";
        protected const string connectionString = $"Data Source={dbPath};Version=3;";

        public List<Artist> GetAll()
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                return connection.Query<Artist>(
                    "SELECT * FROM Artist"
                    ).ToList();
            }
        }

        public Artist? Get(long artistId)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                return connection.QueryFirstOrDefault<Artist>(
                    "SELECT * FROM Artist WHERE ArtistId = @artistId",
                    new { artistId }
                );
            }
        }

        public List<Artist> Search(string nameSearch)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                return connection.Query<Artist>(
                    "SELECT * FROM Artist WHERE Name LIKE @nameSearch",
                    new { nameSearch = $"%{nameSearch}%" }
                    ).ToList();
            }
        }

        public virtual long? InsertArtist(string name)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                return connection.ExecuteScalar<long?>(
                    "INSERT INTO Artist (Name) VALUES (@name); SELECT last_insert_rowid();",
                    new { name }
                );
            }
        }

        public bool DeleteArtist(long artistId)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                int affectedRows = connection.Execute(
                     "DELETE FROM Artist WHERE ArtistId = @artistId",
                     new { artistId }
                 );
                return affectedRows > 0;
            }
        }

        public bool UpdateArtist(long artistId, string name)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                int affectedRows = connection.Execute(
                   "UPDATE Artist SET Name = @name WHERE ArtistId = @artistId",
                   new { artistId, name }
               );
                return affectedRows > 0;
            }
        }

        public bool ArtistIsExist(string name)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                var existing = connection.QueryFirstOrDefault<Artist>(
                    "SELECT * FROM Artist WHERE LOWER(Name) = LOWER(@name)",
                    new { name });

                return existing != null;

            }
        }

    }
}