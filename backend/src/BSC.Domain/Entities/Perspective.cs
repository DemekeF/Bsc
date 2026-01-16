using Domain.Exceptions;

namespace Domain.Entities;

public class Perspective
{
  // For EF Core

    public int Id { get; private set; }
    public string Code { get; private set; } = string.Empty;
    public string NameAm { get; private set; } = string.Empty;
    public string NameEn { get; private set; } = string.Empty;
    public int DefaultWeight { get; private set; }

    // ADD THIS PROPERTY
    public ICollection<StrategicObjective> Objectives { get; private set; } = 
        new List<StrategicObjective>();
//   private Perspective() { } // For EF Core
    public static Perspective Create(string code,string nameAm,string nameEn, int defaultWeight)
    {
        return new Perspective
        {
            Code = (code ?? string.Empty).Trim().ToUpper(),
            NameAm = nameAm?.Trim() ?? string.Empty,
            NameEn = nameEn?.Trim() ?? string.Empty,
            DefaultWeight = defaultWeight
        };
    }
     public void UpdateWeight(int newWeight)
    {
        if (newWeight < 0 || newWeight > 100)
            throw new DomainException("Perspective weight must be between 0 and 100.");

        DefaultWeight = newWeight;
    }

}