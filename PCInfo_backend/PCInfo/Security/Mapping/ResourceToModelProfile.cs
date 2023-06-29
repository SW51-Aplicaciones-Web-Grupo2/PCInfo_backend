using AutoMapper;
using Org.BouncyCastle.Asn1.X509;
using PCInfo_backend.PCInfo.Security.Domain.Models;
using PCInfo_backend.PCInfo.Security.Domain.Services.Communication;

namespace PCInfo_backend.PCInfo.Security.Mapping;

public class ResourceToModelProfile : Profile
{
    public ResourceToModelProfile()
    {
        CreateMap<RegisterRequest, User>(); // Se realizan las asignaciones por defecto.
        CreateMap<UpdateRequest, User>()
            .ForAllMembers(options => options.Condition(
                (source, target, property) =>
                {
                    if (property == null) return false;
                    if (property.GetType() == typeof(string) && string.IsNullOrEmpty((string) property))
                        return false;
                    return true;
                }
                ));
    }
}

/* ForAllMembers() is used to apply a condition to all members of the type.
 * it's defined as a lambda function that receives the source object, the target object, and the property being mapped.
 * if the property is null or empty, the condition returns false, which means that the property won't be mapped.
 * if the property is a string and it's null or empty, the condition returns false, which means that the property won't be mapped.
 * if the property is not null or empty, the condition returns true, which means that the property will be mapped.
 */