using System.ComponentModel.DataAnnotations;

namespace MusicShop.API.Features.MediaType.Reqest
{
    public class MediaTypeDeleteRequest
    {
        [Range(1, long.MaxValue, ErrorMessage = "Введено неверное значение")]
        public long MediaTypeId { get; set; }

    }
}
