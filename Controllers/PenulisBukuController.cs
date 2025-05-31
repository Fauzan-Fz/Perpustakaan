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
    public class PenulisBukuController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PenulisBukuController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PenulisBuku
        public async Task<IActionResult> Index()
        {
            return View(await _context.Penulis.ToListAsync());
        }

        // GET: PenulisBuku/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var penulisBuku = await _context.Penulis
                .FirstOrDefaultAsync(m => m.Id == id);
            if (penulisBuku == null)
            {
                return NotFound();
            }

            return View(penulisBuku);
        }

        // GET: PenulisBuku/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PenulisBuku/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nama")] PenulisBuku penulisBuku)
        {
            if (ModelState.IsValid)
            {
                _context.Add(penulisBuku);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(penulisBuku);
        }

        // GET: PenulisBuku/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var penulisBuku = await _context.Penulis.FindAsync(id);
            if (penulisBuku == null)
            {
                return NotFound();
            }
            return View(penulisBuku);
        }

        // POST: PenulisBuku/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nama")] PenulisBuku penulisBuku)
        {
            if (id != penulisBuku.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(penulisBuku);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PenulisBukuExists(penulisBuku.Id))
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
            return View(penulisBuku);
        }

        // GET: PenulisBuku/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var penulisBuku = await _context.Penulis
                .FirstOrDefaultAsync(m => m.Id == id);
            if (penulisBuku == null)
            {
                return NotFound();
            }

            return View(penulisBuku);
        }

        // POST: PenulisBuku/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var penulisBuku = await _context.Penulis.FindAsync(id);
            if (penulisBuku != null)
            {
                _context.Penulis.Remove(penulisBuku);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PenulisBukuExists(int id)
        {
            return _context.Penulis.Any(e => e.Id == id);
        }
    }
}
