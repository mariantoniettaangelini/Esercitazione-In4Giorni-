using Esercitazione.Models;
using Esercitazione.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Esercitazione.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IClientiService _clientiService;

        public HomeController(ILogger<HomeController> logger, IClientiService clientiService)
        {
            _logger = logger;
            _clientiService = clientiService;
        }

        public IActionResult Index()
        {
            return View();
        }

        // Clienti
        [Authorize]
        [HttpGet]
        public IActionResult creaClienti()
        {
            return View(new Cliente());
        }

        [HttpPost]
        public IActionResult creaClienti(Cliente model)
        {
            if (ModelState.IsValid)
            {
                // Logica per salvare il cliente nel database
                // usando model.TipoCliente per determinare se è un privato o un'azienda
                if (model.TipoCliente == "Privato")
                {
                    // Salva il privato
                }
                else if (model.TipoCliente == "Azienda")
                {
                    // Salva l'azienda
                }

                return RedirectToAction("Index");
            }

            return View(model);
        }

        [Authorize]
        public IActionResult Clienti()
        {
            var clienti = _clientiService.GetAll().ToList();
            return View(clienti);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
