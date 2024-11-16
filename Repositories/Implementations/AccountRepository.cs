using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using UniSyncApi.Dtos.Auth;
using UniSyncApi.Models;
using UniSyncApi.Repositories.Interfaces;

namespace UniSyncApi.Repositories.Implementations;

public class AccountRepository(IConfiguration config) : IAccountRepository
{
    private readonly IDbConnection _dapper = new SqlConnection(config.GetConnectionString("DefaultConnection"));

    public bool DoesEmailExist(string email)
    {
        var sql = $"SELECT Email FROM Auth.Account WHERE Email = '{email}';";
        var existingUsers = _dapper.Query<string>(sql);
        return existingUsers.Any();
    }

    public int RegisterCredentials(AccountRegistrationDto account, byte[] passwordHash, byte[] passwordSalt)
    {
        string sql = @"
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

        return _dapper.Execute(sql, new
        {
            Email = account.Email,
            PasswordHash = passwordHash,
            PasswordSalt = passwordSalt
        });
    }

    public int RegisterAccount(AccountRegistrationDto account)
    {
        var sql = @"
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

        return _dapper.Execute(sql, new
        {
            account.Role,
            account.FirstName,
            account.LastName,
            account.Email
        });
    }

    public LoginConfirmation? GetCredentials(string email)
    {
        var sql = @"
            SELECT
                PasswordHash,
                PasswordSalt
            FROM Auth.Credentials
            WHERE Email = @Email
        ";

        return _dapper.QuerySingleOrDefault<LoginConfirmation>(sql, new { Email = email });
    }

    public int GetId(string email)
    {
        var sql = @"
            SELECT Id
            FROM Auth.Account
            WHERE Email = @Email
        ";

        return _dapper.QuerySingle<int>(sql, new { Email = email });
    }

    public int? VerifyId(string id)
    {
        var sql = @"
            SELECT Id
            FROM Auth.Account
            WHERE Id = @Id
        ";
        
        return _dapper.QuerySingleOrDefault<int?>(sql, new { Id = id });
    }
}