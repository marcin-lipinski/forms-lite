using Core.Entities.Quiz;
using Core.Entities.Session;
using Core.Entities.User;

namespace Infrastructure.Settings;

public class MongoSettings
{
    public string ConnectionString { get; set; } = null!;
    public string DatabaseName { get; set; } = null!;

    public MongoCollectionsNames CollectionsNames { get; set; } = null!;
    
    public Dictionary<string, string> CollectionName =>
        new()
        {
            { nameof(User), CollectionsNames.User },
            { nameof(Quiz), CollectionsNames.Quiz },
            { nameof(Session), CollectionsNames.Session }
        };
}

public class MongoCollectionsNames
{
    public string User { get; set; } = null!;
    public string Quiz { get; set; } = null!;
    public string Session { get; set; } = null!;
}