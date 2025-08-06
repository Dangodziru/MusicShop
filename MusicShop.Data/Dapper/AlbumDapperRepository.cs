using MusicShop.Domain.Entities;
using System.Data.SQLite;
using Dapper;
using System.Linq;

namespace MusicShop.Data.Dapper
{
    public class AlbumDapperRepository : IAlbumRepository
    {
        protected const string dbPath = "C:\\Users\\Goida\\AppData\\Roaming\\DBeaverData\\workspace6\\.metadata\\sample-database-sqlite-1\\Chinook.db";
        protected const string connectionString = $"Data Source={dbPath};Version=3;";

        public List<Album> GetAll()
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                return connection.Query<Album, Artist, Album>(
                    "SELECT al.*, a.* FROM Album al JOIN Artist a ON a.ArtistId = al.ArtistId ",
                    (album, artist) =>
                    {
                        album.Artist = artist;
                        return album;
                    },
                    splitOn: "ArtistId"
                ).ToList();
            }
        }

        public Album? Get(long albumId)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                const string sql = @"SELECT al.AlbumId, al.Title, al.ArtistId, a.ArtistId, a.Name 
                    FROM Album al 
                    JOIN Artist a ON a.ArtistId = al.ArtistId 
                    WHERE al.AlbumId = @albumId";

                return connection.Query<Album, Artist, Album>(
                    sql,
                    map: (album, artist) =>
                    {
                        album.Artist = artist;
                        album.Id = albumId;
                        return album;
                    },
                    param: new { albumId },
                    splitOn: "ArtistId"
                ).FirstOrDefault();
            }
        }

        public List<Album> Search(string titleSearch)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                return connection.Query<Album>(
                    "SELECT * FROM Album WHERE Title LIKE @titleSearch",
                    new { titleSearch = $"%{titleSearch}%" }
                ).ToList();
            }
        }

        public virtual long? InsertAlbum(string title, long artistId)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                return connection.ExecuteScalar<long?>(
                    "INSERT INTO Album(title, ArtistId) VALUES(@title, @artistId); SELECT last_insert_rowid();",
                    new { title, artistId }
                );
            }
        }

        public bool DeleteAlbum(long albumId)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                int rowsAffected = connection.Execute(
                    "DELETE FROM Album WHERE AlbumId = @albumId",
                    new { albumId }
                );
                return rowsAffected > 0;
            }
        }

        public bool UpdateAlbum(long albumId, string title, long artistId)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                int rowsAffected = connection.Execute(
                    @"UPDATE Album 
                      SET Title = @title, ArtistId = @artistId 
                      WHERE AlbumId = @albumId",
                    new { albumId, title, artistId }
                );
                return rowsAffected > 0;
            }
        }

        public bool AlbumIsExist(string title, long artistId) 
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                var existing = connection.QueryFirstOrDefault<Artist>(
                    "SELECT * FROM Album WHERE LOWER(Title) = LOWER(@title) AND ArtistId = @artistId ",
                    new { title, artistId });

                return existing != null;

            }
        }
    }
}