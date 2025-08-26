using System.ComponentModel.DataAnnotations;

namespace MusicShop.Bussines.Features.Genre.Get
{
    public class GenreGetRequests
    {
        [Range(1, long.MaxValue, ErrorMessage = "Введено неверное значение")]
        public long GenreId { get; set; }
    }
}
