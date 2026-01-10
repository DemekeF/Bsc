// using Application.Features.Perspectives.Common; // DTO and Command still in Application
// using Application.Features.Perspectives.Create;
// using Domain.Entities;
// using Infrastructure.Data;
// using MediatR;

// namespace Infrastructure.Features.Perspectives.Create;

// public class CreatePerspectiveCommandHandler 
//     : IRequestHandler<CreatePerspectiveCommand, PerspectiveDto>
// {
//     private readonly AppDbContext _context;

//     public CreatePerspectiveCommandHandler(AppDbContext context)
//     {
//         _context = context;
//     }

//     public async Task<PerspectiveDto> Handle(
//         CreatePerspectiveCommand request,
//         CancellationToken cancellationToken)
//     {
//         var perspective = Perspective.Create(
//             request.Code,
//             request.NameAm,
//             request.NameEn,
//             request.DefaultWeight);

//         _context.Perspectives.Add(perspective);
//         await _context.SaveChangesAsync(cancellationToken);

//         return new PerspectiveDto(
//             perspective.Id,
//             perspective.Code,
//             perspective.NameAm,
//             perspective.NameEn,
//             perspective.DefaultWeight,
//             0);
//     }
// }