using Microsoft.AspNetCore.Mvc;
using EmployeeDirectory.Api.Data;
using EmployeeDirectory.Api.Models;

namespace EmployeeDirectory.Api.Controllers;

[ApiController]
[Route("employees")]
public class EmployeesController : ControllerBase
{
    private readonly AppDbContext _context;

    public EmployeesController(AppDbContext context)
    {
        _context = context;
    }

    // ADD EMPLOYEE
    [HttpPost]
    public IActionResult AddEmployee([FromBody] Employee employee)
    {
        if (string.IsNullOrEmpty(employee.Name))
            return BadRequest("Name is required");

        _context.Employees.Add(employee);
        _context.SaveChanges();

        return Ok(employee);
    }

    // GET ALL EMPLOYEES
    [HttpGet]
    public IActionResult GetEmployees()
    {
        return Ok(_context.Employees.ToList());
    }

    // DELETE EMPLOYEE
    [HttpDelete("{id}")]
    public IActionResult DeleteEmployee(int id)
    {
        var employee = _context.Employees.Find(id);

        if (employee == null)
            return NotFound();

        _context.Employees.Remove(employee);
        _context.SaveChanges();

        return Ok();
    }
}