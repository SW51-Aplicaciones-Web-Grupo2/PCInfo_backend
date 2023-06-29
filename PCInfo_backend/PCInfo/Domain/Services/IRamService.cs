using PCInfo_backend.PCInfo.Domain.Models;
using PCInfo_backend.PCInfo.Domain.Services.Communication;

namespace PCInfo_backend.PCInfo.Domain.Services;

public interface IRamService
{
    Task<IEnumerable<Ram>> ListAsync();
    Task<RamResponse> SaveAsync(Ram ram);
    Task<RamResponse> UpdateAsync(int id, Ram ram);
    Task<RamResponse> DeleteAsync(int id);
}