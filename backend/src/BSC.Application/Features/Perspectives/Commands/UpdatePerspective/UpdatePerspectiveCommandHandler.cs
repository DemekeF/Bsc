using MediatR;
using Application.Common.Exceptions;

using Domain.Entities;
using Application.Common.Interfaces;

public class UpdatePerspectiveCommandHandler
    : IRequestHandler<UpdatePerspectiveCommand>
{
    private readonly IPerspectiveRepository _repository;

    public UpdatePerspectiveCommandHandler(IPerspectiveRepository repository)
    {
        _repository = repository;
    }

    public async Task<Unit> Handle(
        UpdatePerspectiveCommand request,
        CancellationToken cancellationToken)
    {
        var perspective = await _repository.GetByIdAsync(request.Id);

        if (perspective is null)
            throw new NotFoundException(nameof(Perspective), request.Id);

        perspective.UpdateWeight(request.DefaultWeight);

        await _repository.UpdateAsync(perspective);

        return Unit.Value;
    }

    Task IRequestHandler<UpdatePerspectiveCommand>.Handle(UpdatePerspectiveCommand request, CancellationToken cancellationToken)
    {
        return Handle(request, cancellationToken);
    }
}
