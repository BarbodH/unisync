using UniSyncApi.Dtos.Auth;

namespace UniSyncApi.Services.Interfaces;

public interface IAccountService
{
    public Task Register(AccountRegistrationDto account);
    
    public Task<bool> Login(AccountLoginDto account);

    public Task<string> RefreshToken();
}