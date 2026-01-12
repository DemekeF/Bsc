using Application.Features.Perspectives.Common;
using MediatR;

namespace Application.Features.Perspectives.GetAll;

public record GetAllPerspectivesQuery : IRequest<List<PerspectiveDto>>;