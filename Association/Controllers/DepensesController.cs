using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Association.Models;

namespace Association.Controllers
{
    public class DepensesController : Controller
    {
        private readonly AppDBContext _context;

        public DepensesController(AppDBContext context)
        {
            _context = context;
        }

        // GET: Depenses
        public async Task<IActionResult> Index()
        {
            return View(await _context.Depense.ToListAsync());
        }

        // GET: Depenses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var depense = await _context.Depense
                .FirstOrDefaultAsync(m => m.Id == id);
            if (depense == null)
            {
                return NotFound();
            }

            return View(depense);
        }

        // GET: Depenses/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Depenses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Type_depense,Date,Libelle")] Depense depense)
        {
            if (ModelState.IsValid)
            {
                _context.Add(depense);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(depense);
        }

        // GET: Depenses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var depense = await _context.Depense.FindAsync(id);
            if (depense == null)
            {
                return NotFound();
            }
            return View(depense);
        }

        // POST: Depenses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Type_depense,Date,Libelle")] Depense depense)
        {
            if (id != depense.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(depense);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DepenseExists(depense.Id))
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
            return View(depense);
        }

        // GET: Depenses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var depense = await _context.Depense
                .FirstOrDefaultAsync(m => m.Id == id);
            if (depense == null)
            {
                return NotFound();
            }

            return View(depense);
        }

        // POST: Depenses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var depense = await _context.Depense.FindAsync(id);
            _context.Depense.Remove(depense);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DepenseExists(int id)
        {
            return _context.Depense.Any(e => e.Id == id);
        }
    }
}
