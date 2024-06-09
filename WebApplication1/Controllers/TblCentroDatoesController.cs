using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class TblCentroDatoesController : Controller
    {
        private readonly DatabaseContext _context;

        public TblCentroDatoesController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: TblCentroDatoes
        public async Task<IActionResult> Index()
        {
            var databaseContext = _context.TblCentroDatos.Include(t => t.IdCentroNavigation);
            return View(await databaseContext.ToListAsync());
        }

        // GET: TblCentroDatoes/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblCentroDato = await _context.TblCentroDatos
                .Include(t => t.IdCentroNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tblCentroDato == null)
            {
                return NotFound();
            }

            return View(tblCentroDato);
        }

        // GET: TblCentroDatoes/Create
        public IActionResult Create()
        {
            ViewData["IdCentro"] = new SelectList(_context.TblCentros, "IdCentro", "IdCentro");
            return View();
        }

        // POST: TblCentroDatoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IdCentro,Dato,Texto")] TblCentroDato tblCentroDato)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tblCentroDato);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCentro"] = new SelectList(_context.TblCentros, "IdCentro", "IdCentro", tblCentroDato.IdCentro);
            return View(tblCentroDato);
        }

        // GET: TblCentroDatoes/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblCentroDato = await _context.TblCentroDatos.FindAsync(id);
            if (tblCentroDato == null)
            {
                return NotFound();
            }
            ViewData["IdCentro"] = new SelectList(_context.TblCentros, "IdCentro", "IdCentro", tblCentroDato.IdCentro);
            return View(tblCentroDato);
        }

        // POST: TblCentroDatoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,IdCentro,Dato,Texto")] TblCentroDato tblCentroDato)
        {
            if (id != tblCentroDato.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tblCentroDato);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TblCentroDatoExists(tblCentroDato.Id))
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
            ViewData["IdCentro"] = new SelectList(_context.TblCentros, "IdCentro", "IdCentro", tblCentroDato.IdCentro);
            return View(tblCentroDato);
        }

        // GET: TblCentroDatoes/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblCentroDato = await _context.TblCentroDatos
                .Include(t => t.IdCentroNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tblCentroDato == null)
            {
                return NotFound();
            }

            return View(tblCentroDato);
        }

        // POST: TblCentroDatoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var tblCentroDato = await _context.TblCentroDatos.FindAsync(id);
            if (tblCentroDato != null)
            {
                _context.TblCentroDatos.Remove(tblCentroDato);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TblCentroDatoExists(long id)
        {
            return _context.TblCentroDatos.Any(e => e.Id == id);
        }
    }
}
