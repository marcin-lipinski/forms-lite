using Core.Entities;
using Core.Entities.Image;
using Infrastructure.Settings;
using Microsoft.Extensions.Options;
using Services.Interfaces;

namespace Infrastructure.Persistence.Files;

public class FilesService : IFilesService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IOptions<FilesSettings> _filesOptions;

    public FilesService(IHttpContextAccessor httpContextAccessor, IOptions<FilesSettings> filesOptions)
    {
        _httpContextAccessor = httpContextAccessor;
        _filesOptions = filesOptions;
    }
    
    public async Task<ImageMetadata> SaveImage(string quizTitle, int questionNr, IFormFile file)
    {
        var imageName = CreateImageName(quizTitle + questionNr, file);
        var dirPath = Path.Combine(_filesOptions.Value.ImagesRoute(), (quizTitle + DateTime.Now).GetHashCode().ToString());
        if (!Directory.Exists(dirPath)) Directory.CreateDirectory(dirPath);
        
        var image = new ImageMetadata
        {
            FullPath = Path.Combine(dirPath, imageName),
            RelativePath = Path.Combine("/", _filesOptions.Value.ImagesDirectory, quizTitle, imageName)
        };
        
        await SaveImageOnHost(image, file);
        return image;
    }

    public string? CreateImageUrl(string imageRelativePath)
    {
        var scheme = _httpContextAccessor.HttpContext!.Request.Scheme;
        var host = _httpContextAccessor.HttpContext.Request.Host;
        var pathBase = _httpContextAccessor.HttpContext.Request.PathBase;
        return string.Concat(scheme, "://", host, pathBase, imageRelativePath);
    }

    private static string CreateImageName(string toBeHashed, IFormFile file) 
        => (toBeHashed + DateTime.Now).GetHashCode() + "." + file.ContentType.Split("/")[1];

    private async Task SaveImageOnHost(ImageMetadata imageMetadata, IFormFile file)
    {
        await using var fileStream = new FileStream(imageMetadata.FullPath, FileMode.Create);
        await file.CopyToAsync(fileStream);
    }
}