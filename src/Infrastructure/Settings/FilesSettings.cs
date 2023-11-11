namespace Infrastructure.Settings;

public class FilesSettings
{
    public string AppDirectory { get; set; } = null!;
    public string FilesDirectory { get; set; } = null!;
    public string ImagesDirectory { get; set; } = null!;
    public string FilesRoute() => Path.Combine(AppDirectory, FilesDirectory);
    public string ImagesRoute() => Path.Combine(AppDirectory, ImagesDirectory);
    public string RequestPath() => Path.Combine("/", FilesDirectory);
    
}