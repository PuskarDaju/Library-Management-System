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

        if (string.IsNullOrEmpty(secretKeyString) || secretKeyString.Length < 16)
            throw new InvalidOperationException("JWT SecretKey is missing or too short (minimum 16 characters).");

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKeyString));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, userId),
            new Claim(ClaimTypes.Role, role)
        };

        var token = new JwtSecurityToken(
            issuer: config["Jwt:Issuer"],
            audience: config["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(4),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
