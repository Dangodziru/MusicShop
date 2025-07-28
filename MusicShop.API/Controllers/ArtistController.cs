using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MusicShop.API;
using MusicShop.API.Controllers;
using System.Collections.Generic;
using System.Data.SQLite;
using MusicShop.Domain.Entities;
using MusicShop.Data;

[ApiController]
[Route("api/[controller]")]
public class ArtistController : ControllerBase
{

    ArtistRepository artistRepository;
    public ArtistController()
    {
        artistRepository = new ArtistRepository();
    }

    [HttpGet("All")]
    public List<Artist> GetAll()
    {
        var artists = artistRepository.GetAll();
        return artists;
    }
    
    [HttpGet("SearchById")]
    public Artist? Get(long artistId)
    {
        var artist = artistRepository.Get(artistId);
        return artist;
    }

    [HttpGet("Search")]
    public List<Artist> Search(string titleSearch)
    {
        var artists = artistRepository.Search(titleSearch);
        return artists;
    }

    [HttpPost("InsertArtist")]
    public long? InsertArtist(string name)
    {
        var artistId = artistRepository.InsertArtist(name);
        return artistId;
    }
   
    [HttpDelete("DeleteArtist")]
    public IActionResult DeleteArtist(long artistId)
    {
        var deleted = artistRepository.DeleteArtist(artistId);
        if (deleted)
        {
            return Ok($"Artist {artistId} deleted successfully");
        }
        else
        {
            return NotFound($"Artist with ID {artistId} not found");
        }
    }
   
    [HttpPost("UpdateArtist")]
    public IActionResult UpdateAlbum(long artistId, string name)
    {
        var updated = artistRepository.UpdateArtist(artistId, name);
        if (updated)
        {
            return Ok($"Artist {artistId} updated successfully");
        }
        else
        {
            return NotFound($"Artist with ID {artistId} not found");
        }
    }
}   
    

