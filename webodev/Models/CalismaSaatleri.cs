using webodev.Models;
using KeyAttribute = System.ComponentModel.DataAnnotations.KeyAttribute;
namespace webodev.Models

{
    public class CalismaSaatleri
    {
        [Key]
        public int CalismaSaatleriID { get; set; }
        public int CalisanlarID { get; set; }
        public string Gun { get; set; }
        public TimeSpan BaslangicSaati { get; set; }
        public TimeSpan BitisSaati { get; set; }

        public virtual Calisanlar Calisanlar { get; set; }
    }
}