using Cars.Api.Helpers;
using Cars.Api.Models.Users;
using Cars.Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Cars.Api.Controllers;

[ApiController]
[Route("api/user")]
public class UserController : ControllerBase
{
    private readonly IJwtService _jwtService;
    private readonly IUserService _userService;

    public UserController(
        IJwtService jwtService, 
        IUserService userService
        )
    {
        _jwtService = jwtService;
        _userService = userService;
    }

    [HttpGet("token")]
    public IActionResult GenerateToken()
    {
        var token = _jwtService.GenerateToken(0);
        return Ok(token);
    }

    [HttpPost("register")]
    public async Task<IActionResult> RegisterUser([FromBody]UserRequest userRequest)
    {
        var userId = await _userService.Create(userRequest);
        
        var token = _jwtService.GenerateToken(userId);
        
        var response = new CreateUserResponse()
        {
            Id = userId,
            Token = token
        };
        return Ok(response);
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> LoginUser([FromBody] UserRequest userRequest)
    {
        var user = await _userService.GetByLogin(userRequest.Login);
        
        if (!PasswordHelper.VerifyPassword(userRequest.Password, user.PasswordHash))
        {
            return BadRequest("Incorrect password");
        }

        var token = _jwtService.GenerateToken(user.Id);

        return Ok(token);
    }
}