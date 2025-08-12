using System.ComponentModel.DataAnnotations;

namespace MusicShop.Bussines.Features.Albums.Delete
{
    public class AlbumDeleteRequest
    {
        [Range(1, long.MaxValue, ErrorMessage = "Введено неверное значение")]
        public long AlbumId { get; set; }
    }
}
