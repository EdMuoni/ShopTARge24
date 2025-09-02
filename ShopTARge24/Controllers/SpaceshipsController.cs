using Microsoft.AspNetCore.Mvc;
using ShopTARge24.Data;
using ShopTARge24.Models.Spaceships;


namespace ShopTARge24.Controllers
{



    public class SpaceshipsController : Controller
    {
        private readonly ShopTARge24Context _Context;

        public SpaceshipsController
            (
                ShopTARge24Context context
            )
        {
            _Context = context;
        }
        public IActionResult Index()
        {
            var result = _Context.Spaceships
                .Select(x => new SpaceshipIndexViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Classification = x.Classification,
                    BuiltDate = x.BuiltDate

                });

            return View(result);
        }
    }
}
