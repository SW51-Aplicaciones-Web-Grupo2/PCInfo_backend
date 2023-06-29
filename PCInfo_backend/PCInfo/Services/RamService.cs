using PCInfo_backend.PCInfo.Domain.Models;
using PCInfo_backend.PCInfo.Domain.Repositories;
using PCInfo_backend.PCInfo.Domain.Services;
using PCInfo_backend.PCInfo.Domain.Services.Communication;

namespace PCInfo_backend.PCInfo.Services;

public class RamService : IRamService
{
    private readonly IRamRepository _ramRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RamService(IRamRepository ramRepository, IUnitOfWork unitOfWork)
    {
        _ramRepository = ramRepository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<IEnumerable<Ram>> ListAsync()
    {
        return await _ramRepository.ListAsync();
    }

    public async Task<RamResponse> SaveAsync(Ram ram)
    {
        try
        {
            await _ramRepository.SaveAsync(ram);
            await _unitOfWork.CompleteAsync();
            return new RamResponse(ram);
        }
        catch (Exception e)
        {
            return new RamResponse($"An error occurred while saving the user: {e.Message}");
        }
    }

    public Task<RamResponse> UpdateAsync(int id, Ram ram)
    {
        throw new NotImplementedException();
    }

    public Task<RamResponse> DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }
}