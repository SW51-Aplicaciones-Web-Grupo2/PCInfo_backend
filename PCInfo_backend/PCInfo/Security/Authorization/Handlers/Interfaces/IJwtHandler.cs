using PCInfo_backend.PCInfo.Security.Domain.Models;

namespace PCInfo_backend.PCInfo.Security.Authorization.Handlers.Interfaces;

public interface IJwtHandler
{
    public string GenerateToken(User user);
    public int? ValidateToken(string token);
}