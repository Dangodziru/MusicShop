using MusicShop.Bussines.Features.Playlists.Delete;
using MusicShop.Bussines.Features.Playlists.Get;
using MusicShop.Bussines.Features.Playlists.Insert;
using MusicShop.Bussines.Features.Playlists.Search;
using MusicShop.Bussines.Features.Playlists.Update;

namespace MusicShop.Bussines.Features.Playlist.Services
{
    public interface IPlaylistService
    {
        Task<bool> Delete(PlaylistDeleteRequest request);
        Task<Domain.Entities.Playlist?> Get(PlaylistGetRequest request);
        Task<IEnumerable<Domain.Entities.Playlist>> GetAll();
        Task<long?> Insert(PlaylistInsertRequest request);
        Task<IEnumerable<Domain.Entities.Playlist>> Search(PlaylistSearchRequest request);
        Task<bool> Update(PlaylistUpdateRequest request);
    }
}