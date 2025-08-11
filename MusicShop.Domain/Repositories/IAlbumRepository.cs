using MusicShop.Domain.Entities;

namespace MusicShop.Data
{
    public interface IAlbumRepository
    {
        Task<bool> DeleteAlbum(long albumId);
        Task<Album?> Get(long albumId);
        Task<IEnumerable<Album>> GetAll();
        Task<long?> InsertAlbum(string title, long artistId);
        Task<IEnumerable<Album>> Search(string titleSearch);
        Task<bool> UpdateAlbum(long albumId, string title, long artistId);
        Task<bool> AlbumIsExist(string title, long artistId);
    }
}