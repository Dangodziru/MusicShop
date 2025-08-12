using System.ComponentModel.DataAnnotations;

namespace MusicShop.Bussines.Features.Customers.Update
{
    public class CustomerUpdateRequest
    {
        [Range(1, long.MaxValue, ErrorMessage = "Введено неверное значение")]
        public long CustomerId { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Длинна должнабыть больше 3х семиволов и меньше 100")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Длинна должнабыть больше 3х семиволов и меньше 100")]
        public string LastName { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Длинна должнабыть больше 3х семиволов и меньше 100")]
        public string Company { get; set; }
        public string Address { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Длинна должнабыть больше 3х семиволов и меньше 100")]
        public string City { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Длинна должнабыть больше 3х семиволов и меньше 100")]
        public string State { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Длинна должнабыть больше 3х семиволов и меньше 100")]
        public string Conutry { get; set; }
        public string PostalCode { get; set; }

        [Required(ErrorMessage = "Телефон обязателен")]
        [RegularExpression(
        @"^\+?[0-9\s\-\(\)]{10,20}$",
        ErrorMessage = "Некорректный формат телефона (пример: +7 (916) 123-45-67)"
        )]
        public string Phone { get; set; }
        public string Fax { get; set; }
        public long SupportRepId { get; set; }

        [Required(ErrorMessage = "Email обязателен")]
        [EmailAddress(ErrorMessage = "Некорректный формат email")]
        [StringLength(100, ErrorMessage = "Email не должен превышать 100 символов")]
        public string Email { get; set; }
    }
}
