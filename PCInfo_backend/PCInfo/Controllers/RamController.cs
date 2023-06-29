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
public class RamController : ControllerBase
{
    private readonly IRamService _ramService;
    private readonly IMapper _mapper;
    
    public RamController(IRamService ramService, IMapper mapper)
    {
        _ramService = ramService;
        _mapper = mapper;
    }
    
    [HttpGet]
    public async Task<IEnumerable<RamResource>> GetAllAsync()
    {
        var rams = await _ramService.ListAsync();
        var resources = _mapper.Map<IEnumerable<Ram>, IEnumerable<RamResource>>(rams);

        return resources;
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] SaveRamResource resource)
    {
        if(!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());
        var ram = _mapper.Map<SaveRamResource, Ram>(resource);
        var result = await _ramService.SaveAsync(ram);
        if (!result.Success)
            return BadRequest(result.Message);
        var ramResource = _mapper.Map<Ram, RamResource>(result.Resource);
        return Ok(ramResource);
    }
}