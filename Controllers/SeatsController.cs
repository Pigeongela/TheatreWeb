﻿using System;
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
    public class SeatsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SeatsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Double?> Multiply(int? id)
        {
            var seats = await _context.Seats.FindAsync(id);
            int? idz = seats.HallId;
            var hall = await _context.Halls.FindAsync(idz);
            if (seats == null)
            {
                return null;
            }
            double coast = seats.CostCoefficient * hall.CostCoefficient;
            seats.CostCoefficient = coast;
            return coast;
        }

        [Authorize]
        // GET: Seats
        public async Task<IActionResult> Index()
        {
            var seats = _context.Seats.Include(c => c.Halls).AsNoTracking();
            return View(await seats.ToListAsync());
        }

        // GET: Seats/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var seats = await _context.Seats
                .FirstOrDefaultAsync(m => m.Id == id);
            if (seats == null)
            {
                return NotFound();
            }

            return View(seats);
        }

        [Authorize(Roles = "AdminDB , Head")]
        // GET: Seats/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Seats/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,HallId,Level,Row,Number,Status,CostCoefficient")] Seats seats)
        {
            if (ModelState.IsValid)
            {
                _context.Add(seats);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(seats);
        }

        // GET: Seats/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var seats = await _context.Seats.FindAsync(id);
            if (seats == null)
            {
                return NotFound();
            }
            return View(seats);
        }

        // POST: Seats/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,HallId,Level,Row,Number,Status,CostCoefficient")] Seats seats)
        {
            if (id != seats.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(seats);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SeatsExists(seats.Id))
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
            return View(seats);
        }

        // GET: Seats/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var seats = await _context.Seats
                .FirstOrDefaultAsync(m => m.Id == id);
            if (seats == null)
            {
                return NotFound();
            }

            return View(seats);
        }

        // POST: Seats/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var seats = await _context.Seats.FindAsync(id);
            _context.Seats.Remove(seats);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SeatsExists(int id)
        {
            return _context.Seats.Any(e => e.Id == id);
        }
    }
}
