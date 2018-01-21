using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Opera.Models;
using System.Threading.Tasks;
using Markdig;


namespace Opera.Controllers
{
    [Authorize, Route("")] 
    public class AppPagesController : Controller
    {
        private OperaDataContext _db;
        private UserManager<IdentityUser> _userManager;

        public AppPagesController(UserManager<IdentityUser> userManager, OperaDataContext db) 
        {
            _userManager = userManager;
            _db = db;
        }

        [Route("dashboard")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("ask")]
        public IActionResult AddQuestion()
        {
            return View();
        }

        [HttpPost, Route("InsertQuestion")]
        public async Task<IActionResult> InsertQuestion(Question info)
        {
            var userId = _userManager.FindByNameAsync(User.Identity.Name).Result.Id;

            var newQuestion = new Question
            {
                Title_Question = info.Title_Question,
                Description_Question = info.Description_Question,
                Id_User = userId,
                Votes_Question = 0,
                Date_Question = DateTime.Now,
            };

            await _db.Questions.AddAsync(newQuestion);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }

        [Route("question/{id:int}")]
        public IActionResult Question(int id)
        {
            var y = _db.Questions.FirstOrDefault(x => x.Id_Question == id);
            var mx = Markdown.ToHtml(y.Description_Question).Replace(@"&lt;","<").Replace(@"&gt;",">").Replace(@"&#47;", "/");
            ViewBag.Contenido = mx;
            Console.WriteLine(mx);
            return View(y);
        }
        //todo
        //add question action
        //implement a search, looking in the question table if contains the word
    }
}