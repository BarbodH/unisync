namespace UniSyncApi.Models;

public class Department
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Abbreviation { get; set; } = string.Empty; // 4-character abbreviation
    public int FacultyId { get; set; }
}