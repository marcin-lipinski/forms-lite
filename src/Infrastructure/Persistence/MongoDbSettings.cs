using Core.Entities;

namespace Infrastructure.Persistence;

public class MongoDbSettings
{
    public string ConnectionString { get; set; } = null!;
    public string DatabaseName { get; set; } = null!;

    public MongoCollections CollectionsNames { get; set; } = null!;
    
    public Dictionary<string, string> CollectionName =>
        new()
        {
            { nameof(User), CollectionsNames.User },
            { nameof(Quiz), CollectionsNames.Quiz },
            { nameof(Session), CollectionsNames.Session }
        };
}

public class MongoCollections
{
    public string User { get; set; } = null!;
    public string Quiz { get; set; } = null!;
    public string Session { get; set; } = null!;
}