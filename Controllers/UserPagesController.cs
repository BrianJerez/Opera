using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Opera.Models;
using Opera.ViewModels;
using System.Linq;

namespace Opera.Controllers
{
    [Authorize, Route("")]
    public class UserPagesController : Controller
    {
        private UserManager<CustomUserFields> _userManager;
        private OperaDataContext _db;
        public UserPagesController(UserManager<CustomUserFields> userManager, OperaDataContext db)
        {
            _userManager = userManager;
            _db = db;
        }
        //todo
        //implement an user profile
        [Route("/u/{usuario}")]
        public IActionResult Index(string user)
        {
            var userProfileData = _userManager.FindByNameAsync(User.Identity.Name).Result;
            var userInfoData = new UserInfoViewModel
            {
                UserInfo = userProfileData,
                Questions = _db.Questions.Where(x => x.UserId == userProfileData.Id).OrderByDescending(x => x.QuestionDate).Take(5),
                Answers = _db.Answers.Where(x => x.UserId == userProfileData.Id).OrderByDescending(x => x.AnswerDate).Take(5)
            };

            return View(userInfoData);
        }
    }
}