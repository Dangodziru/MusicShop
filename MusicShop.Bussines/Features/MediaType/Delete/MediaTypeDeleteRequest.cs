using System.ComponentModel.DataAnnotations;

namespace MusicShop.Bussines.Features.MediaType.Delete
{
    public class MediaTypeDeleteRequest
    {
        [Range(1, long.MaxValue, ErrorMessage = "Введено неверное значение")]
        public long MediaTypeId { get; set; }

    }
}
