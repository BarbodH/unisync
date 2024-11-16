using System.Security.Cryptography;
using UniSyncApi.Dtos.Auth;
using UniSyncApi.Exceptions;
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
            throw new InvalidFieldException("user", "password confirm");
        }

        if (accountRepository.DoesEmailExist(account.Email))
        {
            throw new DuplicateResourceException("user", "email");
        }

        var passwordSalt = new byte[128 / 8];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetNonZeroBytes(passwordSalt);
        }

        var passwordHash = authUtil.GetPasswordHash(account.Password, passwordSalt);

        if (accountRepository.RegisterCredentials(account, passwordHash, passwordSalt) == 0)
        {
            throw new ResourceCreationException("credentials");
        }

        if (accountRepository.RegisterAccount(account) == 0)
        {
            throw new ResourceCreationException("account");
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