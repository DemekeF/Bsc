using MediatR;
using Microsoft.AspNetCore.Mvc;
using Application.Features.Perspectives.GetAll;
using Application.Features.Perspectives.Create;
using Application.Features.Perspectives.Common;
using Application.DeletePerspective;


[ApiController]                          // Tells ASP.NET: "This is an API controller"
[Route("api/perspectives")]              // All endpoints start with /api/perspectives
public class PerspectivesController : ControllerBase
{
    private readonly IMediator _mediator;    // MediatR is injected automatically

    // Constructor - DI gives us MediatR instance
    public PerspectivesController(IMediator mediator)
        => _mediator = mediator;

    // GET /api/perspectives
    [HttpGet]
    public async Task<ActionResult<List<PerspectiveDto>>> GetAll()
    {
        // Step 1: Create a query object (message)
        var query = new GetAllPerspectivesQuery();

        // Step 2: Send it to MediatR
        // MediatR automatically finds the correct handler in Application layer
        var result = await _mediator.Send(query);

        // Step 3: Return HTTP 200 OK with the data as JSON
        return Ok(result);
    }

    // POST /api/perspectives
    [HttpPost]
    public async Task<ActionResult<PerspectiveDto>> Create([FromBody] CreatePerspectiveCommand command)
    {
        // Step 1: The command already came from the request body (JSON)
        // Step 2: Send it to MediatR → finds CreatePerspectiveHandler
        var dto = await _mediator.Send(command);

        // Step 3: Return HTTP 201 Created with Location header + the new DTO
        // nameof(GetAll) creates link to /api/perspectives
        return CreatedAtAction(nameof(GetAll), new { id = dto.Id }, dto);
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdatePerspectiveCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest("ID in URL does not match ID in body.");
        }

        await _mediator.Send(command);

        return NoContent();
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var command = new DeletePerspectiveCommand(id);
        await _mediator.Send(command);
        return NoContent();
    }



}