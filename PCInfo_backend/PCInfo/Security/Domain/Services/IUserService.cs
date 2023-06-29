using PCInfo_backend.PCInfo.Security.Domain.Models;
using PCInfo_backend.PCInfo.Security.Domain.Services.Communication;

namespace PCInfo_backend.PCInfo.Security.Domain.Services;

public interface IUserService
{
    Task<AuthenticateResponse> Authenticate(AuthenticateRequest model);
    Task<IEnumerable<User>> ListAsync();
    Task<User> GetByIdAsync(int id);
    Task RegisterAsync(RegisterRequest model);
    Task UpdateAsync(int id, UpdateRequest model);
    Task DeleteAsync(int id);
}