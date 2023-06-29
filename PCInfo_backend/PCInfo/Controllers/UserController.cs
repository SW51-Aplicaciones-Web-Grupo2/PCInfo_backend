using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PCInfo_backend.PCInfo.Domain.Models;
using PCInfo_backend.PCInfo.Domain.Services;
using PCInfo_backend.PCInfo.Resources;
using PCInfo_backend.PCInfo.Services;
using PCInfo_backend.Shared.Extensions;

namespace PCInfo_backend.PCInfo.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IMapper _mapper;
    
    public UserController(IUserService userService, IMapper mapper)
    {
        _userService = userService;
        _mapper = mapper;
    }
    
    [HttpGet]
    public async Task<IEnumerable<UserResource>> GetAllAsync()
    {
        var users = await _userService.ListAsync();
        var resources = _mapper.Map<IEnumerable<User>, IEnumerable<UserResource>>(users);

        return resources;
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] SaveUserResource resource)
    {
        if(!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());
        var user = _mapper.Map<SaveUserResource, User>(resource);
        var result = await _userService.SaveAsync(user);
        if (!result.Success)
            return BadRequest(result.Message);
        var userResource = _mapper.Map<User, UserResource>(result.Resource);
        return Ok(userResource);
    }
}