using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;
using webodev.Models;


namespace webOdev3.Controllers
{
    public class HomeController : Controller
    {

        Context c = new Context();

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult RandevuAl()
        {
            
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("Email")))
            {
                return RedirectToAction("GirisYap", "Home");
            }

            
            return RedirectToAction("Index", "Randevu");
        }


        [AllowAnonymous]
        [HttpGet]
        public IActionResult GirisYap()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public IActionResult GirisYap(Kullanicilar g)
        {
            // Admin Giri�i Kontrol�
            if (g.Email == "B221210087@sakarya.edu.tr" && g.Sifre == "sau")
            {
                // Admin olarak giri� yap�ld�
                HttpContext.Session.SetString("Email", g.Email);
                return RedirectToAction("Index", "Admin"); // Admin sayfas�na y�nlendirme
            }

            
            var bilgiler = c.Kullanicilars
                .FirstOrDefault(x => x.Email == g.Email);

            if (bilgiler != null && string.Equals(bilgiler.Sifre, g.Sifre, StringComparison.Ordinal))
            {
                
                HttpContext.Session.SetString("Email", bilgiler.Email);
                HttpContext.Session.SetString("KullaniciId", bilgiler.KullanicilarID.ToString());

               
                return RedirectToAction("Index", "Home");
            }

            
            TempData["ErrorMessage"] = "Ge�ersiz email veya �ifre.";
            return View();
        }
        

        [HttpGet]
        public IActionResult SifremiUnuttum()
        {
            
            return View(new SifremiUnuttumViewModel());
        }

        [HttpPost]
        public IActionResult SifremiUnuttum(SifremiUnuttumViewModel model)
        {
            if (ModelState.IsValid)
            {
                
                var user = c.Kullanicilars.FirstOrDefault(x => x.Email == model.Email);
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
        public IActionResult CikisYap()
        {
            // Kullan�c�n�n oturum bilgilerini temizliyoruz
            HttpContext.Session.Clear();

            // Ana sayfaya y�nlendiriyoruz
            return RedirectToAction("Index", "Home");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}