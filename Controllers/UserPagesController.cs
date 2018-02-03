using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Opera.Models;
using Opera.ViewModels;
using System.Linq;
using System.IO;

namespace Opera.Controllers
{
    [Authorize, Route("")]
    public class UserPagesController : Controller
    {
        private UserManager<CustomUserFields> _userManager;
        private OperaDataContext _db;

        private IdentityDataContext _users;

        private string _folder;

        public UserPagesController(UserManager<CustomUserFields> userManager, IdentityDataContext users, OperaDataContext db, IHostingEnvironment env)
        {
            _userManager = userManager;
            _db = db;
            _users = users;
            _folder = $@"{env.WebRootPath}";
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

        //todo 
        //user setting page

        [Route("ProfileSettings")]
        public IActionResult UserSettings()
        {
            return View();
        }

        [HttpPost, Route("ProfileSettings")]
        public async Task<IActionResult> UserSettings(ProfileUpdate profileUpdate)
        {
            var x = _userManager.FindByNameAsync(User.Identity.Name);
            if(!string.IsNullOrEmpty(profileUpdate.FullName) && !string.IsNullOrWhiteSpace(profileUpdate.FullName) )
            {
                x.Result.FullName = profileUpdate.FullName;
            }

            if(!string.IsNullOrEmpty(profileUpdate.Description) && !string.IsNullOrWhiteSpace(profileUpdate.Description) )
            {
                x.Result.Description = profileUpdate.Description;
            }

            if(profileUpdate.Image != null){
                var fullpath = _folder + $"/img/uploads/{profileUpdate.Image.FileName}";
                var shortPath = $"/img/uploads/{profileUpdate.Image.FileName}";
                using (var stream = new FileStream(fullpath, FileMode.Create))
                {
                    await profileUpdate.Image.CopyToAsync(stream);
                }
                
                x.Result.ImageUrl = shortPath;
            }

            _users.SaveChanges();

            return RedirectToAction("UserSettings");
        }
    }
}