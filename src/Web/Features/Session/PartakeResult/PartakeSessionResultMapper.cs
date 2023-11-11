using Core.Entities;
using FastEndpoints;
using Web.Features.Quiz.Create;

namespace Web.Features.Session.PartakeResult;

public class PartakeSessionResultMapper : Mapper<PartakeSessionResultRequest, EmptyResponse, SessionAnswer>
{
    public override SessionAnswer ToEntity(PartakeSessionResultRequest request)
    {
        return new SessionAnswer
        {
            ParticipantName = request.Participan,
            Answers = request.Answers
        };
    }
}