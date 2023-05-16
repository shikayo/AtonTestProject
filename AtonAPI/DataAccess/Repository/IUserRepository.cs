using Domain.Entites;
using Domain.Models;

namespace DataAccess.Repository;

public interface IUserRepository
{
    Task<User> GetUserByLoginAsync(string login);
    Task<List<User>> GetAllAsync();
    Task<User> GetByIdAsync(Guid id);
    void AddUser(User user);
    Task<List<User>> GetAllSortedByCreation();
    Task<List<User>> GetUsersByAge(int age);
    void UpdateUser(User user);
    void DeleteUser(User user);
}