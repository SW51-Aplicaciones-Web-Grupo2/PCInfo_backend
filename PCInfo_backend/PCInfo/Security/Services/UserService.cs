using AutoMapper;
using PCInfo_backend.PCInfo.Domain.Repositories;
using PCInfo_backend.PCInfo.Security.Authorization.Handlers.Interfaces;
using PCInfo_backend.PCInfo.Security.Domain.Models;
using PCInfo_backend.PCInfo.Security.Domain.Services;
using PCInfo_backend.PCInfo.Security.Domain.Services.Communication;
using PCInfo_backend.PCInfo.Security.Exceptions;
using BCryptNet = BCrypt.Net.BCrypt;
using IUserRepository = PCInfo_backend.PCInfo.Security.Domain.Repositories.IUserRepository;


namespace PCInfo_backend.PCInfo.Security.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IJwtHandler _jwtHandler;
    private readonly IMapper _mapper;
    public UserService(IUserRepository userRepository, IUnitOfWork unitOfWork, IJwtHandler jwtHandler, IMapper mapper)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _jwtHandler = jwtHandler;
        _mapper = mapper;
    }

    public async Task<AuthenticateResponse> Authenticate(AuthenticateRequest request)
    {
        var user = await 
            _userRepository.FindByUsernameAsync(request.Username);
        Console.WriteLine($"Request: {request.Username}, {request.Password}");
        Console.WriteLine($"User: {user.Id}, {user.FirstName}, {user.LastName}, {user.Username}, {user.PasswordHash}");
 
        // validate
        if (user == null || !BCryptNet.Verify(request.Password, 
                user.PasswordHash))
        {
            Console.WriteLine("Authentication Error");
            throw new AppException("Username or password is incorrect");
        }
 
        Console.WriteLine("Authentication successful. About to generate token");
        // authentication successful
        var response = _mapper.Map<AuthenticateResponse>(user);
        Console.WriteLine($"Response: {response.Id}, {response.FirstName}, {response.LastName}, {response.Username}");
        response.Token = _jwtHandler.GenerateToken(user);
        Console.WriteLine($"Generated token is {response.Token}");
        return response;
    }
    
    public async Task<IEnumerable<User>> ListAsync()
    {
        return await _userRepository.ListAsync();
    }

    public async Task<User> GetByIdAsync(int id)
    {
        var user = await _userRepository.FindByIdAsync(id);
        if (user == null) throw new KeyNotFoundException("User not found.");
        return user;
    }

    public async Task RegisterAsync(RegisterRequest model)
    {
        if (_userRepository.ExistsByUsername(model.Username))
            throw new AppException("Username " + model.Username + " is already taken");
        
        var user = _mapper.Map<User>(model);
        
        user.PasswordHash = BCryptNet.HashPassword(model.Password);

        try
        {
            await _userRepository.AddAsync(user);
            await _unitOfWork.CompleteAsync();
        }
        catch (Exception e)
        {
            throw new AppException($"An error occurred while saving the user: {e.Message}");
        }
    }
    
    // helper methods
    private User GetById(int id)
    {
        var user = _userRepository.FindByIdAsync(id);
        if (user == null) throw new KeyNotFoundException("User not found.");
        return user.Result;
    }

    public async Task UpdateAsync(int id, UpdateRequest model)
    {
        var user = GetById(id);
        
        // Validate
        if (_userRepository.ExistsByUsername(model.Username))
            throw new AppException("Username " + model.Username + " is already taken");
        
        // Hash password if it was entered
        if (!string.IsNullOrEmpty(model.Password))
            user.PasswordHash = BCryptNet.HashPassword(model.Password);
        
        // Copy model to user and save
        _mapper.Map(model, user);
        try
        {
            _userRepository.Update(user);
            await _unitOfWork.CompleteAsync();
        }
        catch (Exception e)
        {
            throw new AppException($"An error occurred while updating the user: {e.Message}");
        }
    }

    public async Task DeleteAsync(int id)
    {
        var user = GetById(id);

        try
        {
            _userRepository.Remove(user);
            await _unitOfWork.CompleteAsync();
        }
        catch (Exception e)
        {
            throw new AppException($"An error occurred while deleting the user: {e.Message}");
        }
    }
}