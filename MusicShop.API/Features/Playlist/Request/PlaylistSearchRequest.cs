using System.ComponentModel.DataAnnotations;

namespace MusicShop.API.Features.Playlist.Request
{
    public class PlaylistSearchRequest
    {
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Длинна должнабыть больше 3х семиволов и меньше 100")]
        public string Name { get; set; }
    }
}
