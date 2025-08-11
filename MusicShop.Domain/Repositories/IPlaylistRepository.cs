using MusicShop.Domain.Entities;

namespace MusicShop.Data.Dapper
{
    public interface IPlaylistRepository
    {
        Task<bool> DeletePlaylist(long playlistId);
        Task<Playlist?> Get(long playlistId);
        Task<IEnumerable<Playlist>> GetAll();
        Task<long?> InsertPlaylist(string name);
        Task<bool> PlaylistIsExist(string name);
        Task<IEnumerable<Playlist>> Search(string nameSearch);
        Task<bool> UpdatePlaylist(long playlistId, string name);
    }
}