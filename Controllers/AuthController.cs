using System.Data;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using UniSyncApi.Dtos.Auth;
using UniSyncApi.Repositories.Interfaces;
using UniSyncApi.Utilities;

namespace UniSyncApi.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class AuthController(IAccountRepository accountRepository) : ControllerBase
{
    [AllowAnonymous]
    [HttpPost("Register")]
    public IActionResult Register(AccountRegistrationDto account)
    {
        accountRepository.Register(account);
        return Ok();
    }

    // [AllowAnonymous]
    // [HttpPost("Login")]
    // public IActionResult Login(UserLoginDto user)
    // {
    //     // Verify if the email address is valid, i.e., user exists
    //     string sqlCommand = $@"
    //             SELECT
    //                 PasswordHash,
    //                 PasswordSalt
    //             FROM TutorialAppSchema.Auth
    //             WHERE Email = '{user.Email}'
    //         ";
    //
    //     // In the case where no element is returned (invalid email), SQL will throw an exception
    //     UserLoginConfirmationDto userLoginConfirmation = _dapper
    //         .LoadDataSingle<UserLoginConfirmationDto>(sqlCommand);
    //     if (userLoginConfirmation == null) throw new Exception("Invalid Email!");
    //
    //     // Verify the password
    //     byte[] passwordHash = _authUtilities.GetPasswordHash(user.Password, userLoginConfirmation.PasswordSalt);
    //     for (int index = 0; index < passwordHash.Length; index++)
    //     {
    //         if (passwordHash[index] != userLoginConfirmation.PasswordHash[index])
    //             return StatusCode(401, "Invalid Password!");
    //     }
    //
    //     // Retrieve the user ID from the database to be used as a claim in the JWT token
    //     string sqlRetrieveUserId = $@"
    //             SELECT userId
    //             FROM TutorialAppSchema.Users
    //             WHERE Email = '{user.Email}'
    //         ";
    //     int userId = _dapper.LoadDataSingle<int>(sqlRetrieveUserId);
    //
    //     return Ok(new Dictionary<string, string>
    //     {
    //         { "token", _authUtilities.CreateToken(userId) }
    //     });
    // }
    //
    // [HttpGet("RefreshToken")]
    // public string RefreshToken()
    // {
    //     string sqlRetrieveUserId = $@"
    //             SELECT UserId
    //             FROM TutorialAppSchema.Users
    //             WHERE UserId = '{User.FindFirst("userId")?.Value}'";
    //     int userId = _dapper.LoadDataSingle<int>(sqlRetrieveUserId);
    //     return _authUtilities.CreateToken(userId);
    // }
}