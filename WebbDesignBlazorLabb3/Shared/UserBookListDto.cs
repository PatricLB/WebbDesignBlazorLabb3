namespace WebbDesignBlazorLabb3.Shared;

public class UserBookListDto
{
	public string Id { get; set; } = string.Empty;
	public List<BookListDto> Lists { get; set; } = new ();

	public class BookListDto
	{
		public string Name { get; set; } = string.Empty;
		public string Description { get; set; } = string.Empty;
		public List<long> Content { get; set; } = new();
		public BookListDto(string name, string desc, List<long> content)
		{
			Name = name;
			Description = desc;
			Content = content;

		}
		public BookListDto()
		{

		}
	}

	public UserBookListDto(string id, List<BookListDto> bookList)
	{
		Id = id;
		Lists = bookList;
	}
	public UserBookListDto() { }


}
