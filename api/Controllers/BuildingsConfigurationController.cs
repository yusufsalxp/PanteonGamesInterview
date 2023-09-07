using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[Route("[controller]/[action]")]
[ApiController]
public class BuildingsConfigurationController : ControllerBase
{
    private readonly IBuildingsConfigurationService _buildingsConfigurationService;

    public BuildingsConfigurationController(
        IBuildingsConfigurationService buildingsConfigurationService
        )
    {
        _buildingsConfigurationService = buildingsConfigurationService;
    }
}