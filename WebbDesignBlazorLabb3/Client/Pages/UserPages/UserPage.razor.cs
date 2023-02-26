using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Json;
using WebbDesignBlazorLabb3.Shared;

namespace WebbDesignBlazorLabb3.Client.Pages.UserPages;

public partial class UserPage : ComponentBase
{

	[CascadingParameter]
	private Task<AuthenticationState>? authenticationState { get; set; }

	string? email = string.Empty;
	AuthenticationState a;
	string profileName;
	bool listExists = false;
	bool loadingList = true;
	bool isListEditable = false;

	BookDto recievedBook;
	List<BookDto> booksInList = new();
	UserBookListDto newUserList = new();
	UserBookListDto currentUserList = new();


	protected override async Task OnParametersSetAsync()
	{
		a = await authenticationState;
		if (a.User.Identity?.IsAuthenticated ?? false)
		{
			email = a.User.FindFirst(c => c.Type == "preferred_username")?.Value;
			if (!string.IsNullOrEmpty(email))
			{
				profileName = a.User.Identity?.Name.Replace(" - CDA22 GBG", "'s");
			}
		}
		await LoadBooks();

		StateHasChanged();
	}
	private void ToggleEdit()
	{
		if (isListEditable)
		{
			isListEditable = false;

		}
		else
		{
			isListEditable = true;
		}
		StateHasChanged();
	}

	private void SendUserToBookPage()
	{
		UriHelper.NavigateTo("/BooksPage");
	}

	private async Task RemoveBookFromList(long isbn)
	{
		await _client.DeleteAsync($"BookLists/RemoveBookFromList/{currentUserList.Email}/{isbn}");
		await LoadBooks();
		StateHasChanged();

	}


	private async Task CreateNewList()
	{
		newUserList.Id = email;
		newUserList.Email = email;
		newUserList.Content = new List<long>();
		await _client.PostAsJsonAsync("Booklists/AddList", newUserList);
		await LoadBooks();
		StateHasChanged();
	}

	private async Task LoadList()
	{
		var fetchedList = (await _client.GetFromJsonAsync<UserBookListDto>($"/BookLists/GetList:{email}"));
		currentUserList = fetchedList;
	}

	private async Task LoadBooks()
	{
		booksInList.RemoveRange(0, booksInList.Count);
		BookDto fetchedBook;
		await LoadList();

		if (currentUserList == null)
		{
			listExists = false;
			loadingList = false;
		}
		else
		{
			loadingList = true;
			listExists = true;
			foreach (var book in currentUserList.Content)
			{
				recievedBook = new();
				fetchedBook = (await _client.GetFromJsonAsync<BookDto>($"Book/getBook:{book}"));
				recievedBook.Isbn = fetchedBook.Isbn;
				recievedBook.Title = fetchedBook.Title;
				recievedBook.Authors = fetchedBook.Authors;
				recievedBook.Description = fetchedBook.Description;
				recievedBook.Pages = fetchedBook.Pages;
				recievedBook.ImageLink = fetchedBook.ImageLink;
				booksInList.Add(recievedBook);
			}
			loadingList = false;
		}
	}
}
