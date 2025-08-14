using MusicShop.Bussines.Features.Artists.Delete;
using MusicShop.Bussines.Features.Artists.Get;
using MusicShop.Bussines.Features.Artists.Insert;
using MusicShop.Bussines.Features.Artists.Search;
using MusicShop.Bussines.Features.Artists.Update;
using MusicShop.Data;
using MusicShop.Domain.Entities;

namespace MusicShop.Bussines.Features.Artists.Service
{
    public class ArtistService(IArtistRepository artistRepository) : IArtistService
    {
        public async Task<IEnumerable<Artist>> GetAll()
        {
            return await artistRepository.GetAll();
        }

        public async Task<Artist?> Get(ArtistGetRequest request)
        {
            if (request.ArtistId <= 0)
            {
                return null; 
            }
            return await artistRepository.Get(request.ArtistId);
        }

        public async Task<IEnumerable<Artist>> Search(ArtistSearchRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.NameSearch))
            {
                return await artistRepository.GetAll(); 
            }
            return await artistRepository.Search(request.NameSearch);
        }

        public async Task<long?> Insert(ArtistInsertRequests request)
        {
            if (string.IsNullOrWhiteSpace(request.Name))
            {
                return null; 
            }
            bool artistExists = await artistRepository.ArtistIsExist(request.Name);
            if (artistExists)
            {
                return null; 
            }
            return await artistRepository.InsertArtist(request.Name);
        }

        public async Task<bool> Update(ArtistUpdateRequest request)
        {
            if (request.ArtistId <= 0 || string.IsNullOrWhiteSpace(request.Name))
            {
                return false; 
            }
            return await artistRepository.UpdateArtist(request.ArtistId, request.Name);
        }

        public async Task<bool> Delete(ArtistDeleteRequest request)
        {
            if (request.ArtistId <= 0)
            {
                return false; 
            }
            return await artistRepository.DeleteArtist(request.ArtistId);
        }
    }
}