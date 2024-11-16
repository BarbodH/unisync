using System.Security.Cryptography;
using UniSyncApi.Dtos.Auth;
using UniSyncApi.Exceptions;
using UniSyncApi.Repositories.Interfaces;
using UniSyncApi.Services.Interfaces;
using UniSyncApi.Utilities;

namespace UniSyncApi.Services.Implementations;

public class AccountService(AuthUtil authUtil, IAccountRepository accountRepository) : IAccountService
{
    public void Register(AccountRegistrationDto account)
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
    }

    public string Login(AccountLoginDto account)
    {
        var loginConfirmation = accountRepository.GetCredentials(account.Email);
        if (loginConfirmation == null) throw new AuthenticationException("email");

        var passwordHash = authUtil.GetPasswordHash(account.Password, loginConfirmation.PasswordSalt);
        if (passwordHash.Where((t, index) => t != loginConfirmation.PasswordHash[index]).Any())
            throw new AuthenticationException("password");

        return authUtil.CreateToken(accountRepository.GetId(account.Email));
    }

    public string RefreshToken(string? accountId)
    {
        if (accountId == null) throw new NotAuthenticatedException();
        
        return authUtil.CreateToken(accountRepository.VerifyId(accountId) ??
                                    throw new AccountNoLongerExistsException(accountId));
    }
}