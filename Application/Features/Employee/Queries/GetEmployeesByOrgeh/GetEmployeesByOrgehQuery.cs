// Application/Features/Employee/Queries/GetEmployeesByOrgeh/GetEmployeesByOrgehQuery.cs
using MediatR;
using Application.Features.Employee.Dtos;

namespace Application.Features.Employee.Queries.GetEmployeesByOrgeh;

public record GetEmployeesByOrgehQuery(string Orgeh) : IRequest<List<EmployeeListDto>>;