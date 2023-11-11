namespace Infrastructure.Settings;

public class TokenSettings
{
    public string Key { get; set; } = null!;
    public string ExpireTime { get; set; } = null!;
    public string ExpireTimeFormat { get; set; } = null!;
    
}