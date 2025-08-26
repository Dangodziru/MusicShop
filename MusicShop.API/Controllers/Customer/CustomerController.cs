using Microsoft.AspNetCore.Mvc;
using MusicShop.Bussines.Features.Customers.Delete;
using MusicShop.Bussines.Features.Customers.Get;
using MusicShop.Bussines.Features.Customers.Insert;
using MusicShop.Bussines.Features.Customers.Search;
using MusicShop.Bussines.Features.Customers.Services;
using MusicShop.Bussines.Features.Customers.Update;
using MusicShop.Domain.Entities;

namespace MusicShop.API.Features.Customers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService customerService;

        public CustomerController(ICustomerService customerService)
        {
            this.customerService = customerService;
        }

        [HttpGet("All")]
        public async Task<IEnumerable<Customer>> GetAll()
        {
            return await customerService.GetAll();
        }

        [HttpGet("SearchById")]
        public async Task<IActionResult> Get([FromQuery] long customerId)
        {
            var customer = await customerService.Get(new GetRequest { CustomerId = customerId });
            return customer == null
                ? NotFound($"Клиент {customerId} не найден")
                : Ok(customer);
        }

        [HttpGet("Search")]
        public async Task<IEnumerable<Customer>> Search([FromQuery] string searchTerm)
        {
            return await customerService.Search(new SearchRequest { SearchTerm = searchTerm });
        }

        [HttpPost("InsertCustomer")]
        public async Task<IActionResult> InsertCustomer([FromBody] InsertRequest request)
        {
            var customerId = await customerService.Insert(request);

            return customerId.HasValue
                ? Ok(new { Message = "Клиент успешно создан", CustomerId = customerId.Value })
                : BadRequest("Не удалось создать клиента (возможно, email уже существует)");
        }

        [HttpPut("UpdateCustomer")]
        public async Task<IActionResult> UpdateCustomer([FromBody] UpdateRequest request)
        {
            var success = await customerService.Update(request);

            return success
                ? Ok($"Клиент {request.CustomerId} успешно обновлен")
                : NotFound($"Клиент {request.CustomerId} не найден или email уже используется");
        }

        [HttpDelete("DeleteCustomer")]
        public async Task<IActionResult> DeleteCustomer([FromQuery] long customerId)
        {
            var success = await customerService.Delete(new DeleteRequest { CustomerId = customerId });

            return success
                ? Ok($"Клиент {customerId} успешно удален")
                : NotFound($"Клиент {customerId} не найден");
        }
    }
}