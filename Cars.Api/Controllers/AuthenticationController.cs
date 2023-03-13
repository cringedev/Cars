using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Cars.Api.Models;
using Cars.Api.Options;
using Cars.Api.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Cars.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthenticationController : ControllerBase
{
    private readonly IJwtService _jwtService;

    public AuthenticationController(IJwtService jwtService)
    {
        _jwtService = jwtService;
    }
    
    [HttpGet("token")]
    public IActionResult Get()
    {
        var token = _jwtService.GenerateToken("tempLogin");
        return Ok(token);
    }
    
    [HttpPost("login")]
    public IActionResult Login(User user)
    {
        var token = _jwtService.GenerateToken(user.Login);
        return Ok(token);
    }

    [HttpGet("authorizeEndpoint")]
    [Authorize]
    public IActionResult Test()
    {
        return Ok();
    }
}