using System.Security.Claims;
using System.Text;
using Cars.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Cars.Api.Controllers;

public class AuthenticationController : ControllerBase
{
    [HttpPost("token")]
    public IActionResult GetToken([FromBody] UserCredentials credentials)
    {
        // Проверить логин и пароль пользователя
        if (credentials.Username == "admin" && credentials.Password == "admin123")
        {
            // Создать токен
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("secret_key_for_token_generation");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, credentials.Username)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            // Вернуть токен
            return Ok(new { Token = tokenString });
        }

        // Вернуть ошибку в случае неверных логина или пароля
        return Unauthorized();
    }
}