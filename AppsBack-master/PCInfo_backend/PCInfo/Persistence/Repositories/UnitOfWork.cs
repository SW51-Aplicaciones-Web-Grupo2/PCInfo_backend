using PCInfo_backend.PCInfo.Domain.Repositories;
using PCInfo_backend.PCInfo.Persistence.Contexts;

namespace PCInfo_backend.PCInfo.Persistence.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;
    
    public UnitOfWork(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task CompleteAsync()
    {
        await _context.SaveChangesAsync();
    }
}