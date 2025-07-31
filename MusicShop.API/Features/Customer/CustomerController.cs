using Microsoft.AspNetCore.Mvc;
using MusicShop.API;
using MusicShop.Data;
using MusicShop.Data.Dapper;
using MusicShop.Domain;
using MusicShop.Domain.Entities;
using System.Collections.Generic;
using System.Data.SQLite;
using MusicShop.API.Features.Customer.Request;


[ApiController]
[Route("api/[controller]")]
public class CustomerController : ControllerBase
{
    private readonly ICustomerRepositoty customerRepository;

    public CustomerController()
    {
        customerRepository = new CustomerDapperRepositoty();
    }

    [HttpGet("All")]
    public List<Customer> GetAll()
    {
    return customerRepository.GetAll();
    }

    [HttpGet("SearchById")]
    public IActionResult Get([FromQuery] CustomerGetRequest request)
    {
            var customer = customerRepository.Get(request.CustomerId);
            return customer == null
                ? NotFound($"Пакупатель {request.CustomerId} не найден")
                : Ok(customer);

    }

    [HttpGet("Search")]
    public List<Customer> Search([FromQuery]CustomerSearchRequestcs request)
    {
        return customerRepository.Search(request.SearchTerm);
    }

    [HttpPost("InsertCustomer")]
    public IActionResult InsertCustomer(CustomerInsetRequest request)
    {
        bool customerIsExist = customerRepository.CustomerIsExist(request.Email);
        if (customerIsExist)
        {
            return BadRequest("Пакупатель с таким email или номером телефона уже существует ");
        }
        var customerId = customerRepository.InsertCustomer(request.FirstName,request.LastName,
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
    public IActionResult DeleteCustomer(CustomerDeleteRequest request)
    {
        return customerRepository.DeleteCustomer(request.CustomerId)
            ? Ok($"Пакупатель {request.CustomerId} удален")
            : NotFound($"Пакупатель {request.CustomerId} не найден");
    }

    [HttpPost("UpdateCustomer")]
    public IActionResult UpdateCustomer(CustomerUpdateRequest request)
    {
        return customerRepository.UpdateCustomer(request.CustomerId, request.FirstName, request.LastName,
            request.Company, request.Address, request.City, request.State,
           request.Conutry, request.PostalCode, request.Phone, request.Fax, request.Email, request.SupportRepId)
      ? Ok($"Пакупатель {request.CustomerId} обновлен")
      : NotFound($"Пакупатель {request.CustomerId} не найден");
    }
}