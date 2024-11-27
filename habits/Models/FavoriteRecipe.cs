namespace habits.Models
{
    public class FavoriteRecipe
    {
        public int Id { get; set; }
        public string RecipeName { get; set; }
        public string RecipeDetails { get; set; } // JSON o detalles de la receta
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
