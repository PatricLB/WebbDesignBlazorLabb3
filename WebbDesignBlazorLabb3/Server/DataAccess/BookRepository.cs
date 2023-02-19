using MongoDB.Bson;
using MongoDB.Driver;
using WebbDesignBlazorLabb3.Server.DataAccess.Models;
using WebbDesignBlazorLabb3.Shared;
using static WebbDesignBlazorLabb3.Client.Pages.BooksPage;

namespace WebbDesignBlazorLabb3.Server.DataAccess;

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
            Isbn = entity.Isbn,
            Title = entity.Title,
            Author = entity.Author,
            Description = entity.Description,
            Pages = entity.Pages,
            Price = entity.Price,
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
                Isbn = b.Isbn,
                Author = b.Author,
                Title = b.Title,
                Description = b.Description,
                Pages = b.Pages,
                Price = b.Price,
				ImageLink = b.ImageLink
			});
    }

    public async Task DeleteAsync(object id)
    {
		var deleteFilter = Builders<BookModel>.Filter.Eq("Isbn", id);
		await _bookCollection.DeleteOneAsync(deleteFilter);
	}


    public Task<BookDto> GetAsync(object id)
    {
        throw new NotImplementedException();
    }

    public Task<BookDto> UpdateAsync(BookDto entity)
    {
        throw new NotImplementedException();
    }
}
