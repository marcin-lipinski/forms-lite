using Core.Entities.Image;

namespace Services.Interfaces;

public interface IFilesService
{
    string? CreateImageUrl(string imageRelativePath);
    Task<ImageMetadata> SaveImage(string quizTitle, int questionNr, IFormFile image);
}