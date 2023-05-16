using System.Security.Claims;
using AtonAPI.Helpers;
using AutoMapper;
using DataAccess.Repository;
using Domain.Entites;
using Domain.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Configuration;

namespace Misc.UserService;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IConfiguration _configuration;
    private readonly IMapper _mapper;

    public UserService(IUserRepository userRepository, IConfiguration configuration,IMapper mapper)
    {
        _userRepository = userRepository;
        _configuration = configuration;
        _mapper = mapper;
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

    public bool AdminChecker(Guid id)
    {
        var user = _userRepository.GetByIdAsync(id).Result;
        if (user.Admin)
        {
            return true;
        }

        return false;
    }

    public void CreateUser(CreateUserModel model,User creator)
    {
        var user = new User();
        user = _mapper.Map<User>(model);
        user.CreatedBy = creator.Login;
        user.ModifiedBy = creator.Login;
        _userRepository.AddUser(user);
    }

    public async Task<DeleteResponse> DeleteUser(DeleteUserRequest request)
    {
        var response = new DeleteResponse();
        var user = await _userRepository.GetUserByLoginAsync(request.UserLogin);

        if (user == null)
        {
            response.Message = "User not found";
            response.IsSuccess = false;
            return response;
        }

        if (user.RevokedOn != null)
        {
            response.Message = "User already revoked";
            response.IsSuccess = false;
            return response;
        }

        if (request.IsSoftDelete)
        {
            response.Message = "user was deleted by soft delete";
            response.Revoker = request.Revoker;
            response.IsSuccess = true;

            user.RevokedBy = request.Revoker;
            user.RevokedOn=DateTime.Now;

            _userRepository.UpdateUser(user);
            
            return response;
        }
        else
        {
            response.Message = "user was hard deleted";
            response.Revoker = request.Revoker;
            response.IsSuccess = true;

            _userRepository.DeleteUser(user);

            return response;
        }
    }
}