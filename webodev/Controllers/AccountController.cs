using Microsoft.AspNetCore.Mvc;

namespace webodev.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }
    }
}
