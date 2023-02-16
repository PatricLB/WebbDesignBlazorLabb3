using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.ResponseCompression;
using MongoDB.Bson;
using Newtonsoft.Json;
using WebbDesignBlazorLabb3.Server.DataAccess;
using WebbDesignBlazorLabb3.Server.Hubs;
using WebbDesignBlazorLabb3.Shared;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();


builder.Services.AddScoped<IRepository<BookDto>, BookRepository>();
builder.Services.AddSignalR();

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

app.MapGet("/GetBookThumbnail", async () => {
	HttpClient client = new HttpClient();
	HttpResponseMessage response = await client.GetAsync($"https://www.googleapis.com/books/v1/volumes?q=isbn:9780141184944");
	response.EnsureSuccessStatusCode();
	var responseBody = await response.Content.ReadAsStringAsync();
	var result = JsonConvert.DeserializeObject<Root>(responseBody);

	string thumbnailParsed = string.Empty;
	foreach (var item in result.items)
	{
		thumbnailParsed = item.volumeInfo.imageLinks.thumbnail;
	}
	thumbnailParsed = thumbnailParsed.Replace("\"", "");
	return Results.Ok(thumbnailParsed);
});




// Ifall jag behöver en Hub till projectet är det klart
//app.MapHub<BookHub>("/hubs/Bookhub");

app.MapFallbackToFile("index.html");

app.Run();

public class bookTest
{
	public Dictionary<string, object> items { get; set; }
}

// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
public class ImageLinks
{
	public string smallThumbnail { get; set; }
	public string thumbnail { get; set; }
}


public class Item
{
	public VolumeInfo volumeInfo { get; set; }
}


public class Root
{
	public List<Item> items { get; set; }
}

public class VolumeInfo
{
	public ImageLinks imageLinks { get; set; }

}

