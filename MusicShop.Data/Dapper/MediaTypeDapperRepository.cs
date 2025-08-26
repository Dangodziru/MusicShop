using Dapper;
using Microsoft.Extensions.Configuration;
using MusicShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicShop.Domain;

namespace MusicShop.Data.Dapper
{
    public class MediaTypeDapperRepository : IMediaTypeRepository
    {
        protected readonly string connectionString;

        public MediaTypeDapperRepository(IConfiguration config)
        {
            connectionString = config.GetConnectionString("MusicShop")!;
        }

        public async Task<IEnumerable<MediaType>> GetAll()
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                return await connection.QueryAsync<MediaType>(
                    "SELECT * FROM MediaType"
                    );
            }
        }

        public async Task<MediaType?> Get(long mediaTypeId)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                return await connection.QueryFirstOrDefaultAsync<MediaType>(
                    "SELECT * FROM MediaType WHERE MediaTypeId = @mediaTypeId",
                    new { mediaTypeId }
                );
            }
        }

        public async Task<IEnumerable<MediaType>> Search(string nameSearch)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                return await connection.QueryAsync<MediaType>(
                    "SELECT * FROM MediaType WHERE Name LIKE @nameSearch",
                    new { nameSearch = $"%{nameSearch}%" }
                    );
            }
        }

        public virtual async Task<long?> InsertMediaType(string name)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                return await connection.ExecuteScalarAsync<long?>(
                    "INSERT INTO MediaType (Name) VALUES (@name); SELECT last_insert_rowid();",
                    new { name }
                );
            }
        }

        public async Task<bool> DeleteMediaType(long mediaTypeId)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                int affectedRows = await connection.ExecuteAsync(
                     "DELETE FROM MediaType WHERE MediaTypeId = @mediaTypeId",
                     new { mediaTypeId }
                 );
                return affectedRows > 0;
            }
        }

        public async Task<bool> UpdateMediaType(long mediaTypeId, string name)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                int affectedRows = await connection.ExecuteAsync(
                   "UPDATE MediaType SET Name = @name WHERE mdiaTypeId = @mediaTypeId",
                   new { mediaTypeId, name }
               );
                return affectedRows > 0;
            }
        }

        public async Task<bool> MediaTypeIsExist(string name)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                var existing = await connection.QueryFirstOrDefaultAsync<MediaType>(
                    "SELECT * FROM MediaType WHERE LOWER(Name) = LOWER(@name)",
                    new { name });

                return existing != null;

            }
        }
    }
}
