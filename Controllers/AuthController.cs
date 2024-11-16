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

    [HttpGet("RefreshToken")]
    public IActionResult RefreshToken()
    {
        return Ok(new Dictionary<string, string>
        {
            { "token", accountService.RefreshToken(User.FindFirst("userId")?.Value) }
        });
    }
}