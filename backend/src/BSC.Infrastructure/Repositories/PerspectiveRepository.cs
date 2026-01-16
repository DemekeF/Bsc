using Application.Common.Interfaces;
using Application.Features.Perspectives.Common;
using System.Linq;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class PerspectiveRepository : IPerspectiveRepository
{
    private readonly AppDbContext _context;

    public PerspectiveRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<PerspectiveDto> CreateAsync(Perspective perspective, CancellationToken ct)
    {
        _context.Perspectives.Add(perspective);
        await _context.SaveChangesAsync(ct);

        return new PerspectiveDto(
            perspective.Id,
            perspective.Code,
            perspective.NameAm,
            perspective.NameEn,
            perspective.DefaultWeight,
            perspective.Objectives?.Count ?? 0);
    }

    public Task DeleteAsync(Perspective perspective, CancellationToken ct = default)
    {
        _context.Perspectives.Remove(perspective);
        return _context.SaveChangesAsync(ct);
    }

    public async Task<List<PerspectiveDto>> GetAllAsync(CancellationToken ct)
    {
        return await _context.Perspectives
            .Select(p => new PerspectiveDto(
                p.Id,
                p.Code,
                p.NameAm,
                p.NameEn,
                p.DefaultWeight,
                p.Objectives.Count()))
            .ToListAsync(ct);
    }


    public async Task<Perspective?> GetByIdAsync(int id, CancellationToken ct = default)
    {
        var entry = await _context.Perspectives.FindAsync(new object[] { id }, ct);
        return entry;
    }

    public async Task UpdateAsync(Perspective perspective, CancellationToken ct = default)
    {
        _context.Perspectives.Update(perspective);
        await _context.SaveChangesAsync(ct);
    }
}