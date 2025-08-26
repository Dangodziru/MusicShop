using System.ComponentModel.DataAnnotations;

namespace MusicShop.Bussines.Features.Albums.Delete
{
    public class AlbumDeleteRequest
    {
        [Range(1, int.MaxValue, ErrorMessage = "Введено неверное значение")]
        public int AlbumId { get; set; }
    }
}
