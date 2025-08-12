using Dapper;
using Microsoft.Extensions.Configuration;
using MusicShop.Domain.Entities;
using System.Data.SQLite;
using System.Linq;

namespace MusicShop.Data.Dapper
{
    public class ArtistDapperRepository : IArtistRepository
    {
        protected readonly string connectionString;

        public ArtistDapperRepository(IConfiguration config)
        {
            connectionString = config.GetConnectionString("MusicShop")!;
        }

        public async Task<IEnumerable<Artist>> GetAll()
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                return await connection.QueryAsync<Artist>(
                    "SELECT * FROM Artist"
                    );
            }
        }

        public async Task<Artist?> Get(long artistId)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                return await connection.QueryFirstOrDefaultAsync<Artist>(
                    "SELECT * FROM Artist WHERE ArtistId = @artistId",
                    new { artistId }
                );
            }
        }

        public async Task<IEnumerable<Artist>> Search(string nameSearch)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                return await connection.QueryAsync<Artist>(
                    "SELECT * FROM Artist WHERE Name LIKE @nameSearch",
                    new { nameSearch = $"%{nameSearch}%" }
                    );
            }
        }

        public async virtual Task<long?> InsertArtist(string name)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                return await connection.ExecuteScalarAsync<long?>(
                    "INSERT INTO Artist (Name) VALUES (@name); SELECT last_insert_rowid();",
                    new { name }
                );
            }
        }

        public async Task<bool> DeleteArtist(long artistId)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                int affectedRows = await connection.ExecuteAsync(
                     "DELETE FROM Artist WHERE ArtistId = @artistId",
                     new { artistId }
                 );
                return affectedRows > 0;
            }
        }

        public async Task<bool> UpdateArtist(long artistId, string name)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                int affectedRows = await connection.ExecuteAsync(
                   "UPDATE Artist SET Name = @name WHERE ArtistId = @artistId",
                   new { artistId, name }
               );
                return affectedRows > 0;
            }
        }

        public async Task<bool> ArtistIsExist(string name)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                var existing = await connection.QueryFirstOrDefaultAsync<Artist>(
                    "SELECT * FROM Artist WHERE LOWER(Name) = LOWER(@name)",
                    new { name });

                return existing != null;

            }
        }

    }
}