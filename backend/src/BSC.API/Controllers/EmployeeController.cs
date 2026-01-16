using MediatR;
using Microsoft.AspNetCore.Mvc;
using Application.Features.Employee.Queries.GetAllEmployees;
using Application.Features.Employee.Queries.GetEmployeeById;
using Application.Features.Employee.Queries.GetEmployeesByOrgeh;

namespace API.Controllers
{
    [ApiController]
[Route("api/employees")]
public class EmployeesController : ControllerBase
{
    private readonly ISender _mediator;

    public EmployeesController(ISender mediator) => _mediator = mediator;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var employees = await _mediator.Send(new GetAllEmployeesQuery());
        return Ok(employees);
    }

    [HttpGet("{pernr}")]
    public async Task<IActionResult> GetById(string pernr)
    {
        var employee = await _mediator.Send(new GetEmployeeByIdQuery(pernr));
        return employee == null ? NotFound() : Ok(employee);
    }
    [HttpGet("by-orgunit/{orgeh}")]
    public async Task<IActionResult> GetByOrgUnit(string orgeh)
    {
        var employees = await _mediator.Send(new GetEmployeesByOrgehQuery(orgeh));
        return Ok(employees);
    }
}
}