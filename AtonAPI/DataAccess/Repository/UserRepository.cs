using Domain.Entites;
using Domain.Models;
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

    public void AddUser(User user)
    {
        _context.Users.Add(user);
        _context.SaveChanges();
    }

    public async Task<List<User>> GetAllSortedByCreation()
    {
        return await _context.Users.Where(x=>x.RevokedOn==null).OrderBy(x=>x.CreatedOn).ToListAsync();
    }

    public async Task<List<User>> GetUsersByAge(int age)
    {
        var years = DateTime.Now.AddYears(-age);
        return await _context.Users.Where(x => x.Birthday < years).ToListAsync();
    }

    public async void UpdateUser(User user)
    {
        _context.Users.Update(user);
         await _context.SaveChangesAsync();
    }

    public async void DeleteUser(User user)
    {
        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
    }
}