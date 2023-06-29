using PCInfo_backend.PCInfo.Domain.Models;
using PCInfo_backend.PCInfo.Domain.Repositories;
using PCInfo_backend.PCInfo.Domain.Services;
using PCInfo_backend.PCInfo.Domain.Services.Communication;

namespace PCInfo_backend.PCInfo.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UserService(IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<IEnumerable<User>> ListAsync()
    {
        return await _userRepository.ListAsync();
    }

    public async Task<UserResponse> SaveAsync(User user)
    {
        try
        {
            await _userRepository.SaveAsync(user);
            await _unitOfWork.CompleteAsync();
            return new UserResponse(user);
        }
        catch (Exception e)
        {
            return new UserResponse($"An error occurred while saving the user: {e.Message}");
        }
    }

    public Task<UserResponse> UpdateAsync(int id, User user)
    {
        throw new NotImplementedException();
    }

    public Task<UserResponse> DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }
}