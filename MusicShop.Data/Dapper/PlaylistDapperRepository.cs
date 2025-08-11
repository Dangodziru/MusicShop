using Dapper;
using Microsoft.Extensions.Configuration;
using MusicShop.Domain.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicShop.Data.Dapper
{
    internal class PlaylistDapperRepository : IPlaylistRepository
    {
        protected readonly string connectionString;

        public PlaylistDapperRepository(IConfiguration config)
        {
            connectionString = config.GetConnectionString("MusicShop")!;
        }

        public async Task<IEnumerable<Playlist>> GetAll()
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                return await connection.QueryAsync<Playlist>(
                    "SELECT * FROM Playlist"
                    );
            }
        }

        public async Task<Playlist?> Get(long playlistId)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                return await connection.QueryFirstOrDefaultAsync<Playlist>(
                    "SELECT * FROM Playlist WHERE PlaylistId = @playlistId",
                    new { playlistId }
                );
            }
        }

        public async Task<IEnumerable<Playlist>> Search(string nameSearch)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                return await connection.QueryAsync<Playlist>(
                    "SELECT * FROM Playlist WHERE Name LIKE @nameSearch",
                    new { nameSearch = $"%{nameSearch}%" }
                    );
            }
        }

        public async virtual Task<long?> InsertPlaylist(string name)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                return await connection.ExecuteScalarAsync<long?>(
                    "INSERT INTO Playlist (Name) VALUES (@name); SELECT last_insert_rowid();",
                    new { name }
                );
            }
        }

        public async Task<bool> DeletePlaylist(long playlistId)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                int affectedRows = await connection.ExecuteAsync(
                     "DELETE FROM Playlist WHERE PlaylistId = @playlistId",
                     new { playlistId }
                 );
                return affectedRows > 0;
            }
        }

        public async Task<bool> UpdatePlaylist(long playlistId, string name)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                int affectedRows = await connection.ExecuteAsync(
                   "UPDATE Playlist SET Name = @name WHERE playlistId = @playlistId",
                   new { playlistId, name }
               );
                return affectedRows > 0;
            }
        }

        public async Task<bool> PlaylistIsExist(string name)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                var existing = await connection.QueryFirstOrDefaultAsync<Playlist>(
                    "SELECT * FROM Playlist WHERE LOWER(Name) = LOWER(@name)",
                    new { name });

                return existing != null;

            }
        }
    }
}
