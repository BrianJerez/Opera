using Microsoft.AspNetCore.Mvc;

namespace Opera.Controllers
{
    [Route("")]
    public class LandingPagesController : Controller
    {
        [Route("")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("Registro")]
        public IActionResult Registro()
        {
            return View();
        }
    }
}