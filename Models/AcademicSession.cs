namespace UniSyncApi.Models;

public class AcademicSession
{
    public int Id { get; set; }
    public int Season { get; set; } // 0 = Winter, 1 = Summer, 2 = Fall
    public int Year { get; set; }
}