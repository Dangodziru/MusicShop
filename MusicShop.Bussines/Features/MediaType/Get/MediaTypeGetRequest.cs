using System.ComponentModel.DataAnnotations;

namespace MusicShop.Bussines.Features.MediaType.Get
{
    public class MediaTypeGetRequest
    {
        [Range(1, long.MaxValue, ErrorMessage = "Введено неверное значение")]
        public long MediaTypeId { get; set; }
    }
}
