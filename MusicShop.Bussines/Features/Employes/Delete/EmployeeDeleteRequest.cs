using System.ComponentModel.DataAnnotations;

namespace MusicShop.Bussines.Features.Employes.Delete
{
    public class EmployeeDeleteRequest
    {
        [Range(1, long.MaxValue, ErrorMessage = "Введено неверное значение")]
        public long EmployeeId { get; set; }
    }
}
