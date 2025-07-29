using MusicShop.Domain.Entities;
using System.Data.SQLite;

namespace MusicShop.Data
{
    public class ArtistRepository : IArtistRepository
    {
        const string dbPath = "C:\\Users\\Goida\\AppData\\Roaming\\DBeaverData\\workspace6\\.metadata\\sample-database-sqlite-1\\Chinook.db";
        const string connectionString = $"Data Source={dbPath};Version=3;";

        public List<Artist> GetAll()
        {
            var artists = new List<Artist>();

            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                const string sql = "SELECT * FROM Artist";

                using (var cmd = new SQLiteCommand(sql, connection))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        artists.Add(new Artist
                        {
                            ArtistId = (long)reader["ArtistId"],
                            Name = (string)reader["Name"]
                        });
                    }
                }
            }
            return artists;
        }

        public Artist? Get(long artistId)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                const string sql = "SELECT * FROM Artist WHERE ArtistId = @artistId";

                using (var cmd = new SQLiteCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@artistId", artistId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Artist
                            {
                                ArtistId = (long)reader["ArtistId"],
                                Name = (string)reader["Name"]
                            };
                        }
                    }
                }
            }
            return null;
        }

        public List<Artist> Search(string nameSearch)
        {
            var artists = new List<Artist>();

            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                const string sql = "SELECT * FROM Artist WHERE Name LIKE @pattern";

                using (var cmd = new SQLiteCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@pattern", $"%{nameSearch}%");

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            artists.Add(new Artist
                            {
                                ArtistId = (long)reader["ArtistId"],
                                Name = (string)reader["Name"]
                            });
                        }
                    }
                }
            }
            return artists;
        }

        public long? InsertArtist(string name)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                const string sql = "INSERT INTO Artist (Name) VALUES (@name); SELECT last_insert_rowid();";

                using (var cmd = new SQLiteCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@name", name);
                    return (long)cmd.ExecuteScalar();
                }
            }
        }

        public bool DeleteArtist(long artistId)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                const string sql = "DELETE FROM Artist WHERE ArtistId = @artistId";

                using (var cmd = new SQLiteCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@artistId", artistId);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public bool UpdateArtist(long artistId, string name)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                const string sql = "UPDATE Artist SET Name = @name WHERE ArtistId = @artistId";

                using (var cmd = new SQLiteCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.AddWithValue("@artistId", artistId);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }
    }
}