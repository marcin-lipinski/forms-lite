namespace Web.Handlers.SessionHandlers.PartakeSessionFinish;

public class PartakeSessionFinishResponse
{
    public string QuizTitle { get; set; }
    public List<PartakeSessionSingleResult> Results { get; set; }
}

public class PartakeSessionSingleResult
{
    public string QuestionContent { get; set; }
    public List<PartakeSessionSingleResultSingle> Answers { get; set; }
    public string ParticipantAnswer { get; set; }
}

public class PartakeSessionSingleResultSingle
{
    public string Text { get; set; }
    public double Value { get; set; }
}