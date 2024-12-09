/*using Microsoft.AspNetCore.Mvc;
using webodev.Models;

namespace webodev.Controllers
{
    public class KullanicilarController : Controller
    {
        Context a = new Context();
        public IActionResult Index()
        {
           var kullanicilar=a.Kullanicilars.ToList();
            return View(kullanicilar);
            
            return View();
        }

        public IActionResult KullaniciKaydet(Kullanicilar k)
        {
            if (ModelState.IsValid)
            {
                a.Kullanicilars.Add(k);
                TempData["msj"] = "Kullanıcı Kaydedildi";
                return RedirectToAction("Login");
            }
            return View();
        }
    }
}*/
using Microsoft.AspNetCore.Mvc;
using webodev.Models;

namespace webodev.Controllers
{
    public class KullanicilarController : Controller
    {
        private readonly Context _context;

        public KullanicilarController()
        {
            _context = new Context();
        }

        [HttpGet]
        public IActionResult KullaniciKaydet()
        {
            return View();
        }

        [HttpPost]
        public IActionResult KullaniciKaydet(Kullanicilar k)
        {
            if (ModelState.IsValid)
            {
                _context.Kullanicilars.Add(k);
                _context.SaveChanges(); // Değişiklikleri veritabanına kaydet

                // Başarılı işlem mesajı ve giriş ekranına yönlendirme
                TempData["msj"] = "Kayıt başarılı! Giriş yapabilirsiniz.";
                return RedirectToAction("Login", "Account"); // Account/Login yönlendirme
            }

            // Hata varsa aynı formu yeniden göster
            return View(k);
        }
    }
}
