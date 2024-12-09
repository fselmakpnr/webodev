using webodev.Models;
using KeyAttribute = System.ComponentModel.DataAnnotations.KeyAttribute;
namespace webodev.Models
{
    public class Kullanicilar
    {
        [Key]
        public int KullanicilarID { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public string Email { get; set; }
        public string Sifre { get; set; }
        public string Rol { get; set; }  // Admin veya User

        public List<Randevular> Randevulars { get; set; }
    }
}