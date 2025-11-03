using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Library_Management_System.Helpers;

public class JwtService(IConfiguration config)
{
    public string GenerateToken(string userId, string role)
    {
        var secretKeyString = config["Jwt:SecretKey"];

        if (string.IsNullOrEmpty(secretKeyString) || secretKeyString.Length < 32)
            throw new InvalidOperationException("JWT SecretKey is missing or too short (minimum 32 characters).");

        var issuer = config["Jwt:Issuer"];
        var audience = config["Jwt:Audience"];
        if (string.IsNullOrEmpty(issuer) || string.IsNullOrEmpty(audience))
        {
            throw new InvalidOperationException("JWT Issuer and Audience are missing or not set.");
        }
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKeyString));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, userId),
            new Claim(ClaimTypes.Role, role)
        };

        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: DateTime.UtcNow.AddHours(4),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
