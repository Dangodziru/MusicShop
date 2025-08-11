using System.ComponentModel.DataAnnotations;

namespace MusicShop.API.Features.MediaType.Reqest
{
    public class MediaTypeSearchRequestcs
    {
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Длинна должнабыть больше 3х семиволов и меньше 100")]
        public string Name { get; set; }
    }
}
