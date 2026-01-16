using Application.Features.OrgUnit.Queries.GetAllOrgUnits;
using Application.Features.OrgUnit.Queries.GetOrgUnitById;
using Application.Features.OrgUnit.Queries.GetOrgUnitTree;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
   [ApiController]
[Route("api/orgunits")]
public class OrgUnitsController : ControllerBase
{
    private readonly ISender _mediator;

    public OrgUnitsController(ISender mediator) => _mediator = mediator;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _mediator.Send(new GetAllOrgUnitsQuery());
        return Ok(result);
    }

    [HttpGet("{objid}")]
    public async Task<IActionResult> GetById(string objid)
    {
        var result = await _mediator.Send(new GetOrgUnitByIdQuery(objid));
        return result == null ? NotFound() : Ok(result);
    }

    [HttpGet("tree/{rootObjid}")]
    public async Task<IActionResult> GetTree(string rootObjid)
    {
        var tree = await _mediator.Send(new GetOrgUnitTreeQuery(rootObjid));
        return tree == null ? NotFound() : Ok(tree);
    }
 }
}
