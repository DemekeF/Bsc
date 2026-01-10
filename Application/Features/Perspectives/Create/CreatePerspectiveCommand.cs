using MediatR;
using Application.Features.Perspectives.Common;

namespace Application.Features.Perspectives.Create;

public record CreatePerspectiveCommand(
    string Code,
    string NameAm,
    string NameEn,
    int DefaultWeight) : IRequest<PerspectiveDto>;