namespace Domain.Entities;

public class Perspective
{
    private Perspective() { } // For EF Core

    public int Id { get; private set; }
    public string Code { get; private set; } = string.Empty;
    public string NameAm { get; private set; } = string.Empty;
    public string NameEn { get; private set; } = string.Empty;
    public int DefaultWeight { get; private set; }

    // ADD THIS PROPERTY
    public ICollection<StrategicObjective> Objectives { get; private set; } = 
        new List<StrategicObjective>();

    public static Perspective Create(
        string code,
        string nameAm,
        string nameEn,
        int defaultWeight)
    {
        return new Perspective
        {
            Code = (code ?? string.Empty).Trim().ToUpper(),
            NameAm = nameAm?.Trim() ?? string.Empty,
            NameEn = nameEn?.Trim() ?? string.Empty,
            DefaultWeight = defaultWeight
        };
    }
}