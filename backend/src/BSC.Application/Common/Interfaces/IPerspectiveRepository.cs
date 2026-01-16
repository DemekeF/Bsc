using Application.Features.Perspectives.Common;
using Domain.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces;

public interface IPerspectiveRepository
{
    Task<PerspectiveDto> CreateAsync(Perspective perspective, CancellationToken ct = default);
    Task<List<PerspectiveDto>> GetAllAsync(CancellationToken ct = default);
    Task UpdateAsync(Perspective perspective, CancellationToken ct = default);
    Task<Perspective?> GetByIdAsync(int id, CancellationToken ct = default);
    Task DeleteAsync(Perspective perspective, CancellationToken ct = default);
}