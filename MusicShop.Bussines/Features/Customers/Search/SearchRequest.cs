using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicShop.Bussines.Features.Customers.Search
{
    public class SearchRequest
    {
        [Range(1, long.MaxValue, ErrorMessage = "Введено неверное значение")]
        public string SearchTerm { get; set; }
    }
}
