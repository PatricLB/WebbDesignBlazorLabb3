using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;
using WebbDesignBlazorLabb3.Shared;

namespace WebbDesignBlazorLabb3.Client.Pages.OpenPages;

public partial class BookInfoPage : ComponentBase
{
	BookDto currentBook = new();
	[Parameter]
	public string ISBN { get; set; }
	public long handeledIsbn { get; set; }

	[CascadingParameter]
	private Task<AuthenticationState>? authenticationState { get; set; }

	string? email = string.Empty;
	AuthenticationState a;
	UserBookListDto currentUserList = new();

	protected override async Task OnInitializedAsync()
	{
		if (ISBN != null)
		{
			handeledIsbn = (long)Convert.ToInt64(ISBN);
			currentBook.Isbn = handeledIsbn;

			var fetchedBook = (await _client.GetFromJsonAsync<BookDto>($"Book/getBook:{currentBook.Isbn}"));

			currentBook.Title = fetchedBook.Title;
			currentBook.Authors = fetchedBook.Authors;
			currentBook.Description = fetchedBook.Description;
			currentBook.Pages = fetchedBook.Pages;
			currentBook.ImageLink = fetchedBook.ImageLink;
		}
		else
		{

		}
	}
	protected override async Task OnParametersSetAsync()
	{
		a = await authenticationState;
		if (a.User.Identity?.IsAuthenticated ?? false)
		{
			email = a.User.FindFirst(c => c.Type == "preferred_username")?.Value;
			if (!string.IsNullOrEmpty(email))
			{

			}

			await LoadList();
		}

	}

	private async Task LoadList()
	{
		var fetchedList = (await _client.GetFromJsonAsync<UserBookListDto>($"/BookLists/GetList:{email}"));
		currentUserList = fetchedList;
	}

	private async void AddBookToList(long isbn, string email)
	{
		if (currentUserList != null)
		{
			currentUserList.Content.Add(isbn);
			await _client.PutAsJsonAsync("/BookLists/AddBookToList", currentUserList);
		}

	}


	private void SendUserBack()
	{
		UriHelper.NavigateTo($"/BooksPage");
	}
}
