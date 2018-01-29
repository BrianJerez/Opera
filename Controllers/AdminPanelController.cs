using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel.DataAnnotations;


namespace Opera.Controllers
{
    [Authorize(Roles = "Administrador"), Route("admin")]
    public class AdminPanelController : Controller
    {
        [Route("")]
        public IActionResult Index()
        {
            return View();
        }
    }
}