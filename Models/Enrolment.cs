namespace UniSyncApi.Models;

public class Enrolment
{
    public int Id { get; set; }
    public int StudentId { get; set; }
    public int CourseId { get; set; }
    public int SessionId { get; set; }
    public int InstructorId { get; set; }
}