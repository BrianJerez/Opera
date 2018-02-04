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
        private UserManager<CustomUserFields> _userManager;
        private CustomUserFields _userinfo;

        public AppPagesController(UserManager<CustomUserFields> userManager, OperaDataContext db) 
        {
            _userManager = userManager;
            _db = db;
        }

        [Route("dashboard")]
        public IActionResult Index()
        {
            _userinfo = _userManager.FindByNameAsync(User.Identity.Name).Result;
            
            ViewBag.Notifications = _db.Notifications.OrderBy(x => x.Date)
            .Where(x => x.Seen == false && x.Question.UserId == _userinfo.Id).Count() > 0 ? "notification-color" : "";

            PaginationViewModel paginationView = new PaginationViewModel
            {
                AmmountOfPages = Convert.ToDouble(_db.Questions.Count()) / Convert.ToDouble(5) > ( _db.Questions.Count() / 5 ) ? (_db.Questions.Count() / 5) + 1 : _db.Questions.Count() / 5,
                Questions = _db.Questions.Take(5).OrderByDescending(x=>x.QuestionId)
            };

            return View(paginationView);
        }

        [Route("page/{id:int}")]
        public IActionResult Page(int id)
        {
            _userinfo = _userManager.FindByNameAsync(User.Identity.Name).Result;
            
            ViewBag.Notifications = _db.Notifications.OrderBy(x => x.Date)
            .Where(x => x.Seen == false && x.Question.UserId == _userinfo.Id).Count() > 0 ? "notification-color" : "";

            if(id == 0 || id == 1){
                return RedirectToAction("Index", "AppPages");
            }

            ViewBag.Id = id;

            PaginationViewModel paginationView = new PaginationViewModel
            {
                AmmountOfPages = _db.Questions.Count() / 5,
                Questions = _db.Questions.OrderByDescending(x => x.QuestionId).Skip((id-1)*5).Take(5)
            };

            return View(paginationView);
        }

        [Route("ask")]
        public IActionResult AddQuestion()
        {
            _userinfo = _userManager.FindByNameAsync(User.Identity.Name).Result;
            
            ViewBag.Notifications = _db.Notifications.OrderBy(x => x.Date)
            .Where(x => x.Seen == false && x.Question.UserId == _userinfo.Id).Count() > 0 ? "notification-color" : "";

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
            _userinfo = _userManager.FindByNameAsync(User.Identity.Name).Result;
            
            ViewBag.Notifications = _db.Notifications.OrderBy(x => x.Date)
            .Where(x => x.Seen == false && x.Question.UserId == _userinfo.Id).Count() > 0 ? "notification-color" : "";

            ViewBag.Id = id;
            var QuestionVM = new QuestionViewModel
            {
                GetQuestion = _db.Questions.FirstOrDefault( x => x.QuestionId == id),
                Answers = _db.Answers.Where(x => x.QuestionId == id).OrderByDescending(X => X.AnswerId),
                UserFieldsInfo = _userinfo
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
                QuestionId = id,
                AnswerDate = DateTime.Now,
                UserId = _userManager.FindByNameAsync(User.Identity.Name).Result.Id
            };

            await _db.Answers.AddAsync(newAnswer);
            
            await _db.Notifications.AddAsync(new Notification{
                UserName = User.Identity.Name,
                Date = DateTime.Now,
                Seen = false,
                QuestionId = id
            });
            
            _db.SaveChanges();

            return RedirectToAction("question", id);
        }

        [HttpGet, Route("search")]
        public IActionResult Search(string query)
        {
            _userinfo = _userManager.FindByNameAsync(User.Identity.Name).Result;
            
            ViewBag.Notifications = _db.Notifications.OrderBy(x => x.Date)
            .Where(x => x.Seen == false && x.Question.UserId == _userinfo.Id).Count() > 0 ? "notification-color" : "";

            IQueryable<Question> searchResult = _db.Questions.Where(x => 
                x.QuestionTitle.Contains(query) || x.QuestionDescription.Contains(query)
            );

            PaginationViewModel paginationView = new PaginationViewModel
            {
                AmmountOfPages = Convert.ToDouble(Convert.ToDouble(searchResult.Count()) / 5.0) > (searchResult.Count() / 5) ? (searchResult.Count() / 5) + 1 : searchResult.Count() / 5,
                Questions = searchResult.OrderByDescending(x => x.QuestionId).Take(5)
            };

            ViewBag.Query = query;
            ViewBag.Id = 1;

            return View(paginationView);
        }

        [Route("search/{query}/{id:int}")]
        public IActionResult SearchPage(string query, int id)
        {
            _userinfo = _userManager.FindByNameAsync(User.Identity.Name).Result;
            
            ViewBag.Notifications = _db.Notifications.OrderBy(x => x.Date)
            .Where(x => x.Seen == false && x.Question.UserId == _userinfo.Id).Count() > 0 ? "notification-color" : "";

            IQueryable<Question> searchResult = _db.Questions.Where(x => 
                x.QuestionTitle.Contains(query) || x.QuestionDescription.Contains(query)
            );

            PaginationViewModel paginationView = new PaginationViewModel
            {
                AmmountOfPages = Convert.ToDouble(Convert.ToDouble(searchResult.Count()) / 5.0) > (searchResult.Count() / 5) ? (searchResult.Count() / 5) + 1 : searchResult.Count() / 5,
                Questions = searchResult.OrderByDescending(x => x.QuestionId).Skip(id*5).Take(5)
            };

            if(id == 0){
                paginationView.Questions = searchResult.OrderByDescending(x => x.QuestionId).Take(5);
            }

            ViewBag.Query = query;
            ViewBag.Id = id;

            return View(paginationView);
        }

        [Route("reportquestion/{id:int}")]
        public IActionResult ReportQuestion(int id)
        {
            _userinfo = _userManager.FindByNameAsync(User.Identity.Name).Result;
            
            ViewBag.Notifications = _db.Notifications.OrderBy(x => x.Date)
            .Where(x => x.Seen == false && x.Question.UserId == _userinfo.Id).Count() > 0 ? "notification-color" : "";

            _db.QuestionReports.Add(new QuestionReport{
                QuestionId = id
            });

            _db.SaveChanges();

            return RedirectToAction("question", id);
        }

        [Route("reportanswer/{id:int}")]
        public IActionResult ReportAnswer(int id)
        {
            _userinfo = _userManager.FindByNameAsync(User.Identity.Name).Result;
            
            ViewBag.Notifications = _db.Notifications.OrderBy(x => x.Date)
            .Where(x => x.Seen == false && x.Question.UserId == _userinfo.Id).Count() > 0 ? "notification-color" : "";
            
            _db.AnswerReports.Add(new AnswerReport{
                AnswerId = id
            });

            _db.SaveChanges();

            id = _db.Answers.Find(id).QuestionId;
            return RedirectToAction("question", id);
        }

        //todo
        //add voting functionality
    }
}