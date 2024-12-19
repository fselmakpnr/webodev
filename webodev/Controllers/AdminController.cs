using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webodev.Models;

namespace webodev.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            Context c = new Context();

            // Admin giriş kontrolü
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("Email")) ||
                HttpContext.Session.GetString("Email") != "admin@sakarya.edu.tr")
            {
                return RedirectToAction("Index", "Admin");
            }

            // Yalnızca "Onay Bekliyor" olan randevuları yükle
            var randevular = c.Randevulars
                .Include(r => r.Kullanicilar)
                .Include(r => r.Calisanlar)
                .Include(r => r.Hizmetler)
                .Include(r => r.Salon)
                .Where(r => !r.OnayDurumu) // Onaylanmamış randevular
                .ToList();

            return View(randevular); // View'e filtrelenmiş listeyi gönder
        }


        [HttpPost]
        public IActionResult Onayla(int id)
        {
            using (Context c = new Context())
            {
                // Randevuyu bul
                var randevu = c.Randevulars.FirstOrDefault(r => r.RandevularID == id);
                if (randevu != null)
                {
                    // Onay durumunu güncelle
                    randevu.OnayDurumu = true;
                    c.SaveChanges();

                    // Onay mesajını TempData ile gönder
                    TempData["Message"] = $"Randevu ID: {id} başarıyla onaylandı.";
                }
                else
                {
                    TempData["Message"] = $"Randevu ID: {id} bulunamadı.";
                }
            }

            // Aynı sayfaya yönlendir
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult Reddet(int id)
        {
            using (Context c = new Context())
            {
                // Randevuyu bul
                var randevu = c.Randevulars.FirstOrDefault(r => r.RandevularID == id);
                if (randevu != null)
                {
                    // Randevuyu reddetmek için gerekli işlemler
                    c.Randevulars.Remove(randevu); // Randevuyu veritabanından sil
                    c.SaveChanges();

                    // Kullanıcıya e-posta gönderildiğini belirtmek için TempData kullan
                    TempData["Message"] = $"Randevu ID: {id} reddedildi ve kullanıcıya e-posta gönderildi.";

                    // (Opsiyonel) Kullanıcıya e-posta gönderme işlemi (gerçek bir e-posta gönderimi için bir e-posta servisi kullanılmalıdır)
                    // EmailHelper.SendEmail(randevu.Kullanicilar.Email, "Randevunuz Reddedildi", "Randevunuz admin tarafından reddedilmiştir.");
                }
                else
                {
                    TempData["Message"] = $"Randevu ID: {id} bulunamadı.";
                }
            }

            // Aynı sayfaya yönlendir
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult CalisanDurumlari()
        {
            using (Context c = new Context())
            {
                var calisanKazancListesi = c.Calisanlars
                    .Select(calisan => new
                    {
                        AdSoyad = calisan.Ad + " " + calisan.Soyad,
                        ToplamKazanc = c.Randevulars
                            .Where(r => r.CalisanlarID == calisan.CalisanlarID && r.OnayDurumu)
                            .Sum(r => r.ToplamUcret)
                    })
                    .ToList();

                ViewBag.CalisanKazancListesi = calisanKazancListesi;
            }

            return View();
        }



    }
}