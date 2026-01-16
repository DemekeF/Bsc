// Application/Features/OrgUnit/Queries/GetAllOrgUnits/GetAllOrgUnitsQuery.cs
using MediatR;
using Application.Features.OrgUnit.Dtos;

namespace Application.Features.OrgUnit.Queries.GetAllOrgUnits;

public record GetAllOrgUnitsQuery() : IRequest<List<OrgUnitListDto>>;