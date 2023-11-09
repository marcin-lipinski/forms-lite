namespace Web.Features.User.Login;

public class UserLoginRequest
{
    public string Login { get; set; } = null!;
    public string Password { get; set; } = null!;
}