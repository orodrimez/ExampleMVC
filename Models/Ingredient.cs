namespace EPractico_Optim.Models;

public class Ingredient
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string? Measure { get; set; }

    public string CocktailId { get; set; }

    public Cocktail Cocktail { get; set; }
}