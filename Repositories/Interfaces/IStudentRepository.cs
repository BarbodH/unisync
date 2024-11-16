using UniSyncApi.Dtos;

namespace UniSyncApi.Repositories.Interfaces;

public interface IStudentRepository
{
    public int Create(StudentDto studentDto, int programId);

    public bool IsDuplicate(string email);
    
    public int? GetProgramId(string program);
}