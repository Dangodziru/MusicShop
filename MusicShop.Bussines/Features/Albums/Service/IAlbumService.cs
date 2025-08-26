using MusicShop.Bussines.Features.Albums.Delete;
using MusicShop.Bussines.Features.Albums.Insert;
using MusicShop.Bussines.Features.Albums.Search;
using MusicShop.Bussines.Features.Albums.Update;
using MusicShop.Domain.Entities;

namespace MusicShop.Bussines.Features.Albums.Service
{
    public interface IAlbumService
    {
        Task<bool> Delete(AlbumDeleteRequest request);
        Task<IEnumerable<Album>> GetAll();
        Task<long?> Insert(AlbumInsertRequest request);
        Task<IEnumerable<Album>> Search(AlbumSearchRequest request);
        Task<bool> Update(AlbumUpdateRequest request);
    }
}