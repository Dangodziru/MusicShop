using MusicShop.Domain.Entities;
using System.ComponentModel.DataAnnotations;


namespace MusicShop.Bussines.Features.Track.Insert
{
    public class TrackInsertRequest
    {
        public string Name { get; set; } = null!;
        [Range(1, long.MaxValue, ErrorMessage = "Введено неверное значение")]
        public long? AlbumId { get; set; }
        [Range(1, long.MaxValue, ErrorMessage = "Введено неверное значение")]
        public long MediaTypeId { get; set; }
        [Range(1, long.MaxValue, ErrorMessage = "Введено неверное значение")]
        public long? GenreId { get; set; }

        [StringLength(100, MinimumLength = 3, ErrorMessage = "Длина должна быть больше 3-х символов и меньше 100")]
        public string? Composer { get; set; }

        [StringLength(100, MinimumLength = 3, ErrorMessage = "Длина должна быть больше 3-х символов и меньше 100")]
        public long Milliseconds { get; set; }
        public long? Bytes { get; set; }

        [StringLength(100, MinimumLength = 3, ErrorMessage = "Длина должна быть больше 3-х символов и меньше 100")]
        public decimal UnitPrice { get; set; }

        public Album? Album { get; set; }
        public MusicShop.Domain.Entities.MediaType? MediaType { get; set; }
        public MusicShop.Domain.Entities.Genre? Genre { get; set; }
    }
}
