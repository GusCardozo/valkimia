using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Valkimia.Context;
using Valkimia.Models;
using Valkimia.Utilities;

namespace Valkimia.Controllers
{
    public class LoginController : Controller
    {
        private readonly ValkimiaContext _context;

        public LoginController(ValkimiaContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([Bind("Email, Password")] Cliente login)
        {
            var log = await _context.Clientes.Where(x => x.Email == login.Email && x.Password == Security.CalculateMD5Hash(login.Password)).FirstOrDefaultAsync();

            if (log == null)
            {
                return NotFound("Usuario no registrado");
            }
            
            HttpContext.Session.SetInt32("Logued", 1);
            HttpContext.Session.SetInt32("Usuario", log.Id);
            return RedirectToAction("Index", "Factura");
            
        }
    }
}
