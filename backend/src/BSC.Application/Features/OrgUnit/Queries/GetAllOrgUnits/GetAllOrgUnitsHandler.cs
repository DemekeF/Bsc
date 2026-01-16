// Application/Features/OrgUnit/Queries/GetAllOrgUnits/GetAllOrgUnitsHandler.cs
using MediatR;
using Application.Features.OrgUnit.Dtos;
using Application.Common.Interfaces;

namespace Application.Features.OrgUnit.Queries.GetAllOrgUnits;

public class GetAllOrgUnitsHandler : IRequestHandler<GetAllOrgUnitsQuery, List<OrgUnitListDto>>
{
    private readonly IOrgUnitRepository _repository;

    public GetAllOrgUnitsHandler(IOrgUnitRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<OrgUnitListDto>> Handle(GetAllOrgUnitsQuery request, CancellationToken ct)
    {
        return await _repository.GetAllOrgUnitsAsync(ct);
    }
}