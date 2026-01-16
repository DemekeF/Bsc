// Application/Common/Interfaces/IOrgUnitRepository.cs
using Application.Features.OrgUnit.Dtos;

namespace Application.Common.Interfaces;

public interface IOrgUnitRepository
{
    Task<List<OrgUnitListDto>> GetAllOrgUnitsAsync(CancellationToken ct = default);
    Task<OrgUnitTreeDto?> GetOrgUnitTreeAsync(string rootObjid, CancellationToken ct = default);
    Task<OrgUnitDetailDto?> GetOrgUnitByObjidAsync(string objid, CancellationToken ct = default);
}