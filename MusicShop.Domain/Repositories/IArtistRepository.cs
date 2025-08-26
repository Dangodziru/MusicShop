using MusicShop.Domain.Entities;

namespace MusicShop.Data
{
    public interface IArtistRepository
    {
        Task<bool> DeleteArtist(int artistId);
        Task<Artist?> Get(int artistId);
        Task<IEnumerable<Artist>> GetAll();
        Task<int?> InsertArtist(string name);
        Task<IEnumerable<Artist>> Search(string nameSearch);
        Task<bool> UpdateArtist(int artistId, string name);
        Task<bool> ArtistIsExist(string name);
    }
}