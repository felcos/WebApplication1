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
    public class TblPersonaDatoesController : Controller
    {
        private readonly DatabaseContext _context;

        public TblPersonaDatoesController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: TblPersonaDatoes
        public async Task<IActionResult> Index()
        {
            var databaseContext = _context.TblPersonaDatos.Include(t => t.IdPersonaNavigation);
            return View(await databaseContext.ToListAsync());
        }

        // GET: TblPersonaDatoes/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblPersonaDato = await _context.TblPersonaDatos
                .Include(t => t.IdPersonaNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tblPersonaDato == null)
            {
                return NotFound();
            }

            return View(tblPersonaDato);
        }

        // GET: TblPersonaDatoes/Create
        public IActionResult Create()
        {
            ViewData["IdPersona"] = new SelectList(_context.TblPersonas, "IdPersona", "IdPersona");
            return View();
        }

        // POST: TblPersonaDatoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IdPersona,Dato,Texto")] TblPersonaDato tblPersonaDato)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tblPersonaDato);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdPersona"] = new SelectList(_context.TblPersonas, "IdPersona", "IdPersona", tblPersonaDato.IdPersona);
            return View(tblPersonaDato);
        }

        // GET: TblPersonaDatoes/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblPersonaDato = await _context.TblPersonaDatos.FindAsync(id);
            if (tblPersonaDato == null)
            {
                return NotFound();
            }
            ViewData["IdPersona"] = new SelectList(_context.TblPersonas, "IdPersona", "IdPersona", tblPersonaDato.IdPersona);
            return View(tblPersonaDato);
        }

        // POST: TblPersonaDatoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,IdPersona,Dato,Texto")] TblPersonaDato tblPersonaDato)
        {
            if (id != tblPersonaDato.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tblPersonaDato);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TblPersonaDatoExists(tblPersonaDato.Id))
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
            ViewData["IdPersona"] = new SelectList(_context.TblPersonas, "IdPersona", "IdPersona", tblPersonaDato.IdPersona);
            return View(tblPersonaDato);
        }

        // GET: TblPersonaDatoes/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblPersonaDato = await _context.TblPersonaDatos
                .Include(t => t.IdPersonaNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tblPersonaDato == null)
            {
                return NotFound();
            }

            return View(tblPersonaDato);
        }

        // POST: TblPersonaDatoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var tblPersonaDato = await _context.TblPersonaDatos.FindAsync(id);
            if (tblPersonaDato != null)
            {
                _context.TblPersonaDatos.Remove(tblPersonaDato);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TblPersonaDatoExists(long id)
        {
            return _context.TblPersonaDatos.Any(e => e.Id == id);
        }
    }
}
