using Mediator;
using Microsoft.AspNetCore.Mvc;
using MusicShop.Bussines.Features.Albums.Delete;
using MusicShop.Bussines.Features.Albums.Get;
using MusicShop.Bussines.Features.Albums.GetAll;
using MusicShop.Bussines.Features.Albums.Insert;
using MusicShop.Bussines.Features.Albums.Search;
using MusicShop.Bussines.Features.Albums.Service;
using MusicShop.Bussines.Features.Albums.Update;
using MusicShop.Domain.Entities;

namespace MusicShop.API.Features.Albums
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlbumController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IAlbumService _albumService;

        public AlbumController(IMediator mediator, IAlbumService albumService)
        {
            _mediator = mediator;
            _albumService = albumService;
        }

        [HttpGet("All")]
        public async ValueTask<IEnumerable<Album>> GetAll([FromQuery] GetAllRequest request, CancellationToken ct)
        {
            return await _mediator.Send(request, ct);
        }

        [HttpGet("SearchById")]
        public async ValueTask<IActionResult> Get([FromQuery] GetRequest request, CancellationToken ct)
        {
            var album = await _mediator.Send(request, ct);
            return album == null
                ? NotFound($"Альбом {request.AlbumId} не найден")
                : Ok(album);
        }

        [HttpGet("Search")]
        public async Task<IEnumerable<Album>> Search([FromQuery] AlbumSearchRequest request)
        {
            return await _albumService.Search(request);
        }

        [HttpPost("InsertAlbum")]
        public async Task<IActionResult> InsertAlbum([FromBody] AlbumInsertRequest request)
        {
            var albumId = await _albumService.Insert(request);
            if (albumId.HasValue)
            {
                return Ok(new { AlbumId = albumId });
            }
            return BadRequest("Не удалось создать альбом или такой альбом уже существует");
        }

        [HttpPost("UpdateAlbum")]
        public async Task<IActionResult> UpdateAlbum([FromBody] AlbumUpdateRequest request)
        {
            var success = await _albumService.Update(request);
            return success ? Ok($"Альбом {request.AlbumId} обновлен") : BadRequest($"Не удалось обновить альбом {request.AlbumId}");
        }

        [HttpDelete("DeleteAlbum")]
        public async Task<IActionResult> DeleteAlbum([FromQuery] AlbumDeleteRequest request)
        {
            var success = await _albumService.Delete(request);
            return success ? Ok($"Альбом {request.AlbumId} удален") : NotFound($"Альбом {request.AlbumId} не найден");
        }
    }
}