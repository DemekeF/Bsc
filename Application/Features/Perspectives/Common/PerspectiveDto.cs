namespace Application.Features.Perspectives.Common;

public record PerspectiveDto(
    int Id,
    string Code,
    string NameAm,
    string NameEn,
    int DefaultWeight,
    int ObjectivesCount // Optional: useful for display
);