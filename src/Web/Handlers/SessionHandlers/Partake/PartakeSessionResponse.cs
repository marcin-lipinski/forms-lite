using Web.Handlers.QuizHandlers.GetAll;

namespace Web.Handlers.SessionHandlers.Partake;

public class PartakeSessionResponse
{
    public QuizDto Quiz { get; set; } = null!;
}