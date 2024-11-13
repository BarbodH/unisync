namespace UniSyncApi.Models;

public class Faculty
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Abbreviation { get; set; } = string.Empty; // 2-character abbreviation
}