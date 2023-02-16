using MongoDB.Driver;
using WebbDesignBlazorLabb3.Server.DataAccess.Models;
using WebbDesignBlazorLabb3.Shared;

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
            ("Books", new() { AssignIdOnInsert = true });
    }


    public async Task AddAsync(BookDto entity)
    {
        await _bookCollection.InsertOneAsync(new BookModel()
        {
            Isbn = entity._isbn,
            Title = entity._title,
            Author = entity._author,
            Description = entity._Description,
            Pages = entity._pages,
            Price = entity._price

        });
    }
    public async Task<IEnumerable<BookDto>> GetAllAsync()
    {
        var filter = Builders<BookModel>.Filter.Empty;
        var allBooks = await _bookCollection.FindAsync(filter);

        return allBooks.ToEnumerable()
            .Select(b => new BookDto()
            {
                _isbn = b.Isbn,
                _author = b.Author,
                _title = b.Title,
                _Description = b.Description,
                _pages = b.Pages,
                _price = b.Price,
            });
    }

    public Task DeleteAsync(object id)
    {
        throw new NotImplementedException();
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
