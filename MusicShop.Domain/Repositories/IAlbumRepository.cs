using MusicShop.Domain.Entities;

namespace MusicShop.Data
{
    public interface IAlbumRepository
    {
        Task<bool> DeleteAlbum(int albumId);
        Task<Album?> Get(int albumId);
        Task<IEnumerable<Album>> GetAll();
        Task<int?> InsertAlbum(string title, int artistId);
        Task<IEnumerable<Album>> Search(string titleSearch);
        Task<bool> UpdateAlbum(int albumId, string title, int artistId);
        Task<bool> AlbumIsExist(string title, int artistId);
    }
}