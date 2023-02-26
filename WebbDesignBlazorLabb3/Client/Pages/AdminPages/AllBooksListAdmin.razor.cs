using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Json;
using WebbDesignBlazorLabb3.Shared;

namespace WebbDesignBlazorLabb3.Client.Pages.AdminPages;

public partial class AllBooksListAdmin : ComponentBase
{
	[CascadingParameter]
	private Task<AuthenticationState>? authenticationState { get; set; }

	string? email = string.Empty;
	AuthenticationState a;

	List<BookDto> BookList = new();
	long bookToRemove;

	protected override async Task OnParametersSetAsync()
	{
		a = await authenticationState;
		if (a.User.Identity?.IsAuthenticated ?? false)
		{
			email = a.User.FindFirst(c => c.Type == "preferred_username")?.Value;

		}
	}

	protected override async Task OnInitializedAsync()
	{
		await GetAllBooks();
	}

	private void BookToRemove(long isbn)
	{
		bookToRemove = isbn;
	}

	private async Task GetAllBooks()
	{
		BookList = (await _client.GetFromJsonAsync<IEnumerable<BookDto>>("book/getAll")).ToList();
	}

	private void GoToEdit(long isbn)
	{
		UriHelper.NavigateTo($"Editbook/{isbn}");
	}
	private async void DeleteBook(long isbn)
	{

		await _client.DeleteAsync($"book/delete/{isbn}");
		await GetAllBooks();
		StateHasChanged();
	}
}
