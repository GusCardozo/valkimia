using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Valkimia.Context;
using Valkimia.Models;

namespace Valkimia.Controllers
{
    public class CiudadController : Controller
    {
        private readonly ValkimiaContext _context;

        public CiudadController(ValkimiaContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Ciudades.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ciudades = await _context.Ciudades
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ciudades == null)
            {
                return NotFound();
            }

            return View(ciudades);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre")] Ciudad ciudades)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ciudades);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(ciudades);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ciudades = await _context.Ciudades.FindAsync(id);
            if (ciudades == null)
            {
                return NotFound();
            }
            return View(ciudades);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre")] Ciudad ciudades)
        {
            if (id != ciudades.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ciudades);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CiudadesExists(ciudades.Id))
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
            return View(ciudades);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ciudades = await _context.Ciudades
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ciudades == null)
            {
                return NotFound();
            }

            return View(ciudades);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ciudades = await _context.Ciudades.FindAsync(id);
            _context.Ciudades.Remove(ciudades);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CiudadesExists(int id)
        {
            return _context.Ciudades.Any(e => e.Id == id);
        }
    }
}
