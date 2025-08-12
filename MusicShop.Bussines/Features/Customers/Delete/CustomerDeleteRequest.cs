using System.ComponentModel.DataAnnotations;

namespace MusicShop.Bussines.Features.Customers.Delete
{
    public class CustomerDeleteRequest
    {
        [Range(1, long.MaxValue, ErrorMessage = "Введено неверное значение")]
        public long CustomerId { get; set; }
    }
}
