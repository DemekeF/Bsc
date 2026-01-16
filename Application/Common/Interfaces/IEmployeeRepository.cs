using Application.Features.Employee.Dtos;

namespace Application.Common.Interfaces;

public interface IEmployeeRepository
{
Task<List<EmployeeListDto>> GetAllEmployeesAsync(CancellationToken ct = default);
Task<EmployeeDetailDto?> GetEmployeeByPernrAsync(string pernr,  CancellationToken ct = default);
Task<List<EmployeeListDto>> GetEmployeesByOrgehAsync(string orgeh, CancellationToken ct = default);

}