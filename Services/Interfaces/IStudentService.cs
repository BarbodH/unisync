using UniSyncApi.Dtos;

namespace UniSyncApi.Services.Interfaces;

public interface IStudentService
{
    public Task Create(StudentDto student);
}