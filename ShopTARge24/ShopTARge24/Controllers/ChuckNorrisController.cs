using Microsoft.AspNetCore.Mvc;
using ShopTARge24.ApplicationServices.Services;
using ShopTARge24.Core.ServiceInterface;

namespace ShopTARge24.Controllers
{
    public class ChuckNorrisController : Controller
    {
        private readonly IChuckNorrisServices _service;

        public ChuckNorrisController(IChuckNorrisServices service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var categories = await _service.GetCategoriesAsync();
            ViewBag.Categories = categories; // simple pass to view
            return View();
        }

        // AJAX endpoint: /ChuckNorris/Random?category=dev
        [HttpGet]
        public async Task<IActionResult> Random(string? category)
        {
            var vm = await _service.GetRandomAsync(category);
            return Json(vm);
        }
    }
}
