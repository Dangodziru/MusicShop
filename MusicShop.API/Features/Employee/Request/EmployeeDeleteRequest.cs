using System.ComponentModel.DataAnnotations;

namespace MusicShop.API.Features.Employee.Request
{
    public class EmployeeDeleteRequest
    {
        [Range(1, long.MaxValue, ErrorMessage = "Введено неверное значение")]
        public long EmployeeId { get; set; }
    }
}
