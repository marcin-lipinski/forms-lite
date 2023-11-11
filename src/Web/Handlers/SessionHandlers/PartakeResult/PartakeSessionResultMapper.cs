using Core.Entities.Session;
using FastEndpoints;

namespace Web.Handlers.SessionHandlers.PartakeResult;

public class PartakeSessionResultMapper : Mapper<PartakeSessionResultRequest, EmptyResponse, SessionPartake>
{
    public override SessionPartake ToEntity(PartakeSessionResultRequest request)
    {
        return new SessionPartake
        {
            ParticipantName = request.Participan,
            Answers = request.Answers
        };
    }
}