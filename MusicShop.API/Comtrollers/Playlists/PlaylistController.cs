using Microsoft.AspNetCore.Mvc;
using MusicShop.Bussines.Features.Playlists.Delete;
using MusicShop.Bussines.Features.Playlists.Get;
using MusicShop.Bussines.Features.Playlists.Insert;
using MusicShop.Bussines.Features.Playlists.Search;
using MusicShop.Bussines.Features.Playlists.Update;
using MusicShop.Bussines.Features.Playlist.Services;

namespace MusicShop.API.Features.Playlists
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlaylistController : ControllerBase
    {
        private readonly IPlaylistService playlistService;

        public PlaylistController(IPlaylistService playlistService)
        {
            this.playlistService = playlistService;
        }

        [HttpGet("All")]
        public async Task<IEnumerable<MusicShop.Domain.Entities.Playlist>> GetAll()
        {
            return await playlistService.GetAll();
        }

        [HttpGet("SearchById")]
        public async Task<IActionResult> Get([FromQuery] PlaylistGetRequest request)
        {
            var playlist = await playlistService.Get(request);
            return playlist == null ? NotFound($"Плейлист {request.PlaylistId} не найден") : Ok(playlist);
        }

        [HttpGet("Search")]
        public async Task<IEnumerable<MusicShop.Domain.Entities.Playlist>> Search([FromQuery] PlaylistSearchRequest request)
        {
            return await playlistService.Search(request);
        }

        [HttpPost("InsertPlaylist")]
        public async Task<IActionResult> InsertPlaylist([FromBody] PlaylistInsertRequest request)
        {
            var playlistId = await playlistService.Insert(request);
            if (playlistId.HasValue)
            {
                return Ok(new { PlaylistId = playlistId });
            }
            return BadRequest("Не удалось создать плейлист или такой плейлист уже существует");
        }

        [HttpPost("UpdatePlaylist")]
        public async Task<IActionResult> UpdatePlaylist([FromBody] PlaylistUpdateRequest request)
        {
            var success = await playlistService.Update(request);
            return success ? Ok($"Плейлист {request.PlaylistId} обновлен") : BadRequest($"Не удалось обновить плейлист {request.PlaylistId}");
        }

        [HttpDelete("DeletePlaylist")]
        public async Task<IActionResult> DeletePlaylist([FromQuery] PlaylistDeleteRequest request)
        {
            var success = await playlistService.Delete(request);
            return success ? Ok($"Плейлист {request.PlaylistId} удален") : NotFound($"Плейлист {request.PlaylistId} не найден");
        }
    }
}
