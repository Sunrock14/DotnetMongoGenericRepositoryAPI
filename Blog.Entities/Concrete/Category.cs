using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Blog.Entities.Concrete;

public class Category
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    public string Name { get; set; }
    public string Slug { get; set; }
    public string Description { get; set; }
}