using MaterMan.Entity;
using MaterMan.Entity.Concrete;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaterMan.Data
{
    public class AppDbContext : IdentityDbContext<AppUser, AppRole, string>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Fiyat> Fiyatlar { get; set; }
        public DbSet<Malzeme> Malzemeler { get; set; }
        public DbSet<MalzemeBirim> MalzemeBirimleri { get; set; }
        public DbSet<MalzemeGrup> MalzemeGruplari { get; set; }
        public DbSet<ReceteBaslik> ReceteBasliklar { get; set; }
        public DbSet<ReceteKalem> ReceteKalemler { get; set; }
        public DbSet<Stok> Stoklar { get; set; }
        public DbSet<UrunTipi> UrunTips { get; set; }

        public DbSet<Message> Messages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           
            
            // MalzemeBirim Seed Data (Varsayılan Veriler)
            modelBuilder.Entity<MalzemeBirim>().HasData(
                new MalzemeBirim { Id = 1, BirimAdi = "Kg" },
                new MalzemeBirim { Id = 2, BirimAdi = "Metre" },
                new MalzemeBirim { Id = 3, BirimAdi = "Adet" },
                new MalzemeBirim { Id = 4, BirimAdi = "Litre" }
            );

            // MalzemeGrup Seed Data (Varsayılan Veriler)
            modelBuilder.Entity<MalzemeGrup>().HasData(
                new MalzemeGrup { Id = 1, GrupAdi = "Hammadde" },
                new MalzemeGrup { Id = 2, GrupAdi = "Yan Ürün" },
                new MalzemeGrup { Id = 3, GrupAdi = "Nihai Ürün" }
            );
            modelBuilder.Entity<Stok>()
        .HasOne(s => s.Malzeme)
        .WithMany(m => m.Stoklar)
        .HasForeignKey(s => s.MalzemeId);



            modelBuilder.Entity<Message>()
                .HasOne(m => m.Sender)
                .WithMany()
                .HasForeignKey(m => m.SenderId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Message>()
                .HasOne(m => m.Receiver)
                .WithMany()
                .HasForeignKey(m=>m.ReceiverId)
                .OnDelete(DeleteBehavior.Restrict);
                



            base.OnModelCreating(modelBuilder); // Sadece bir kere çağırmalısı

            /*
                        // Fiyat Tablosu
                        modelBuilder.Entity<Fiyat>(entity =>
                        {
                            entity.Property(f => f.BaslangicTarihi).IsRequired();
                            entity.Property(f => f.BitisTarihi).IsRequired();
                            entity.Property(f => f.Aktif).IsRequired();
                        });

                        // Malzeme Tablosu
                        modelBuilder.Entity<Malzeme>(entity =>
                        {
                            entity.Property(m => m.MalzemeAdi).IsRequired().HasMaxLength(100);
                            entity.Property(m => m.StokMiktari).IsRequired();
                            entity.Property(m => m.Birim).IsRequired();
                            entity.Property(m => m.Price).IsRequired();
                        });

                        // ReceteBaslik Tablosu
                        modelBuilder.Entity<ReceteBaslik>(entity =>
                        {
                            entity.Property(r => r.VersiyonNo).HasMaxLength(50).IsRequired();
                            entity.Property(r => r.OlusturulmaTarihi).IsRequired();
                        });

                        // ReceteKalem Tablosu
                        modelBuilder.Entity<ReceteKalem>(entity =>
                        {
                            entity.Property(k => k.Miktar).IsRequired();
                            entity.Property(k => k.Birim).IsRequired();
                        });

                        // Stok Tablosu
                        modelBuilder.Entity<Stok>(entity =>
                        {
                            entity.Property(s => s.Miktar).IsRequired();
                            entity.Property(s => s.IslemTipi).IsRequired();
                            entity.Property(s => s.Tarih).IsRequired();
                        });
                    }

            */

        }

    }
}
