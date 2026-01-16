namespace Domain.Entities;

public class StrategicObjective
{
    private StrategicObjective() { } // For EF Core

    public int Id { get; private set; }
    public string Code { get; private set; } = string.Empty;
    public string NameAm { get; private set; } = string.Empty;
    public string NameEn { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;

    public int PerspectiveId { get; private set; }
    public Perspective Perspective { get; private set; } = null!;
}