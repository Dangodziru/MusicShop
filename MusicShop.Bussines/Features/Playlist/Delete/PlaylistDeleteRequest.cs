using System.ComponentModel.DataAnnotations;

namespace MusicShop.Bussines.Features.Playlist.Delete
{
    public class PlaylistDeleteRequest
    {
        [Range(1, long.MaxValue, ErrorMessage = "Введено неверное значение")]
        public long PlaylistId { get; set; }
    }
}
