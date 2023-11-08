using FastEndpoints;

namespace Server.Application.Services.Quiz.Create;

public class CreateQuizMapper : Mapper<CreateQuizRequest, CreateQuizResponse, Domain.Entities.Quiz>
{
    public override Domain.Entities.Quiz ToEntity(CreateQuizRequest r)
    {
        return base.ToEntity(r);
    }

    public override CreateQuizResponse FromEntity(Domain.Entities.Quiz e)
    {
        return base.FromEntity(e);
    }
}