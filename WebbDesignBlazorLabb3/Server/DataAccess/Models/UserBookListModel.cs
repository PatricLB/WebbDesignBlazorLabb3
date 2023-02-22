namespace WebbDesignBlazorLabb3.Server.DataAccess.Models;

public class UserBookListModel
{
	public string Id { get; set; }
	public List<BookList> Lists { get; set; }

	public class BookList
	{
		public string Name { get; set; }
		public string Description { get; set; }
		public List<long> Content { get; set; }
	}



}
