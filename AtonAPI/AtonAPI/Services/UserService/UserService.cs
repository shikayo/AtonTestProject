using AtonAPI.Helpers;
using AutoMapper;
using DataAccess.Repository;
using Domain.Entites;
using Domain.Models;
using Microsoft.Extensions.Configuration;

namespace Misc.UserService;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IConfiguration _configuration;

    public UserService(IUserRepository userRepository, IConfiguration configuration)
    {
        _userRepository = userRepository;
        _configuration = configuration;
    }
    
    public AuthResponse Authenticate(LoginModel model)
    {
        var user = _userRepository
            .GetAllAsync().Result
            .FirstOrDefault(x => x.Login == model.Login && BCrypt.Net.BCrypt.Verify(model.Password,x.Password));

        if (user == null)
        {
            // todo: need to add logger
            return null;
        }

        var token = _configuration.GenerateJwtToken(user);

        return new AuthResponse(user, token);
    }

    public async Task<List<User>> GetAll()
    {
        return await _userRepository.GetAllAsync();
    }
    

    public async Task<User> GetById(Guid id)
    {
        return await _userRepository.GetByIdAsync(id);
    }
}