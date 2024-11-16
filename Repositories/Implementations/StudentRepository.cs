using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using UniSyncApi.Dtos;
using UniSyncApi.Repositories.Interfaces;

namespace UniSyncApi.Repositories.Implementations;

public class StudentRepository(IConfiguration config) : IStudentRepository
{
    private readonly IDbConnection _db = new SqlConnection(config.GetConnectionString("DefaultConnection"));

    public bool IsDuplicate(string email)
    {
        return _db.QuerySingleOrDefault("SELECT * FROM Core.Student WHERE Email = @Email", new { Email = email }) != null;
    }

    public int? GetProgramId(string program)
    {
        return _db.QuerySingle<int?>("SELECT Id FROM Core.Program WHERE Name = @Name", new { Name = program });
    }

    public int Create(StudentDto student, int programId)
    {
        return _db.Execute(
            "INSERT INTO Core.Student (FirstName, LastName, Email, ProgramId) VALUES (@FirstName, @LastName, @Email, @ProgramId)",
            new { student.FirstName, student.LastName, student.Email, ProgramId = programId });
    }
}