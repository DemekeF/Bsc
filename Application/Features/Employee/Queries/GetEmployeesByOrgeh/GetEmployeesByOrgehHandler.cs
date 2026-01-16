// Application/Features/Employee/Queries/GetEmployeesByOrgeh/GetEmployeesByOrgehHandler.cs
using MediatR;
using Application.Features.Employee.Dtos;
using Application.Common.Interfaces;

namespace Application.Features.Employee.Queries.GetEmployeesByOrgeh;

public class GetEmployeesByOrgehHandler : IRequestHandler<GetEmployeesByOrgehQuery, List<EmployeeListDto>>
{
    private readonly IEmployeeRepository _repository;

    public GetEmployeesByOrgehHandler(IEmployeeRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<EmployeeListDto>> Handle(GetEmployeesByOrgehQuery request, CancellationToken ct)
    {
        return await _repository.GetEmployeesByOrgehAsync(request.Orgeh, ct);
    }
}