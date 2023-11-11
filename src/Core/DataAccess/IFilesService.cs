using Core.Entities;

namespace Core.DataAccess;

public interface IFilesService
{
    string CreateImageUrl(string imageRelativePath);
    Task<Image> SaveImage(string quizTitle, int questionNr, IFormFile image);
}