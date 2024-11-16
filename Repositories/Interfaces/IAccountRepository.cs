using UniSyncApi.Dtos.Auth;

namespace UniSyncApi.Repositories.Interfaces;

public interface IAccountRepository
{
    public Task Register(AccountRegistrationDto account);
    
    public Task<bool> Login(AccountLoginDto account);

    public Task<string> RefreshToken();
}