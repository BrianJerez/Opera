using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Opera.Controllers
{
    [Authorize] 
    public class AppPagesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}