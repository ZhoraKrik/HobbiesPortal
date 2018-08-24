using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HobbiesPortal.Controllers
{
    public class HomeController : Controller
    {
        // GET: /<controller>/
        public IActionResult Default()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Наш сервер очень хороший и забавный.";

            return View();
        }

        public IActionResult Contacts()
        {
            ViewData["Message"] = "Наши контакты:";

            return View();
        }
    }
}
