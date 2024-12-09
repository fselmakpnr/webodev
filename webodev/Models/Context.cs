using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using webodev.Models;

namespace webodev.Models
{
    public class Context : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb; Database=Kuafor; Trusted_Connection=True;");
        }

        public DbSet<Kullanicilar> Kullanicilars { get; set; }
        public DbSet<Salon> Salonlars { get; set; }
        public DbSet<Calisanlar> Calisanlars { get; set; }
        public DbSet<Hizmetler> Hizmetlers { get; set; }
        public DbSet<CalisanUzmanlik> CalisanUzmanliks { get; set; }
        public DbSet<Randevular> Randevulars { get; set; }
        public DbSet<CalismaSaatleri> CalismaSaatleris { get; set; }
        public DbSet<HizmetKategorileri> HizmetKategorileris { get; set; }
        public DbSet<HizmetKategorisiLink> HizmetKategorisiLinks { get; set; }
    }
}