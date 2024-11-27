using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using habits.Data;
using habits.Models;
using System.Linq;
using System.Threading.Tasks;

namespace habits.Controllers
{
    [ApiController]
    [Route("api/favorite-foods")]
    public class FavoriteFoodController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public FavoriteFoodController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var favoriteFoods = await _context.FavoriteFoods.Include(ff => ff.User).ToListAsync();
            return Ok(favoriteFoods);
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetByUserId(int userId)
        {
            var foods = await _context.FavoriteFoods.Where(ff => ff.UserId == userId).ToListAsync();
            return Ok(foods);
        }

        [HttpPost]
        public async Task<IActionResult> AddFavorite([FromBody] FavoriteFood favoriteFood)
        {
            _context.FavoriteFoods.Add(favoriteFood);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetAll), new { id = favoriteFood.Id }, favoriteFood);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFavorite(int id)
        {
            var favorite = await _context.FavoriteFoods.FindAsync(id);

            if (favorite == null)
                return NotFound("Alimento favorito no encontrado.");

            _context.FavoriteFoods.Remove(favorite);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
