using Mediator;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MusicShop.API.Features.Albums.Requests;
using MusicShop.Bussines.Features.Albums.Get;
using MusicShop.Bussines.Features.Albums.GetAll;
using MusicShop.Data;
using MusicShop.Data.Dapper;
using MusicShop.Domain;
using MusicShop.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace MusicShop.API.Features.Albums
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlbumController : ControllerBase
    {
        private readonly IAlbumRepository albumRepository;
        private readonly IMediator mediator;

        public AlbumController(IAlbumRepository albumRepository, IMediator mediator)
        {
            this.albumRepository = albumRepository;
            this.mediator = mediator;
        }

        [HttpGet("All")]
        public async ValueTask<IEnumerable<Album>> GetAll([FromQuery] GetAllRequest request, CancellationToken ct)
        {
            return await mediator.Send(request, ct);
        }

        [HttpGet("SearchById")]
        public async ValueTask<IActionResult> Get([FromQuery]GetRequest request, CancellationToken ct)
        {
            var album = await mediator.Send(request, ct);
            return album == null
                ? NotFound(request.AlbumId)
                : Ok(album);
        }

        [HttpGet("Search")]
        public async Task<IEnumerable<Album>> Search([FromQuery] AlbumSearchRequest request)
        {
           return await albumRepository.Search(request.TitleSearch);
        }
        
        [HttpPost("InsertAlbum")]
        public async Task<IActionResult> InsertAlbum(AlbumInsertRequest request)
        {
            bool albumIsExist = await albumRepository.AlbumIsExist(request.Title, request.ArtistId);
            if (albumIsExist)
            {
                return BadRequest("Такой альбом уже существует у данного исполнителя");
            }
            var albumId =await albumRepository.InsertAlbum(request.Title, request.ArtistId);
            if (albumId.HasValue)
            {
                return Ok(new { AlbumId = albumId });
            }
            else 
            {
                return BadRequest("Не удалось создать альбом");
            }

        }



        [HttpDelete("DeleteAlbum")]
        public async Task<IActionResult> DeleteAlbum(AlbumDeleteRequest request)
        {
            return await albumRepository.DeleteAlbum(request.AlbumId)
                ? Ok(request.AlbumId)
                : NotFound($"Альбом {request.AlbumId} не найден");
        }

        [HttpPost("UpdateAlbum")]
        public async Task<IActionResult> UpdateAlbum(AlbumUpdateRequest request)
        {
            return await albumRepository.UpdateAlbum(request.AlbumId, request.Title, request.ArtistId)
                ? Ok(request.AlbumId)
                : NotFound($"Альбом {request.AlbumId} не найден");
        }
    }
}