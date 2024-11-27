using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using habits.Data;
using habits.Models;
using System.Linq;
using System.Threading.Tasks;

namespace habits.Controllers
{
    [ApiController]
    [Route("api/favorite-recipes")]
    public class FavoriteRecipeController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public FavoriteRecipeController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var favoriteRecipes = await _context.FavoriteRecipes.Include(fr => fr.User).ToListAsync();
            return Ok(favoriteRecipes);
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetByUserId(int userId)
        {
            var recipes = await _context.FavoriteRecipes.Where(fr => fr.UserId == userId).ToListAsync();
            return Ok(recipes);
        }

        [HttpPost]
        public async Task<IActionResult> AddFavorite([FromBody] FavoriteRecipe favoriteRecipe)
        {
            _context.FavoriteRecipes.Add(favoriteRecipe);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetAll), new { id = favoriteRecipe.Id }, favoriteRecipe);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFavorite(int id)
        {
            var favorite = await _context.FavoriteRecipes.FindAsync(id);

            if (favorite == null)
                return NotFound("Receta favorita no encontrada.");

            _context.FavoriteRecipes.Remove(favorite);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
