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
    public class TblTipoCentroesController : Controller
    {
        private readonly DatabaseContext _context;

        public TblTipoCentroesController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: TblTipoCentroes
        public async Task<IActionResult> Index()
        {
            return View(await _context.TblTipoCentros.ToListAsync());
        }

        // GET: TblTipoCentroes/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblTipoCentro = await _context.TblTipoCentros
                .FirstOrDefaultAsync(m => m.IdTipoCentro == id);
            if (tblTipoCentro == null)
            {
                return NotFound();
            }

            return View(tblTipoCentro);
        }

        // GET: TblTipoCentroes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TblTipoCentroes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdTipoCentro,TipoCentro")] TblTipoCentro tblTipoCentro)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tblTipoCentro);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tblTipoCentro);
        }

        // GET: TblTipoCentroes/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblTipoCentro = await _context.TblTipoCentros.FindAsync(id);
            if (tblTipoCentro == null)
            {
                return NotFound();
            }
            return View(tblTipoCentro);
        }

        // POST: TblTipoCentroes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("IdTipoCentro,TipoCentro")] TblTipoCentro tblTipoCentro)
        {
            if (id != tblTipoCentro.IdTipoCentro)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tblTipoCentro);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TblTipoCentroExists(tblTipoCentro.IdTipoCentro))
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
            return View(tblTipoCentro);
        }

        // GET: TblTipoCentroes/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblTipoCentro = await _context.TblTipoCentros
                .FirstOrDefaultAsync(m => m.IdTipoCentro == id);
            if (tblTipoCentro == null)
            {
                return NotFound();
            }

            return View(tblTipoCentro);
        }

        // POST: TblTipoCentroes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var tblTipoCentro = await _context.TblTipoCentros.FindAsync(id);
            if (tblTipoCentro != null)
            {
                _context.TblTipoCentros.Remove(tblTipoCentro);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TblTipoCentroExists(long id)
        {
            return _context.TblTipoCentros.Any(e => e.IdTipoCentro == id);
        }
    }
}
