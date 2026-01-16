// Application/Features/OrgUnit/Queries/GetOrgUnitById/GetOrgUnitByIdQuery.cs
using MediatR;
using Application.Features.OrgUnit.Dtos;

namespace Application.Features.OrgUnit.Queries.GetOrgUnitById;

public record GetOrgUnitByIdQuery(string Objid) : IRequest<OrgUnitDetailDto?>;