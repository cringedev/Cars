using System.Data;
using Dapper;
using Microsoft.Data.Sqlite;

namespace Cars.Api.Helpers;

public class DataContext
{
    protected readonly IConfiguration Configuration;

    public DataContext(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IDbConnection CreateConnection()
    {
        return new SqliteConnection(Configuration.GetConnectionString("CarsDatabase"));
    }

    public async Task Init()
    {
        using var connection = CreateConnection();
        var tablesSql = "SELECT name FROM sqlite_master WHERE type='table' AND name IN ('Users', 'Cars')";
        var tables = await connection.QueryAsync<string>(tablesSql);
        if (tables.Count() == 2) 
        {
            return; 
        }
        
        var sql =
            "CREATE TABLE IF NOT EXISTS Users (id INTEGER PRIMARY KEY, login TEXT, passwordHash TEXT); " +
            "CREATE TABLE IF NOT EXISTS Cars (id INTEGER PRIMARY KEY, make TEXT, model TEXT, price INTEGER, userId INTEGER, FOREIGN KEY (userId) REFERENCES Users(id)); " +
            "INSERT INTO Users (login, passwordHash) VALUES ('emptyUser', 'badHash'); " +
            "INSERT INTO Cars (make, model, price, userId) VALUES ('Ford', 'Mustang', 35000, 1), ('Toyota', 'Camry', 25000, 1), ('Audi', 'A8', 60000, 1);";
        await connection.ExecuteAsync(sql);
    }
}