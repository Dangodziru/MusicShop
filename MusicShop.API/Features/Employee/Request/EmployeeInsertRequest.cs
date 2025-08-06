using System.ComponentModel.DataAnnotations;

namespace MusicShop.API.Features.Employee.Request
{
    public class EmployeeInsertRequest
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Title { get; set; }
        public long? ReportsTo { get; set; }
        public DateTime? BirthDate { get; set; }
        public DateTime? HireDate { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string PostalCode { get; set; }

        [Required(ErrorMessage = "Телефон обязателен")]
        [RegularExpression(
        @"^\+?[0-9\s\-\(\)]{10,20}$",
        ErrorMessage = "Некорректный формат телефона (пример: +7 (916) 123-45-67)"
        )]
        public string Phone { get; set; }
        public string Fax { get; set; }

        [Required(ErrorMessage = "Email обязателен")]
        [EmailAddress(ErrorMessage = "Некорректный формат email")]
        [StringLength(100, ErrorMessage = "Email не должен превышать 100 символов")]
        public string Email { get; set; }
    }
}
