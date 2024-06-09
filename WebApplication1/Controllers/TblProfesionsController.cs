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
    public class TblProfesionsController : Controller
    {
        private readonly DatabaseContext _context;

        public TblProfesionsController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: TblProfesions
        public async Task<IActionResult> Index()
        {
            return View(await _context.TblProfesions.ToListAsync());
        }

        // GET: TblProfesions/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblProfesion = await _context.TblProfesions
                .FirstOrDefaultAsync(m => m.IdProfesion == id);
            if (tblProfesion == null)
            {
                return NotFound();
            }

            return View(tblProfesion);
        }

        // GET: TblProfesions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TblProfesions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdProfesion,Profesion")] TblProfesion tblProfesion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tblProfesion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tblProfesion);
        }

        // GET: TblProfesions/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblProfesion = await _context.TblProfesions.FindAsync(id);
            if (tblProfesion == null)
            {
                return NotFound();
            }
            return View(tblProfesion);
        }

        // POST: TblProfesions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("IdProfesion,Profesion")] TblProfesion tblProfesion)
        {
            if (id != tblProfesion.IdProfesion)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tblProfesion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TblProfesionExists(tblProfesion.IdProfesion))
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
            return View(tblProfesion);
        }

        // GET: TblProfesions/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblProfesion = await _context.TblProfesions
                .FirstOrDefaultAsync(m => m.IdProfesion == id);
            if (tblProfesion == null)
            {
                return NotFound();
            }

            return View(tblProfesion);
        }

        // POST: TblProfesions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var tblProfesion = await _context.TblProfesions.FindAsync(id);
            if (tblProfesion != null)
            {
                _context.TblProfesions.Remove(tblProfesion);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TblProfesionExists(long id)
        {
            return _context.TblProfesions.Any(e => e.IdProfesion == id);
        }
    }
}
