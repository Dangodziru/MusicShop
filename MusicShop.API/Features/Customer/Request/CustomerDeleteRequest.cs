using System.ComponentModel.DataAnnotations;

namespace MusicShop.API.Features.Customer.Request
{
    public class CustomerDeleteRequest
    {
        [Range(1, long.MaxValue, ErrorMessage = "Введено неверное значение")]
        public long CustomerId { get; set; }
    }
}
