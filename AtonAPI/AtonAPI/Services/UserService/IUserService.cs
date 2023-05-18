using Domain.Entites;
using Domain.Models;

namespace AtonAPI.Services.UserService;

public interface IUserService
{
    AuthResponse Authenticate(LoginModel model);
    Task<List<User>> GetAll();
    Task<User> GetById(Guid id);
    Task<User> GetByLogin(string login);
    Task<List<User>> GetAllByCreation();
    Task<List<User>> GetAllByAge(int age);
    bool AdminChecker(Guid id);
    Task<CreateUserResponse> CreateUser(CreateUserModel model, User creator);
    Task<DeleteResponse> DeleteUser(DeleteUserRequest request,string Revoker);
    Task<ActivateResponse> ActivateUser(ActivateUserRequest request,string Activator);
    Task<UpdateInfoResponse> UpdateUserInfo(string login, UpdateUserInfoModel model,string modifiedBy);
    Task<UpdateUserLoginResponse> UpdateUserLogin(string login, UpdateUserLoginModel model, string modifiedBy);
    Task<ChangePasswordResponse> ChangeUserPassword(string login, ChangePasswordModel model, string modifiedBy);
}