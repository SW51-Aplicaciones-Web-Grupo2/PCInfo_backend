using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PCInfo_backend.PCInfo.Security.Domain.Models;

namespace PCInfo_backend.PCInfo.Security.Authorization.Attributes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class AuthorizeAttribute : Attribute, IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var allowAnonymous = context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any();
        if (allowAnonymous) return;
        
        var user = (User) context.HttpContext.Items["User"];
        if (user == null)
        {
            context.Result = new JsonResult(new {message = "Unauthorized"})
            {
                StatusCode = StatusCodes.Status401Unauthorized
            };
        }
    }
}