using PCInfo_backend.PCInfo.Domain.Models;
using PCInfo_backend.Shared.Domain.Services.Communication;

namespace PCInfo_backend.PCInfo.Domain.Services.Communication;

public class UserResponse : BaseResponse<User>
{
    public UserResponse(string message) : base(message)
    {
    }
    public UserResponse(User resource) : base(resource)
    {
    }
}