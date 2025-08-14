using MusicShop.Bussines.Features.Playlists.Delete;
using MusicShop.Bussines.Features.Playlists.Get;
using MusicShop.Bussines.Features.Playlists.Insert;
using MusicShop.Bussines.Features.Playlists.Search;
using MusicShop.Bussines.Features.Playlists.Update;
using MusicShop.Data.Dapper;

namespace MusicShop.Bussines.Features.Playlist.Services
{
    public class PlaylistService : IPlaylistService
    {
        private readonly IPlaylistRepository playlistRepository;

        public PlaylistService(IPlaylistRepository playlistRepository)
        {
            this.playlistRepository = playlistRepository;
        }

        public async Task<IEnumerable<MusicShop.Domain.Entities.Playlist>> GetAll()
        {
            return await playlistRepository.GetAll();
        }

        public async Task<MusicShop.Domain.Entities.Playlist?> Get(PlaylistGetRequest request)
        {
            if (request.PlaylistId <= 0)
            {
                return null;
            }
            return await playlistRepository.Get(request.PlaylistId);
        }

        public async Task<IEnumerable<MusicShop.Domain.Entities.Playlist>> Search(PlaylistSearchRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Name))
            {
                return await playlistRepository.GetAll();
            }
            return await playlistRepository.Search(request.Name);
        }

        public async Task<long?> Insert(PlaylistInsertRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Name))
            {
                return null;
            }
            bool playlistExists = await playlistRepository.PlaylistIsExist(request.Name);
            if (playlistExists)
            {
                return null;
            }
            return await playlistRepository.InsertPlaylist(request.Name);
        }

        public async Task<bool> Update(PlaylistUpdateRequest request)
        {
            if (request.PlaylistId <= 0 || string.IsNullOrWhiteSpace(request.Name))
            {
                return false;
            }
            return await playlistRepository.UpdatePlaylist(request.PlaylistId, request.Name);
        }

        public async Task<bool> Delete(PlaylistDeleteRequest request)
        {
            if (request.PlaylistId <= 0)
            {
                return false;
            }
            return await playlistRepository.DeletePlaylist(request.PlaylistId);
        }
    }
}   