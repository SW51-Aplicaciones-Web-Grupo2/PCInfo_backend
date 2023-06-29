using Microsoft.EntityFrameworkCore;
using PCInfo_backend.PCInfo.Domain.Models;
using PCInfo_backend.PCInfo.Domain.Repositories;
using PCInfo_backend.PCInfo.Persistence.Contexts;
using PCInfo_backend.PCInfo.Services;

namespace PCInfo_backend.PCInfo.Persistence.Repositories;

public class UserRepository : BaseRepository, IUserRepository
{
    public UserRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<User>> ListAsync()
    {
        return await _context.Users.ToListAsync();
    }

    public async Task SaveAsync(User user)
    {
        await _context.Users.AddAsync(user);
    }

    public void UpdateAsync(User user)
    {
        _context.Users.Update(user);
    }

    public void DeleteAsync(User user)
    {
        _context.Users.Remove(user);
    }
}