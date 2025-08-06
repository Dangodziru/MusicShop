using System.ComponentModel.DataAnnotations;

namespace MusicShop.API.Features.Genre.Request
{
    public class GenreGetRequestcs
    {
        [Range(1, long.MaxValue, ErrorMessage = "Введено неверное значение")]
        public long GenreId { get; set; }
    }
}
