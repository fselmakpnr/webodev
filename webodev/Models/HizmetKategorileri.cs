using webodev.Models;
using KeyAttribute = System.ComponentModel.DataAnnotations.KeyAttribute;
namespace webodev.Models
{

    public class HizmetKategorileri
    {
        [Key]
        public int HizmetKtgID { get; set; }
        public string Ad { get; set; }

        public List<HizmetKategorisiLink> Links { get; set; }
    }
}