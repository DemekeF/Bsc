// Application/Features/OrgUnit/Dtos/OrgUnitListDto.cs
public class OrgUnitListDto
{
    public string Objid       { get; set; } = string.Empty;
    public string Short       { get; set; } = string.Empty;
    public string Stext       { get; set; } = string.Empty;
    public string Parentid    { get; set; } = string.Empty;   // ← Parent ID
    public int    Level       { get; set; }                   // Hierarchy level
}