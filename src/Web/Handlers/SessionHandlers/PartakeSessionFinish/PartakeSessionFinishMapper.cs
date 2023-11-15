using Core.Entities.Session;
using FastEndpoints;

namespace Web.Handlers.SessionHandlers.PartakeResult;

public class PartakeSessionFinishMapper : Mapper<PartakeSessionFinishRequest, EmptyResponse, SessionPartake>
{
    public override SessionPartake ToEntity(PartakeSessionFinishRequest request)
    {
        return new SessionPartake
        {
            ParticipantName = request.Participant,
            Answers = request.Answers
        };
    }
}