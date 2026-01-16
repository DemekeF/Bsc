using MediatR;
using Application.Features.Employee.Dtos;
using Application.Features.Employee.Queries.GetEmployeeById;
using Application.Common.Interfaces;

namespace Application.Features.Employee.Queries.GetEmployeeById;

public class GetEmployeeByIdHandler : IRequestHandler<GetEmployeeByIdQuery, EmployeeDetailDto?>
{
    private readonly IEmployeeRepository _repository;

    public GetEmployeeByIdHandler(IEmployeeRepository repository)
    {
        _repository = repository;
    }

    public async Task<EmployeeDetailDto?> Handle(GetEmployeeByIdQuery request, CancellationToken ct)
    {
        return await _repository.GetEmployeeByPernrAsync(request.Pernr, ct);
    }
}