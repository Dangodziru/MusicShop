using System.ComponentModel.DataAnnotations;

namespace MusicShop.Bussines.Features.Artists.Update
{
    public class ArtistUpdateRequest
    {

        [Range(1, long.MaxValue, ErrorMessage = "Введено неверное значение")]
        public long ArtistId { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Длина должнабыть больше 3-х семиволов и меньше 100")]
        public string Name { get; set; }
    }
}
