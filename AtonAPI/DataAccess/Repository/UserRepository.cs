using Domain.Entites;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repository;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;

    public UserRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    
    
    public async Task<User> GetUserByLoginAsync(string login)
    {
        return (await _context.Users.SingleOrDefaultAsync(x=>x.Login==login))!;
    }

    public async Task<List<User>> GetAllAsync()
    {
        return await _context.Users.ToListAsync();
    }

    public async Task<User> GetByIdAsync(Guid id)
    {
        return (await _context.Users.SingleOrDefaultAsync(x=>x.Id==id))!;
    }
}