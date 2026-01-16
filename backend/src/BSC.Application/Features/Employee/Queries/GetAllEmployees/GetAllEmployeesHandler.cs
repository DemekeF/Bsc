using MediatR;
using Application.Features.Employee.Dtos;
using Application.Features.Employee.Queries.GetAllEmployees;
using Application.Common.Interfaces;

namespace Application.Features.Employee.Queries.GetAllEmployees;

public class GetAllEmployeesHandler : IRequestHandler<GetAllEmployeesQuery, List<EmployeeListDto>>
{
    private readonly IEmployeeRepository _repository;

    public GetAllEmployeesHandler(IEmployeeRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<EmployeeListDto>> Handle(GetAllEmployeesQuery request, CancellationToken ct)
    {
        return await _repository.GetAllEmployeesAsync(ct);
    }
}