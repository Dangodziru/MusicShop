using Microsoft.AspNetCore.Mvc;
using MusicShop.Bussines.Features.Artists.Delete;
using MusicShop.Bussines.Features.Artists.Get;
using MusicShop.Bussines.Features.Artists.Insert;
using MusicShop.Bussines.Features.Artists.Search;
using MusicShop.Bussines.Features.Artists.Service;
using MusicShop.Bussines.Features.Artists.Update;
using MusicShop.Data;
using MusicShop.Domain.Entities;

[ApiController]
[Route("api/[controller]")]
public class ArtistController : ControllerBase
{
    private readonly IArtistService artistService;

    public ArtistController(IArtistRepository artistRepository, IArtistService artistService)
    {
        this.artistService = artistService;
    }

    [HttpGet("All")]
    public async Task<IEnumerable<Artist>> GetAll()
    {
        return await artistService.GetAll();
    }

    [HttpGet("SearchById")]
    public async Task<IActionResult> Get([FromQuery] ArtistGetRequest request)
    {
        var artist = await artistService.Get(request);
        return artist == null ? NotFound($"Исполнитель {request.ArtistId} не найден") : Ok(artist);
    }

    [HttpGet("Search")]
    public async Task<IEnumerable<Artist>> Search([FromQuery] ArtistSearchRequest request)
    {
        return await artistService.Search(request);
    }

    [HttpPost("InsertArtist")]
    public async Task<IActionResult> InsertArtist([FromBody] ArtistInsertRequests request)
    {
        var artistId = await artistService.Insert(request);
        if (artistId.HasValue)
        {
            return Ok(new { ArtistId = artistId });
        }
        return BadRequest("Не удалось создать исполнителя или такой исполнитель уже существует");
    }

    [HttpPost("UpdateArtist")]
    public async Task<IActionResult> UpdateArtist([FromBody] ArtistUpdateRequest request)
    {
        var success = await artistService.Update(request);
        return success ? Ok($"Исполнитель {request.ArtistId} обновлен") : BadRequest($"Не удалось обновить исполнителя {request.ArtistId}");
    }

    [HttpDelete("DeleteArtist")]
    public async Task<IActionResult> DeleteArtist([FromQuery] ArtistDeleteRequest request)
    {
        var success = await artistService.Delete(request);
        return success ? Ok($"Исполнитель {request.ArtistId} удален") : NotFound($"Исполнитель {request.ArtistId} не найден");
    }
}