using Microsoft.AspNetCore.Mvc;
using MusicShop.API.Features.Employee.Request;
using MusicShop.Data.Dapper;
using MusicShop.Domain;
using MusicShop.Domain.Entities;
using System;
using System.Collections.Generic;

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
    public List<Employee> GetAll() => employeeRepository.GetAll();

    [HttpGet("SearchById")]
    public IActionResult Get([FromQuery] EmployeeGetRequest request)
    {
        var employee = employeeRepository.Get(request.EmployeeId);
        return employee == null
            ? NotFound($"Сотрудник {request.EmployeeId} не найден")
            : Ok(employee);
    }

    [HttpGet("Search")]
    public List<Employee> Search([FromQuery] EmployeeSearchRequest request) => employeeRepository.Search(request.SearchTerm);

    [HttpPost("InsertEmployee")]
    public IActionResult InsertEmployee(EmployeeInsertRequest request)
    {
        var newId = employeeRepository.InsertEmployee(
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
    public IActionResult DeleteEmployee(EmployeeDeleteRequest request)
    {
        return employeeRepository.DeleteEmployee(request.EmployeeId)
            ? Ok($"Сотрудник {request.EmployeeId} удален")
            : NotFound($"Сотрудник {request.EmployeeId} не найден");
    }

    [HttpPost("UpdateEmployee")]
    public IActionResult UpdateEmployee(EmployeeUpdateRequest request)
    {
        var updated = employeeRepository.UpdateEmployee(
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