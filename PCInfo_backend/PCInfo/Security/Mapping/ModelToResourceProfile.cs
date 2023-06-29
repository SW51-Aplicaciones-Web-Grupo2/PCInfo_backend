using AutoMapper;
using PCInfo_backend.PCInfo.Security.Domain.Models;
using PCInfo_backend.PCInfo.Security.Domain.Services.Communication;
using PCInfo_backend.PCInfo.Security.Resources;

namespace PCInfo_backend.PCInfo.Security.Mapping;

public class ModelToResourceProfile : Profile
{
    public ModelToResourceProfile()
    { // Mapping from Model to Resource.
        CreateMap<User, AuthenticateResponse>();
        CreateMap<User, UserResource>();
    }
}