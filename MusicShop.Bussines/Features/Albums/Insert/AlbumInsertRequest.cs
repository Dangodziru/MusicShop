using System.ComponentModel.DataAnnotations;

namespace MusicShop.Bussines.Features.Albums.Insert
{
    public class AlbumInsertRequest
    {
        [Range(1, long.MaxValue, ErrorMessage = "Введено неверное значение")]
        public long ArtistId { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Длинна должнабыть больше 3х семиволов и меньше 100")]
        public string Title { get; set; }
    }
}
