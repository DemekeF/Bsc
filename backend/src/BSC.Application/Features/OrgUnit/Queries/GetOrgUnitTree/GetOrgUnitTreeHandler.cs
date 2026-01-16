// Application/Features/OrgUnit/Queries/GetOrgUnitTree/GetOrgUnitTreeHandler.cs
using MediatR;
using Application.Features.OrgUnit.Dtos;
using Application.Common.Interfaces;

namespace Application.Features.OrgUnit.Queries.GetOrgUnitTree;

public class GetOrgUnitTreeHandler : IRequestHandler<GetOrgUnitTreeQuery, OrgUnitTreeDto?>
{
    private readonly IOrgUnitRepository _repository;

    public GetOrgUnitTreeHandler(IOrgUnitRepository repository)
    {
        _repository = repository;
    }

    public async Task<OrgUnitTreeDto?> Handle(GetOrgUnitTreeQuery request, CancellationToken ct)
    {
        return await _repository.GetOrgUnitTreeAsync(request.RootObjid, ct);
    }
}