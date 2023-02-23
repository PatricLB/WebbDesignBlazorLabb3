using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json;
using SharpCompress.Common;
using WebbDesignBlazorLabb3.Server.DataAccess.Models;
using WebbDesignBlazorLabb3.Shared;
using static WebbDesignBlazorLabb3.Client.Pages.OpenPages.BooksPage;

namespace WebbDesignBlazorLabb3.Server.DataAccess.Repositories;

public class BookRepository : IRepository<BookDto>
{

    private readonly IMongoCollection<BookModel> _bookCollection;

    public BookRepository()
    {
        var host = "localhost";
        var databaseName = "Books";
        var connectionString = $"mongodb://{host}:27017";

        var client = new MongoClient(connectionString);
        var database = client.GetDatabase(databaseName);
        _bookCollection = database.GetCollection<BookModel>
            ("Books");
    }


    public async Task AddAsync(BookDto entity)
    {
        await _bookCollection.InsertOneAsync(new BookModel()
        {
			IsbnId = entity.Isbn,
            Isbn = entity.Isbn,
            Title = entity.Title,
            Authors = entity.Authors,
            Description = entity.Description,
            Pages = entity.Pages,
            ImageLink = entity.ImageLink
        });
    }
    public async Task<IEnumerable<BookDto>> GetAllAsync()
    {
        var filter = Builders<BookModel>.Filter.Empty;
        var allBooks = await _bookCollection.FindAsync(filter);

        return allBooks.ToEnumerable()
            .Select(b => new BookDto()
            {
                IsbnId = b.IsbnId,
                Isbn = b.Isbn,
                Authors = b.Authors,
                Title = b.Title,
                Description = b.Description,
                Pages = b.Pages,
                ImageLink = b.ImageLink
            });
    }

    public async Task DeleteAsync(long id)
    {
        var deleteFilter = Builders<BookModel>.Filter.Eq("Isbn", id);
        await _bookCollection.DeleteOneAsync(deleteFilter);
    }

	public async Task UpdateAsync(BookDto entity)
    {
		var updateFilter = Builders<BookModel>.Filter.Eq("Isbn", entity.Isbn);
        var updateBook = Builders<BookModel>.Update
            .Set("Title", entity.Title)
            .Set("Description", entity.Description)
            .Set("Authors", entity.Authors)
            .Set("Pages", entity.Pages)
            .Set("ImageLink", entity.ImageLink);

         await _bookCollection.UpdateOneAsync(updateFilter, updateBook);

	}

    public async Task<BookDto> GetAsync(long isbn)
    {
		var book = await _bookCollection.Find(x => x.Isbn == isbn).FirstOrDefaultAsync();

        var bookToString = JsonConvert.SerializeObject(book);
        var stringToBookDto = JsonConvert.DeserializeObject<BookDto>(bookToString);

		return stringToBookDto;
	}

}
