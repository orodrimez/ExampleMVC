namespace EPractico_Optim.Models;

public class Favorite
{
    public int Id { get; set; }

    public string CocktailId { get; set; }
    public Cocktail Cocktail { get; set; }
}