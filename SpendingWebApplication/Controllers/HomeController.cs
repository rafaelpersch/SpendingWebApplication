using Microsoft.AspNetCore.Mvc;
using SpendingWebApplication.Models;
using SpendingWebApplication.Tools;
using System.Diagnostics;

namespace SpendingWebApplication.Controllers
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
            var user = SessionManagement.GetSession(HttpContext);

            if (user != null)
            {
                return RedirectToAction("Index", "Gasto");
            }

            /*if (TempData["ApplicationMessage"] != null)
            {
                ViewBag.ApplicationMessage = TempData["ApplicationMessage"].ToString();
            }*/

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(string username, string password)
        {
            //try
            //{
                if (username != "username" || password != "password")
                {
                    return RedirectToAction("Index", "Home");
                }

                SessionManagement.CreateSession(HttpContext, new UsuarioModel() { Id = Guid.NewGuid() });

                return RedirectToAction("Index", "Gasto");
            //}
            //catch (Exception ex)
            //{
            //    TempData["ApplicationMessage"] = ex.Message;
            //    return RedirectToAction("Index", "Home");
            //}
        }

        public IActionResult Logout()
        {
            SessionManagement.DeleteSession(HttpContext);
            return RedirectToAction("Index", "Home");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
