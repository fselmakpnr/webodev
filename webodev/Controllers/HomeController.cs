using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;
using webodev.Models;

namespace webodev.Controllers
{
    
    public class HomeController : Controller
    {
        Context b = new Context();
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult GirisYap()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> GirisYap(Kullanicilar g)
        {
            if (ModelState.IsValid)
            {
                // Kullanýcýyý veritabanýnda ara
                var bilgiler = b.Kullanicilars.FirstOrDefault(x => x.Email == g.Email && x.Sifre == g.Sifre);

                if (bilgiler != null)
                {
                    // Kullanýcý bulundu, kimlik doðrulama iþlemi
                    var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, g.Email)
            };
                    var userIdentity = new ClaimsIdentity(claims, "Index");
                    ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);
                    //await HttpContext.SignInAsync(principal);

                    return RedirectToAction("Index", "Kullanicilar");
                }

                // Kullanýcý bulunamadý, hata mesajý
                ModelState.AddModelError("", "Email veya þifre yanlýþ. Lütfen tekrar deneyin.");
            }

            // Model valid deðil veya kullanýcý bulunamadý
            return View(g);
        }
        [HttpGet]
        public IActionResult SifremiUnuttum()
        {
            return View(); // SifremiUnuttum.cshtml görünümü yüklenecek
        }

        [HttpPost]
        public IActionResult SifremiUnuttum(SifremiUnuttumViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Þifre sýfýrlama iþlemleri
                var user = b.Kullanicilars.FirstOrDefault(x => x.Email == model.Email);
                if (user != null)
                {
                    TempData["Message"] = "Þifre sýfýrlama baðlantýnýz e-posta adresinize gönderildi.";
                }
                else
                {
                    TempData["Message"] = "Bu e-posta adresi sistemde bulunamadý.";
                }
            }

            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
