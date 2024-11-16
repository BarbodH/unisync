using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UniSyncApi.Dtos.Auth;
using UniSyncApi.Services.Interfaces;

namespace UniSyncApi.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class AuthController(IAccountService accountService) : ControllerBase
{
    [AllowAnonymous]
    [HttpPost("Register")]
    public IActionResult Register(AccountRegistrationDto account)
    {
        accountService.Register(account);
        return Ok();
    }

    [AllowAnonymous]
    [HttpPost("Login")]
    public IActionResult Login(AccountLoginDto account)
    {
        return Ok(new Dictionary<string, string>
        {
            { "token", accountService.Login(account) }
        });
    }

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