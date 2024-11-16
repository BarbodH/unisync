using UniSyncApi.Dtos.Auth;
using UniSyncApi.Models;

namespace UniSyncApi.Repositories.Interfaces;

public interface IAccountRepository
{
    public bool DoesEmailExist(string email);

    public int RegisterCredentials(AccountRegistrationDto account, byte[] passwordHash, byte[] passwordSalt);

    public int RegisterAccount(AccountRegistrationDto account);

    public LoginConfirmation? GetCredentials(string email);

    public int GetId(string email);

    public int? VerifyId(string id);
}