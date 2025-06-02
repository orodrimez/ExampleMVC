using EPractico_Optim.Models;

namespace EPractico_Optim.Repository.IRepository;

public interface ICocktailRepository
{
    Task<Cocktail?> GetByIdAsync(string id);
    Task<List<Cocktail>> SearchByNameAsync(string name);
    Task<List<Cocktail>> SearchByIngredientAsync(string ingredient);

    Task<List<Favorite>> GetFavoritesAsync();
    Task AddFavoriteAsync(Cocktail cocktail);
    Task RemoveFavoriteAsync(string cocktailId);
    Task<bool> IsFavoriteAsync(string cocktailId);
}