using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicShop.Bussines.Features.Customers.Insert
{
    public class InsertRequest
    {

        [Range(1, long.MaxValue, ErrorMessage = "Введено неверное значение")]
        public string FirstName { get; set; }

        [Range(1, long.MaxValue, ErrorMessage = "Введено неверное значение")]
        public string LastName { get; set; }

        [Range(1, long.MaxValue, ErrorMessage = "Введено неверное значение")]
        public string Company { get; set; }

        [Range(1, long.MaxValue, ErrorMessage = "Введено неверное значение")]
        public string Address { get; set; }

        [Range(1, long.MaxValue, ErrorMessage = "Введено неверное значение")]
        public string City { get; set; }

        [Range(1, long.MaxValue, ErrorMessage = "Введено неверное значение")]
        public string State { get; set; }

        [Range(1, long.MaxValue, ErrorMessage = "Введено неверное значение")]
        public string Country { get; set; }

        [Range(1, long.MaxValue, ErrorMessage = "Введено неверное значение")]
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
        public long? SupportRepId { get; set; }
    }
}
