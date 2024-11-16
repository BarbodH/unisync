using UniSyncApi.Dtos.Auth;

namespace UniSyncApi.Repositories.Interfaces;

public interface IAccountRepository
{
    public bool DoesEmailExist(string email);

    public int RegisterCredentials(AccountRegistrationDto account, byte[] passwordHash, byte[] passwordSalt);

    public int RegisterAccount(AccountRegistrationDto account);


    public Task<bool> Login(AccountLoginDto account);

    public Task<string> RefreshToken();
}