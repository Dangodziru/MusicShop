using Dapper;
using Microsoft.Extensions.Configuration;
using MusicShop.Domain.Entities;
using System.Data.SQLite;

namespace MusicShop.Data.Dapper
{
    public class TrackDapperRepository : ITrackDapperRepository
    {
        protected readonly string connectionString;

        public TrackDapperRepository(IConfiguration config)
        {
            connectionString = config.GetConnectionString("MusicShop")!;
        }

        public async Task<IEnumerable<Track>> GetAll()
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                return await connection.QueryAsync<Track>(
                    "SELECT * FROM Track"
                );
            }
        }

        public async Task<Track?> Get(long trackId)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                return await connection.QueryFirstOrDefaultAsync<Track>(
                    "SELECT * FROM Track WHERE TrackId = @trackId",
                    new { trackId }
                );
            }
        }

        public async Task<IEnumerable<Track>> Search(string nameSearch)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                return await connection.QueryAsync<Track>(
                    "SELECT * FROM Track WHERE Name LIKE @nameSearch",
                    new { nameSearch = $"%{nameSearch}%" }
                );
            }
        }

        public async Task<long?> Insert(Track track)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                return await connection.ExecuteScalarAsync<long?>(
                    @"INSERT INTO Track 
                    (Name, AlbumId, MediaTypeId, GenreId, Composer, Milliseconds, Bytes, UnitPrice) 
                    VALUES (@Name, @AlbumId, @MediaTypeId, @GenreId, @Composer, @Milliseconds, @Bytes, @UnitPrice);
                    SELECT last_insert_rowid();",
                    track
                );
            }
        }

        public async Task<bool> Delete(long trackId)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                int affectedRows = await connection.ExecuteAsync(
                    "DELETE FROM Track WHERE TrackId = @trackId",
                    new { trackId }
                );
                return affectedRows > 0;
            }
        }

        public async Task<bool> Update(Track track)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                int affectedRows = await connection.ExecuteAsync(
                    @"UPDATE Track SET 
                    Name = @Name,
                    AlbumId = @AlbumId,
                    MediaTypeId = @MediaTypeId,
                    GenreId = @GenreId,
                    Composer = @Composer,
                    Milliseconds = @Milliseconds,
                    Bytes = @Bytes,
                    UnitPrice = @UnitPrice
                    WHERE TrackId = @TrackId",
                    track
                );
                return affectedRows > 0;
            }
        }

        public async Task<bool> IsExist(string name)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                var existing = await connection.QueryFirstOrDefaultAsync<Track>(
                    "SELECT * FROM Track WHERE LOWER(Name) = LOWER(@name)",
                    new { name }
                );
                return existing != null;
            }
        }
    }
}