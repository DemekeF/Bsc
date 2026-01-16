// Application/Features/OrgUnit/Queries/GetOrgUnitById/GetOrgUnitByIdHandler.cs
using MediatR;
using Application.Features.OrgUnit.Dtos;
using Application.Common.Interfaces;

namespace Application.Features.OrgUnit.Queries.GetOrgUnitById;

public class GetOrgUnitByIdHandler : IRequestHandler<GetOrgUnitByIdQuery, OrgUnitDetailDto?>
{
    private readonly IOrgUnitRepository _repository;

    public GetOrgUnitByIdHandler(IOrgUnitRepository repository)
    {
        _repository = repository;
    }

    public async Task<OrgUnitDetailDto?> Handle(GetOrgUnitByIdQuery request, CancellationToken ct)
    {
        return await _repository.GetOrgUnitByObjidAsync(request.Objid, ct);
    }
}