using System.ComponentModel.DataAnnotations;

namespace MusicShop.Bussines.Features.Genre.Delete
{
    public class GenreDeleteRequest
    {
        [Range(1, long.MaxValue, ErrorMessage = "Введено неверное значение")]
        public long GenreId { get; set; }
    }
}
