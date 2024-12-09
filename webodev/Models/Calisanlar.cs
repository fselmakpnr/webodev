using webodev.Models;
using KeyAttribute = System.ComponentModel.DataAnnotations.KeyAttribute;
namespace webodev.Models
{
    public class Calisanlar
    {
        [Key]
        public int CalisanlarID { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public int SalonID { get; set; }
        public string Telefon { get; set; }

        public virtual Salon Salon { get; set; }

        public List<CalismaSaatleri> calismaSaatleris { get; set; }

    }
}