using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using System.Dynamic;
using WebbDesignBlazorLabb3.Shared;

namespace WebbDesignBlazorLabb3.Client.Pages;

public partial class AddBook : ComponentBase
{
    BookDto bookToAdd = new();
    public List<BookDto> books = new();
    public int trigger = 0;

    public async void BookSubmit()
    {

        //books.Clear();
        trigger = 2;
        await GetBookInfo(bookToAdd.Isbn);

		//books.Add(bookToAdd);

        //Skriv mer kod för att faktiskt lägga till i databasen.
    }

    private async Task GetBookInfo(long isbn)
    {
        HttpResponseMessage response = await client.GetAsync($"https://localhost:7294/GetBookInfo:{isbn}");
        response.EnsureSuccessStatusCode();
        var responseBody = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<BookDto>(responseBody);

        bookToAdd.Author = result.Author;
        bookToAdd.Title = result.Title;
        bookToAdd.Description = result.Description;
        bookToAdd.Pages = result.Pages;
        bookToAdd.ImageLink = result.ImageLink;
        trigger = 1;
        StateHasChanged();

    }

    private async Task GetThumbnail(long isbn)
    {

        HttpResponseMessage response = await client.GetAsync($"https://localhost:7294/GetBookThumbnail:{isbn}");
        response.EnsureSuccessStatusCode();
        var responseBody = await response.Content.ReadAsStringAsync();
        if (responseBody.ToString() == "No image available")
        {
            bookToAdd.ImageLink = "https://via.placeholder.com/128x197.png";
            Console.WriteLine(bookToAdd.ImageLink);
        }
        else
        {
            bookToAdd.ImageLink = responseBody.ToString();
            Console.WriteLine(bookToAdd.ImageLink);

        }
    }
}
