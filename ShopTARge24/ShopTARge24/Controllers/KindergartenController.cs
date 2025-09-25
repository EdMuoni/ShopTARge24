using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ShopTARge24.Core.Domain;
using ShopTARge24.Data;
using ShopTARge24.Models.Kindergartens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopTARge24.Controllers
{
    public class KindergartenController : Controller
    {
        private readonly ShopTARge24Context _context;

        public KindergartenController(ShopTARge24Context context)
        {
            _context = context;
        }

      
        public async Task<IActionResult> Index()
        {
            return View(await _context.Kindergartens.ToListAsync());
        }

       
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kindergarten = await _context.Kindergartens
                .FirstOrDefaultAsync(m => m.Id == id);
            if (kindergarten == null)
            {
                return NotFound();
            }

            return View(kindergarten);
        }

        public IActionResult Create()
        {
            return View();
        }

     
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,GroupName,ChildrenCount,KindergartenName,TeacherName,CreatedAt,UpdatedAt")] Kindergarten kindergarten)
        {
            if (ModelState.IsValid)
            {
                _context.Add(kindergarten);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(kindergarten);
        }

        // GET: Kindergarten/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kindergarten = await _context.Kindergartens.FindAsync(id);
            if (kindergarten == null)
            {
                return NotFound();
            }
            return View(kindergarten);
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,GroupName,ChildrenCount,KindergartenName,TeacherName,CreatedAt,UpdatedAt")] Kindergarten kindergarten)
        {
            if (id != kindergarten.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(kindergarten);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KindergartenExists(kindergarten.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(kindergarten);
        }

                public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kindergarten = await _context.Kindergartens
                .FirstOrDefaultAsync(m => m.Id == id);
            if (kindergarten == null)
            {
                return NotFound();
            }

            return View(kindergarten);
        }

        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var kindergarten = await _context.Kindergartens.FindAsync(id);
            if (kindergarten != null)
            {
                _context.Kindergartens.Remove(kindergarten);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KindergartenExists(int id)
        {
            return _context.Kindergartens.Any(e => e.Id == id);
        }
    }
}
