using Microsoft.AspNetCore.Mvc;
using ShopTARge24.Core.Dto;
using ShopTARge24.Data;
using ShopTARge24.Models.Spaceships;
using ShopTARge24.Core.ServiceInterface;

namespace ShopTARge24.Controllers
{
    public class SpaceshipsController : Controller
    {
        private readonly ShopTARge24Context _context;
        private readonly ISpaceshipServices _spaceshipServices;

        public SpaceshipsController
            (
                ShopTARge24Context context,
                ISpaceshipServices spaceshipServices

            )
        {
            _context = context;
            _spaceshipServices = spaceshipServices;
        }


        public IActionResult Index()
        {
            var result = _context.Spaceships
                .Select(x => new SpaceshipIndexViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Classification = x.Classification,
                    BuiltDate = x.BuiltDate,
                    Crew = x.Crew

                });

            return View(result);
        }

        [HttpGet]
        public IActionResult Create()
        {
            SpaceshipCreateViewModel result = new();

            return View("Create", result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(SpaceshipCreateViewModel viewModel)
        {
            var dto = new SpaceshipDto()
            {
                Id = viewModel.Id,
                Name = viewModel.Name,
                Classification = viewModel.Classification,
                BuiltDate = viewModel.BuiltDate,
                Crew = viewModel.Crew,
                EnginePower = viewModel.EnginePower,
                CreatedAt = viewModel.CreatedAt,
                ModifiedAt = viewModel.ModifiedAt
            };

            var result = await _spaceshipServices.Create(dto);

            if (result == null || result.Id == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]

        public async Task<IActionResult> Delete(Guid id)
        {
            var spaceship = await _spaceshipServices.DetailAsync(id);

            if (spaceship == null || spaceship.Id == null)
            {
                return NotFound();
            }

            var vm = new SpaceshipDeleteViewModel();

            vm.Id = spaceship.Id;
            vm.Name = spaceship.Name;
            vm.Classification = spaceship.Classification;
            vm.BuiltDate = spaceship.BuiltDate;
            vm.Crew = spaceship.Crew;
            vm.EnginePower = spaceship.EnginePower;
            vm.CreatedAt = spaceship.CreatedAt;
            vm.ModifiedAt = spaceship.ModifiedAt;

            return View(vm);

        }
    }
}
