using ApiToolKit.Application.DTOs.Search.KnowledgeGraph;
using ApiToolKit.Application.Interfaces.Services.Search;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.Search;

[ApiController]
[Route("api/search/knowledge-graph")]
public class KnowledgeGraphController : ControllerBase
{
    private readonly IKnowledgeGraphService _knowledgeGraphService;

    public KnowledgeGraphController(IKnowledgeGraphService knowledgeGraphService)
    {
        _knowledgeGraphService = knowledgeGraphService;
    }

    /// <summary>Search for entities in the Google Knowledge Graph.</summary>
    /// <remarks>
    /// Returns structured entity information including name, description, image, and detailed description.
    /// Use the Types parameter to filter by entity type (e.g., "Person", "Organization", "Place").
    /// Results are scored by relevance — higher ResultScore indicates a stronger match.
    /// </remarks>
    [HttpGet]
    public async Task<IActionResult> Search([FromQuery] KnowledgeGraphRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _knowledgeGraphService.SearchAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
