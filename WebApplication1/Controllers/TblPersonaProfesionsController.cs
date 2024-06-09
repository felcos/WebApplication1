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
    public class TblPersonaProfesionsController : Controller
    {
        private readonly DatabaseContext _context;
        private DatabaseContext dbt = new DatabaseContext();

        public TblPersonaProfesionsController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: TblPersonaProfesions
        public async Task<IActionResult> Index(long? idPersona)
        {
            var xViewPersona = dbt.ViewPersonas.Where(x => x.IdPersona == idPersona);
            return View(await xViewPersona.ToListAsync());
        }

        // GET: TblPersonaProfesions/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblPersonaProfesion = await _context.TblPersonaProfesions
                .Include(t => t.IdPersonaNavigation)
                .Include(t => t.IdProfesionNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tblPersonaProfesion == null)
            {
                return NotFound();
            }

            return View(tblPersonaProfesion);
        }

        // GET: TblPersonaProfesions/Create
        public IActionResult Create(long? IdPersona)
        {
            ViewData["IdPersona"] = new SelectList(_context.TblPersonas, "IdPersona", "Persona");
            ViewData["IdProfesion"] = new SelectList(_context.TblProfesions, "IdProfesion", "Profesion");
            return View();
        }

        // POST: TblPersonaProfesions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IdPersona,IdProfesion")] TblPersonaProfesion tblPersonaProfesion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tblPersonaProfesion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdPersona"] = new SelectList(_context.TblPersonas, "IdPersona", "IdPersona", tblPersonaProfesion.IdPersona);
            ViewData["IdProfesion"] = new SelectList(_context.TblProfesions, "IdProfesion", "IdProfesion", tblPersonaProfesion.IdProfesion);
            return View(tblPersonaProfesion);
        }

        // GET: TblPersonaProfesions/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblPersonaProfesion = await _context.TblPersonaProfesions.FindAsync(id);
            if (tblPersonaProfesion == null)
            {
                return NotFound();
            }
            ViewData["IdPersona"] = new SelectList(_context.TblPersonas, "IdPersona", "IdPersona", tblPersonaProfesion.IdPersona);
            ViewData["IdProfesion"] = new SelectList(_context.TblProfesions, "IdProfesion", "IdProfesion", tblPersonaProfesion.IdProfesion);
            return View(tblPersonaProfesion);
        }

        // POST: TblPersonaProfesions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,IdPersona,IdProfesion")] TblPersonaProfesion tblPersonaProfesion)
        {
            if (id != tblPersonaProfesion.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tblPersonaProfesion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TblPersonaProfesionExists(tblPersonaProfesion.Id))
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
            ViewData["IdPersona"] = new SelectList(_context.TblPersonas, "IdPersona", "IdPersona", tblPersonaProfesion.IdPersona);
            ViewData["IdProfesion"] = new SelectList(_context.TblProfesions, "IdProfesion", "IdProfesion", tblPersonaProfesion.IdProfesion);
            return View(tblPersonaProfesion);
        }

        // GET: TblPersonaProfesions/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblPersonaProfesion = await _context.TblPersonaProfesions
                .Include(t => t.IdPersonaNavigation)
                .Include(t => t.IdProfesionNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tblPersonaProfesion == null)
            {
                return NotFound();
            }

            return View(tblPersonaProfesion);
        }

        // POST: TblPersonaProfesions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var tblPersonaProfesion = await _context.TblPersonaProfesions.FindAsync(id);
            if (tblPersonaProfesion != null)
            {
                _context.TblPersonaProfesions.Remove(tblPersonaProfesion);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TblPersonaProfesionExists(long id)
        {
            return _context.TblPersonaProfesions.Any(e => e.Id == id);
        }
    }
}
