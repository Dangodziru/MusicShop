using Microsoft.AspNetCore.Mvc;
using MusicShop.Bussines.Features.Employes.Delete;
using MusicShop.Bussines.Features.Employes.Get;
using MusicShop.Bussines.Features.Employes.Insert;
using MusicShop.Bussines.Features.Employes.Search;
using MusicShop.Bussines.Features.Employes.Update;
using MusicShop.Data.Dapper;
using MusicShop.Domain;
using MusicShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class EmployeeController : ControllerBase
{
    private readonly IEmployeeRepository employeeRepository;

    public EmployeeController(IEmployeeRepository employeeRepository)
    {
        this.employeeRepository = employeeRepository;
    }

    [HttpGet("All")]
    public async Task<IEnumerable<Employee>> GetAll() => await employeeRepository.GetAll();

    [HttpGet("SearchById")]
    public async Task<IActionResult> Get([FromQuery] EmployeeGetRequest request)
    {
        var employee = await employeeRepository.Get(request.EmployeeId);
        return employee == null
            ? NotFound($"Сотрудник {request.EmployeeId} не найден")
            : Ok(employee);
    }

    [HttpGet("Search")]
    public async Task<IEnumerable<Employee>> Search([FromQuery] EmployeeSearchRequest request) => await employeeRepository.Search(request.SearchTerm);

    [HttpPost("InsertEmployee")]
    public async Task<IActionResult> InsertEmployee(EmployeeInsertRequest request)
    {
        var newId = await employeeRepository.InsertEmployee(
            request.LastName,
            request.FirstName,
            request.Title,
            request.ReportsTo,
            request.BirthDate,
            request.HireDate,
            request.Address,
            request.City,
            request.State,
            request.Country,
            request.PostalCode,
            request.Phone,
            request.Fax,
            request.Email
        );

        if (newId.HasValue)
        {
            return Ok(new { EmployeeId = newId });
        }
        else
        {
            return BadRequest("Не удалось создать сотрудника");
        }
    }

    [HttpDelete("DeleteEmployee")]
    public async Task<IActionResult> DeleteEmployee(EmployeeDeleteRequest request)
    {
        return await employeeRepository.DeleteEmployee(request.EmployeeId)
            ? Ok($"Сотрудник {request.EmployeeId} удален")
            : NotFound($"Сотрудник {request.EmployeeId} не найден");
    }

    [HttpPost("UpdateEmployee")]
    public async Task<IActionResult> UpdateEmployee(EmployeeUpdateRequest request)
    {
        var updated = await employeeRepository.UpdateEmployee(
            request.EmployeeId,
            request.LastName,
            request.FirstName,
            request.Title,
            request.ReportsTo,
            request.BirthDate,
            request.HireDate,
            request.Address,
            request.City,
            request.State,
            request.Country,
            request.PostalCode,
            request.Phone,
            request.Fax,
            request.Email
        );

        return updated
            ? Ok($"Сотрудник {request.EmployeeId} обновлен")
            : NotFound($"Сотрудник {request.EmployeeId} не найден");
    }
}