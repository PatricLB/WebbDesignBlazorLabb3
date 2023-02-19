using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace WebbDesignBlazorLabb3.Shared;

public class BookDto
{
	public long Isbn { get; set; }
	public string Title { get; set; }
	public string Description { get; set; }
	public string Author { get; set; }
	public int Pages { get; set; }
	public int Price { get; set; }

	public string ImageLink { get; set; }

	public BookDto(long isbn, string title, string desc, string auth, int pages, int price)
	{
		Isbn = isbn;
		Title = title;
		Description = desc;
		Author = auth;
		Pages = pages;
		Price = price;
	}
	public BookDto() 
	{ 

	}


}


