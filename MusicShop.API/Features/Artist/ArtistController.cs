using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MusicShop.API.Features.Albums.Requests;
using MusicShop.API.Features.Artist.Request;
using MusicShop.Data;
using MusicShop.Data.Dapper;
using MusicShop.Domain.Entities;
using System;

[ApiController]
[Route("api/[controller]")]
public class ArtistController : ControllerBase
{
    private readonly IArtistRepository artistRepository;

    public ArtistController()
    {
        artistRepository = new ArtistDapperRepository();
    }

    [HttpGet("All")]
    public List<Artist> GetAll()
    {
        return artistRepository.GetAll();
    }

    [HttpGet("SearchById")]
    public IActionResult Get([FromQuery] ArtistGetRequest request)
    {
        var artist = artistRepository.Get(request.ArtistId);
        return artist == null ? NotFound($"Исполнитель {request.ArtistId} не найден") : Ok(artist);
    }

    [HttpGet("Search")]
    public List<Artist> Search([FromQuery] ArtistSearchReqest request)
    {
        return artistRepository.Search(request.NameSearch);
    }

    [HttpPost("InsertArtist")]
    public IActionResult InsertArtist(ArtistInsertReqestcs request)
    {
        bool artistExist = artistRepository.ArtistIsExist(request.Name);
        if (artistExist)
        {
            return BadRequest("Такой исполнитель уже существует");
        }
        var artistId = artistRepository.InsertArtist(request.Name);
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
    public IActionResult DeleteArtist(ArtistDeleteRequest request)
    {
        return artistRepository.DeleteArtist(request.ArtistId)
            ? Ok($"Исполнитель {request.ArtistId} удален")
            : NotFound($"Исполнитель {request.ArtistId} не найден");
    }

    [HttpPost("UpdateArtist")]
    public IActionResult UpdateArtist(ArtistUpdateRequest request)
    {
        return artistRepository.UpdateArtist(request.ArtistId, request.Name)
            ? Ok($"Исполнитель {request.ArtistId} обновлен")
            : NotFound($"Исполнитель {request.ArtistId} не найден");
    }
}