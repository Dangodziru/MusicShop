using Microsoft.AspNetCore.Mvc;
using MusicShop.Domain;
using MusicShop.Domain.Entities;
using MusicShop.Bussines.Features.Customers.Delete;
using MusicShop.Bussines.Features.Customers.Get;
using MusicShop.Bussines.Features.Customers.Insert;
using MusicShop.Bussines.Features.Customers.Search;
using MusicShop.Bussines.Features.Customers.Update;


[ApiController]
[Route("api/[controller]")]
public class CustomerController : ControllerBase
{
    private readonly ICustomerRepositoty customerRepository;

    public CustomerController(ICustomerRepositoty customerRepository)
    {
        this.customerRepository = customerRepository;
    }

    [HttpGet("All")]
    public async Task<IEnumerable<Customer>> GetAll()
    {
    return await customerRepository.GetAll();
    }

    [HttpGet("SearchById")]
    public async Task<IActionResult> Get([FromQuery] CustomerGetRequest request)
    {
            var customer = await customerRepository.Get(request.CustomerId);
            return customer == null
                ? NotFound($"Пакупатель {request.CustomerId} не найден")
                : Ok(customer);

    }

    [HttpGet("Search")]
    public async Task<IEnumerable<Customer>> Search([FromQuery]CustomerSearchRequestcs request)
    {
        return await customerRepository.Search(request.SearchTerm);
    }

    [HttpPost("InsertCustomer")]
    public async Task<IActionResult> InsertCustomer(CustomerInsetRequest request)
    {
        bool customerIsExist = await customerRepository.CustomerIsExist(request.Email);
        if (customerIsExist)
        {
            return BadRequest("Пакупатель с таким email или номером телефона уже существует ");
        }
        var customerId = await customerRepository.InsertCustomer(request.FirstName,request.LastName,
            request.Company,request.Address,request.City,request.State,
           request.Conutry,request.PostalCode,request.Phone,request.Fax, request.Email,  request.SupportRepId);
        if (customerId.HasValue)
        {
            return Ok(new { CustomerId = customerId });
        }
        else
        {
            return BadRequest("Не удалось создать пакупателя");
        }
    }

    [HttpDelete("DeleteCustomer")]
    public async Task<IActionResult> DeleteCustomer(CustomerDeleteRequest request)
    {
        return await customerRepository.DeleteCustomer(request.CustomerId)
            ? Ok($"Пакупатель {request.CustomerId} удален")
            : NotFound($"Пакупатель {request.CustomerId} не найден");
    }

    [HttpPost("UpdateCustomer")]
    public async Task<IActionResult> UpdateCustomer(CustomerUpdateRequest request)
    {
        return await customerRepository.UpdateCustomer(request.CustomerId, request.FirstName, request.LastName,
            request.Company, request.Address, request.City, request.State,
           request.Conutry, request.PostalCode, request.Phone, request.Fax, request.Email, request.SupportRepId)
      ? Ok($"Пакупатель {request.CustomerId} обновлен")
      : NotFound($"Пакупатель {request.CustomerId} не найден");
    }
}