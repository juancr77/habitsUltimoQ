using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class NutritionController : ControllerBase
{
    private readonly NutritionixService _nutritionixService;

    public NutritionController(NutritionixService nutritionixService)
    {
        _nutritionixService = nutritionixService;
    }

    [HttpGet("search")]
    public async Task<IActionResult> SearchFood([FromQuery] string query)
    {
        if (string.IsNullOrEmpty(query))
        {
            return BadRequest("Debe proporcionar un término de búsqueda.");
        }

        try
        {
            var result = await _nutritionixService.SearchFood(query);
            return Ok(result);
        }
        catch (HttpRequestException ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
}
