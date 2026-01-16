namespace Application.Features.Employee.Dtos;

public class EmployeeListDto
{
    public string Pernr { get; set; } = string.Empty;
    public string Ename { get; set; } = string.Empty;
    public string Orgeh { get; set; } = string.Empty;  // ← must have this
    public string PositionId { get; set; } = string.Empty;
    // ... add other relevant fields if necessary
}