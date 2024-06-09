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
    public class TblRedSocialsController : Controller
    {
        private readonly DatabaseContext _context;

        public TblRedSocialsController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: TblRedSocials
        public async Task<IActionResult> Index()
        {
            return View(await _context.TblRedSocials.ToListAsync());
        }

        // GET: TblRedSocials/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblRedSocial = await _context.TblRedSocials
                .FirstOrDefaultAsync(m => m.IdRedSocial == id);
            if (tblRedSocial == null)
            {
                return NotFound();
            }

            return View(tblRedSocial);
        }

        // GET: TblRedSocials/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TblRedSocials/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdRedSocial,RedSocial")] TblRedSocial tblRedSocial)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tblRedSocial);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tblRedSocial);
        }

        // GET: TblRedSocials/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblRedSocial = await _context.TblRedSocials.FindAsync(id);
            if (tblRedSocial == null)
            {
                return NotFound();
            }
            return View(tblRedSocial);
        }

        // POST: TblRedSocials/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("IdRedSocial,RedSocial")] TblRedSocial tblRedSocial)
        {
            if (id != tblRedSocial.IdRedSocial)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tblRedSocial);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TblRedSocialExists(tblRedSocial.IdRedSocial))
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
            return View(tblRedSocial);
        }

        // GET: TblRedSocials/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblRedSocial = await _context.TblRedSocials
                .FirstOrDefaultAsync(m => m.IdRedSocial == id);
            if (tblRedSocial == null)
            {
                return NotFound();
            }

            return View(tblRedSocial);
        }

        // POST: TblRedSocials/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var tblRedSocial = await _context.TblRedSocials.FindAsync(id);
            if (tblRedSocial != null)
            {
                _context.TblRedSocials.Remove(tblRedSocial);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TblRedSocialExists(long id)
        {
            return _context.TblRedSocials.Any(e => e.IdRedSocial == id);
        }
    }
}
