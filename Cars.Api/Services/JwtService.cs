using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Cars.Api.Options;
using Cars.Api.Services.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Cars.Api.Services;

public class JwtService : IJwtService
{
    private readonly JwtOptions _options;

    public JwtService(IOptions<JwtOptions> options)
    {
        _options = options.Value;
    }

    public string GenerateToken(string login)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, "tempLogin")
        };
        
        var jwt = new JwtSecurityToken(
            issuer: _options.Issuer,
            audience: _options.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddSeconds(_options.ExpiresInSeconds),
            signingCredentials: new SigningCredentials(GetSymmetricSecurityKey(_options.Secret), SecurityAlgorithms.HmacSha256));
            
        var token = new JwtSecurityTokenHandler().WriteToken(jwt);
        return token;
    }
    
    public static SymmetricSecurityKey GetSymmetricSecurityKey(string secret) => 
        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
    
}