using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Blog.Entities.Concrete;

public class Post
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public ObjectId Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime? UpdatedAt { get; set; } = DateTime.Now;
    public bool IsPublished { get; set; } = true;
    public DateTime? PublishedAt { get; set; } = DateTime.Now;
    public string FeaturedImageUrl { get; set; } = string.Empty;

    public List<Category> Categories { get; set; } = new List<Category>();
    public List<Comment> Comments { get; set; } = new List<Comment>();
    public List<string> Tags { get; set; } = new List<string>();
    public List<string> ImageUrls { get; set; } = new List<string>();

    [BsonRepresentation(BsonType.ObjectId)]
    public string AuthorId { get; set; } = string.Empty;
}