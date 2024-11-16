using System.Security.Cryptography;
using UniSyncApi.Dtos.Auth;
using UniSyncApi.Repositories.Interfaces;
using UniSyncApi.Services.Interfaces;
using UniSyncApi.Utilities;

namespace UniSyncApi.Services.Implementations;

public class AccountService(AuthUtil authUtil, IAccountRepository accountRepository) : IAccountService
{
    public Task Register(AccountRegistrationDto account)
    {
        if (account.Password != account.PasswordConfirm)
        {
            throw new Exception("Passwords do not match!");
        }

        if (accountRepository.DoesEmailExist(account.Email))
        {
            throw new Exception("User with this email already exists!");
        }

        var passwordSalt = new byte[128 / 8];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetNonZeroBytes(passwordSalt);
        }

        var passwordHash = authUtil.GetPasswordHash(account.Password, passwordSalt);

        if (accountRepository.RegisterCredentials(account, passwordHash, passwordSalt) == 0)
        {
            throw new Exception("Failed to Register User!");
        }

        if (accountRepository.RegisterAccount(account) == 0)
        {
            throw new Exception("Failed to Add User");
        }

        return Task.CompletedTask;
    }

    public Task<bool> Login(AccountLoginDto account)
    {
        throw new NotImplementedException();
    }

    public Task<string> RefreshToken()
    {
        throw new NotImplementedException();
    }
}