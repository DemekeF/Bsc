// using Application.Features.Perspectives.Common;
// using Infrastructure.Data;
// using MediatR;
// using Microsoft.EntityFrameworkCore;

// namespace Application.Features.Perspectives.GetAll;

// public class GetAllPerspectivesQueryHandler 
//     : IRequestHandler<GetAllPerspectivesQuery, List<PerspectiveDto>>
// {
//     private readonly AppDbContext _context;

//     public GetAllPerspectivesQueryHandler(AppDbContext context)
//     {
//         _context = context;
//     }

//     public async Task<List<PerspectiveDto>> Handle(
//         GetAllPerspectivesQuery request,
//         CancellationToken cancellationToken)
//     {
//         var perspectives = await _context.Perspectives
//             .AsNoTracking()
//             .Select(p => new PerspectiveDto(
//                 p.Id,
//                 p.Code,
//                 p.NameAm,
//                 p.NameEn,
//                 p.DefaultWeight,
//                 p.Objectives.Count))
//             .ToListAsync(cancellationToken);

//         return perspectives;
//     }
// }