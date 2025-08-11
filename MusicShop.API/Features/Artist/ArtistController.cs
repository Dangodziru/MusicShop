using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MusicShop.API.Features.Albums.Requests;
using MusicShop.API.Features.Artist.Request;
using MusicShop.Data;
using MusicShop.Data.Dapper;
using MusicShop.Domain.Entities;
using System;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class ArtistController : ControllerBase
{
    private readonly IArtistRepository artistRepository;

    public ArtistController(IArtistRepository artistRepository)
    {
        this.artistRepository = artistRepository;
    }

    [HttpGet("All")]
    public Task<IEnumerable<Artist>> GetAll()
    {
        return artistRepository.GetAll();
    }

    [HttpGet("SearchById")]
    public async Task<IActionResult> Get([FromQuery] ArtistGetRequest request)
    {
        var artist = await artistRepository.Get(request.ArtistId);
        return artist == null ? NotFound($"Исполнитель {request.ArtistId} не найден") : Ok(artist);
    }

    [HttpGet("Search")]
    public Task<IEnumerable<Artist>> Search([FromQuery] ArtistSearchReqest request)
    {
        return artistRepository.Search(request.NameSearch);
    }

    [HttpPost("InsertArtist")]
    public async Task<IActionResult> InsertArtist(ArtistInsertReqestcs request)
    {
        bool artistExist = await artistRepository.ArtistIsExist(request.Name);
        if (artistExist)
        {
            return BadRequest("Такой исполнитель уже существует");
        }
        var artistId = await artistRepository.InsertArtist(request.Name);
        if (artistId.HasValue)
        {
            return Ok(new { ArtistId = artistId });
        }
        else
        {
            return BadRequest("Не удалось создать исполнителя");
        }
    }

    [HttpDelete("DeleteArtist")]
    public async Task<IActionResult> DeleteArtist(ArtistDeleteRequest request)
    {
        return await artistRepository.DeleteArtist(request.ArtistId)
            ? Ok($"Исполнитель {request.ArtistId} удален")
            : NotFound($"Исполнитель {request.ArtistId} не найден");
    }

    [HttpPost("UpdateArtist")]
    public async Task<IActionResult> UpdateArtist(ArtistUpdateRequest request)
    {
        return await artistRepository.UpdateArtist(request.ArtistId, request.Name)
            ? Ok($"Исполнитель {request.ArtistId} обновлен")
            : NotFound($"Исполнитель {request.ArtistId} не найден");
    }
}