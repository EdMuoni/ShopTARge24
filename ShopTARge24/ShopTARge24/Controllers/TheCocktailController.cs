// ShopTARge24.Web/Controllers/TheCocktailController.cs
using Microsoft.AspNetCore.Mvc;
using ShopTARge24.ApplicationServices.Services;
using ShopTARge24.Core.Dto;
using ShopTARge24.Core.ServiceInterface;

namespace ShopTARge24.Controllers
{
    public class TheCocktailController : Controller
    {
        private readonly ITheCocktailServices _service;
        public TheCocktailController(ITheCocktailServices service) => _service = service; // already had this

        // /TheCocktail?q=margarita
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var drinks = await _service.SearchByNameAsync("margarita");
            return View(drinks);
        }

        // /TheCocktail/Recipe/11007   (example id)
        [HttpGet]
        public async Task<IActionResult> Order(string id)
        {
            var drink = await _service.LookupByIdAsync(id);  // This one searches by Id
            if (drink == null) return NotFound();
            return View(drink);
        }
    }
}
