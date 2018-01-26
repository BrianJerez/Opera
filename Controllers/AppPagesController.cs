using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Opera.Models;
using Opera.ViewModels;
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
                QuestionTitle = info.QuestionTitle,
                QuestionDescription = info.QuestionDescription,
                UserId = userId,
                QuestionVotes = 0,
                QuestionDate = DateTime.Now,
            };

            await _db.Questions.AddAsync(newQuestion);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }

        [Route("question/{id:int}")]
        public IActionResult Question(int id)
        {
            var QuestionVM = new QuestionViewModel
            {
                GetQuestion = _db.Questions.FirstOrDefault( x => x.QuestionId == id),
                Answers = _db.Answers.Where(x => x.QuestionId == id).OrderByDescending(X => X.AnswerId),
            };

            ViewBag.Contenido = Markdown.ToHtml(QuestionVM.GetQuestion.QuestionDescription).ToString();
            
            return View(QuestionVM);
        }
        
        [HttpPost, Route("AddAnswer/{id:int}")]
        public async Task<IActionResult> AddAnswer(int id, QuestionViewModel answerData)
        {
            var newAnswer = new Answer
            {
                Content_Answer = answerData.AnswerContent,
                QuestionId = id
            };

            await _db.Answers.AddAsync(newAnswer);
            _db.SaveChanges();

            return RedirectToAction("question", id);
        }

        [HttpGet, Route("search")]
        public IActionResult Search(string query)
        {
            IQueryable<Question> searchResult = _db.Questions.Where(x => x.QuestionTitle.Contains(query));
            return View(searchResult);
        }

        //todo
        //Add the most recent questions on the index
        //implement a search, almost done I just have to stylish it
    }
}