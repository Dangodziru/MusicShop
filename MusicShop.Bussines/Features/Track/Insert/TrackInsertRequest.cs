using MusicShop.Domain.Entities;
using System.ComponentModel.DataAnnotations;


namespace MusicShop.Bussines.Features.Track.Insert
{
    public class TrackInsertRequest
    {
        public string Name { get; set; } = null!;
        [Range(1, int.MaxValue, ErrorMessage = "Введено неверное значение")]
        public int? AlbumId { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Введено неверное значение")]
        public int MediaTypeId { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Введено неверное значение")]
        public int? GenreId { get; set; }

        [StringLength(100, MinimumLength = 3, ErrorMessage = "Длина должна быть больше 3-х символов и меньше 100")]
        public string? Composer { get; set; }

        [StringLength(100, MinimumLength = 3, ErrorMessage = "Длина должна быть больше 3-х символов и меньше 100")]
        public int Milliseconds { get; set; }
        public int? Bytes { get; set; }

        [StringLength(100, MinimumLength = 3, ErrorMessage = "Длина должна быть больше 3-х символов и меньше 100")]
        public double UnitPrice { get; set; }

        public Album? Album { get; set; }
        public MusicShop.Domain.Entities.MediaType? MediaType { get; set; }
        public MusicShop.Domain.Entities.Genre? Genre { get; set; }
    }
}
