using Domain.Entites;
using Domain.Models;

namespace Misc.UserService;

public interface IUserService
{
    AuthResponse Authenticate(LoginModel model);
    Task<List<User>> GetAll();
    Task<User> GetById(Guid id);
    Task<User> GetByLogin(string login);
    Task<List<User>> GetAllByCreation();
    Task<List<User>> GetAllByAge(int age);
    bool AdminChecker(Guid id);
    void CreateUser(CreateUserModel model, User creator);
    Task<DeleteResponse> DeleteUser(DeleteUserRequest request,string Revoker);
    Task<ActivateResponse> ActivateUser(ActivateUserRequest request,string Activator);
}