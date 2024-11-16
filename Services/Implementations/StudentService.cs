using UniSyncApi.Dtos;
using UniSyncApi.Exceptions;
using UniSyncApi.Repositories.Interfaces;
using UniSyncApi.Services.Interfaces;

namespace UniSyncApi.Services.Implementations;

public class StudentService(IStudentRepository studentRepository) : IStudentService
{
    public Task Create(StudentDto student)
    {
        if (studentRepository.IsDuplicate(student.Email))
        {
            throw new DuplicateResourceException("student", "email");
        }

        var programId = studentRepository.GetProgramId(student.Program);
        if (programId == null)
        {
            throw new DuplicateResourceException("program", "id");
        }

        if (studentRepository.Create(student, programId.Value) == 0)
        {
            throw new ResourceCreationException("student");
        }
        
        return Task.CompletedTask;
    }
}