using PCInfo_backend.PCInfo.Domain.Models;
using PCInfo_backend.Shared.Domain.Services.Communication;

namespace PCInfo_backend.PCInfo.Domain.Services.Communication;

public class RamResponse : BaseResponse<Ram>
{
    public RamResponse(string message) : base(message)
    {
    }
    public RamResponse(Ram resource) : base(resource)
    {
    }
}