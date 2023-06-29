using System.ComponentModel.DataAnnotations;

namespace PCInfo_backend.PCInfo.Security.Domain.Services.Communication;

public class AuthenticateRequest
{
    [Required] public string Username { get; set; }
    [Required] public string Password { get; set; }
}