using Cars.Api.Entities;
using Cars.Api.Helpers;
using Cars.Api.Repositories.Interfaces;
using Dapper;

namespace Cars.Api.Repositories;

public class UserRepository : IUserRepository
{
    private readonly DataContext _context;

    public UserRepository(DataContext context)
    {
        _context = context;
    }
    
    public async Task<User?> GetByLogin(string login)
    {
        using var connection = _context.CreateConnection();
        var sql = "SELECT * FROM Users WHERE Login = @login";
        return await connection.QuerySingleOrDefaultAsync<User>(sql, new { login });
    }

    public async Task<int> Create(User user)
    {
        using var connection = _context.CreateConnection();
        var sql = "INSERT INTO Users (Login, PasswordHash) VALUES (@Login, @PasswordHash); SELECT last_insert_rowid();";
        return await connection.ExecuteScalarAsync<int>(sql, user);
    }
}