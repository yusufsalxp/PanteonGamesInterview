public interface IBuildingsConfigurationService
{

    public Task<List<BuildingsConfiguration>> GetAll();

    public Task<BuildingsConfiguration?> GetById(string id);

    public Task<BuildingsConfiguration> Create(BuildingsConfigurationCreateDto dto);

    public Task<BuildingsConfiguration> Update(string id, BuildingsConfigurationUpdateDto dto);

    public Task Delete(string id);
}