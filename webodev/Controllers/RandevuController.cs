using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using webodev.Models;


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
        [HttpPost]
        public IActionResult RandevuEkle(string tarih, string saat, string calisanAdi, string hizmetAd, float hizmetUcret)
        {
            if (string.IsNullOrWhiteSpace(saat))
            {
                ViewBag.Message = "Saat seçimi boş olamaz.";
                return View();
            }

            if (!DateTime.TryParse($"{tarih} {saat}:00", out var randevuTarihi))
            {
                ViewBag.Message = "Geçersiz tarih veya saat formatı.";
                return View();
            }

            if (!TimeSpan.TryParse(saat, out var saatDilimi))
            {
                ViewBag.Message = "Geçersiz saat formatı.";
                return View();
            }

            var kullaniciId = 13; // Kullanıcı ID'sini burada alabilirsiniz.

            // Çalışan ve hizmet bilgilerini almak
            var calisan = _context.Calisanlars.FirstOrDefault(c => c.Ad + " " + c.Soyad == calisanAdi);
            var hizmet = _context.Hizmetlers.FirstOrDefault(h => h.Ad == hizmetAd);

            if (calisan == null || hizmet == null)
            {
                ViewBag.Message = "Çalışan veya hizmet bulunamadı.";
                return View();
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

            ViewBag.Message = $"Randevu başarıyla oluşturuldu! Tarih: {randevuTarihi:dd MMMM yyyy HH:mm}, Hizmet: {hizmetAd}, Ücret: {hizmetUcret} ₺";

            return View();
        }
        [HttpPost]
        public IActionResult Onayla(int id)
        {
            var randevu = _context.Randevulars.Find(id);
            if (randevu == null)
            {
                return NotFound();
            }

            randevu.OnayDurumu = true; // Onay Durumunu Güncelle
            _context.SaveChanges();

            return RedirectToAction("Index"); // Randevu listesine geri dön
        }



    }
}