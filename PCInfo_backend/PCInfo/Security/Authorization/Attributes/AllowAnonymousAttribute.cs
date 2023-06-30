using Microsoft.AspNetCore.Mvc;

namespace PCInfo_backend.PCInfo.Security.Authorization.Attributes;

[AttributeUsage(AttributeTargets.Method)]
public class AllowAnonymousAttribute : Attribute
{
    [AllowAnonymous]
    public IActionResult Invoke()
    {
        return new OkResult();
    }
}