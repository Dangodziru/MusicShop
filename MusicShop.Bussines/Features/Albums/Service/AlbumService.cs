using MusicShop.Bussines.Features.Albums.Get;
using MusicShop.Bussines.Features.Albums.Insert;
using MusicShop.Bussines.Features.Albums.Search;
using MusicShop.Bussines.Features.Albums.Update;
using MusicShop.Data;
using MusicShop.Domain.Entities;
using MusicShop.Bussines.Features.Albums.Delete; 

namespace MusicShop.Bussines.Features.Albums.Service
{
    public class AlbumService(IAlbumRepository albumRepository) : IAlbumService
    {
        public async Task<IEnumerable<Album>> GetAll()
        {
            return await albumRepository.GetAll();
        }

        public async Task<IEnumerable<Album>> Search(AlbumSearchRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.TitleSearch))
            {
                return await albumRepository.GetAll();
            }
            return await albumRepository.Search(request.TitleSearch);
        }

        public async Task<int?> Insert(AlbumInsertRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Title) || request.ArtistId <= 0)
            {
                return null;
            }
            return await albumRepository.InsertAlbum(request.Title, request.ArtistId);
        }

        public async Task<bool> Update(AlbumUpdateRequest request)
        {
            if (request.AlbumId <= 0 ||
                string.IsNullOrWhiteSpace(request.Title) ||
                request.ArtistId <= 0)
            {
                return false;
            }
            return await albumRepository.UpdateAlbum(request.AlbumId, request.Title, request.ArtistId);
        }

        public async Task<bool> Delete(AlbumDeleteRequest request)
        {
            if (request.AlbumId <= 0)
            {
                return false;
            }
            return await albumRepository.DeleteAlbum(request.AlbumId);
        }
    }
}