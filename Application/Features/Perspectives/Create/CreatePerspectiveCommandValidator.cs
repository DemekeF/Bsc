using FluentValidation;

namespace Application.Features.Perspectives.Create;

public class CreatePerspectiveCommandValidator : AbstractValidator<CreatePerspectiveCommand>
{
    public CreatePerspectiveCommandValidator()
    {
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
    }
}