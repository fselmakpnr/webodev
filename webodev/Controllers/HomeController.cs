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
            // Kullan�c� giri� yapmam��sa, giri� yap sayfas�na y�nlendir
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("Email")))
            {
                return RedirectToAction("GirisYap", "Home");
            }

            // Kullan�c� giri� yapt�ysa, RandevuController'a y�nlendir
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
            if (g.Email == "admin@sakarya.edu.tr" && g.Sifre == "sau")
            {
                // Admin olarak giri� yap�ld�
                HttpContext.Session.SetString("Email", g.Email);
                return RedirectToAction("Index", "Admin"); // Admin sayfas�na y�nlendirme
            }

            // Kullan�c�y� veritaban�nda sorgula
            var bilgiler = c.Kullanicilars.FirstOrDefault(x => x.Email == g.Email && x.Sifre == g.Sifre);

            if (bilgiler != null)
            {
                // Kullan�c� ba�ar�yla giri� yapt�ysa, kullan�c�n�n email bilgisini Session'a kaydet
                HttpContext.Session.SetString("Email", g.Email);

                // Giri� ba�ar�l� ise, ana sayfaya y�nlendir
                return RedirectToAction("Index", "Home");
            }

            // Giri� ba�ar�s�zsa, giri� sayfas�n� yeniden g�ster
            TempData["ErrorMessage"] = "Ge�ersiz email veya �ifre."; // Hata mesaj� g�stermek i�in TempData
            return View();
        }


        [HttpGet]
        public IActionResult SifremiUnuttum()
        {
            // Bo� bir model ile sayfay� d�nd�r
            return View(new SifremiUnuttumViewModel());
        }

        [HttpPost]
        public IActionResult SifremiUnuttum(SifremiUnuttumViewModel model)
        {
            if (ModelState.IsValid)
            {
                // �ifre s�f�rlama i�lemleri
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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}