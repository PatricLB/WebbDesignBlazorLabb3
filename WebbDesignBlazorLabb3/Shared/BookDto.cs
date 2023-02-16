using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebbDesignBlazorLabb3.Shared;

public class BookDto
{
	public int _isbn { get; set; }
	public string _title { get; set; }
	public string _Description { get; set; }
	public string _author { get; set; }
	public int _pages { get; set; }
	public int _price { get; set; }

	public BookDto(int isbn, string title, string desc, string auth, int pages, int price)
	{
		_isbn = isbn;
		_title = title;
		_Description = desc;
		_author = auth;
		_pages = pages;
		_price = price;
	}
	public BookDto() 
	{ 

	}


}


