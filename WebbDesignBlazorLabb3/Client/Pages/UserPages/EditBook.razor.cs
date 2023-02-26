using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;
using WebbDesignBlazorLabb3.Shared;

namespace WebbDesignBlazorLabb3.Client.Pages.UserPages;

public partial class EditBook : ComponentBase
{
	[Parameter]
	public string ISBN { get; set; }
	public bool doneLoading { get; set; }
	BookDto bookToEdit = new();
	public long handeledIsbn;

	private int trigger;


	protected override async Task OnInitializedAsync()
	{
		handeledIsbn = (long)Convert.ToInt64(ISBN);
		bookToEdit.Isbn = handeledIsbn;
		var fetchedBook = (await client.GetFromJsonAsync<BookDto>($"Book/getBook:{bookToEdit.Isbn}"));
		bookToEdit.Title = fetchedBook.Title;
		bookToEdit.Authors = fetchedBook.Authors;
		bookToEdit.Description = fetchedBook.Description;
		bookToEdit.Pages = fetchedBook.Pages;
		bookToEdit.ImageLink = fetchedBook.ImageLink;

		trigger = 0;
	}

	private async void UpdateBook()
	{
		var response = (await client.PutAsJsonAsync($"Book/UpdateBook", bookToEdit));
		if (response.IsSuccessStatusCode)
		{
			trigger = 1;
			StateHasChanged();
			await Task.Delay(2500);
			doneLoading = true;
			StateHasChanged();
			await Task.Delay(1000);
			UriHelper.NavigateTo($"/AllBooksList");
		}
		else
		{
			trigger = 2;
			StateHasChanged();
		}

	}

	private void SendUserBack()
	{
		UriHelper.NavigateTo($"/AllBooksList");
	}


}
