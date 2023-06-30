using PCInfo_backend.PCInfo.Security.Authorization.Handlers.Interfaces;
using PCInfo_backend.PCInfo.Security.Authorization.Settings;
using PCInfo_backend.PCInfo.Security.Domain.Services;

namespace PCInfo_backend.PCInfo.Security.Authorization.Middleware;

public class JwtMiddleware
{
    private readonly RequestDelegate _next;
    private readonly AppSettings _appSettings;

    public JwtMiddleware(RequestDelegate next, AppSettings appSettings)
    {
        _next = next;
        _appSettings = appSettings;
    }
    public async Task Invoke(HttpContext context, IUserService 
        userService, IJwtHandler handler)
    {
        var token = 
            context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
        var userId = handler.ValidateToken(token);
        if (userId != null)
        {
            // attach user to context on successful jwt validation
            context.Items["User"] = await 
                userService.GetByIdAsync(userId.Value);
        }
        await _next(context);
    }

}