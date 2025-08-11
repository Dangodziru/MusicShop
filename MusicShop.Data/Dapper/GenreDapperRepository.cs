using Dapper;
using Microsoft.Extensions.Configuration;
using MusicShop.Domain;
using MusicShop.Domain.Entities;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;

namespace MusicShop.Data.Dapper
{
    public class GenreDapperRepository : IGenreRepository
    {
        protected readonly string connectionString;

        public GenreDapperRepository(IConfiguration config)
        {
            connectionString = config.GetConnectionString("MusicShop")!;
        }

        public async Task<IEnumerable<Genre>> GetAll()
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                return await connection.QueryAsync<Genre>("SELECT * FROM Genre");
            }
        }

        public async Task<Genre?> Get(long genreId)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                return await connection.QueryFirstOrDefaultAsync<Genre>(
                    "SELECT * FROM Genre WHERE GenreId = @genreId",
                    new { genreId }
                );
            }
        }

        public async Task<IEnumerable<Genre>> Search(string term)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                return await connection.QueryAsync<Genre>(
                    "SELECT * FROM Genre WHERE Name LIKE @searchTerm",
                    new { searchTerm = $"%{term}%" }
                );
            }
        }

        public async Task<long?> Insert(Genre genre)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                return await connection.ExecuteScalarAsync<long?>(
                    "INSERT INTO Genre (Name) VALUES (@Name); SELECT last_insert_rowid();",
                    new { genre.Name }
                );
            }
        }

        public async Task<bool> Update(Genre genre)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                int affectedRows = await connection.ExecuteAsync(
                    "UPDATE Genre SET Name = @Name WHERE GenreId = @GenreId",
                    new { genre.GenreId, genre.Name }
                );
                return affectedRows > 0;
            }
        }

        public async Task<bool> Delete(long genreId)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                int affectedRows = await connection.ExecuteAsync(
                    "DELETE FROM Genre WHERE GenreId = @genreId",
                    new { genreId }
                );
                return affectedRows > 0;
            }
        }
    }
}