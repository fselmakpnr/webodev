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
            // Kullanýcý giriþ yapmamýþsa, giriþ yap sayfasýna yönlendir
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("Email")))
            {
                return RedirectToAction("GirisYap", "Home");
            }

            // Kullanýcý giriþ yaptýysa, RandevuController'a yönlendir
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
            // Admin Giriþi Kontrolü
            if (g.Email == "admin@sakarya.edu.tr" && g.Sifre == "sau")
            {
                // Admin olarak giriþ yapýldý
                HttpContext.Session.SetString("Email", g.Email);
                return RedirectToAction("Index", "Admin"); // Admin sayfasýna yönlendirme
            }

            // Kullanýcýyý veritabanýnda sorgula
            var bilgiler = c.Kullanicilars.FirstOrDefault(x => x.Email == g.Email && x.Sifre == g.Sifre);

            if (bilgiler != null)
            {
                // Kullanýcý baþarýyla giriþ yaptýysa, kullanýcýnýn email bilgisini Session'a kaydet
                HttpContext.Session.SetString("Email", g.Email);

                // Giriþ baþarýlý ise, ana sayfaya yönlendir
                return RedirectToAction("Index", "Home");
            }

            // Giriþ baþarýsýzsa, giriþ sayfasýný yeniden göster
            TempData["ErrorMessage"] = "Geçersiz email veya þifre."; // Hata mesajý göstermek için TempData
            return View();
        }


        [HttpGet]
        public IActionResult SifremiUnuttum()
        {
            // Boþ bir model ile sayfayý döndür
            return View(new SifremiUnuttumViewModel());
        }

        [HttpPost]
        public IActionResult SifremiUnuttum(SifremiUnuttumViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Þifre sýfýrlama iþlemleri
                var user = c.Kullanicilars.FirstOrDefault(x => x.Email == model.Email);
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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}