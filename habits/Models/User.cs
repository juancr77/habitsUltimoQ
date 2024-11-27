using System.Collections.Generic;

namespace habits.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public ICollection<FavoriteRecipe> FavoriteRecipes { get; set; }
        public ICollection<FavoriteFood> FavoriteFoods { get; set; }
    }
}
