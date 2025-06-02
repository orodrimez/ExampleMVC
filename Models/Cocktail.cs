using System.ComponentModel.DataAnnotations;
namespace EPractico_Optim.Models;

public class Cocktail
{
    [Key]
    public string Id { get; set; }

    public string Name { get; set; }

    public string Instructions { get; set; }

    public string Thumbnail { get; set; }

    public ICollection<Ingredient> Ingredients { get; set; } = new List<Ingredient>();
}