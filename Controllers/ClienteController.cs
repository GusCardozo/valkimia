using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Valkimia.Context;
using Valkimia.Models;
using Valkimia.Utilities;

namespace Valkimia.Controllers
{
    public class ClienteController : Controller
    {
        private readonly ValkimiaContext _context;

        public ClienteController(ValkimiaContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetInt32("Logued") == 1)
            {
                int? clienteId = HttpContext.Session.GetInt32("Usuario");
                var client = await _context.Clientes.Include(x => x.Ciudad).Where(x => x.Id == clienteId && x.Habilitado).ToListAsync();
                return View(client);
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

                var clientes = await _context.Clientes
                    .Include(x => x.Ciudad)
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (clientes == null)
                {
                    return NotFound();
                }

                return View(clientes);
            }

            return NotFound(); 
        }

        public IActionResult Create()
        {
            ViewData["Ciudad"] = new SelectList(_context.Ciudades, "Id", "Nombre");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Apellido,Domicilio,Email,Password,CiudadId")] Cliente clientes)
        {
            if (ModelState.IsValid)
            {
                clientes.Habilitado = true;
                clientes.Password = Security.CalculateMD5Hash(clientes.Password);
                _context.Add(clientes);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Login");
            }

            ViewData["Ciudad"] = new SelectList(_context.Ciudades, "Id", "Nombre", clientes.CiudadId);

            return View(clientes);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (HttpContext.Session.GetInt32("Logued") == 1)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var clientes = await _context.Clientes.FindAsync(id);
                if (clientes == null)
                {
                    return NotFound();
                }

                ViewData["Ciudad"] = new SelectList(_context.Ciudades, "Id", "Nombre", clientes.CiudadId);

                return View(clientes);
            }

            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Apellido,Domicilio,Email,Password,CiudadId,Habilitado")] Cliente clientes)
        {
            if (HttpContext.Session.GetInt32("Logued") == 1)
            {
                if (id != clientes.Id)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(clientes);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!ClientesExists(clientes.Id))
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

                ViewData["Ciudad"] = new SelectList(_context.Ciudades, "Id", "Nombre", clientes.CiudadId);

                return View(clientes);
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

                var clientes = await _context.Clientes
                    .Include(x => x.Ciudad)
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (clientes == null)
                {
                    return NotFound();
                }

                return View(clientes);
            }

            return NotFound();
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (HttpContext.Session.GetInt32("Logued") == 1)
            {
                var clientes = await _context.Clientes.FindAsync(id);
                clientes.Habilitado = false;
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return NotFound();
        }

        private bool ClientesExists(int id)
        {
            return _context.Clientes.Any(e => e.Id == id);
        }
    }
}
