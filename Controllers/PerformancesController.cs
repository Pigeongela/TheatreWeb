using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TheatreWeb.Data;
using TheatreWeb.Models;

namespace TheatreWeb.Controllers
{
    public class PerformancesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PerformancesController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> GetImage(int? id)
        {
            var perf = await _context.Performances.FindAsync(id);

            if (perf == null || perf.Image == null || perf.Image.Length == 0)
            {
                return NotFound();
            }

            return File(perf.Image, "image/jpeg");
        }

        // GET: Performances
        public async Task<IActionResult> Index()
        {
            return View(await _context.Performances.ToListAsync());
        }

        // GET: Performances/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var performances = await _context.Performances
                .FirstOrDefaultAsync(m => m.Id == id);
            if (performances == null)
            {
                return NotFound();
            }
            return View(performances);
        }

        // GET: Performances/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Performances/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,Duration,Image,IsArchived")] Performances performances, IFormFile perfImage)
        {
            if (ModelState.IsValid)
            {
                if (perfImage != null)
                {
                    using (var ms = new MemoryStream())
                    {
                        await perfImage.CopyToAsync(ms);
                        performances.Image = ms.ToArray();
                    }
                }
                _context.Add(performances);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(performances);
        }

        // GET: Performances/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var performances = await _context.Performances.FindAsync(id);
            if (performances == null)
            {
                return NotFound();
            }
            return View(performances);
        }

        // POST: Performances/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,Duration,Image,IsArchived")] Performances performances, IFormFile perfImage)
        {
            if (id != performances.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (perfImage != null)
                {
                    using (var ms = new MemoryStream())
                    {
                        await perfImage.CopyToAsync(ms);
                        performances.Image = ms.ToArray();
                    }
                }
                try
                {
                    _context.Update(performances);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PerformancesExists(performances.Id))
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
            return View(performances);
        }

        // GET: Performances/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var performances = await _context.Performances
                .FirstOrDefaultAsync(m => m.Id == id);
            if (performances == null)
            {
                return NotFound();
            }

            return View(performances);
        }

        // POST: Performances/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var performances = await _context.Performances.FindAsync(id);
            _context.Performances.Remove(performances);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PerformancesExists(int id)
        {
            return _context.Performances.Any(e => e.Id == id);
        }
    }
}
