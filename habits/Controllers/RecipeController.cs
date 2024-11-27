using Microsoft.AspNetCore.Mvc;
using habits.Services;

namespace habits.Controllers
{
    [ApiController]
    [Route("api/recipes")]
    public class RecipeController : ControllerBase
    {
        private readonly MealDBService _mealDBService;

        public RecipeController(MealDBService mealDBService)
        {
            _mealDBService = mealDBService;
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchRecipe([FromQuery] string name)
        {
            if (string.IsNullOrEmpty(name))
                return BadRequest("Debe proporcionar el nombre de una receta para buscar.");

            try
            {
                var result = await _mealDBService.SearchRecipeByName(name);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al buscar recetas: {ex.Message}");
            }
        }

        [HttpGet("category")]
        public async Task<IActionResult> GetRecipesByCategory([FromQuery] string category)
        {
            if (string.IsNullOrEmpty(category))
                return BadRequest("Debe proporcionar una categoría.");

            try
            {
                var result = await _mealDBService.GetRecipesByCategory(category);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al obtener recetas por categoría: {ex.Message}");
            }
        }
    }
}
