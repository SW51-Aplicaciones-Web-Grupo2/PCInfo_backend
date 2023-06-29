using Microsoft.EntityFrameworkCore;
using PCInfo_backend.PCInfo.Domain.Models;
using PCInfo_backend.PCInfo.Domain.Repositories;
using PCInfo_backend.PCInfo.Persistence.Contexts;
using PCInfo_backend.PCInfo.Services;

namespace PCInfo_backend.PCInfo.Persistence.Repositories;

public class RamRepository : BaseRepository, IRamRepository
{
    public RamRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Ram>> ListAsync()
    {
        return await _context.Rams.ToListAsync();
    }

    public async Task SaveAsync(Ram ram)
    {
        await _context.Rams.AddAsync(ram);
    }

    public void UpdateAsync(Ram ram)
    {
        _context.Rams.Update(ram);
    }

    public void DeleteAsync(Ram ram)
    {
        _context.Rams.Remove(ram);
    }
}