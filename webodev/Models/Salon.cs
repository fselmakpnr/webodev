using MessagePack;
using webodev.Models;
using KeyAttribute = System.ComponentModel.DataAnnotations.KeyAttribute;

namespace webodev.Models
{
    public class Salon
    {
        [Key]
        public int SalonID { get; set; }
        public string Ad { get; set; }
        public string Adres { get; set; }
        public string Telefon { get; set; }
        public string Tur { get; set; }
        public TimeSpan CalismaBaslangic { get; set; }
        public TimeSpan CalismaBitis { get; set; }

        public List<Calisanlar> Calisanlars { get; set; }
        public List<Hizmetler> Hizmetlers { get; set; }
    }
}