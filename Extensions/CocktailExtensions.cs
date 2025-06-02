using EPractico_Optim.Models;

namespace EPractico_Optim.Extensions;

public static class CocktailExtensions
{
    public static Cocktail ToCocktail(this CocktailRaw raw)
    {
        return new Cocktail
        {
            Id = raw.idDrink,
            Name = raw.strDrink,
            Thumbnail = raw.strDrinkThumb,
            Instructions = raw.strInstructions,
            Ingredients = raw.GetIngredientsWithMeasures()
                .Select(i => new Ingredient
                {
                    Name = i.Key,
                    Measure = i.Value,
                    CocktailId = raw.idDrink
                }).ToList()
        };
    }

    public static Dictionary<string, string?> GetIngredientsWithMeasures(this CocktailRaw raw)
    {
        var result = new Dictionary<string, string?>();

        for (int i = 1; i <= 15; i++)
        {
            var ingredient = typeof(CocktailRaw).GetProperty($"strIngredient{i}")?.GetValue(raw)?.ToString();
            var measure = typeof(CocktailRaw).GetProperty($"strMeasure{i}")?.GetValue(raw)?.ToString();

            if (!string.IsNullOrWhiteSpace(ingredient))
            {
                result[ingredient] = measure;
            }
        }

        return result;
    }
}
