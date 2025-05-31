using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Perpustakaan.Data;
using Perpustakaan.Models;

namespace Perpustakaan.Controllers
{
    public class GenreBukuController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GenreBukuController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: GenreBuku
        public async Task<IActionResult> Index()
        {
            return View(await _context.Genre.ToListAsync());
        }

        // GET: GenreBuku/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var genreBuku = await _context.Genre
                .FirstOrDefaultAsync(m => m.Id == id);
            if (genreBuku == null)
            {
                return NotFound();
            }

            return View(genreBuku);
        }

        // GET: GenreBuku/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: GenreBuku/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] GenreBuku genreBuku)
        {
            if (ModelState.IsValid)
            {
                _context.Add(genreBuku);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(genreBuku);
        }

        // GET: GenreBuku/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var genreBuku = await _context.Genre.FindAsync(id);
            if (genreBuku == null)
            {
                return NotFound();
            }
            return View(genreBuku);
        }

        // POST: GenreBuku/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] GenreBuku genreBuku)
        {
            if (id != genreBuku.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(genreBuku);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GenreBukuExists(genreBuku.Id))
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
            return View(genreBuku);
        }

        // GET: GenreBuku/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var genreBuku = await _context.Genre
                .FirstOrDefaultAsync(m => m.Id == id);
            if (genreBuku == null)
            {
                return NotFound();
            }

            return View(genreBuku);
        }

        // POST: GenreBuku/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var genreBuku = await _context.Genre.FindAsync(id);
            if (genreBuku != null)
            {
                _context.Genre.Remove(genreBuku);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GenreBukuExists(int id)
        {
            return _context.Genre.Any(e => e.Id == id);
        }
    }
}
