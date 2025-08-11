using System.ComponentModel.DataAnnotations;

namespace MusicShop.API.Features.Playlist.Request
{
    public class PlaylistDeleteRequest
    {
        [Range(1, long.MaxValue, ErrorMessage = "Введено неверное значение")]
        public long PlaylistId { get; set; }
    }
}
