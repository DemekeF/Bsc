using MediatR;

using Domain.Entities;
using Application.Features.Perspectives.Common;
using Application.Common.Interfaces;

namespace Application.Features.Perspectives.Create;

public class CreatePerspectiveHandler : IRequestHandler<CreatePerspectiveCommand, PerspectiveDto>
{
    private readonly IPerspectiveRepository _repository;

    public CreatePerspectiveHandler(IPerspectiveRepository repository)
    {
        _repository = repository;
    }

    public async Task<PerspectiveDto> Handle(CreatePerspectiveCommand request, CancellationToken ct)
    {
        var perspective = Perspective.Create(
            request.Code,
            request.NameAm,
            request.NameEn,
            request.DefaultWeight);

        return await _repository.CreateAsync(perspective, ct);
    }
}