using AutoMapper;
using Cars.Api.Entities;
using Cars.Api.Models.Users;
using Cars.Api.Repositories.Interfaces;
using Cars.Api.Services.Interfaces;

namespace Cars.Api.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public UserService(IUserRepository userRepository,
        IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }
    
    public async Task<User> GetByLogin(string login)
    {
        var user = await _userRepository.GetByLogin(login);
        if (user is null)
            throw new KeyNotFoundException($"User \"{login}\" not found");
        return user;
    }

    public async Task<int> Create(UserRequest request)
    {
        var user = await _userRepository.GetByLogin(request.Login);
        if (user is not null)
        {
            throw new ArgumentException($"{request.Login} already exist", nameof(request.Login));
        }
        user = _mapper.Map<User>(request);
        return await _userRepository.Create(user);
    }
}