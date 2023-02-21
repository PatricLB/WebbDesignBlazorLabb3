namespace WebbDesignBlazorLabb3.Server.DataAccess.Models;

public class UserBookListModel
{
	public string Id { get; set; }
	public List<List> Lists { get; set; }


	public class List
	{
		public string Name { get; set; }
		public string Description { get; set; }
		public List<Content> Content { get; set; }
	}
	public class Content
	{
		public string ISBN { get; set; }
	}


}
