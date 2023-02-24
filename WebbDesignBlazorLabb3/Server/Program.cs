using Newtonsoft.Json;
using WebbDesignBlazorLabb3.Server.DataAccess;
using WebbDesignBlazorLabb3.Server.DataAccess.Repositories;
using WebbDesignBlazorLabb3.Shared;
using static WebbDesignBlazorLabb3.Server.DataAccess.ParseBookBaseClass;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();


builder.Services.AddScoped<IRepository<BookDto>, BookRepository>();
builder.Services.AddScoped<IRepository<UserBookListDto>, UserBookListRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseWebAssemblyDebugging();
}
else
{
	app.UseExceptionHandler("/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();
app.MapRazorPages();


//API funktioner

app.MapPost("/Booklists/AddList", async (IRepository<UserBookListDto> listRep, UserBookListDto UserBookListDto) =>
{
    await listRep.AddAsync(UserBookListDto);
});

app.MapGet("/BookLists/GetList:{email}", async (IRepository<UserBookListDto> listRep, string email) =>
{
    return await listRep.GetAsync(email);
});

app.MapGet("/Book/getAll", async (IRepository<BookDto> bookRep) =>
{
    return await bookRep.GetAllAsync();
});
app.MapGet("/Book/getBook:{isbn}", async (IRepository<BookDto> bookRep, long isbn) =>
{
    return await bookRep.GetAsync(isbn);
});

app.MapPost("/Book/addBook", async (IRepository<BookDto> bookRep, BookDto addedBook) =>
{
    await bookRep.AddAsync(addedBook);
});
app.MapPut("/Book/UpdateBook", async (IRepository<BookDto> bookRep, BookDto book) =>
{
    await bookRep.UpdateAsync(book);
});

app.MapDelete("book/delete/{isbn}", async (IRepository<BookDto> bookRep, long isbn) =>
{
    await bookRep.DeleteAsync(isbn);
});


app.MapGet("/GetBookInfo:{isbn}", async (long isbn) =>
{
    HttpClient client = new HttpClient();
    HttpResponseMessage response = await client.GetAsync($"https://www.googleapis.com/books/v1/volumes?q=isbn:{isbn}");
    response.EnsureSuccessStatusCode();
    var responseBody = await response.Content.ReadAsStringAsync();
    var result = JsonConvert.DeserializeObject<Root>(responseBody);
    BookDto? returnBook = null;

    if (result.TotalItems == 0)
        return returnBook;

    returnBook = new();


    returnBook.Title = result.items[0].volumeInfo.title;

	if (result.items[0].volumeInfo.authors == null)
        returnBook.Authors.Add("No data about the author");
    else
        foreach (var author in result.items[0].volumeInfo.authors)
        {
            returnBook.Authors.Add(author);
		}

    returnBook.Pages = result.items[0].volumeInfo.pageCount;

    if (result.items[0].volumeInfo.description != null)
        returnBook.Description = result.items[0].volumeInfo.description;
    else if (result.items[0].searchInfo != null)
		returnBook.Description = result.items[0].searchInfo.textSnippet;
    else
        returnBook.Description = "No description available.";


    if (result.items[0].volumeInfo.imageLinks != null)
        returnBook.ImageLink = result.items[0].volumeInfo.imageLinks.thumbnail;
    else
        returnBook.ImageLink = "https://via.placeholder.com/128x197.png";

    return returnBook;

});

// Ifall jag behöver en Hub till projectet är det klart
//app.MapHub<BookHub>("/hubs/Bookhub");

app.MapFallbackToFile("index.html");

app.Run();

