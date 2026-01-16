using FluentValidation;

public class UpdatePerspectiveCommandValidator
    : AbstractValidator<UpdatePerspectiveCommand>
{
    public UpdatePerspectiveCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0)
            .WithMessage("Perspective Id must be valid.");

        RuleFor(x => x.DefaultWeight)
            .InclusiveBetween(0, 100)
            .WithMessage("Weight must be between 0 and 100.");
    }
}
