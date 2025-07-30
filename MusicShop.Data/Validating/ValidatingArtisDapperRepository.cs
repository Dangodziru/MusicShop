using Dapper;
using MusicShop.Data.Dapper;
using MusicShop.Domain.Entities;
using System;
using System.Data.SQLite;

namespace MusicShop.Data.Validating
{
    public class ValidatingAlbumDapperRepository : AlbumDapperRepository
    {
        public override long? InsertAlbum(string title, long artistId)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                var existing = connection.QueryFirstOrDefault<Album>(
                    "SELECT * FROM Album WHERE LOWER(Title) = LOWER(@title) AND ArtistId = @artistId",
                    new { title, artistId });

                if (existing != null)
                {
                    throw new InvalidOperationException($"Album '{title}' already exists for this artist");
                }

                return connection.ExecuteScalar<long?>(
                    "INSERT INTO Album(Title, ArtistId) VALUES (@title, @artistId); SELECT last_insert_rowid();",
                    new { title, artistId }
                );
            }
        }
    }
}

//public override long? InsertArtist(string name)
//{
//    using (var connection = new SQLiteConnection(connectionString))
//    {
//        connection.Open();

//        var existing = connection.QueryFirstOrDefault<Artist>(
//            "SELECT * FROM Artist WHERE LOWER(Name) = LOWER(@name)",
//            new { name });

//        if (existing != null)
//        {
//            throw new InvalidOperationException($"Artist with name '{name}' already exists");
//        }

//        return connection.ExecuteScalar<long?>(
//            "INSERT INTO Artist (Name) VALUES (@name); SELECT last_insert_rowid();",
//            new { name }
//        );
//    }
//}