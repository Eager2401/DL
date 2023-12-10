using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace DL.Models
{
    public partial class DBSanBong : DbContext
    {
        public DBSanBong()
            : base("name=DBSanBongC")
        {
        }

        public virtual DbSet<HoaDon> HoaDons { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<TaiKhoanAd> TaiKhoanAds { get; set; }
        public virtual DbSet<TaiKhoanUser> TaiKhoanUsers { get; set; }
        public virtual DbSet<ThongTinSan> ThongTinSans { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<HoaDon>()
                .Property(e => e.TaiKhoanUser)
                .IsFixedLength();

            modelBuilder.Entity<HoaDon>()
                .Property(e => e.loaiSan)
                .IsFixedLength();

            modelBuilder.Entity<TaiKhoanAd>()
                .Property(e => e.TaiKhoanAdmin)
                .IsFixedLength();

            modelBuilder.Entity<TaiKhoanAd>()
                .Property(e => e.MatKhauAdmin)
                .IsFixedLength();

            modelBuilder.Entity<TaiKhoanUser>()
                .Property(e => e.TaiKhoanUser1)
                .IsFixedLength();

            modelBuilder.Entity<TaiKhoanUser>()
                .Property(e => e.MatKhauUser)
                .IsFixedLength();

            modelBuilder.Entity<TaiKhoanUser>()
                .HasMany(e => e.HoaDons)
                .WithOptional(e => e.TaiKhoanUser1)
                .HasForeignKey(e => e.TaiKhoanUser);

            modelBuilder.Entity<ThongTinSan>()
                .Property(e => e.TinhTrang)
                .IsFixedLength();
        }
    }
}
