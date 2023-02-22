using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace WebbDesignBlazorLabb3.Shared;

public class BookDto
{
	public long Isbn { get; set; } = 0;
	public string Title { get; set; } = string.Empty;
	public string Description { get; set; } = string.Empty;
	public List<string> Authors { get; set; } = new List<string>();
	public int Pages { get; set; } = 0;
	public string ImageLink { get; set; } = string.Empty;

	public BookDto(long isbn, string title, string desc, string auth, int pages, string imgLink)
	{
		Isbn = isbn;
		Title = title;
		Description = desc;
		Authors.Add(auth);
		Pages = pages;
		ImageLink = imgLink;
	}
	public BookDto() 
	{ 

	}


}


