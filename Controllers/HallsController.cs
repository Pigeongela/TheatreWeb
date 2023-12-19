using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TheatreWeb.Data;
using TheatreWeb.Models;

namespace TheatreWeb.Controllers
{
    public class HallsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HallsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> GetImage(int? id)
        {
            var halls = await _context.Halls.FindAsync(id);

            if (halls == null || halls.Schema == null || halls.Schema.Length == 0)
            {
                return NotFound();
            }

            return File(halls.Schema, "image/jpeg"); // Может потребоваться изменить mime-тип в зависимости от вашего формата
        }

        // GET: Halls
        public async Task<IActionResult> Index()
        {
            return View(await _context.Halls.ToListAsync());
        }

        [Authorize]
        public async Task<IActionResult> Index2(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var halls = await _context.Halls
                .FirstOrDefaultAsync(m => m.Id == id);
            if (halls == null)
            {
                return NotFound();
            }
            var filteredData = _context.Seats
            .Where(item => item.HallId.Equals(id))
            .ToList();
            return View(filteredData);
        }

        // GET: Halls/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var halls = await _context.Halls
                .FirstOrDefaultAsync(m => m.Id == id);
            if (halls == null)
            {
                return NotFound();
            }

            return View(halls);
        }

        // GET: Halls/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Halls/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,HallName,CostCoefficient,Schema")] Halls halls, IFormFile schemaImage)
        {
            if (ModelState.IsValid)
            {
                if (schemaImage != null)
                {
                    using (var ms = new MemoryStream())
                    {
                        await schemaImage.CopyToAsync(ms);
                        halls.Schema = ms.ToArray();
                    }
                }

                _context.Add(halls);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(halls);
        }

        // GET: Halls/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var halls = await _context.Halls.FindAsync(id);
            if (halls == null)
            {
                return NotFound();
            }
            return View(halls);
        }

        // POST: Halls/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,HallName,Schema,CostCoefficient")] Halls halls, IFormFile schemaImage)
        {
            if (id != halls.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (schemaImage != null)
                {
                    using (var ms = new MemoryStream())
                    {
                        await schemaImage.CopyToAsync(ms);
                        halls.Schema = ms.ToArray();
                    }
                }

                try
                {
                    _context.Update(halls);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HallsExists(halls.Id))
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
            return View(halls);
        }

        // GET: Halls/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var halls = await _context.Halls
                .FirstOrDefaultAsync(m => m.Id == id);
            if (halls == null)
            {
                return NotFound();
            }

            return View(halls);
        }

        // POST: Halls/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var halls = await _context.Halls.FindAsync(id);
            _context.Halls.Remove(halls);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HallsExists(int id)
        {
            return _context.Halls.Any(e => e.Id == id);
        }
    }
}
