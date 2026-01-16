using Application.Features.Perspectives.Common;
using Domain.Entities;

namespace Application.Interfaces;

public interface IPerspectiveRepository
{
    Task<PerspectiveDto> CreateAsync(Domain.Entities.Perspective perspective, CancellationToken ct);
    Task<List<PerspectiveDto>> GetAllAsync(CancellationToken ct);
    Task UpdateAsync(Perspective perspective);
    Task<Perspective?> GetByIdAsync(int id);
    Task DeleteAsync(Perspective perspective);
    Task<Perspective> GetByIdAsync(object id);
}