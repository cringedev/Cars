using Cars.Api.Entities;
using Cars.Api.Helpers;
using Cars.Api.Repositories.Interfaces;
using Dapper;

namespace Cars.Api.Repositories;

public class CarRepository : ICarRepository
{
    private readonly DataContext _context;

    public CarRepository(DataContext context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<Car>> GetCars(int offset, int limit)
    {
        using var connection = _context.CreateConnection();
        var sql = $"SELECT * FROM Cars LIMIT {limit} OFFSET {offset};";
        return await connection.QueryAsync<Car>(sql);
    }

    public async Task<Car> GetById(int id)
    {
        using var connection = _context.CreateConnection();
        var sql = "SELECT * FROM Cars WHERE Id = @id";
        return await connection.QuerySingleOrDefaultAsync<Car>(sql, new { id });
    }

    public async Task<int> Create(Car car)
    {
        using var connection = _context.CreateConnection();
        var sql = "INSERT INTO Cars (Make, Model, Price, UserId) VALUES (@Make, @Model, @Price, @UserId); SELECT last_insert_rowid();";
        return await connection.ExecuteScalarAsync<int>(sql, car);
    }

    public async Task Update(Car car)
    {
        using var connection = _context.CreateConnection();
        var sql = "UPDATE Cars SET Price = @Price WHERE Id = @Id";
        await connection.ExecuteAsync(sql, car);
    }

    public async Task Delete(int id)
    {
        using var connection = _context.CreateConnection();
        var sql = "DELETE FROM Cars WHERE Id = @id";
        await connection.ExecuteAsync(sql, new { id });
    }
}