using Microsoft.AspNetCore.Mvc;
using MusicShop.API;
using MusicShop.Data;
using MusicShop.Data.Dapper;
using MusicShop.Domain.Entities;
using System.Collections.Generic;
using System.Data.SQLite;
using MusicShop.Domain;
using MusicShop.API.Features.Genre.Request;
using System.Threading.Tasks;



[ApiController]
[Route("api/[controller]")]
public class GenreController : ControllerBase
{
        private readonly IGenreRepository genreRepository;

    public GenreController(IGenreRepository genreRepository)
    {
        this.genreRepository = genreRepository;
    }


    [HttpGet("All")]
    public async Task<IEnumerable<Genre>> GetAll() => await genreRepository.GetAll();

    
    [HttpGet("SearchById")]
    public async Task<ActionResult<Genre>> Get([FromQuery]GenreGetRequestcs request)
    {
        var genre = await genreRepository.Get(request.GenreId);
        return genre == null
            ? NotFound(request.GenreId)
            : Ok(genre);
    }

    [HttpGet("Search")]
    public async Task<IEnumerable<Genre>> Search([FromQuery] GenreSearchRequestcs request)
    {
        return await genreRepository.Search(request.Term);
    }


    [HttpPost("InsertGenre")]
    public async Task<IActionResult> InsertGenre(GenreInsertRequest request)
    {
        var genre = new Genre { Name = request.Name };
        var newId = await genreRepository.Insert(genre);

        if (newId.HasValue)
        {
            return Ok(new
            {
                Message = $"Жанр успешно создан",
                GenreId = newId.Value
            });
        }
        else
        {
            return BadRequest("Не удалось создать жанр");
        }
    }
        

    [HttpPut("UpdateGenre")]
    public async Task<IActionResult> UpdateGenre(GenreUpdateRequest request)
    {
        var genre = new Genre
        {
            GenreId = request.GenreId,
            Name = request.Name
        };

        var updated = await genreRepository.Update(genre);

        return updated
            ? Ok($"Жанр {request.GenreId} успешно обновлен")
            : NotFound($"Жанр с ID {request.GenreId} не найден");
    }

    [HttpDelete("DeleteGenre")]
    public async Task<IActionResult> DeleteGenre(long genreId)
    {
        var deleted = await genreRepository.Delete(genreId);

        return deleted
            ? Ok($"Жанр {genreId} успешно удален")
            : NotFound($"Жанр с ID {genreId} не найден");
    }
}