using MusicShop.Domain.Entities;
using System.Data.SQLite;

namespace MusicShop.Data
{
    public class AlbumRepository
    {
        const string dbPath = "C:\\Users\\Goida\\AppData\\Roaming\\DBeaverData\\workspace6\\.metadata\\sample-database-sqlite-1\\Chinook.db";
        const string connectionString = $"Data Source={dbPath};Version=3;";

        public List<Album> GetAll()
        {

            var list = new List<Album>();

            // Создание подключения
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string sql = "SELECT * FROM Album";

                using (var cmd = new SQLiteCommand(sql, connection))
                using (SQLiteDataReader reader = cmd.ExecuteReader())
                {

                    while (reader.Read())
                    {

                        list.Add(new Album { Id = (long)reader["AlbumId"], Title = (string)reader["Title"] });

                    }
                }

            }

            return list;

        }

        public Album? Get(long albumId)
        {

            Album? album = null;


            // Создание подключения
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string sql = "SELECT * FROM Album WHERE AlbumId =" + albumId;

                using (var cmd = new SQLiteCommand(sql, connection))
                using (SQLiteDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {

                        album = new Album { Id = (long)reader["AlbumId"], Title = (string)reader["Title"] };

                    }
                }

            }

            return album;

        }

        public List<Album> Search(string titleSearch)
        {

            var list = new List<Album>();

            // Создание подключения
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string sql = "SELECT * FROM Album WHERE Title LIKE '%" + titleSearch + "%'";

                using (var cmd = new SQLiteCommand(sql, connection))
                using (SQLiteDataReader reader = cmd.ExecuteReader())
                {

                    while (reader.Read())
                    {

                        list.Add(new Album { Id = (long)reader["AlbumId"], Title = (string)reader["Title"] });

                    }
                }

            }

            return list;
        }
    }
}
