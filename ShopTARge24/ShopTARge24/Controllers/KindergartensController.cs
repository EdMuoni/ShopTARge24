using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopTARge24.Core.ServiceInterface;
using ShopTARge24.Data;
using ShopTARge24.Models.Kindergarten;
using System.Threading.Tasks;

namespace ShopTARge24.Controllers
{
    public class KindergartenController : Controller
    {

        private readonly KindergartenContext _context;
        private readonly IKindergartenServices _kindergartenServices;

        public KindergartenController
            (
                KindergartenContext context,
                IKindergartenServices kindergartenServices
            )
        {
            _context = context;
            _kindergartenServices = kindergartenServices;
        }

        public async Task<IActionResult> Index()
        {
            // Retrieve data from your database.
            // Replace "Kindergartens" with the actual name of your DbSet.
            var kindergartens = await _context.Kindergartens
                .Select(x => new KindergartenIndexViewModel
                {
                    Id = x.Id,
                    GroupName = x.GroupName,
                    ChildrenCount = x.ChildrenCount,
                    KindergartenName = x.KindergartenName,
                    TeacherName = x.TeacherName,
                    CreatedAt = x.CreatedAt,
                    UpdatedAt = x.UpdatedAt
                })
                .ToListAsync();

            // Pass the populated list to the view.
            return View(kindergartens);
        }
    }
}