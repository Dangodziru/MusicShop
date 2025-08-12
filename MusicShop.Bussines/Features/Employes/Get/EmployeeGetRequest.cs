using System.ComponentModel.DataAnnotations;

namespace MusicShop.Bussines.Features.Employes.Get
{
    public class EmployeeGetRequest
    {
        [Range(1, long.MaxValue, ErrorMessage = "Введено неверное значение")]
        public long EmployeeId { get; set; }
    }
}
