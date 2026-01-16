namespace Application.Features.Employee.Dtos;

public class EmployeeDetailDto
{
    public string Pernr { get; set; } = string.Empty;
    public string Ename { get; set; } = string.Empty;
    public string OrgUnitObjid { get; set; } = string.Empty;
    public string PositionId { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty; // Vorna
    public string LastName { get; set; } = string.Empty;  // Nachn
}