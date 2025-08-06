using Microsoft.AspNetCore.Mvc;
using MusicShop.API;
using MusicShop.Data;
using MusicShop.Data.Dapper;
using MusicShop.Domain.Entities;
using System.Collections.Generic;
using System.Data.SQLite;
using MusicShop.Domain;
using MusicShop.API.Features.Genre.Request;



[ApiController]
[Route("api/[controller]")]
public class GenreController : ControllerBase
{
        private readonly IGenreRepository genreRepository;

        public GenreController()
        {
            genreRepository = new GenreDapperRepository();
        }


    [HttpGet("All")]
    public List<Genre> GetAll() => genreRepository.GetAll();

    
    [HttpGet("SearchById")]
    public ActionResult<Genre> Get([FromQuery]GenreGetRequestcs request)
    {
        var genre = genreRepository.Get(request.GenreId);
        return genre == null
            ? NotFound(request.GenreId)
            : Ok(genre);
    }

    [HttpGet("Search")]
    public List<Genre> Search([FromQuery] GenreSearchRequestcs request)
    {
        return genreRepository.Search(request.Term);
    }


    [HttpPost("InsertGenre")]
    public IActionResult InsertGenre(GenreInsertRequest request)
    {
        var genre = new Genre { Name = request.Name };
        var newId = genreRepository.Insert(genre);

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
    public IActionResult UpdateGenre(GenreUpdateRequest request)
    {
        var genre = new Genre
        {
            GenreId = request.GenreId,
            Name = request.Name
        };

        var updated = genreRepository.Update(genre);

        return updated
            ? Ok($"Жанр {request.GenreId} успешно обновлен")
            : NotFound($"Жанр с ID {request.GenreId} не найден");
    }

    [HttpDelete("DeleteGenre")]
    public IActionResult DeleteGenre(long genreId)
    {
        var deleted = genreRepository.Delete(genreId);

        return deleted
            ? Ok($"Жанр {genreId} успешно удален")
            : NotFound($"Жанр с ID {genreId} не найден");
    }
}