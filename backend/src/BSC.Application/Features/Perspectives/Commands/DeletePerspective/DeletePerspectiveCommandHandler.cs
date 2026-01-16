using System;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.DeletePerspective;

public class DeletePerspectiveCommandHandler: IRequestHandler<DeletePerspectiveCommand> 
{
        private readonly IPerspectiveRepository _repository;
        public DeletePerspectiveCommandHandler(IPerspectiveRepository repository)
        {
            _repository = repository;
        }
        public async Task<Unit> Handle(DeletePerspectiveCommand request, CancellationToken cancellationToken)
        {
            var perspective = await _repository.GetByIdAsync(request.Id);

            if (perspective is null)
                throw new NotFoundException(nameof(Perspective), request.Id);

            await  _repository.DeleteAsync(perspective);

            return Unit.Value;
        }

    Task IRequestHandler<DeletePerspectiveCommand>.Handle(DeletePerspectiveCommand request, CancellationToken cancellationToken)
    {
        return Handle(request, cancellationToken);
    }
}
