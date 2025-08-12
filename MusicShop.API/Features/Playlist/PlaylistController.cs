using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MusicShop.API.Features.Albums.Requests;
using MusicShop.API.Features.Artist.Request;
using MusicShop.Data;
using MusicShop.Data.Dapper;
using MusicShop.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace MusicShop.API.Features.Playlist
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
        public async Task<IEnumerable<MusicShop.Domain.Entities.Playlist>> GetAll()
        {
            return await playlistRepository.GetAll();
        }
    }
}
