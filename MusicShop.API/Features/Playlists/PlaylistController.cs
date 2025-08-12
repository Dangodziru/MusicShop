using Microsoft.AspNetCore.Mvc;
using MusicShop.Data.Dapper;
using MusicShop.Domain.Entities;
using MusicShop.Bussines.Features.Playlist.Delete;
using MusicShop.Bussines.Features.Playlist.Get;
using MusicShop.Bussines.Features.Playlist.Insert;
using MusicShop.Bussines.Features.Playlist.Search;
using MusicShop.Bussines.Features.Playlist.Update;

namespace MusicShop.API.Features.Playlists
{

    [ApiController]
    [Route("api/[controller]")]
    public class PlaylistController : ControllerBase
    {
        private readonly IPlaylistRepository playlistRepository;

        public PlaylistController(IPlaylistRepository playlistRepository)
        {
            this.playlistRepository = playlistRepository;
        }

        [HttpGet("All")]
        public async Task<IEnumerable<Playlist>> GetAll()
        {
            return await playlistRepository.GetAll();
        }

        [HttpGet("SearchById")]
        public async Task<IActionResult> Get([FromQuery] PlaylistGetRequest request)
        {
            var artist = await playlistRepository.Get(request.PlaylistId);
            return artist == null ? NotFound($"Плейлист {request.PlaylistId} не найден") : Ok(artist);
        }

        [HttpGet("Search")]
        public Task<IEnumerable<Playlist>> Search([FromQuery] PlaylistSearchRequest request)
        {
            return playlistRepository.Search(request.Name);
        }

        [HttpPost("InsertPlaylist")]
        public async Task<IActionResult> InsertPlaylist(PlaylistInsertRequest request)
        {
            bool playlistExist = await playlistRepository.PlaylistIsExist(request.Name);
            if (playlistExist)
            {
                return BadRequest("Такой плейлист уже существует");
            }
            var playlistId = await playlistRepository.InsertPlaylist(request.Name);
            if (playlistId.HasValue)
            {
                return Ok(new { PlaylistId = playlistId });
            }
            else
            {
                return BadRequest("Не удалось создать плейлист");
            }
        }

        [HttpDelete("DeletePlaylist")]
        public async Task<IActionResult> DeletePlaylist(PlaylistDeleteRequest request)
        {
            return await playlistRepository.DeletePlaylist(request.PlaylistId)
                ? Ok($"Пдейлист {request.PlaylistId} удален")
                : NotFound($"Плейлист {request.PlaylistId} не найден");
        }

        [HttpPost("UpdatePlaylist")]
        public async Task<IActionResult> UpdatePlaylist(PlaylistUpdateRequest request)
        {
            return await playlistRepository.UpdatePlaylist(request.PlaylistId, request.Name)
                ? Ok($"Исполнитель {request.PlaylistId} обновлен")
                : NotFound($"Исполнитель {request.PlaylistId} не найден");
        }
    }
}
