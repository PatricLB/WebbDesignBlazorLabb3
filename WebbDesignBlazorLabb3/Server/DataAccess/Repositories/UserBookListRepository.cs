using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Newtonsoft.Json;
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
		var connectionString = $"mongodb://{host}:27017";

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
			Lists = dtoConverted.Lists
		}) ;
	}
	public async Task<UserBookListDto> UpdateAsync(UserBookListDto entity)
	{
		UserBookListModel dtoConverted = JsonConvert.DeserializeObject<UserBookListModel>(JsonConvert.SerializeObject(entity));

		throw new NotImplementedException();
	}

	public Task DeleteAsync(object id)
	{
		throw new NotImplementedException();
	}

	public Task<IEnumerable<UserBookListDto>> GetAllAsync()
	{
		throw new NotImplementedException();
	}

	public Task<UserBookListDto> GetAsync(object id)
	{
		throw new NotImplementedException();
	}

}
