using Microsoft.AspNetCore.Mvc;
using ShopTARge24.Data;


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
            var result = 

            return View();
        }
    }
}
