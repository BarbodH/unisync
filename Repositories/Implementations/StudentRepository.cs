using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;

namespace UniSyncApi.Repositories.Implementations;

public class StudentRepository(IConfiguration config)
{
    private readonly IDbConnection _db = new SqlConnection(config.GetConnectionString("DefaultConnection"));

    public DateTime TestConnection()
    {
        return _db.QuerySingle<DateTime>("SELECT GETDATE()");
    }

    public bool IsDuplicate(string email)
    {
        return _db.QuerySingleOrDefault("SELECT * FROM Core.Student WHERE Email = @Email", new { Email = email }) != null;
    }

    public int? GetProgramId(string name)
    {
        return _db.QuerySingle<int?>("SELECT Id FROM Core.Program WHERE Name = @Name", new { Name = name });
    }

    public bool CreateStudent(string firstName, string lastName, string email, int programId)
    {
        return _db.Execute(
            "INSERT INTO Core.Student (FirstName, LastName, Email, ProgramId) VALUES (@FirstName, @LastName, @Email, @ProgramId)",
            new { FirstName = firstName, LastName = lastName, Email = email, ProgramId = programId }) == 1;
    }
}