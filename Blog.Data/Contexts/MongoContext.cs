using Blog.Entities.Concrete;
using Blog.Entities.Configuration;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Blog.Data.Contexts;

public class MongoContext
{
    private readonly IMongoDatabase _database;

    public IMongoCollection<Post> BlogPosts => _database.GetCollection<Post>("BlogPosts");
    public IMongoCollection<Comment> Comments => _database.GetCollection<Comment>("Comments");
    public IMongoCollection<User> Users => _database.GetCollection<User>("Users");

    public MongoContext(IOptions<MongoDbSettings> settings)
    {
        var client = new MongoClient(settings.Value.ConnectionString);
        _database = client.GetDatabase(settings.Value.DatabaseName);
    }

    public IMongoCollection<T> GetCollection<T>(string name)
    {
        return _database.GetCollection<T>(name);
    }
}