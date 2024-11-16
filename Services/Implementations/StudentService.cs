using UniSyncApi.Dtos;
using UniSyncApi.Repositories.Interfaces;
using UniSyncApi.Services.Interfaces;

namespace UniSyncApi.Services.Implementations;

public class StudentService(IStudentRepository studentRepository) : IStudentService
{
    public Task Create(StudentDto student)
    {
        if (studentRepository.IsDuplicate(student.Email))
        {
            throw new Exception("Duplicate email.");
        }

        var programId = studentRepository.GetProgramId(student.Program);
        if (programId == null)
        {
            throw new Exception("Program not found.");
        }

        if (studentRepository.Create(student, programId.Value) == 0)
        {
            throw new Exception("Could not create student.");
        }
        
        return Task.CompletedTask;
    }
}