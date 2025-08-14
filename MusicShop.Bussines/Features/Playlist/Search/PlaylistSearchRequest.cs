using System.ComponentModel.DataAnnotations;

namespace MusicShop.Bussines.Features.Playlists.Search
{
    public class PlaylistSearchRequest
    {
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Длинна должнабыть больше 3х семиволов и меньше 100")]
        public string Name { get; set; }
    }
}
