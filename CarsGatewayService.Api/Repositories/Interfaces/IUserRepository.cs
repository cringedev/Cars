using Cars.Api.Entities;

namespace Cars.Api.Repositories.Interfaces;

public interface IUserRepository
{
    Task<User?> GetByLogin(string login);
    Task<int> Create(User user);
}