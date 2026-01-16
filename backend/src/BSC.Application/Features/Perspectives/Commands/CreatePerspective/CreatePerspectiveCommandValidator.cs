
using Application.Interfaces;
using FluentValidation;

namespace Application.Features.Perspectives.Create
{
    public class CreatePerspectiveCommandValidator : AbstractValidator<CreatePerspectiveCommand>
    {
        private readonly IPerspectiveRepository _repository;

        public CreatePerspectiveCommandValidator(IPerspectiveRepository repository)
        {
            _repository = repository;

            RuleFor(x => x.Code)
                .NotEmpty().WithMessage("Code is required.")
                .Length(1).WithMessage("Code must be exactly one character.")
                .Must(c => "FCIL".Contains(c.ToUpper()))
                .WithMessage("Code must be one of: F, C, I, or L.");

            RuleFor(x => x.NameEn)
                .NotEmpty().WithMessage("English name is required.")
                .MaximumLength(100);

            RuleFor(x => x.NameAm)
                .MaximumLength(100);

            RuleFor(x => x.DefaultWeight)
                .InclusiveBetween(0, 100)
                .WithMessage("Default weight must be between 0 and 100.");

            // ✅ Business rule: total weight ≤ 100
            RuleFor(x => x)
                .MustAsync(async (command, cancellationToken) =>
                {
                    var perspectives = await _repository.GetAllAsync(cancellationToken);
                    var total = perspectives.Sum(p => p.DefaultWeight) + command.DefaultWeight;
                    return total <= 100;
                })
                .WithMessage("The total default weight of all perspectives cannot exceed 100%.");
        }
    }
}
