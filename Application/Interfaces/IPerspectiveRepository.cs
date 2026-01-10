using Application.Features.Perspectives.Common;

namespace Application.Interfaces;

public interface IPerspectiveRepository
{
    Task<PerspectiveDto> CreateAsync(Domain.Entities.Perspective perspective, CancellationToken ct);
    Task<List<PerspectiveDto>> GetAllAsync(CancellationToken ct);
}