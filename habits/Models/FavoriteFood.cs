namespace habits.Models
{
    public class FavoriteFood
    {
        public int Id { get; set; }
        public string FoodName { get; set; }
        public string NutritionalInfo { get; set; } // JSON o detalles nutricionales
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
