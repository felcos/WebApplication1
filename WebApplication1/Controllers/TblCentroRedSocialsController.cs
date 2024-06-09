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
    public class TblCentroRedSocialsController : Controller
    {
        private readonly DatabaseContext _context;

        public TblCentroRedSocialsController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: TblCentroRedSocials
        public async Task<IActionResult> Index()
        {
            var databaseContext = _context.TblCentroRedSocials.Include(t => t.IdCentroNavigation).Include(t => t.IdRedSocialNavigation);
            return View(await databaseContext.ToListAsync());
        }

        // GET: TblCentroRedSocials/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblCentroRedSocial = await _context.TblCentroRedSocials
                .Include(t => t.IdCentroNavigation)
                .Include(t => t.IdRedSocialNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tblCentroRedSocial == null)
            {
                return NotFound();
            }

            return View(tblCentroRedSocial);
        }

        // GET: TblCentroRedSocials/Create
        public IActionResult Create()
        {
            ViewData["IdCentro"] = new SelectList(_context.TblCentros, "IdCentro", "IdCentro");
            ViewData["IdRedSocial"] = new SelectList(_context.TblRedSocials, "IdRedSocial", "IdRedSocial");
            return View();
        }

        // POST: TblCentroRedSocials/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IdCentro,IdRedSocial,RedSocial")] TblCentroRedSocial tblCentroRedSocial)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tblCentroRedSocial);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCentro"] = new SelectList(_context.TblCentros, "IdCentro", "IdCentro", tblCentroRedSocial.IdCentro);
            ViewData["IdRedSocial"] = new SelectList(_context.TblRedSocials, "IdRedSocial", "IdRedSocial", tblCentroRedSocial.IdRedSocial);
            return View(tblCentroRedSocial);
        }

        // GET: TblCentroRedSocials/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblCentroRedSocial = await _context.TblCentroRedSocials.FindAsync(id);
            if (tblCentroRedSocial == null)
            {
                return NotFound();
            }
            ViewData["IdCentro"] = new SelectList(_context.TblCentros, "IdCentro", "IdCentro", tblCentroRedSocial.IdCentro);
            ViewData["IdRedSocial"] = new SelectList(_context.TblRedSocials, "IdRedSocial", "IdRedSocial", tblCentroRedSocial.IdRedSocial);
            return View(tblCentroRedSocial);
        }

        // POST: TblCentroRedSocials/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,IdCentro,IdRedSocial,RedSocial")] TblCentroRedSocial tblCentroRedSocial)
        {
            if (id != tblCentroRedSocial.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tblCentroRedSocial);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TblCentroRedSocialExists(tblCentroRedSocial.Id))
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
            ViewData["IdCentro"] = new SelectList(_context.TblCentros, "IdCentro", "IdCentro", tblCentroRedSocial.IdCentro);
            ViewData["IdRedSocial"] = new SelectList(_context.TblRedSocials, "IdRedSocial", "IdRedSocial", tblCentroRedSocial.IdRedSocial);
            return View(tblCentroRedSocial);
        }

        // GET: TblCentroRedSocials/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblCentroRedSocial = await _context.TblCentroRedSocials
                .Include(t => t.IdCentroNavigation)
                .Include(t => t.IdRedSocialNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tblCentroRedSocial == null)
            {
                return NotFound();
            }

            return View(tblCentroRedSocial);
        }

        // POST: TblCentroRedSocials/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var tblCentroRedSocial = await _context.TblCentroRedSocials.FindAsync(id);
            if (tblCentroRedSocial != null)
            {
                _context.TblCentroRedSocials.Remove(tblCentroRedSocial);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TblCentroRedSocialExists(long id)
        {
            return _context.TblCentroRedSocials.Any(e => e.Id == id);
        }
    }
}
