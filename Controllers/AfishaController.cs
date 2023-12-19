using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TheatreWeb.Data;
using TheatreWeb.Models;

namespace TheatreWeb.Controllers
{
    public class AfishaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AfishaController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "Employee")]
        // GET: Afisha
        public async Task<IActionResult> Index()
        {
            var afisha = _context.Afisha
                .Include(c => c.Performance).Include(c => c.Halls)
                .AsNoTracking();
                return View(await afisha.ToListAsync());
        }

        // GET: Afisha
        public async Task<IActionResult> Index2()
        {
            var afisha = _context.Afisha
                .Include(c => c.Performance).Include(c => c.Halls)
                .AsNoTracking();
            return View(await afisha.ToListAsync());
        }

        [Authorize(Roles = "Employee")]
        // GET: Afisha/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var afisha = await _context.Afisha
                .FirstOrDefaultAsync(m => m.Id == id);
            if (afisha == null)
            {
                return NotFound();
            }

            return View(afisha);
        }

        [Authorize(Roles = "Employee")]
        // GET: Afisha/Create
        public IActionResult Create()
        {

            ViewBag.performance = new SelectList(_context.Performances, "Id", "Title");
            ViewBag.hall = new SelectList(_context.Halls, "Id", "HallName");

            return View();
        }

        // POST: Afisha/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Date,DayOfWeek,Note,Spectacle,Hall,BaseCost,IsArchived")] Afisha afisha)
        {
            if (ModelState.IsValid)
            {
                _context.Add(afisha);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(afisha);
        }

        [Authorize(Roles = "Employee")]
        // GET: Afisha/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var afisha = await _context.Afisha.FindAsync(id);
            if (afisha == null)
            {
                return NotFound();
            }
            ViewBag.performance = new SelectList(_context.Performances, "Id", "Title");
            ViewBag.hall = new SelectList(_context.Halls, "Id", "HallName");

            return View(afisha);
        }

        // POST: Afisha/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Date,DayOfWeek,Note,Spectacle,Hall,BaseCost,IsArchived")] Afisha afisha)
        {
            if (id != afisha.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(afisha);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AfishaExists(afisha.Id))
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
            return View(afisha);
        }

        [Authorize(Roles = "Employee")]
        // GET: Afisha/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var afisha = await _context.Afisha
                .FirstOrDefaultAsync(m => m.Id == id);
            if (afisha == null)
            {
                return NotFound();
            }

            return View(afisha);
        }

        // POST: Afisha/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var afisha = await _context.Afisha.FindAsync(id);
            _context.Afisha.Remove(afisha);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AfishaExists(int id)
        {
            return _context.Afisha.Any(e => e.Id == id);
        }
    }
}
