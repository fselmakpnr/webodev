using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Runtime.CompilerServices;
using webodev.Models;


namespace webodev.Controllers
{
    public class KullanicilarController : Controller
    {
        Context c = new Context();

        public IActionResult Index()
        {
           
            return RedirectToAction("Index", "Home");
        }

        public IActionResult KullaniciEkle()
        {
            return View();
        }

        public IActionResult KullaniciKaydet(Kullanicilar k)
        {
            var mevcutKullanici = c.Kullanicilars.FirstOrDefault(x => x.Email == k.Email);
            if (mevcutKullanici != null)
            {
               
                ModelState.AddModelError("Email", "Bu e-posta adresiyle bir kullanıcı zaten mevcut.");
                return View("KullaniciEkle", k);
            }

            c.Kullanicilars.Add(k);
            c.SaveChanges();
            TempData["Message"] = "Yeni kullanıcı başarıyla eklendi!";
            return RedirectToAction("Index");

        }
    }
}