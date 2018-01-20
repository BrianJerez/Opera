using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Opera.Models;


namespace Opera.Controllers
{
    [Authorize, Route("")] 
    public class AppPagesController : Controller
    {
        private OperaDataContext _db;

        public AppPagesController(OperaDataContext db)
        {
            _db = db;
        }

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