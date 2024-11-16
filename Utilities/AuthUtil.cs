using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.IdentityModel.Tokens;

namespace UniSyncApi.Utilities;

public class AuthUtil(IConfiguration config)
{
    public byte[] GetPasswordHash(string password, byte[] passwordSalt)
    {
        string? passwordKey = config.GetSection("AppSettings:PasswordKey").Value;
        string extendedPasswordSalt = passwordKey + Convert.ToBase64String(passwordSalt);

        return KeyDerivation.Pbkdf2(
            password: password,
            salt: Encoding.ASCII.GetBytes(extendedPasswordSalt),
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 100000,
            numBytesRequested: 256 / 8
        );
    }

    public string CreateToken(int userId)
    {
        Claim[] claims = new Claim[]
        {
            new Claim("userId", userId.ToString())
        };

        string? tokenKeyString = config.GetSection("AppSettings:TokenKey").Value;
        SymmetricSecurityKey tokenKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(
                tokenKeyString != null ? tokenKeyString : ""
            )
        );

        SigningCredentials credentials = new SigningCredentials(tokenKey, SecurityAlgorithms.HmacSha512Signature);
            
        SecurityTokenDescriptor descriptor = new SecurityTokenDescriptor()
        {
            Subject = new ClaimsIdentity(claims),
            SigningCredentials = credentials,
            Expires = DateTime.Now.AddDays(1)
        };

        JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            
        SecurityToken token = handler.CreateToken(descriptor);

        return handler.WriteToken(token);
    }
}