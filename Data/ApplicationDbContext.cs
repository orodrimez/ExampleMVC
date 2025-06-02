using EPractico_Optim.Models;
using Microsoft.EntityFrameworkCore;

namespace EPractico_Optim.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

    public DbSet<Cocktail> Cocktails { get; set; }
    public DbSet<Ingredient> Ingredients { get; set; }
    public DbSet<Favorite> Favorites { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cocktail>()
            .HasMany(c => c.Ingredients)
            .WithOne(i => i.Cocktail)
            .HasForeignKey(i => i.CocktailId);

        modelBuilder.Entity<Favorite>()
            .HasOne(f => f.Cocktail)
            .WithMany()
            .HasForeignKey(f => f.CocktailId);

        base.OnModelCreating(modelBuilder);
    }

}