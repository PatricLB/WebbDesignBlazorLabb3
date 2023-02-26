using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;
using WebbDesignBlazorLabb3.Shared;

namespace WebbDesignBlazorLabb3.Client.Pages.OpenPages;

public partial class BooksPage :ComponentBase
{
	[CascadingParameter]
	private Task<AuthenticationState>? authenticationState { get; set; }
	AuthenticationState a;
	string? email = string.Empty;

	List<BookDto> BookList = new();

	UserBookListDto currentUserList = new();

	protected override async Task OnInitializedAsync()
	{
		await GetAllBooks();
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


	private async Task GetAllBooks()
	{
		BookList = (await _client.GetFromJsonAsync<IEnumerable<BookDto>>("book/getAll")).ToList();
	}

	private void GoToBookView(long isbn)
	{
		UriHelper.NavigateTo($"Book/{isbn}");
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
}
