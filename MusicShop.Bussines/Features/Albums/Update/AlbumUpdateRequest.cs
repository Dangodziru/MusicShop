using System.ComponentModel.DataAnnotations;

namespace MusicShop.Bussines.Features.Albums.Update
{
    public class AlbumUpdateRequest
    {
        [Range(1, long.MaxValue, ErrorMessage = "Введено неверное значение")]
        public long AlbumId { get; set; }

        [Range(1, long.MaxValue, ErrorMessage = "Введено неверное значение")]
        public long ArtistId { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Длина должнабыть больше 3-х семиволов и меньше 100")]
        public string Title { get; set; }
    }
}
