using Core.Entities.Image;

namespace Services.Interfaces;

public interface IFilesService
{
    string? CreateImageUrl(string imageRelativePath);
    Task<ImageMetadata> SaveImage(string quizTitle, string questionId, IFormFile image);
}