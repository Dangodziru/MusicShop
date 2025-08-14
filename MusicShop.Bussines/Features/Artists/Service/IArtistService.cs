using MusicShop.Bussines.Features.Artists.Delete;
using MusicShop.Bussines.Features.Artists.Get;
using MusicShop.Bussines.Features.Artists.Insert;
using MusicShop.Bussines.Features.Artists.Search;
using MusicShop.Bussines.Features.Artists.Update;
using MusicShop.Domain.Entities;

namespace MusicShop.Bussines.Features.Artists.Service
{
    public interface IArtistService
    {
        Task<IEnumerable<Artist>> GetAll();
        Task<Artist?> Get(ArtistGetRequest request);
        Task<IEnumerable<Artist>> Search(ArtistSearchRequest request);
        Task<long?> Insert(ArtistInsertRequests request);
        Task<bool> Update(ArtistUpdateRequest request);
        Task<bool> Delete(ArtistDeleteRequest request);
    }
}