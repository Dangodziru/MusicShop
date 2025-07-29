using MusicShop.Domain.Entities;

namespace MusicShop.Data
{
    public interface IAlbumRepository
    {
        bool DeleteAlbum(long albumId);
        Album? Get(long albumId);
        List<Album> GetAll();
        long? InsertAlbum(string title, long artistId);
        List<Album> Search(string titleSearch);
        bool UpdateAlbum(long albumId, string title, long artistId);
    }
}