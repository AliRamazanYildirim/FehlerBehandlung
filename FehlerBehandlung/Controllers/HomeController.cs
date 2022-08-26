using FehlerBehandlung.Filter;
using FehlerBehandlung.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FehlerBehandlung.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        
        public IActionResult Index()
        {
            int zahl1 = 7;
            int zahl2 = 0;
            int resultat = zahl1 / zahl2;
            return View();
        }
        
        public IActionResult Privacy()
        {
            throw new FileNotFoundException();
            return View();
        }
        [AllowAnonymous]//Jeder kann auf die Seite zugreifen. 
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            var ausnahme = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            ViewBag.weg = ausnahme.Path;
            ViewBag.nachricht = ausnahme.Error.Message;
            return View();
        }

        public IActionResult Fehler()
        {
            return View();
        }

        public IActionResult Fehler1()
        {
            return View();
        }
    }
}