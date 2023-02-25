using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Newtonsoft.Json;
using SharpCompress.Common;
using WebbDesignBlazorLabb3.Server.DataAccess.Models;
using WebbDesignBlazorLabb3.Shared;

namespace WebbDesignBlazorLabb3.Server.DataAccess.Repositories;

public class UserBookListRepository : IRepository<UserBookListDto>
{

	private readonly IMongoCollection<UserBookListModel> _bookListCollection;

	public UserBookListRepository()
	{
		var host = "localhost";
		var databaseName = "Books";
		//var connectionString = $"mongodb://{host}:27017";
		var connectionString = "mongodb+srv://AzureAccount:QBYPavOXnDo04zu3@cluster0.i51me48.mongodb.net/?retryWrites=true&w=majority";

		var client = new MongoClient(connectionString);
		var database = client.GetDatabase(databaseName);
		_bookListCollection = database.GetCollection<UserBookListModel>
			("UserBookLists");
	}

	public async Task AddAsync(UserBookListDto entity)
	{
		UserBookListModel dtoConverted = JsonConvert.DeserializeObject<UserBookListModel>(JsonConvert.SerializeObject(entity));
		await _bookListCollection.InsertOneAsync(new UserBookListModel()
		{
			Id = dtoConverted.Id,
			Email = dtoConverted.Email,
			Name = dtoConverted.Name,
			Description= dtoConverted.Description,
			Content = dtoConverted.Content
		});;
	}
	public async Task<IEnumerable<UserBookListDto>> GetAllAsync()
	{
		var filter = Builders<UserBookListModel>.Filter.Empty;
		var allBooks = await _bookListCollection.FindAsync(filter);

		return allBooks.ToEnumerable()
			.Select(b => new UserBookListDto()
			{
				Id= b.Id,
				Name = b.Name,
				Description = b.Description,
				Content = b.Content
			});
	}
	public async Task UpdateAsync(UserBookListDto entity)
	{
		var updateFilter = Builders<UserBookListModel>.Filter.Eq("_id", entity.Id);
		var updateBooksInList = Builders<UserBookListModel>.Update.Push("Content", entity.Content[0]);

		await _bookListCollection.UpdateOneAsync(updateFilter, updateBooksInList);
	}
	public async Task<UserBookListDto> GetAsync(string email)
	{
		var list = await _bookListCollection.Find(x => x.Email == email).FirstOrDefaultAsync();

		var listToString = JsonConvert.SerializeObject(list);
		var stringToListDto = JsonConvert.DeserializeObject<UserBookListDto>(listToString);

		return stringToListDto;
	}

	public Task DeleteAsync(long id)
	{
		throw new NotImplementedException();
	}


	public Task<UserBookListDto> GetAsync(long isbn)
	{
		throw new NotImplementedException();
	}


	public Task DeleteAsync(object id)
	{
		throw new NotImplementedException();
	}
}
