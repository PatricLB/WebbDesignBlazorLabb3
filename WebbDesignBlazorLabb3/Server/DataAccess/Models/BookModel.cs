using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace WebbDesignBlazorLabb3.Server.DataAccess.Models;

public class BookModel
{
	[BsonId]
    public long IsbnId { get; set; }
    [BsonElement]
	public long Isbn { get; set; }
    [BsonElement]
    public string Title { get; set; }
    [BsonElement]
    public string Description { get; set; }
    [BsonElement]
    public List<string> Authors { get; set; }
    [BsonElement]
    public int Pages { get; set; }
	[BsonElement]
	public string ImageLink { get; set; }

}
