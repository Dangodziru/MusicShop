using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MusicShop.API.Features.Albums.Requests;
using MusicShop.Data;
using MusicShop.Data.Dapper;
using MusicShop.Data.Validating;
using MusicShop.Domain.Entities;
using System;

namespace MusicShop.API.Features.Albums
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlbumController : ControllerBase
    {
        private readonly IAlbumRepository albumRepository;

        public AlbumController()
        {
            albumRepository = new AlbumDapperRepository();
        }

        [HttpGet("All")]
        public List<Album> GetAll() => albumRepository.GetAll();

        [HttpGet("SearchById")]
        public IActionResult Get([FromQuery]AlbumGetRequest request)
        {
            var album = albumRepository.Get(request.AlbumId);
            return album == null
                ? NotFound($"Album {request.AlbumId} not found")
                : Ok(album);
        }

        [HttpGet("Search")]
        public List<Album> Search([FromQuery] AlbumSearchRequest request)
        {
           return albumRepository.Search(request.TitleSearch);
        }
        
        [HttpPost("InsertAlbum")]
        public IActionResult InsertAlbum(AlbumInsertRequest request)
        {
            bool albumIsExist = albumRepository.AlbumIsExist(request.Title, request.ArtistId);
            if (albumIsExist)
            {
                return BadRequest("Такой альбом уже существует у данного исполнителя");
            }
            var albumId = albumRepository.InsertAlbum(request.Title, request.ArtistId);
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
        public IActionResult DeleteAlbum(AlbumDeleteRequest request)
        {
            return albumRepository.DeleteAlbum(request.AlbumId)
                ? Ok($"Album {request.AlbumId} deleted")
                : NotFound($"Album {request.AlbumId} not found");
        }

        [HttpPost("UpdateAlbum")]
        public IActionResult UpdateAlbum(AlbumUpdateRequest request)
        {
            return albumRepository.UpdateAlbum(request.AlbumId, request.Title, request.ArtistId)
                ? Ok($"Album {request.AlbumId} updated")
                : NotFound($"Album {request.AlbumId} not found");
        }
    }
}