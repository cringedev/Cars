using Cars.Api.Entities;
using Cars.Api.Models.Users;

namespace Cars.Api.Services.Interfaces;

public interface IUserService
{
    Task<User> GetByLogin(string login);
    Task<int> Create(UserRequest user);
}