using System.Xml.Linq;

namespace WebbDesignBlazorLabb3.Shared;

public class UserBookListDto
{
	public string Id { get; set; } = string.Empty;
	public string Email { get; set; } = string.Empty;
	public string Name { get; set; } = string.Empty;
	public string Description { get; set; } = string.Empty;
	public List<string> Content { get; set; }


	public UserBookListDto(string id,string email, string name, string desc, List<string> content )
	{
		Id = id;
		Email = email;
		Name = name;
		Description = desc;
		Content = content;
	}
	public UserBookListDto()
	{

	}


}
