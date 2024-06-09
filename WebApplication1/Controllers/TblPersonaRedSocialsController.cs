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
    public class TblPersonaRedSocialsController : Controller
    {
        private readonly DatabaseContext _context;

        public TblPersonaRedSocialsController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: TblPersonaRedSocials
        public async Task<IActionResult> Index()
        {
            var databaseContext = _context.TblPersonaRedSocials.Include(t => t.IdPersonaNavigation).Include(t => t.IdRedSocialNavigation);
            return View(await databaseContext.ToListAsync());
        }

        // GET: TblPersonaRedSocials/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblPersonaRedSocial = await _context.TblPersonaRedSocials
                .Include(t => t.IdPersonaNavigation)
                .Include(t => t.IdRedSocialNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tblPersonaRedSocial == null)
            {
                return NotFound();
            }

            return View(tblPersonaRedSocial);
        }

        // GET: TblPersonaRedSocials/Create
        public IActionResult Create()
        {
            ViewData["IdPersona"] = new SelectList(_context.TblPersonas, "IdPersona", "IdPersona");
            ViewData["IdRedSocial"] = new SelectList(_context.TblRedSocials, "IdRedSocial", "IdRedSocial");
            return View();
        }

        // POST: TblPersonaRedSocials/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IdRedSocial,IdPersona,RedSocial")] TblPersonaRedSocial tblPersonaRedSocial)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tblPersonaRedSocial);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdPersona"] = new SelectList(_context.TblPersonas, "IdPersona", "IdPersona", tblPersonaRedSocial.IdPersona);
            ViewData["IdRedSocial"] = new SelectList(_context.TblRedSocials, "IdRedSocial", "IdRedSocial", tblPersonaRedSocial.IdRedSocial);
            return View(tblPersonaRedSocial);
        }

        // GET: TblPersonaRedSocials/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblPersonaRedSocial = await _context.TblPersonaRedSocials.FindAsync(id);
            if (tblPersonaRedSocial == null)
            {
                return NotFound();
            }
            ViewData["IdPersona"] = new SelectList(_context.TblPersonas, "IdPersona", "IdPersona", tblPersonaRedSocial.IdPersona);
            ViewData["IdRedSocial"] = new SelectList(_context.TblRedSocials, "IdRedSocial", "IdRedSocial", tblPersonaRedSocial.IdRedSocial);
            return View(tblPersonaRedSocial);
        }

        // POST: TblPersonaRedSocials/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,IdRedSocial,IdPersona,RedSocial")] TblPersonaRedSocial tblPersonaRedSocial)
        {
            if (id != tblPersonaRedSocial.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tblPersonaRedSocial);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TblPersonaRedSocialExists(tblPersonaRedSocial.Id))
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
            ViewData["IdPersona"] = new SelectList(_context.TblPersonas, "IdPersona", "IdPersona", tblPersonaRedSocial.IdPersona);
            ViewData["IdRedSocial"] = new SelectList(_context.TblRedSocials, "IdRedSocial", "IdRedSocial", tblPersonaRedSocial.IdRedSocial);
            return View(tblPersonaRedSocial);
        }

        // GET: TblPersonaRedSocials/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblPersonaRedSocial = await _context.TblPersonaRedSocials
                .Include(t => t.IdPersonaNavigation)
                .Include(t => t.IdRedSocialNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tblPersonaRedSocial == null)
            {
                return NotFound();
            }

            return View(tblPersonaRedSocial);
        }

        // POST: TblPersonaRedSocials/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var tblPersonaRedSocial = await _context.TblPersonaRedSocials.FindAsync(id);
            if (tblPersonaRedSocial != null)
            {
                _context.TblPersonaRedSocials.Remove(tblPersonaRedSocial);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TblPersonaRedSocialExists(long id)
        {
            return _context.TblPersonaRedSocials.Any(e => e.Id == id);
        }
    }
}
