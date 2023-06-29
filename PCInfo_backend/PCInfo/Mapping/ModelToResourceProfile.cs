using AutoMapper;
using PCInfo_backend.PCInfo.Domain.Models;
using PCInfo_backend.PCInfo.Resources;

namespace PCInfo_backend.PCInfo.Mapping;

public class ModelToResourceProfile : Profile
{
    public ModelToResourceProfile()
    {
        CreateMap<User, UserResource>();
        CreateMap<Ram, RamResource>();
        
    }
}