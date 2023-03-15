namespace Cars.Api.Services.Interfaces;

public interface IJwtService
{
    string GenerateToken(int userId);
}