using webodev.Models;
using KeyAttribute = System.ComponentModel.DataAnnotations.KeyAttribute;
namespace webodev.Models
{
    public class Randevular
    {
        [Key]
        public int RandevularID { get; set; }
        public int KullanicilarID { get; set; }
        public int CalisanlarID { get; set; }
        public int HizmetlerID { get; set; }
        public int SalonID { get; set; }
        public DateTime Tarih { get; set; }
        public TimeSpan Saat { get; set; }
        public float ToplamUcret { get; set; }
        public bool OnayDurumu { get; set; } = false;
        public Kullanicilar Kullanicilar { get; set; }
        public Calisanlar Calisanlar { get; set; }
        public Hizmetler Hizmetler { get; set; }
        public Salon Salon { get; set; }

    }
}