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
    public class TblPersonasController : Controller
    {
        private readonly DatabaseContext _context;

        public TblPersonasController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: TblPersonas
        public async Task<IActionResult> Index()
        {
            return View(await _context.TblPersonas.ToListAsync());
        }

        // GET: TblPersonas/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblPersona = await _context.TblPersonas
                .FirstOrDefaultAsync(m => m.IdPersona == id);
            if (tblPersona == null)
            {
                return NotFound();
            }

            return View(tblPersona);
        }

        // GET: TblPersonas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TblPersonas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdPersona,Apellidos,Nombres,Direccion,Telefono")] TblPersona tblPersona)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tblPersona);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tblPersona);
        }

        // GET: TblPersonas/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblPersona = await _context.TblPersonas.FindAsync(id);
            if (tblPersona == null)
            {
                return NotFound();
            }
            return View(tblPersona);
        }

        // POST: TblPersonas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("IdPersona,Apellidos,Nombres,Direccion,Telefono")] TblPersona tblPersona)
        {
            if (id != tblPersona.IdPersona)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tblPersona);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TblPersonaExists(tblPersona.IdPersona))
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
            return View(tblPersona);
        }

        // GET: TblPersonas/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblPersona = await _context.TblPersonas
                .FirstOrDefaultAsync(m => m.IdPersona == id);
            if (tblPersona == null)
            {
                return NotFound();
            }

            return View(tblPersona);
        }

        // POST: TblPersonas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var tblPersona = await _context.TblPersonas.FindAsync(id);
            if (tblPersona != null)
            {
                _context.TblPersonas.Remove(tblPersona);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TblPersonaExists(long id)
        {
            return _context.TblPersonas.Any(e => e.IdPersona == id);
        }
    }
}
