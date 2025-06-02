using EPractico_Optim.Data;
using EPractico_Optim.Models;
using EPractico_Optim.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPractico_Optim.Test;


public class CocktailRepositoryTests
{
    private ApplicationDbContext GetDbContext()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: $"TestDb_{Guid.NewGuid()}")
            .Options;

        return new ApplicationDbContext(options);
    }

    [Fact]
    public async Task SearchByNameAsync_ReturnsMatchingCocktails()
    {
        // Arrange
        var db = GetDbContext();
        db.Cocktails.Add(new Cocktail { Id = "1", Name = "Margarita" });
        db.Cocktails.Add(new Cocktail { Id = "2", Name = "Martini" });
        await db.SaveChangesAsync();

        var repo = new CocktailRepository(db, null); // null porque no usamos la API en este test

        // Act
        var result = await repo.SearchByNameAsync("Martini");

        // Assert
        Assert.Single(result);
        Assert.Equal("Martini", result.First().Name);
    }

    [Fact]
    public async Task AddFavoriteAsync_AddsToFavorites()
    {
        var db = GetDbContext();
        var cocktail = new Cocktail { Id = "1", Name = "Negroni" };
        db.Cocktails.Add(cocktail);
        await db.SaveChangesAsync();

        var repo = new CocktailRepository(db, null);

        await repo.AddFavoriteAsync(cocktail);

        var fav = await db.Favorites.FirstOrDefaultAsync();
        Assert.NotNull(fav);
        Assert.Equal("1", fav.CocktailId);
    }

    [Fact]
    public async Task GetFavoritesAsync_ReturnsAllFavorites()
    {
        var db = GetDbContext();
        var cocktail = new Cocktail { Id = "1", Name = "Daiquiri" };
        db.Cocktails.Add(cocktail);
        db.Favorites.Add(new Favorite { CocktailId = "1", Cocktail = cocktail });
        await db.SaveChangesAsync();

        var repo = new CocktailRepository(db, null);

        var result = await repo.GetFavoritesAsync();

        Assert.Single(result);
        Assert.Equal("Daiquiri", result.First().Cocktail.Name);
    }

    [Fact]
    public async Task IsFavoriteAsync_ReturnsTrueIfFavoriteExists()
    {
        var db = GetDbContext();
        db.Favorites.Add(new Favorite { CocktailId = "1" });
        await db.SaveChangesAsync();

        var repo = new CocktailRepository(db, null);

        var result = await repo.IsFavoriteAsync("1");

        Assert.True(result);
    }
}