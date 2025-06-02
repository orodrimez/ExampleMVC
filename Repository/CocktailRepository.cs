using EPractico_Optim.Data;
using EPractico_Optim.Extensions;
using EPractico_Optim.Models;
using EPractico_Optim.Repository.IRepository;
using EPractico_Optim.Services;
using Microsoft.EntityFrameworkCore;

namespace EPractico_Optim.Repository;

public class CocktailRepository : ICocktailRepository
{
    private readonly ApplicationDbContext _db;
    private readonly CocktailApiService _api;

    public CocktailRepository(ApplicationDbContext db, CocktailApiService api)
    {
        _db = db;
        _api = api;
    }

    public async Task<List<Cocktail>> SearchByNameAsync(string name)
    {
        return await _api.SearchByNameAsync(name);
    }

    public async Task<List<Cocktail>> SearchByIngredientAsync(string ingredient)
    {
        return await _api.GetCocktailsByIngredientAsync(ingredient);
    }

    public async Task<Cocktail?> GetByIdAsync(string id)
    {
        return await _api.GetCocktailByIdAsync(id);
    }

    public async Task<List<Favorite>> GetFavoritesAsync()
    {
        return await _db.Favorites
            .Include(f => f.Cocktail)
            .ToListAsync();
    }

    public async Task AddFavoriteAsync(Cocktail cocktail)
    {
        if (!_db.Favorites.Any(f => f.CocktailId == cocktail.Id))
        {
            // Guardar cóctel si no está
            if (!_db.Cocktails.Any(c => c.Id == cocktail.Id))
            {
                _db.Cocktails.Add(cocktail);
                _db.Ingredients.AddRange(cocktail.Ingredients);
            }

            _db.Favorites.Add(new Favorite { CocktailId = cocktail.Id });
            await _db.SaveChangesAsync();
        }
    }

    public async Task RemoveFavoriteAsync(string cocktailId)
    {
        var fav = await _db.Favorites.FirstOrDefaultAsync(f => f.CocktailId == cocktailId);
        if (fav != null)
        {
            _db.Favorites.Remove(fav);
            await _db.SaveChangesAsync();
        }
    }

    public async Task<bool> IsFavoriteAsync(string cocktailId)
    {
        return await _db.Favorites.AnyAsync(f => f.CocktailId == cocktailId);
    }
    

}
