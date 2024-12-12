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
                // Kullan�c�y� veritaban�nda ara
                var bilgiler = b.Kullanicilars.FirstOrDefault(x => x.Email == g.Email && x.Sifre == g.Sifre);

                if (bilgiler != null)
                {
                    // Kullan�c� bulundu, kimlik do�rulama i�lemi
                    var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, g.Email)
            };
                    var userIdentity = new ClaimsIdentity(claims, "Index");
                    ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);
                    //await HttpContext.SignInAsync(principal);

                    return RedirectToAction("Index", "Kullanicilar");
                }

                // Kullan�c� bulunamad�, hata mesaj�
                ModelState.AddModelError("", "Email veya �ifre yanl��. L�tfen tekrar deneyin.");
            }

            // Model valid de�il veya kullan�c� bulunamad�
            return View(g);
        }
        [HttpGet]
        public IActionResult SifremiUnuttum()
        {
            return View(); // SifremiUnuttum.cshtml g�r�n�m� y�klenecek
        }

        [HttpPost]
        public IActionResult SifremiUnuttum(SifremiUnuttumViewModel model)
        {
            if (ModelState.IsValid)
            {
                // �ifre s�f�rlama i�lemleri
                var user = b.Kullanicilars.FirstOrDefault(x => x.Email == model.Email);
                if (user != null)
                {
                    TempData["Message"] = "�ifre s�f�rlama ba�lant�n�z e-posta adresinize g�nderildi.";
                }
                else
                {
                    TempData["Message"] = "Bu e-posta adresi sistemde bulunamad�.";
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
