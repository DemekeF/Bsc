using Application.Features.Perspectives.Common;
using MediatR;


public record CreatePerspectiveCommand(string Code,string NameAm,string NameEn,int DefaultWeight) : IRequest<PerspectiveDto>;