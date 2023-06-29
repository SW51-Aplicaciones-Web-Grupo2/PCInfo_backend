using PCInfo_backend.PCInfo.Domain.Models;

namespace PCInfo_backend.PCInfo.Domain.Repositories;

public interface IRamRepository
{
    Task<IEnumerable<Ram>> ListAsync();
    Task SaveAsync(Ram ram);
    void UpdateAsync(Ram ram);
    void DeleteAsync(Ram ram);
}