using FastEndpoints;

namespace Web.Features.Quiz.Create;

public class CreateQuizMapper : Mapper<CreateQuizRequest, CreateQuizResponse, Core.Entities.Quiz>
{
    public override Core.Entities.Quiz ToEntity(CreateQuizRequest r)
    {
        return new Core.Entities.Quiz{AuthorId = r.Info};
    }

    public override CreateQuizResponse FromEntity(Core.Entities.Quiz e)
    {
        return new CreateQuizResponse();
    }
}