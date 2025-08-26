using MusicShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicShop.Bussines.Features.Track.Update
{
    public class TrackUpdateRequest
    {
        [Range(1, long.MaxValue, ErrorMessage = "Введено неверное значение")]
        public int TrackId { get; set; }
        public string Name { get; set; } = null!;

        [Range(1, long.MaxValue, ErrorMessage = "Введено неверное значение")]
        public long? AlbumId { get; set; }

        [Range(1, long.MaxValue, ErrorMessage = "Введено неверное значение")]
        public long MediaTypeId { get; set; }

        [Range(1, long.MaxValue, ErrorMessage = "Введено неверное значение")]
        public long? GenreId { get; set; }

        [StringLength(100, MinimumLength = 3, ErrorMessage = "Длина должна быть больше 3-х символов и меньше 100")]
        public string? Composer { get; set; }

        [StringLength(100, MinimumLength = 1, ErrorMessage = "Длина должна быть больше 1 символа и меньше 100")]
        public long Milliseconds { get; set; }
        public long? Bytes { get; set; }

        [StringLength(100, MinimumLength = 3, ErrorMessage = "Длина должна быть больше 3-х символов и меньше 100")]
        public decimal UnitPrice { get; set; }

        public Album? Album { get; set; }
        public MusicShop.Domain.Entities.MediaType? MediaType { get; set; }
        public MusicShop.Domain.Entities.Genre? Genre { get; set; }
    }
}
