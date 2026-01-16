// Application/Features/OrgUnit/Queries/GetOrgUnitTree/GetOrgUnitTreeQuery.cs
using MediatR;
using Application.Features.OrgUnit.Dtos;

namespace Application.Features.OrgUnit.Queries.GetOrgUnitTree;

public record GetOrgUnitTreeQuery(string RootObjid) : IRequest<OrgUnitTreeDto?>;