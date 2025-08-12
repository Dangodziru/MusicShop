using System.ComponentModel.DataAnnotations;

namespace MusicShop.Bussines.Features.Customers.Get
{
    public class CustomerGetRequest
    {
        [Range(1, long.MaxValue, ErrorMessage = "Введено неверное значение")]
        public long CustomerId { get; set; }
    }
}
