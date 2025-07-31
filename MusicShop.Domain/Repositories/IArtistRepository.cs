using MusicShop.Domain.Entities;

namespace MusicShop.Data
{
    public interface IArtistRepository
    {
        bool DeleteArtist(long artistId);
        Artist? Get(long artistId);
        List<Artist> GetAll();
        long? InsertArtist(string name);
        List<Artist> Search(string nameSearch);
        bool UpdateArtist(long artistId, string name);
        bool ArtistIsExist(string title);
    }
}