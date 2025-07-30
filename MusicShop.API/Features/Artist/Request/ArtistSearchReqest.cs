using System.ComponentModel.DataAnnotations;

namespace MusicShop.API.Features.Artist.Request
{
    public class ArtistSearchReqest
    {
        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string NameSearch { get; set; }
    }
}
