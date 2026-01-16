using MediatR;
using Application.Features.Employee.Dtos;

namespace Application.Features.Employee.Queries.GetAllEmployees;

public record GetAllEmployeesQuery : IRequest<List<EmployeeListDto>>;