using System.Data;
using System.Security.Cryptography;
using Dapper;
using Microsoft.Data.SqlClient;
using UniSyncApi.Dtos.Auth;
using UniSyncApi.Repositories.Interfaces;
using UniSyncApi.Utilities;

namespace UniSyncApi.Repositories.Implementations;

public class AccountRepository(IConfiguration config, AuthUtil authUtil) : IAccountRepository
{
    private readonly IDbConnection _dapper = new SqlConnection(config.GetConnectionString("DefaultConnection"));

    public Task Register(AccountRegistrationDto account)
    {
        // Ensure that password and password confirmation match
        if (account.Password != account.PasswordConfirm) throw new Exception("Passwords do not match!");

        // Ensure that the email does not already exist in the database
        string command = $"SELECT Email FROM Auth.Account WHERE Email = '{account.Email}';";
        IEnumerable<string> existingUsers = _dapper.Query<string>(command);
        if (existingUsers.Count() > 0) throw new Exception("User with this email already exists!");

        // Register the new user and hash their password
        byte[] passwordSalt = new byte[128 / 8];
        using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
        {
            rng.GetNonZeroBytes(passwordSalt);
        }

        byte[] passwordHash = authUtil.GetPasswordHash(account.Password, passwordSalt);

        // Generate the SQL command required for registering the user
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

        var rowsAffected = _dapper.Execute(sqlCommand, new
        {
            Email = account.Email,
            PasswordHash = passwordHash,
            PasswordSalt = passwordSalt
        });

        if (rowsAffected == 0) throw new Exception("Failed to Register User!");

        // Add the newly registered user to the users table
        // By default, the Active properly is set to 1 (true)
        string sqlUpdateUsers = $@"
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
        
        var rowsAffected2 = _dapper.Execute(sqlUpdateUsers, new
        {
            Role = 0,
            account.FirstName,
            account.LastName,
            account.Email
        });
        
        if (rowsAffected2 == 0) throw new Exception("Failed to Add User");
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