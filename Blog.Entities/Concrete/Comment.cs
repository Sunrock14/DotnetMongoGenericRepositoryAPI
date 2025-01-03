using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Blog.Entities.Concrete;

public class Comment
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    [BsonRepresentation(BsonType.ObjectId)]
    public string PostId { get; set; }

    [BsonRepresentation(BsonType.ObjectId)]
    public string UserId { get; set; }

    public string Content { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool IsApproved { get; set; }
}