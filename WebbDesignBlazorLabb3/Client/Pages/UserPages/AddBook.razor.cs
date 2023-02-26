using Microsoft.AspNetCore.Components;
using MongoDB.Bson;
using Newtonsoft.Json;
using System.Net.Http.Json;
using WebbDesignBlazorLabb3.Shared;

namespace WebbDesignBlazorLabb3.Client.Pages.UserPages;

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
        var response = await _client.GetFromJsonAsync<BookDto>($"/GetBookInfo:{isbn}");

        if (response == null)
        {
            bookOk = false;
            trigger = 1;
            StateHasChanged();
        }
        else
        {
            bookOk = true;
            bookToAdd.IsbnId = response.IsbnId;
            bookToAdd.Authors = response.Authors;
            bookToAdd.Title = response.Title;
            bookToAdd.Description = response.Description;
            bookToAdd.Pages = response.Pages;
            bookToAdd.ImageLink = response.ImageLink;
            trigger = 1;
            StateHasChanged();

        }
    }

    private async Task SubmitBook()
    {
        await _client.PostAsJsonAsync("Book/addBook", bookToAdd);
        trigger = 3;
    }

}
