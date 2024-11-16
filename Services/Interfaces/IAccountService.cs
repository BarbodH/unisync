using UniSyncApi.Dtos.Auth;

namespace UniSyncApi.Services.Interfaces;

public interface IAccountService
{
    public void Register(AccountRegistrationDto account);
    
    public string Login(AccountLoginDto account);

    public string RefreshToken(string? accountId);
}