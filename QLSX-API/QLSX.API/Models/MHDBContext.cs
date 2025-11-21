using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using QLSX.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace SaleAPI.Models
{
    public partial class MHDBContext : DbContext
    {
        public MHDBContext()
        {
        }

        public MHDBContext(DbContextOptions<MHDBContext> options)
            : base(options)
        {
        }
        public virtual DbSet<DanhMucKhachHangModel> DanhMucTenDonVis { get; set; }
       
        public virtual DbSet<TongHopCongNo> TongHopCongNos { get; set; }
        public virtual DbSet<DonHang> GetDonHangTheoKhachHangs { get; set; }
        public virtual DbSet<TongHopDongTien> TongHopDongTiens { get; set; }
        public virtual DbSet<BangLuongNhanVien> BangLuongNhanViens { get; set; }
        public virtual DbSet<BangLuongKinhDoanh> BangLuongKinhDoanhs { get; set; }

       
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Name=KeToan");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DanhMucKhachHangModel>(entity =>
            {
                entity.HasKey(e => e.Id)
                 .HasName("PK__TenDonVi__2515F222DDC013AD");
                entity.ToTable("Customers");
            });
            modelBuilder.Entity<TongHopCongNo>().HasNoKey();


            modelBuilder.Entity<TongHopDongTien>(entity =>
            {
                entity.HasKey(e => e.ID)
                 .HasName("PK__DongTien__2515F222DDC013AD");
               
            });
            modelBuilder.Entity<BangLuongNhanVien>().HasNoKey();

            modelBuilder.Entity<BangLuongKinhDoanh>().HasNoKey();

            modelBuilder.Entity<NhapXuatModel>(entity =>
            {
                entity.HasKey(e => e.Id)
                 .HasName("PK__NhapXuat__2515F222DDC013AD");
                entity.ToTable("NhapXuats");
            });

            modelBuilder.Entity<NoiDungNhapXuatModel>(entity =>
            {
                entity.HasKey(e => e.Id)
                 .HasName("PK__NoiDungNhapXuat__2515F222DDC013AD");
                entity.ToTable("NoiDungNhapXuats");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    }
}
