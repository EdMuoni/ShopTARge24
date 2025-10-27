using Microsoft.AspNetCore.Mvc;
using ShopTARge24.Core.ServiceInterface;               // interface from Core
using ShopTARge24.Models.ChuckNorris;                  // ViewModel in Web

namespace ShopTARge24.Controllers
{
    public class ChuckNorrisController : Controller
    {
        private readonly IChuckNorrisServices _service;
        public ChuckNorrisController(IChuckNorrisServices service) => _service = service;

        public IActionResult Index() => View();

        public async Task<IActionResult> Joke(string? category)
        {
            var dto = await _service.GetRandomAsync(category);

            var vm = new ChuckNorrisViewModel
            {
                Id = dto.Id,
                IconUrl = dto.IconUrl,
                Url = dto.Url,
                Value = dto.Value,
                Categories = dto.Categories
            };

            return View(vm); 
        }
    }
}

//[HttpPost]

//public IActionResult SearchChuckNorrisJokes()
//{
//    return RedirectToAction(nameof(Joke));        
//}

//[HttpGet]

//public IActionResult Joke()
//{
//    ChuckNorrisResultDto Dto = new ();

//    _chuckNorrisServices.ChuckNorrisResult(Dto);
//    ChuckNorrisViewModel vm = new ();

//    vm.Categories = Dto.Categories;
//    vm.CreatedAt = Dto.CreatedAt;
//    vm.IconUrl = Dto.IconUrl;
//    vm.Id = Dto.Id;
//    vm.UpdatedAt = Dto.UpdatedAt;
//    vm.Url = Dto.Url;
//    vm.Value = Dto.Value;

//    return View(vm);
//}


