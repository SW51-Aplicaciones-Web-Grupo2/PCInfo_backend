using AutoMapper;
using PCInfo_backend.PCInfo.Domain.Models;
using PCInfo_backend.PCInfo.Resources;

namespace PCInfo_backend.PCInfo.Mapping;

public class ResourceToModelProfile : Profile
{
    public ResourceToModelProfile()
    {
        CreateMap<SaveUserResource, User>();
        CreateMap<SaveRamResource, Ram>();
        
    }
}