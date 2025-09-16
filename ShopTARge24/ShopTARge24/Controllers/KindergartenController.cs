using Kindergarten.Core.Domain;
using Kindergarten.Core.Dto;
using Kindergarten.Core.ServiceInterface;
using Kindergarten.Data;
using Kindergarten.Models.Kindergarten;
using Microsoft.AspNetCore.Mvc;
using ShopTARge24.Core.ServiceInterface;
using ShopTARge24.Data;


namespace Kindergarten.Controllers
{

    public class KindergartenController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
