using Microsoft.AspNetCore.Components;
using MongoDB.Bson;
using Newtonsoft.Json;
using System.Net.Http.Json;
using WebbDesignBlazorLabb3.Shared;

namespace WebbDesignBlazorLabb3.Client.Pages.OpenPages;

public partial class AddBook : ComponentBase
{
    BookDto bookToAdd = new();
    public List<BookDto> books = new();
    public int trigger = 0;
    public bool bookOk;

    public async void FetchBook()
    {
        trigger = 2;
        await GetBookInfo(bookToAdd.Isbn);
    }

    private async Task GetBookInfo(long isbn)
    {
        HttpResponseMessage response = await client.GetAsync($"https://favbooks.azurewebsites.net/GetBookInfo:{isbn}");
        response.EnsureSuccessStatusCode();
        var responseBody = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<BookDto>(responseBody);

        if (result == null)
        {
            bookOk = false;
            trigger = 1;
            StateHasChanged();
		}
        else
        {
        bookOk = true;
        bookToAdd.IsbnId = result.IsbnId;
		bookToAdd.Authors = result.Authors;
        bookToAdd.Title = result.Title;
        bookToAdd.Description = result.Description;
        bookToAdd.Pages = result.Pages;
        bookToAdd.ImageLink = result.ImageLink;
        trigger = 1;
        StateHasChanged();

        }
    }

    private async Task SubmitBook()
    {
		var response = await client.PostAsJsonAsync("Book/addBook", bookToAdd);
        trigger = 3;
	}

}
