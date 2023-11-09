namespace Web.Features.User.Register;

public class UserRegisterRequest
{
    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string PasswordRepeat { get; set; } = null!;
}