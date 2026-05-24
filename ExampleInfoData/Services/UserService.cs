using ExampleInfoData.Models;
using MongoDB.Driver;

namespace ExampleInfoData.Services
{
    public class UserService
    {
        private readonly IMongoCollection<User> _usersCollection;

        public UserService(IConfiguration configuration)
        {
            var client = new MongoClient(
                configuration["MongoDbSettings:ConnectionString"]);

            var database = client.GetDatabase(
                configuration["MongoDbSettings:DatabaseName"]);

            _usersCollection = database.GetCollection<User>(
                configuration["MongoDbSettings:CollectionName"]);
        }

        public async Task<List<User>> GetAsync() =>
           await _usersCollection.Find(_ => true).ToListAsync();

        // GET BY ID
        public async Task<User?> GetByIdAsync(string id) =>
            await _usersCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        // CREATE
        public async Task CreateAsync(User user) =>
            await _usersCollection.InsertOneAsync(user);

        // UPDATE
        public async Task UpdateAsync(string id, User user) =>
            await _usersCollection.ReplaceOneAsync(x => x.Id == id, user);

        // DELETE
        public async Task DeleteAsync(string id) =>
            await _usersCollection.DeleteOneAsync(x => x.Id == id);
    }
}
