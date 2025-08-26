using Microsoft.AspNetCore.Mvc;
using MusicShop.Bussines.Features.Track.Delete;
using MusicShop.Bussines.Features.Track.Get;
using MusicShop.Bussines.Features.Track.Insert;
using MusicShop.Bussines.Features.Track.Search;
using MusicShop.Bussines.Features.Track.Update;
using MusicShop.Bussines.Features.Tracks.Services;
using MusicShop.Domain.Entities;

[ApiController]
[Route("api/[controller]")]
public class TrackController : ControllerBase
{
    private readonly ITrackService trackService;

    public TrackController(ITrackService trackService)
    {
        this.trackService = trackService;
    }

    [HttpGet("All")]
    public async Task<IEnumerable<Track>> GetAll()
    {
        return await trackService.GetAll();
    }

    [HttpGet("SearchById")]
    public async Task<IActionResult> Get([FromQuery] TrackGetRequest request)
    {
        var track = await trackService.Get(request);
        return track == null ? NotFound($"Трек {request.TrackId} не найден") : Ok(track);
    }

    [HttpGet("Search")]
    public async Task<IEnumerable<Track>> Search([FromQuery] TrackSearchRequest request)
    {
        return await trackService.Search(request);
    }

    [HttpPost("InsertTrack")]
    public async Task<IActionResult> InsertTrack([FromBody] TrackInsertRequest request)
    {
        var trackId = await trackService.Insert(request);
        if (trackId.HasValue)
        {
            return Ok(new { TrackId = trackId });
        }
        return BadRequest("Не удалось создать трек или такой трек уже существует");
    }

    [HttpPut("UpdateTrack")]
    public async Task<IActionResult> UpdateTrack([FromBody] TrackUpdateRequest request)
    {
        var success = await trackService.Update(request);
        return success ? Ok($"Трек {request.TrackId} обновлен") : BadRequest($"Не удалось обновить трек {request.TrackId}");
    }

    [HttpDelete("DeleteTrack")]
    public async Task<IActionResult> DeleteTrack([FromQuery] TrackDeleteRequest request)
    {
        var success = await trackService.Delete(request);
        return success ? Ok($"Трек {request.TrackId} удален") : NotFound($"Трек {request.TrackId} не найден");
    }
}