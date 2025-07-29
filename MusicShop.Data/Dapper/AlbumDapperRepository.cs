using MusicShop.Domain.Entities;
using System.Data.SQLite;
using Dapper;

namespace MusicShop.Data.Dapper
{
    public class AlbumDapperRepository : IAlbumRepository
    {
        const string dbPath = "C:\\Users\\Goida\\AppData\\Roaming\\DBeaverData\\workspace6\\.metadata\\sample-database-sqlite-1\\Chinook.db";
        const string connectionString = $"Data Source={dbPath};Version=3;";

        public List<Album> GetAll()
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                using (var cmd = new SQLiteCommand(connection))
                {
                    return connection.Query<Album, Artist, Album>(
                        "SELECT al.*, a.* FROM Album al JOIN Artist a ON a.ArtistId = al.ArtistId ",
                        (album, artist) => {
                            album.Artist = artist;
                            return album;
                        },splitOn: "ArtistId"
                        ).ToList();
                    
                }

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
                        return album;
                    },
                    param: new { albumId },
                    splitOn: "ArtistId" 
                ).FirstOrDefault();
            }
        }

        public List<Album> Search(string titleSearch)
        {

            var list = new List<Album>();

            // Создание подключения
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string sql = "SELECT * FROM Album WHERE Title LIKE @titleSearch";

                using (var cmd = new SQLiteCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@titleSearch", "%"+titleSearch+"%");

                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {

                        while (reader.Read())
                        {

                            list.Add(new Album { Id = (long)reader["AlbumId"],
                                Title = (string)reader["Title"],
                                ArtistId = (long)reader["ArtistId"] });

                        }
                    }
                }

            }

            return list;
        }

        public bool DeleteAlbum(long albumId)
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
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }

        public long? InsertAlbum(string title, long artistId)
        {
            long? newId;

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

        public bool UpdateAlbum(long albumId, string title, long artistId)
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
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }
    }
}
