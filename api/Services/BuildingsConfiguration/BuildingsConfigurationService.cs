using AutoMapper;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

public class BuildingsConfigurationService : IBuildingsConfigurationService
{
    private readonly IMongoCollection<BuildingsConfiguration> _buildingsConfigurationCollection;
    private readonly IMapper _mapper;

    public BuildingsConfigurationService(
        IOptions<BuildingsConfigurationsDatabaseSettings> buildingsConfigurationsDatabaseSettings,
        IMapper mapper
    )
    {
        var mongoClient = new MongoClient(
            buildingsConfigurationsDatabaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            buildingsConfigurationsDatabaseSettings.Value.DatabaseName);

        _buildingsConfigurationCollection = mongoDatabase.GetCollection<BuildingsConfiguration>(
            buildingsConfigurationsDatabaseSettings.Value.BooksCollectionName);

        _mapper = mapper;
    }

    public async Task<List<BuildingsConfiguration>> GetAll()
    {
        var result = await _buildingsConfigurationCollection.Find(_ => true).ToListAsync();
        return result;
    }

    public async Task<BuildingsConfiguration?> GetById(string id)
    {
        return await _buildingsConfigurationCollection.Find(x => x.Id == new ObjectId(id)).FirstOrDefaultAsync();
    }

    public async Task<BuildingsConfiguration> Create(BuildingsConfigurationCreateDto dto)
    {
        var model = _mapper.Map<BuildingsConfiguration>(dto);
        await _buildingsConfigurationCollection.InsertOneAsync(model);

        return model;
    }

    public async Task<BuildingsConfiguration> Update(string id, BuildingsConfigurationUpdateDto dto)
    {
        var model = await GetById(id);

        if (model == null)
        {
            throw new Exception("There is no model!");
        }

        if (dto.Type != null)
        {
            model.Type = (BuildingType)dto.Type;
        }


        if (dto.BuildingCost != null)
        {
            model.BuildingCost = (decimal)dto.BuildingCost;
        }


        if (dto.ConstructionTime != null)
        {
            model.ConstructionTime = (int)dto.ConstructionTime;
        }

        await _buildingsConfigurationCollection.ReplaceOneAsync(x => x.Id == model.Id, model);

        return model;
    }

    public async Task Delete(string id)
    {
        await _buildingsConfigurationCollection.DeleteOneAsync(x => x.Id == new ObjectId(id));
    }
}