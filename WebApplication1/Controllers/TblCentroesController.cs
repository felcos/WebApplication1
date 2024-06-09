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
    public class TblCentroesController : Controller
    {
        private readonly DatabaseContext _context;

        public TblCentroesController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: TblCentroes
        public async Task<IActionResult> Index()
        {
            var databaseContext = _context.TblCentros.Include(t => t.IdTipoCentroNavigation);
            return View(await databaseContext.ToListAsync());
        }

        // GET: TblCentroes/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblCentro = await _context.TblCentros
                .Include(t => t.IdTipoCentroNavigation)
                .FirstOrDefaultAsync(m => m.IdCentro == id);
            if (tblCentro == null)
            {
                return NotFound();
            }

            return View(tblCentro);
        }

        // GET: TblCentroes/Create
        public IActionResult Create()
        {
            ViewData["IdTipoCentro"] = new SelectList(_context.TblTipoCentros, "IdTipoCentro", "TipoCentro");
            return View();
        }

        // POST: TblCentroes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdTipoCentro,Nombre,Direccion,Telefono,Email,Web")] TblCentro tblCentro)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tblCentro);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdTipoCentro"] = new SelectList(_context.TblTipoCentros, "IdTipoCentro", "TipoCentro", tblCentro.IdTipoCentro);
            return View(tblCentro);
        }

        // GET: TblCentroes/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblCentro = await _context.TblCentros.FindAsync(id);
            if (tblCentro == null)
            {
                return NotFound();
            }
            ViewData["IdTipoCentro"] = new SelectList(_context.TblTipoCentros, "IdTipoCentro", "IdTipoCentro", tblCentro.IdTipoCentro);
            return View(tblCentro);
        }

        // POST: TblCentroes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("IdCentro,IdTipoCentro,Nombre,Direccion,Telefono,Email,Web")] TblCentro tblCentro)
        {
            if (id != tblCentro.IdCentro)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tblCentro);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TblCentroExists(tblCentro.IdCentro))
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
            ViewData["IdTipoCentro"] = new SelectList(_context.TblTipoCentros, "IdTipoCentro", "IdTipoCentro", tblCentro.IdTipoCentro);
            return View(tblCentro);
        }

        // GET: TblCentroes/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblCentro = await _context.TblCentros
                .Include(t => t.IdTipoCentroNavigation)
                .FirstOrDefaultAsync(m => m.IdCentro == id);
            if (tblCentro == null)
            {
                return NotFound();
            }

            return View(tblCentro);
        }

        // POST: TblCentroes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var tblCentro = await _context.TblCentros.FindAsync(id);
            if (tblCentro != null)
            {
                _context.TblCentros.Remove(tblCentro);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TblCentroExists(long id)
        {
            return _context.TblCentros.Any(e => e.IdCentro == id);
        }
    }
}
