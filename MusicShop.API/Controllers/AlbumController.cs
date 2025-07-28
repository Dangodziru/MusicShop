using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MusicShop.Data;
using MusicShop.Domain.Entities;
using System.Collections.Generic;
using System.Data.SQLite;

namespace MusicShop.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlbumController : ControllerBase
    {

        AlbumRepository albumRepository;
        public AlbumController()
        {
            albumRepository = new AlbumRepository();
        }

        [HttpGet("All")]
        public List<Album> GetAll()
        {
            var albums =  albumRepository.GetAll();
            return albums;
        }

        [HttpGet("SearchById")]
        public Album? Get(long albumId)
        {
            var album = albumRepository.Get(albumId);
            return album;
        }

        [HttpGet("Search")]
        public List<Album> Search(string titleSearch)
        {
            var albums = albumRepository.Search(titleSearch);
            return albums;
        }

        [HttpPost("InsertAlbum")]
        public long? InsertAlbum(string title, long artistId)
        {
            var albumId = albumRepository.InsertAlbum(title, artistId);
            return albumId;
        }
        
        [HttpDelete("DeleteAlbum")]
        public IActionResult DeleteAlbum(long albumId)
        {
            var deleted = albumRepository.DeleteAlbum(albumId);

            if (deleted)
            {
                return Ok($"Album {albumId} deleted successfully");
            }
            else
            {
                return NotFound($"Album with ID {albumId} not found");
            }

        }
        
        [HttpPost("UpdateAlbum")]
        public IActionResult UpdateAlbum(long albumId, string title, long artistId)
        {
            var updated = albumRepository.UpdateAlbum(albumId, title, artistId);

            if (updated)
            {
                return Ok($"Album {albumId} updated successfully");
            }
            else
            {
                return NotFound($"Album with ID {albumId} not found");
            }
        }
    }
}