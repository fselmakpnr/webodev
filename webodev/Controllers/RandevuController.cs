using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using webodev.Models;
using System.Security.Claims;

namespace webOdev3.Controllers
{
    public class RandevuController : Controller
    {
        Context _context = new Context();

        public IActionResult Index()
        {
            var calisanHizmetler = _context.CalisanUzmanliks
                .Include(cu => cu.Calisanlar)
                .Include(cu => cu.Hizmetler)
                .GroupBy(cu => new { cu.Calisanlar.Ad, cu.Calisanlar.Soyad })
                .Select(g => new CalisanHizmetViewModel
                {
                    CalisanAdi = g.Key.Ad,
                    CalisanSoyadi = g.Key.Soyad,
                    HizmetDetaylari = g.Select(x => new HizmetDetayViewModel
                    {
                        Ad = x.Hizmetler.Ad,
                        Sure = x.Hizmetler.Sure,
                        Ucret = x.Hizmetler.Ucret
                    }).ToList()
                })
                .ToList();

            if (!calisanHizmetler.Any())
            {
                ViewBag.Message = "Henüz eklenmiş bir çalışan veya hizmet yok.";
                return View();
            }

            // Modeli JSON olarak View'a gönderiyoruz
            ViewBag.CalisanHizmetler = JsonConvert.SerializeObject(calisanHizmetler);

            return View(calisanHizmetler);
        }

        [HttpGet]
        public IActionResult RandevuEkle()
        {
            return View();
        }

        [HttpPost]
        public IActionResult RandevuEkle(string tarih, string saat, string calisanAdi, string hizmetAd, float hizmetUcret)
        {
            if (string.IsNullOrWhiteSpace(saat))
            {
                TempData["Message"] = "Saat seçimi boş olamaz.";
                return RedirectToAction("Index");
            }

            if (!DateTime.TryParse($"{tarih} {saat}:00", out var randevuTarihi))
            {
                TempData["Message"] = "Geçersiz tarih veya saat formatı.";
                return RedirectToAction("Index");
            }

            if (randevuTarihi <= DateTime.Now)
            {
                TempData["Message"] = "Geçmiş bir tarihe randevu oluşturamazsınız. Lütfen ileri bir tarih seçin.";
                return RedirectToAction("Index");
            }

            if (!TimeSpan.TryParse(saat, out var saatDilimi))
            {
                TempData["Message"] = "Geçersiz saat formatı.";
                return RedirectToAction("Index");
            }

            var kullaniciIdString = HttpContext.Session.GetString("KullaniciId");
            if (string.IsNullOrEmpty(kullaniciIdString) || !int.TryParse(kullaniciIdString, out int kullaniciId))
            {
                TempData["Message"] = "Kullanıcı kimliği bulunamadı.";
                return RedirectToAction("Index");
            }

            // Çalışan ve hizmet bilgilerini almak
            var calisan = _context.Calisanlars.FirstOrDefault(c => c.Ad + " " + c.Soyad == calisanAdi);
            var hizmet = _context.Hizmetlers.FirstOrDefault(h => h.Ad == hizmetAd);

            if (calisan == null || hizmet == null)
            {
                TempData["Message"] = "Çalışan veya hizmet bulunamadı.";
                return RedirectToAction("Index");
            }

            // Çalışan için seçilen tarih ve saat aralığında mevcut bir randevu olup olmadığını kontrol et
            var hizmetSuresi = TimeSpan.FromMinutes(hizmet.Sure);
            var randevuBitisSaati = saatDilimi + hizmetSuresi;

            var mevcutRandevular = _context.Randevulars
                .Where(r => r.CalisanlarID == calisan.CalisanlarID && r.Tarih.Date == randevuTarihi.Date)
                .AsEnumerable() // LINQ-to-Objects'e geçiş
                .Where(r => r.Saat < randevuBitisSaati && r.Saat + TimeSpan.FromMinutes(hizmet.Sure) > saatDilimi)
                .ToList();

            if (mevcutRandevular.Any())
            {
                TempData["Message"] = "Seçilen tarih ve saat aralığında bu çalışan zaten dolu. Lütfen başka bir zaman seçin.";
                return RedirectToAction("Index");
            }
            var yeniRandevu = new Randevular
            {
                KullanicilarID = kullaniciId,
                CalisanlarID = calisan.CalisanlarID,
                HizmetlerID = hizmet.HizmetlerID,
                SalonID = calisan.SalonID,
                Tarih = randevuTarihi,
                Saat = saatDilimi,
                ToplamUcret = hizmetUcret
            };

            _context.Randevulars.Add(yeniRandevu);
            _context.SaveChanges();

            TempData["Message"] = $"Randevu başarıyla oluşturuldu! Tarih: {randevuTarihi:dd MMMM yyyy HH:mm}, Hizmet: {hizmetAd}, Ücret: {hizmetUcret} ₺";

            return RedirectToAction("Index");
        }
    }
}