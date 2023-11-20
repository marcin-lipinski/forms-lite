using Core.Entities.Session;
using FastEndpoints;

namespace Web.Handlers.SessionHandlers.PartakeSessionFinish;

public class PartakeSessionFinishMapper : Mapper<PartakeSessionFinishRequest, EmptyResponse, SessionPartake>
{
    public override SessionPartake ToEntity(PartakeSessionFinishRequest request)
    {
        return new SessionPartake
        {
            ParticipantName = request.ParticipantName,
            Answers = request.Answers
        };
    }
}