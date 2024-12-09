using webodev.Models;
using KeyAttribute = System.ComponentModel.DataAnnotations.KeyAttribute;

namespace webodev.Models
{
    public class Hizmetler
    {
        [Key]
        public int HizmetlerID { get; set; }
        public string Ad { get; set; }
        public int Sure { get; set; }  // Hizmet süresi dakika olarak
        public float Ucret { get; set; }
        public int SalonID { get; set; }

        public virtual Salon Salon { get; set; }

        public List<CalisanUzmanlik> calisanUzmanliks { get; set; }

        public List<HizmetKategorisiLink> Links { get; set; }
    }
}