using MusicShop.Domain.Entities;
using System.Data.SQLite;
using Dapper;
using System.Linq;
using System.Configuration.Internal;
using Microsoft.Extensions.Configuration;

namespace MusicShop.Data.Dapper
{
    public class AlbumDapperRepository : IAlbumRepository
    {
        protected readonly string connectionString;

        public AlbumDapperRepository(IConfiguration config)
        {
            connectionString = config.GetConnectionString("MusicShop")!;
        }

        public Task<IEnumerable<Album>> GetAll()
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                return connection.QueryAsync<Album, Artist, Album>(
                    "SELECT al.*, a.* FROM Album al JOIN Artist a ON a.ArtistId = al.ArtistId ",
                    (album, artist) =>
                    {
                        album.Artist = artist;
                        return album;
                    },
                    splitOn: "ArtistId"
                );
            }
        }

        public async Task<Album?> Get(int albumId)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                const string sql = @"SELECT al.AlbumId, al.Title, al.ArtistId, a.ArtistId, a.Name 
                    FROM Album al 
                    JOIN Artist a ON a.ArtistId = al.ArtistId 
                    WHERE al.AlbumId = @albumId";

                var result = await connection.QueryAsync<Album, Artist, Album>(
                    sql,
                    map: (album, artist) =>
                    {
                        album.Artist = artist;
                        album.AlbumId = albumId;
                        return album;
                    },
                    param: new { albumId },
                    splitOn: "ArtistId"
                );
               return result.FirstOrDefault();
            }
        }

        public async Task<IEnumerable<Album>> Search(string titleSearch)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                return await connection.QueryAsync<Album>(
                    "SELECT * FROM Album WHERE Title LIKE @titleSearch",
                    new { titleSearch = $"%{titleSearch}%" }
                );
            }
        }

        public virtual async Task<int?> InsertAlbum(string title, int artistId)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                await connection.OpenAsync();
                return connection.ExecuteScalar<int?>(
                    "INSERT INTO Album(title, ArtistId) VALUES(@title, @artistId); SELECT last_insert_rowid();",
                    new { title, artistId }
                );
            }
        }

        public async Task<bool> DeleteAlbum(int albumId)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                await connection.OpenAsync();
                int rowsAffected = await connection.ExecuteAsync(
                    "DELETE FROM Album WHERE AlbumId = @albumId",
                    new { albumId }
                );
                return rowsAffected > 0;
            }
        }

        public async Task<bool> UpdateAlbum(int albumId, string title, int artistId)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                await connection.OpenAsync();
                int rowsAffected = await connection.ExecuteAsync(
                    @"UPDATE Album SET Title = @title, ArtistId = @artistId 
              WHERE AlbumId = @albumId",
                    new { albumId, title, artistId }
                );
                return rowsAffected > 0;
            }
        }

        public async Task<bool> AlbumIsExist(string title, int artistId)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                await connection.OpenAsync();
                var existing = await connection.QueryFirstOrDefaultAsync<Album>(
                    "SELECT * FROM Album WHERE LOWER(Title) = LOWER(@title) AND ArtistId = @artistId",
                    new { title, artistId }
                );
                return existing != null;
            }
        }
    }
}