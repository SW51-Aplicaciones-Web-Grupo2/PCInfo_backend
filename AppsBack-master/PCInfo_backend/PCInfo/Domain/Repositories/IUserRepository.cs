using PCInfo_backend.PCInfo.Domain.Models;

namespace PCInfo_backend.PCInfo.Domain.Repositories;

public interface IUserRepository
{
    Task<IEnumerable<User>> ListAsync();
    Task SaveAsync(User user);
    void UpdateAsync(User user);
    void DeleteAsync(User user);
}