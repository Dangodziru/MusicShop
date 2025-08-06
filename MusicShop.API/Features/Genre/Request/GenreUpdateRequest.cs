using System.ComponentModel.DataAnnotations;

namespace MusicShop.API.Features.Genre.Request
{
    public class GenreUpdateRequest
    {
        [Range(1, long.MaxValue, ErrorMessage = "Введено неверное значение")]
        public long GenreId { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Длинна должнабыть больше 3х семиволов и меньше 100")]
        public string Name { get; set; }
    }
}
