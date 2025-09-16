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
        private readonly KindergartenContext _context;
        public KindergartenController(KindergartenContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var result = _context.Kindergartens
                .Select(x => new KindergartenIndexViewModel
                {
                    Id = x.Id,
                    GroupName = x.GroupName,
                    ChildrenCount = x.ChildrenCount,
                    KindergartenName = x.KindergartenName,
                    TeacherName = x.TeacherName,
                    CreatedAt = x.CreatedAt,
                    UpdateAt = x.UpdateAt
                });
            return View(result);
        }
    }
}
