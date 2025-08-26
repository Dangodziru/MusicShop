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
        [Range(1, int.MaxValue, ErrorMessage = "Введено неверное значение")]
        public int TrackId { get; set; }
        public string Name { get; set; } = null!;

        [Range(1, int.MaxValue, ErrorMessage = "Введено неверное значение")]
        public int? AlbumId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Введено неверное значение")]
        public int MediaTypeId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Введено неверное значение")]
        public int? GenreId { get; set; }

        [StringLength(100, MinimumLength = 3, ErrorMessage = "Длина должна быть больше 3-х символов и меньше 100")]
        public string? Composer { get; set; }

        [StringLength(100, MinimumLength = 1, ErrorMessage = "Длина должна быть больше 1 символа и меньше 100")]
        public int Milliseconds { get; set; }
        public int? Bytes { get; set; }

        [StringLength(100, MinimumLength = 3, ErrorMessage = "Длина должна быть больше 3-х символов и меньше 100")]
        public double UnitPrice { get; set; }

        public Album? Album { get; set; }
        public MusicShop.Domain.Entities.MediaType? MediaType { get; set; }
        public MusicShop.Domain.Entities.Genre? Genre { get; set; }
    }
}
