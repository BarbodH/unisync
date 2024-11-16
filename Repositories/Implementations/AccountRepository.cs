using System.Data;
using System.Security.Cryptography;
using Dapper;
using Microsoft.Data.SqlClient;
using UniSyncApi.Dtos.Auth;
using UniSyncApi.Repositories.Interfaces;
using UniSyncApi.Utilities;

namespace UniSyncApi.Repositories.Implementations;

public class AccountRepository(IConfiguration config) : IAccountRepository
{
    private readonly IDbConnection _dapper = new SqlConnection(config.GetConnectionString("DefaultConnection"));

    public bool DoesEmailExist(string email)
    {
        var command = $"SELECT Email FROM Auth.Account WHERE Email = '{email}';";
        var existingUsers = _dapper.Query<string>(command);
        return existingUsers.Any();
    }

    public int RegisterCredentials(AccountRegistrationDto account, byte[] passwordHash, byte[] passwordSalt)
    {
        string sqlCommand = @"
                INSERT INTO Auth.Credentials (
                    Email,
                    PasswordHash,
                    PasswordSalt
                ) VALUES (
                    @Email,
                    @PasswordHash,
                    @PasswordSalt
                );
            ";

        return _dapper.Execute(sqlCommand, new
        {
            Email = account.Email,
            PasswordHash = passwordHash,
            PasswordSalt = passwordSalt
        });
    }

    public int RegisterAccount(AccountRegistrationDto account)
    {
        var sqlUpdateUsers = @"
                INSERT INTO Auth.Account (
                    Role,
                    FirstName,
                    LastName,
                    Email
                ) VALUES (
                    @Role,
                    @FirstName,
                    @LastName,
                    @Email
                );
            ";

        return _dapper.Execute(sqlUpdateUsers, new
        {
            account.Role,
            account.FirstName,
            account.LastName,
            account.Email
        });
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