using MusicShop.Domain.Entities;

namespace MusicShop.Data
{
    public interface IArtistRepository
    {
        Task<bool> DeleteArtist(long artistId);
        Task<Artist?> Get(long artistId);
        Task<IEnumerable<Artist>> GetAll();
        Task<long?> InsertArtist(string name);
        Task<IEnumerable<Artist>> Search(string nameSearch);
        Task<bool> UpdateArtist(long artistId, string name);
        Task<bool> ArtistIsExist(string name);
    }
}