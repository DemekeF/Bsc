using MediatR;
using Application.Features.Employee.Dtos;

namespace Application.Features.Employee.Queries.GetEmployeeById;

public record GetEmployeeByIdQuery(string Pernr) : IRequest<EmployeeDetailDto?>;