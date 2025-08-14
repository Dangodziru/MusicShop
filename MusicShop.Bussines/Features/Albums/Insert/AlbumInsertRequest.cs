using Mediator;
using MusicShop.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace MusicShop.Bussines.Features.Albums.Insert
{
    public class AlbumInsertRequest : IRequest<Album?>
    {
        [Range(1, long.MaxValue, ErrorMessage = "Введено неверное значение")]
        public long ArtistId { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Длинна должнабыть больше 3х семиволов и меньше 100")]
        public string Title { get; set; }
    }
}
