using MongoDB.Driver;
using WebbDesignBlazorLabb3.Shared;
using Microsoft.AspNetCore.SignalR;
using WebbDesignBlazorLabb3.Server.DataAccess.Repositories;
using WebbDesignBlazorLabb3.Server.DataAccess;

namespace WebbDesignBlazorLabb3.Server.Hubs;

public class BookHub
{
	private readonly IRepository<BookDto> _bookRepository;

	public BookHub(IRepository<BookDto> bookRepository)
	{
		_bookRepository = bookRepository;
	}


	public async Task GetBook(BookDto book)
	{
		await _bookRepository.GetAsync(book);

		//Skicka till alla klienter som lyssnar på sidan. Ifall det behövs så finns det här
		//await Client.All.SendAsync("SendBook", book);
	}
}
