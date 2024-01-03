using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthFlowINMVC.Controllers
{

    [Authorize(Roles = "admin,editor")]
    public class AdminController : Controller
    {
        //[AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }
    }
}
