using Domain.Entites;

namespace DataAccess.Repository;

public interface IUserRepository
{
    Task<User> GetUserByLoginAsync(string login);
    Task<List<User>> GetAllAsync();
    Task<User> GetByIdAsync(Guid id);
}