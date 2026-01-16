// Application/Features/OrgUnit/Dtos/OrgUnitDetailDto.cs
namespace Application.Features.OrgUnit.Dtos;

public class OrgUnitDetailDto : OrgUnitListDto
{
    public DateTime? ValidFrom { get; set; }
    public DateTime? ValidTo   { get; set; }
    // Add more if needed: CostCenter, ManagerPosition, etc.
}