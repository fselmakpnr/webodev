namespace webodev.Models
{
    
        public class CalisanHizmetViewModel
        {
            public string CalisanAdi { get; set; }
            public string CalisanSoyadi { get; set; }
            public List<HizmetDetayViewModel> HizmetDetaylari { get; set; }
        }

        public class HizmetDetayViewModel
        {
            public string Ad { get; set; }
            public int Sure { get; set; }
            public float Ucret { get; set; }
        }
    }

