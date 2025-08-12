using Microsoft.AspNetCore.Mvc;
using MusicShop.Bussines.Features.MediaType.Delete;
using MusicShop.Bussines.Features.MediaType.Get;
using MusicShop.Bussines.Features.MediaType.Insert;
using MusicShop.Bussines.Features.MediaType.Search;
using MusicShop.Bussines.Features.MediaType.Update;
using MusicShop.Domain;
using MediaType = MusicShop.Domain.Entities.MediaType;

[ApiController]
[Route("api/[controller]")]
public class MediaTypeController : ControllerBase
{
    private readonly IMediaTypeRepository mediaTypeRepository;

    public MediaTypeController(IMediaTypeRepository mediaTypeRepository)
    {
        this.mediaTypeRepository = mediaTypeRepository;
    }

    [HttpGet("All")]
    public async Task<IEnumerable<MediaType>> GetAll()
    {
        return await mediaTypeRepository.GetAll();
    }

    [HttpGet("SearchById")]
    public async Task<IActionResult> Get([FromQuery] MediaTypeGetRequest request)
    {
        var media = await mediaTypeRepository.Get(request.MediaTypeId);
        return media == null ? NotFound($"Исполнитель {request.MediaTypeId} не найден") : Ok(media);
    }

    [HttpGet("Search")]
    public async Task<IEnumerable<MediaType>> Search([FromQuery] MediaTypeSearchRequestcs request)
    {
        return await mediaTypeRepository.Search(request.Name);
    }

    [HttpPost("InsertMediaType")]
    public async Task<IActionResult> Media(MediaTypeInsertRequest request)
    {
        bool mediaExist = await mediaTypeRepository.MediaTypeIsExist(request.Name);
        if (mediaExist)
        {
            return BadRequest("Такой исполнитель уже существует");
        }
        var mediaTypeId = await mediaTypeRepository.InsertMediaType(request.Name);
        if (mediaTypeId.HasValue)
        {
            return Ok(new { MediaTypetId = mediaTypeId });
        }
        else
        {
            return BadRequest("Не удалось создать исполнителя");
        }
    }

    [HttpDelete("DeleteMediaType")]
    public async Task<IActionResult> DeleteArtist(MediaTypeDeleteRequest request)
    {
        return await mediaTypeRepository.DeleteMediaType(request.MediaTypeId)
            ? Ok($"Исполнитель {request.MediaTypeId} удален")
            : NotFound($"Исполнитель {request.MediaTypeId} не найден");
    }

    [HttpPost("UpdateMediaType")]
    public async Task<IActionResult> UpdateMediaType(MediaTypeUpdateRequest request)
    {
        return await mediaTypeRepository.UpdateMediaType(request.MediaTypeId, request.Name)
            ? Ok($"Исполнитель {request.MediaTypeId} обновлен")
            : NotFound($"Исполнитель {request.MediaTypeId} не найден");
    }
}