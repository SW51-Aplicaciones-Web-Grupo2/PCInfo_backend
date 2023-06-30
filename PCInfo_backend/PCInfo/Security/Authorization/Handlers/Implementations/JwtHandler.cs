using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PCInfo_backend.PCInfo.Security.Authorization.Handlers.Interfaces;
using PCInfo_backend.PCInfo.Security.Authorization.Settings;
using PCInfo_backend.PCInfo.Security.Domain.Models;

namespace PCInfo_backend.PCInfo.Security.Authorization.Handlers.Implementations;

public class JwtHandler : IJwtHandler
{
    private readonly AppSettings _appSettings;
    public JwtHandler(IOptions<AppSettings> appSettings)
    {
        _appSettings = appSettings.Value;
    }


    public string GenerateToken(User user)
    {
        // Generate Token for a valid period of 7 days
        var tokenHandler = new JwtSecurityTokenHandler();
        Console.WriteLine($"token handler: {tokenHandler.TokenType}");
        var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
        Console.WriteLine($"Secret Key: {key}");
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim("id", user.Id.ToString())
            }),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        Console.WriteLine($"token: {token.Id}, {token.Issuer}, {token.SecurityKey?.ToString()}");
        return tokenHandler.WriteToken(token);

    }

    public int? ValidateToken(string token)
    {
        if (token == null)
            return null;
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
        // Execute Token validation
        try
        {
            tokenHandler.ValidateToken(token, new 
                TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    // Expiration with no delay
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);
            var jwtToken = (JwtSecurityToken)validatedToken;
            var userId = int.Parse(jwtToken.Claims.First(
                claim => claim.Type == "id").Value);
            return userId;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
    }
}