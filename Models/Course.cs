namespace UniSyncApi.Models;

public class Course
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public int DepartmentId { get; set; }
    public int Number { get; set; }
    public int Credits { get; set; }
}