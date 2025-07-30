using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MusicShop.API.Features.Albums.Requests;
using MusicShop.API.Features.Artist.Request;
using MusicShop.Data;
using MusicShop.Data.Dapper;
using MusicShop.Data.Validating;
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
        return artist == null ? NotFound($"Artist {request.ArtistId} not found") : Ok(artist);
    }

    [HttpGet("Search")]
    public List<Artist> Search([FromQuery] ArtistSearchReqest request)
    {
        return artistRepository.Search(request.NameSearch);
    }

    [HttpPost("InsertArtist")]
    public IActionResult InsertArtist(ArtistInsertReqestcs reqest)
    {
        try
        {
            var artistId = artistRepository.InsertArtist(reqest.Name);
            return artistId.HasValue
                ? Ok(new { ArtistId = artistId })
                : BadRequest("Failed to create artist");
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(ex.Message);
        }
    }

    [HttpDelete("DeleteArtist")]
    public IActionResult DeleteArtist(ArtistDeleteRequest request)
    {
        return artistRepository.DeleteArtist(request.ArtistId)
            ? Ok($"Artist {request.ArtistId} deleted")
            : NotFound($"Artist {request.ArtistId} not found");
    }

    [HttpPost("UpdateArtist")]
    public IActionResult UpdateArtist(ArtistUpdateRequest request)
    {
        return artistRepository.UpdateArtist(request.ArtistId, request.Name)
            ? Ok($"Artist {request.ArtistId} updated")
            : NotFound($"Artist {request.ArtistId} not found");
    }
}