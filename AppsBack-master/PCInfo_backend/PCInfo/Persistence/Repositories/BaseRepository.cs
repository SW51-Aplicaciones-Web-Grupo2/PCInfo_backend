using PCInfo_backend.PCInfo.Persistence.Contexts;

namespace PCInfo_backend.PCInfo.Persistence.Repositories;

public class BaseRepository
{
    protected readonly AppDbContext _context;
    
    public BaseRepository(AppDbContext context)
    {
        _context = context;
    }
}