using Microsoft.AspNetCore.Mvc;
using Opera.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace Opera.Controllers
{
    [Route("")]
    public class LandingPagesController : Controller
    {
        
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;

        public LandingPagesController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
        {
            _signInManager = signInManager;
            _userManager =userManager;
        }

        [Route("")]
        public IActionResult Index()
        {
            ModelState.Clear();
            return View();
        }

        [HttpPost, Route("LoginBD")]
        public async Task<IActionResult> LoginBD(UserLoginAndRegister currentUser)
        {
            var result = await _signInManager.PasswordSignInAsync(currentUser.UserName, currentUser.Password, true, false);

            if(!result.Succeeded)
            {
                return RedirectToAction("Index", "LandingPages");
            }

            return RedirectToAction("Index", "AppPages");
        }

        [Route("Registro")]
        public IActionResult Registro()
        {
            ModelState.Clear();
            return View();
        }

        [HttpPost, Route("RegistroBD")] 
        public async Task<IActionResult> RegistroBD(UserLoginAndRegister newUser)
        {
            IdentityUser _newUser = new IdentityUser
            {
                UserName = newUser.UserName,
                Email = newUser.Email
            };

            IdentityResult result = await _userManager.CreateAsync(_newUser, newUser.Password);

            if(!result.Succeeded)
            {
                return RedirectToAction("Registro", "LandingPages");
            }

            return RedirectToAction("Index", "LandingPages");
        }


        [HttpPost, Route("Logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "LandingPages")
        }
    }
}