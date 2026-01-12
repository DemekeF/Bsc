using MediatR;

public record UpdatePerspectiveCommand(
    int Id,
    int DefaultWeight
) : IRequest;
