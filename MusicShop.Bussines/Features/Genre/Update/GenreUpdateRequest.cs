using System.ComponentModel.DataAnnotations;

namespace MusicShop.Bussines.Features.Genre.Update
{
    public class GenreUpdateRequest
    {
        [Range(1, long.MaxValue, ErrorMessage = "Введено неверное значение")]
        public long GenreId { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Длина должна быть больше 3-х символов и меньше 100")]
        public string Name { get; set; }
    }
}
