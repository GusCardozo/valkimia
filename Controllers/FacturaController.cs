using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Valkimia.Context;
using Valkimia.Models;

namespace Valkimia.Controllers
{
    public class FacturaController : Controller
    {
        private readonly ValkimiaContext _context;

        public FacturaController(ValkimiaContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetInt32("Logued") == 1)
            {
                return View(await _context.Facturas.Include(x => x.Cliente).ToListAsync());
            }

            return NotFound();
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (HttpContext.Session.GetInt32("Logued") == 1)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var facturas = await _context.Facturas
                    .Include(x => x.Cliente)
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (facturas == null)
                {
                    return NotFound();
                }

                return View(facturas);
            }

            return NotFound();
        }

        public IActionResult Create()
        {
            if (HttpContext.Session.GetInt32("Logued") == 1)
            {
                ViewData["Cliente"] = new SelectList(_context.Clientes, "Id", "Nombre");

                return View();
            }

            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ClienteId,Fecha,Detalle,Importe")] Factura facturas)
        {
            if (HttpContext.Session.GetInt32("Logued") == 1)
            {
                if (ModelState.IsValid)
                {
                    _context.Add(facturas);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }

                ViewData["Cliente"] = new SelectList(_context.Clientes, "Id", "Nombre", facturas.ClienteId);

                return View(facturas);
            }

            return NotFound();
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (HttpContext.Session.GetInt32("Logued") == 1)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var facturas = await _context.Facturas.FindAsync(id);
                if (facturas == null)
                {
                    return NotFound();
                }

                ViewData["Cliente"] = new SelectList(_context.Clientes, "Id", "Nombre", facturas.ClienteId);

                return View(facturas);
            }

            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ClienteId,Fecha,Detalle,Importe")] Factura facturas)
        {
            if (HttpContext.Session.GetInt32("Logued") == 1)
            {
                if (id != facturas.Id)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(facturas);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!FacturasExists(facturas.Id))
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

                ViewData["Cliente"] = new SelectList(_context.Clientes, "Id", "Nombre", facturas.ClienteId);

                return View(facturas);
            }

            return NotFound();
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (HttpContext.Session.GetInt32("Logued") == 1)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var facturas = await _context.Facturas
                    .Include(x => x.Cliente)
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (facturas == null)
                {
                    return NotFound();
                }

                return View(facturas);
            }

            return NotFound();
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (HttpContext.Session.GetInt32("Logued") == 1)
            {
                var facturas = await _context.Facturas.FindAsync(id);
                _context.Facturas.Remove(facturas);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return NotFound();
        }

        private bool FacturasExists(int id)
        {
            return _context.Facturas.Any(e => e.Id == id);
        }
    }
}
