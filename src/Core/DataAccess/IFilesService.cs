using Core.Entities;

namespace Core.DataAccess;

public interface IFilesService
{
    string CreateImageUrl(string imageRelativePath);
    Task<Image> SaveImage(string quizId, string questionId, IFormFile image);
}