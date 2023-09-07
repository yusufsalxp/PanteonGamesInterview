using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[Route("[controller]")]
[ApiController]
[Produces("application/json")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class BuildingsConfigurationController : ControllerBase
{
    private readonly IBuildingsConfigurationService _buildingsConfigurationService;

    public BuildingsConfigurationController(
        IBuildingsConfigurationService buildingsConfigurationService
        )
    {
        _buildingsConfigurationService = buildingsConfigurationService;
    }

    [HttpGet]
    public async Task<ActionResult<List<BuildingsConfiguration>>> FindAll()
    {
        return await _buildingsConfigurationService.GetAll();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<BuildingsConfiguration>> Find(string id)
    {
        var result = await _buildingsConfigurationService.GetById(id);
        if (result != null)
        {
            return Ok(result);
        }
        else
        {
            return NotFound();
        }
    }

    [HttpPost]
    public async Task<ActionResult<BuildingsConfiguration>> Create([FromBody] BuildingsConfigurationCreateDto dto)
    {
        return await _buildingsConfigurationService.Create(dto);
    }


    [HttpPatch("{id}")]
    public async Task<ActionResult<BuildingsConfiguration>> Update(string id, [FromBody] BuildingsConfigurationUpdateDto dto)
    {
        return await _buildingsConfigurationService.Update(id, dto);
    }


    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(string id)
    {
        await _buildingsConfigurationService.Delete(id);

        return Ok();
    }

}