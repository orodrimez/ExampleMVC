using EPractico_Optim.Models;
using EPractico_Optim.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace EPractico_Optim.Controllers;

public class CocktailsController : Controller
{
    private readonly ICocktailRepository _repo;


    public CocktailsController(ICocktailRepository repo) => _repo = repo;


    public IActionResult Index()
    {
        return View();
    }

    // Búsqueda por nombre
    [HttpPost]
    public async Task<IActionResult> SearchByName(string name)
    {
        var cocktails = await _repo.SearchByNameAsync(name);
        return View("Index", cocktails);
    }

    // Búsqueda por ingrediente
    [HttpPost]
    public async Task<IActionResult> SearchByIngredient(string ingredient)
    {
        var cocktails = await _repo.SearchByIngredientAsync(ingredient);
        return View("Index", cocktails);
    }

    public async Task<IActionResult> Details(string id)
    {
        var cocktail = await _repo.GetByIdAsync(id);
        if (cocktail == null) return NotFound();

        var isFavorite = await _repo.IsFavoriteAsync(id);

        var viewModel = new CocktailDetailsViewModel
        {
            Cocktail = cocktail,
            IsFavorite = isFavorite
        };

        return View(viewModel);
    }

    public async Task<IActionResult> Favorites()
    {
        var favs = await _repo.GetFavoritesAsync();
        return View(favs);
    }

    [HttpPost]
    public async Task<IActionResult> AddFavorite(string id)
    {
        var cocktail = await _repo.GetByIdAsync(id);
        if (cocktail != null)
            await _repo.AddFavoriteAsync(cocktail);

        return RedirectToAction("Details", new { id });
    }

    [HttpPost]
    public async Task<IActionResult> RemoveFavorite(string id)
    {
        await _repo.RemoveFavoriteAsync(id);
        return RedirectToAction("Favorites");
    }

}
