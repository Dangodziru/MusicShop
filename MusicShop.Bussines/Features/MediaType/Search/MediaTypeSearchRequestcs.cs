using System.ComponentModel.DataAnnotations;

namespace MusicShop.Bussines.Features.MediaType.Search
{
    public class MediaTypeSearchRequestcs
    {
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Длина должнабыть больше 3-х семиволов и меньше 100")]
        public string Name { get; set; }
    }
}
