using Microsoft.Extensions.Options;
using MongoDB.Driver;

public class BuildingsConfigurationService : IBuildingsConfigurationService
{
    private readonly IMongoCollection<BuildingsConfiguration> _buildingsConfigurationCollection;

    public BuildingsConfigurationService(
        IOptions<BuildingsConfigurationsDatabaseSettings> buildingsConfigurationsDatabaseSettings)
    {
        var mongoClient = new MongoClient(
            buildingsConfigurationsDatabaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            buildingsConfigurationsDatabaseSettings.Value.DatabaseName);

        _buildingsConfigurationCollection = mongoDatabase.GetCollection<BuildingsConfiguration>(
            buildingsConfigurationsDatabaseSettings.Value.BooksCollectionName);
    }

    public async Task<List<BuildingsConfiguration>> GetAsync() =>
        await _buildingsConfigurationCollection.Find(_ => true).ToListAsync();

    public async Task<BuildingsConfiguration?> GetAsync(string id) =>
        await _buildingsConfigurationCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(BuildingsConfiguration newBuildingsConfiguration) =>
        await _buildingsConfigurationCollection.InsertOneAsync(newBuildingsConfiguration);

    public async Task UpdateAsync(string id, BuildingsConfiguration updatedBuildingsConfiguration) =>
        await _buildingsConfigurationCollection.ReplaceOneAsync(x => x.Id == id, updatedBuildingsConfiguration);

    public async Task RemoveAsync(string id) =>
        await _buildingsConfigurationCollection.DeleteOneAsync(x => x.Id == id);
}