using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using webodev.Models;

namespace webodev.Controllers
{
    public class AdminController : Controller
    {
        Context b = new Context();

        public IActionResult Index()
        {
            // Admin giriş kontrolü
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("Email")) ||
                HttpContext.Session.GetString("Email") != "admin@sakarya.edu.tr")
            {
                return RedirectToAction("Index", "Admin");
            }

            // Randevuları, ilişkili tablolarla birlikte yükle
            var randevular = b.Randevulars
                .Include(r => r.Kullanicilar)
                .Include(r => r.Calisanlar)
                .Include(r => r.Hizmetler)
                .Include(r => r.Salon)
                .ToList();

            return View(randevular); // View'e randevu listesini gönder
        }
    }
}
