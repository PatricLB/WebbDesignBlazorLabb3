using MongoDB.Bson.Serialization.Attributes;

namespace WebbDesignBlazorLabb3.Server.DataAccess.Models;

public class UserBookListModel
{
	[BsonId]
	public string Id { get; set; }
	[BsonElement]
	public string Email { get; set; }
	[BsonElement]
	public string Name { get; set; }
	[BsonElement]
	public string Description { get; set; }
	[BsonElement]
	public List<string> Content { get; set; }

}
