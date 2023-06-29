using PCInfo_backend.Shared.Persistence;

namespace PCInfo_backend.PCInfo.Security.Persistence.Repositories;

public class BaseRepository
{
    protected readonly AppDbContext _context;

    public BaseRepository(AppDbContext context)
    {
        _context = context;
    }
}