using Microsoft.AspNetCore.Mvc;

namespace Opera.Controllers
{
    public class LandingPagesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }
    }
}