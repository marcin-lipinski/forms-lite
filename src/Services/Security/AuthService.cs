using Core.DataAccess;
using Core.Entities;

namespace Services.Security;

public class AuthService
{
    private readonly IPasswordManager _passwordManager;
    private readonly IRepository<User> _userRepository;

    public AuthService(IPasswordManager passwordManager, IRepository<User> userRepository)
    {
        _passwordManager = passwordManager;
        _userRepository = userRepository;
    }

    public async Task<AuthResult> Login(string username, string password)
    {
        var users = await _userRepository.GetAllAsync();
        var user = users.SingleOrDefault(u => u.Username.Equals(username));

        if (user is null) 
            return AuthResult.Create(false);

        if (!_passwordManager.VerifyPassword(user, user.PasswordHashed, password))
            return AuthResult.Create(false);
        
        
            
        return AuthResult.Create(true);
    }
}

public class AuthResult
{
    public string? Token { get; init; }
    public bool IsSuccess { get; init; }

    public static AuthResult Create(bool success, string? token = null)
    {
        return new AuthResult { IsSuccess = success, Token = token };
    }
}