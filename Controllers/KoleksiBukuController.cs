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
    public class KoleksiBukuController : Controller
    {
        private readonly ApplicationDbContext _context;

        public KoleksiBukuController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: KoleksiBuku
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Buku.Include(k => k.Genre).Include(k => k.Penulis);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: KoleksiBuku/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var koleksiBuku = await _context.Buku
                .Include(k => k.Genre)
                .Include(k => k.Penulis)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (koleksiBuku == null)
            {
                return NotFound();
            }

            return View(koleksiBuku);
        }

        // GET: KoleksiBuku/Create
        public IActionResult Create()
        {
            ViewData["GenreId"] = new SelectList(_context.Genre, "Id", "Id");
            ViewData["PenulisId"] = new SelectList(_context.Penulis, "Id", "Id");
            return View();
        }

        // POST: KoleksiBuku/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Judul,PenulisId,GenreId,TahunTerbit")] KoleksiBuku koleksiBuku)
        {
            if (ModelState.IsValid)
            {
                _context.Add(koleksiBuku);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GenreId"] = new SelectList(_context.Genre, "Id", "Id", koleksiBuku.GenreId);
            ViewData["PenulisId"] = new SelectList(_context.Penulis, "Id", "Id", koleksiBuku.PenulisId);
            return View(koleksiBuku);
        }

        // GET: KoleksiBuku/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var koleksiBuku = await _context.Buku.FindAsync(id);
            if (koleksiBuku == null)
            {
                return NotFound();
            }
            ViewData["GenreId"] = new SelectList(_context.Genre, "Id", "Id", koleksiBuku.GenreId);
            ViewData["PenulisId"] = new SelectList(_context.Penulis, "Id", "Id", koleksiBuku.PenulisId);
            return View(koleksiBuku);
        }

        // POST: KoleksiBuku/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Judul,PenulisId,GenreId,TahunTerbit")] KoleksiBuku koleksiBuku)
        {
            if (id != koleksiBuku.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(koleksiBuku);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KoleksiBukuExists(koleksiBuku.Id))
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
            ViewData["GenreId"] = new SelectList(_context.Genre, "Id", "Id", koleksiBuku.GenreId);
            ViewData["PenulisId"] = new SelectList(_context.Penulis, "Id", "Id", koleksiBuku.PenulisId);
            return View(koleksiBuku);
        }

        // GET: KoleksiBuku/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var koleksiBuku = await _context.Buku
                .Include(k => k.Genre)
                .Include(k => k.Penulis)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (koleksiBuku == null)
            {
                return NotFound();
            }

            return View(koleksiBuku);
        }

        // POST: KoleksiBuku/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var koleksiBuku = await _context.Buku.FindAsync(id);
            if (koleksiBuku != null)
            {
                _context.Buku.Remove(koleksiBuku);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KoleksiBukuExists(int id)
        {
            return _context.Buku.Any(e => e.Id == id);
        }
    }
}
