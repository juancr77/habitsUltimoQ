using Microsoft.EntityFrameworkCore;
using habits.Models;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace habits.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<FavoriteRecipe> FavoriteRecipes { get; set; }
        public DbSet<FavoriteFood> FavoriteFoods { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<FavoriteRecipe>()
                .HasOne(fr => fr.User)
                .WithMany(u => u.FavoriteRecipes)
                .HasForeignKey(fr => fr.UserId);

            modelBuilder.Entity<FavoriteFood>()
                .HasOne(ff => ff.User)
                .WithMany(u => u.FavoriteFoods)
                .HasForeignKey(ff => ff.UserId);
        }
    }
}
