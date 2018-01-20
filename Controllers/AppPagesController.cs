using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Opera.Controllers
{
    [Authorize, Route("")] 
    public class AppPagesController : Controller
    {
        [Route("dashboard")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("question/{id:int}/{title}")]
        public IActionResult Question(int id, string title)
        {
            return View();
        }
    }
}