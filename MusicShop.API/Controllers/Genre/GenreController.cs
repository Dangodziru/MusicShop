using Microsoft.AspNetCore.Mvc;
using MusicShop.Bussines.Features.Genre;
using MusicShop.Bussines.Features.Genre.Delete;
using MusicShop.Bussines.Features.Genre.Get;
using MusicShop.Bussines.Features.Genre.Insert;
using MusicShop.Bussines.Features.Genre.Search;
using MusicShop.Bussines.Features.Genre.Services;
using MusicShop.Bussines.Features.Genre.Update;
using MusicShop.Domain.Entities;

namespace MusicShop.API.Features.Genres
{
    [ApiController]
    [Route("api/[controller]")]
    public class GenreController : ControllerBase
    {
        private readonly IGenreService genreService;

        public GenreController(IGenreService genreService)
        {
            this.genreService = genreService;
        }

        [HttpGet("All")]
        public async Task<IEnumerable<Genre>> GetAll()
        {
            return await genreService.GetAll();
        }

        [HttpGet("SearchById")]
        public async Task<IActionResult> Get([FromQuery] GenreGetRequests request)
        {
            var genre = await genreService.Get(request);
            return genre == null ? NotFound($"Жанр {request.GenreId} не найден") : Ok(genre);
        }

        [HttpGet("Search")]
        public async Task<IEnumerable<Genre>> Search([FromQuery] GenreSearchRequests request)
        {
            return await genreService.Search(request);
        }

        [HttpPost("InsertGenre")]
        public async Task<IActionResult> InsertGenre([FromBody] GenreInsertRequest request)
        {
            var genreId = await genreService.Insert(request);
            if (genreId.HasValue)
            {
                return Ok(new { Message = "Жанр успешно создан", GenreId = genreId.Value });
            }
            return BadRequest("Не удалось создать жанр или такой жанр уже существует");
        }

        [HttpPut("UpdateGenre")]
        public async Task<IActionResult> UpdateGenre([FromBody] GenreUpdateRequest request)
        {
            var success = await genreService.Update(request);
            return success ? Ok($"Жанр {request.GenreId} успешно обновлен") : NotFound($"Жанр {request.GenreId} не найден");
        }

        [HttpDelete("DeleteGenre")]
        public async Task<IActionResult> DeleteGenre([FromQuery] GenreDeleteRequest request)
        {
            var success = await genreService.Delete(request);
            return success ? Ok($"Жанр {request.GenreId} успешно удален") : NotFound($"Жанр {request.GenreId} не найден");
        }
    }
}
