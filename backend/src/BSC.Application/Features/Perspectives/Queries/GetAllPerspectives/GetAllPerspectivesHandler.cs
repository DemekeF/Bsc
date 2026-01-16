using MediatR;
using Application.Features.Perspectives.Common;
using Application.Common.Interfaces;

namespace Application.Features.Perspectives.GetAll;

public class GetAllPerspectivesHandler : IRequestHandler<GetAllPerspectivesQuery, List<PerspectiveDto>>
{
    private readonly IPerspectiveRepository _repository;

    public GetAllPerspectivesHandler(IPerspectiveRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<PerspectiveDto>> Handle(GetAllPerspectivesQuery request, CancellationToken ct)
    {
        return await _repository.GetAllAsync(ct);
    }
}