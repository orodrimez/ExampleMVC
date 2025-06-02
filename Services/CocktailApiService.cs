using EPractico_Optim.Extensions;
using EPractico_Optim.Models;
using EPractico_Optim.Services.Abstract;

namespace EPractico_Optim.Services;
public class CocktailApiService : BaseApiService
{
    public CocktailApiService(HttpClient httpClient) : base(httpClient)
    {
    }

    public async Task<List<Cocktail>> SearchByNameAsync(string name)
    {
        var apiResponse = await GetFromApiAsync<ApiResponse>($"search.php?s={name}");
        return apiResponse?.Drinks.Select(d => d.ToCocktail()).ToList() ?? new();
    }

    public async Task<List<Cocktail>> GetCocktailsByIngredientAsync(string ingredient)
    {
        var response = await _httpClient.GetFromJsonAsync<ApiResponse>($"filter.php?i={ingredient}");

        if (response?.Drinks == null) return new List<Cocktail>();

        var tasks = response.Drinks.Select(d => GetCocktailByIdAsync(d.idDrink));
        var detailedCocktails = await Task.WhenAll(tasks);

        return detailedCocktails.Where(c => c != null).ToList()!;
    }
    public async Task<Cocktail?> GetCocktailByIdAsync(string id)
    {
        var response = await _httpClient.GetFromJsonAsync<ApiResponse>($"lookup.php?i={id}");
        var raw = response?.Drinks?.FirstOrDefault();
        return raw?.ToCocktail();
    }
}
    
    

