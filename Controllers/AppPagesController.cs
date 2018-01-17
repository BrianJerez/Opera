using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Opera.Controllers
{
    [Authorize]
    public class AppPagesController
    {
        public IActionResult Index()
        {
            return new ContentResult {Content = "usted esta logeado!"};
        }
    }
}