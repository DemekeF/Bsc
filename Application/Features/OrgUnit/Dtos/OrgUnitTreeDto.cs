// Application/Features/OrgUnit/Dtos/OrgUnitTreeDto.cs  (for full tree)
using Application.Features.Employee.Dtos;

public class OrgUnitTreeDto
{
    public string Objid       { get; set; } = string.Empty;
    public string Short       { get; set; } = string.Empty;
    public string Stext       { get; set; } = string.Empty;
    public int    Level       { get; set; }
    public List<OrgUnitTreeDto> Children { get; set; } = new();
    public List<EmployeeListDto> Employees { get; set; } = new();
}