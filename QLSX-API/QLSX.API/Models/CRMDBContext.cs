using System;
using QLSX.Shared.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using QLSX.Shared.Entities;
using PhanQuyenBaoCao = QLSX.Shared.Entities.PhanQuyenBaoCao;
using BaoCao = QLSX.Shared.Entities.BaoCao;
using NhatKy = QLSX.Shared.Entities.NhatKy;
using DonDatHang = QLSX.Shared.Entities.DonDatHang;
using User = QLSX.Shared.Entities.User;

namespace SaleAPI.Models
{
    public partial class CRMDBContext : DbContext
    {
        public CRMDBContext()
        {
        }

        public CRMDBContext(DbContextOptions<CRMDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CoQuan> CoQuanRepository { get; set; }
        public virtual DbSet<NhapXuat> NhapXuatRepository { get; set; }
        public virtual DbSet<NoiDungNhapXuat> NoiDungNhapXuatRepository { get; set; }
        public virtual DbSet<DonDatHang> DonDatHangRepository { get; set; }
        public virtual DbSet<DanhMucHangHoa> DanhMucHangHoaRepository { get; set; }
        public virtual DbSet<DanhMucKhoanChi> DanhMucKhoanChiRepository { get; set; }
        public virtual DbSet<DanhMucKhoanThu> DanhMucKhoanThuRepository { get; set; }
        public virtual DbSet<DanhMucKhuVuc> DanhMucKhuVucRepository { get; set; }
        public virtual DbSet<DanhMucHinhThucTT> DanhMucHinhThucTTRepository { get; set; }
        public virtual DbSet<ListNhomHang> ListNhomHangRepository { get; set; }
        public virtual DbSet<DanhMucNhomHang> DanhMucNhomHangRepository { get; set; }
        public virtual DbSet<DanhMucDoDay> DanhMucDoDayRepository { get; set; }
        public virtual DbSet<DanhMucMauSac> DanhMucMauSacRepository { get; set; }
        public virtual DbSet<DanhMucChungLoai> DanhMucChungLoaiRepository { get; set; }
        public virtual DbSet<DanhMucLoaiTon> DanhMucLoaiTonRepository { get; set; }
        public virtual DbSet<DanhMucKieuSong> DanhMucKieuSongRepository { get; set; }
        public virtual DbSet<PhanQuyenBaoCao> PhanQuyenBaoCaoRepository { get; set; }
        public virtual DbSet<DanhMucLoaiTien> DanhMucLoaiTienRepository { get; set; }
        public virtual DbSet<QuyenSuDung> QuyenSuDungRepository { get; set; }
        public virtual DbSet<BaoCao> BaoCaoRepository { get; set; }
        public virtual DbSet<NhatKy> NhatKyRepository { get; set; }
        public virtual DbSet<User> UserRepository { get; set; }
        public virtual DbSet<DanhMucKhoHang> DanhMucKhoHangRepository { get; set; }
        public virtual DbSet<DanhMucKhachHang> DanhMucKhachHangRepository { get; set; }
        public virtual DbSet<ThuChi> ThuChiRepository { get; set; }
        public virtual DbSet<DanhMucSoChungTu> DanhMucSoChungTuRepository { get; set; }
        public virtual DbSet<NhapXuatTonCuon> NhapXuatTonCuonRepository { get; set; }
        public virtual DbSet<NoiDungNhapXuatTonCuon> NoiDungNhapXuatTonCuonRepository { get; set; }
        public virtual DbSet<DanhMucHangHoaTonCuon> DanhMucHangHoaTonCuonRepository { get; set; }
        public virtual DbSet<DanhMucNhomKhachHang> DanhMucNhomKhachHangRepository { get; set; }
        public virtual DbSet<DanhMucTinhThanh> DanhMucTinhThanhRepository { get; set; }
        public virtual DbSet<Settings> SettingRepository { get; set; }
        public virtual DbSet<PhieuNhapXuatAll> PhieuNhapXuatAllRepository { get; set; }
        public virtual DbSet<NoiDungNhapXuatTraNo> NoiDungNhapXuatTraNoRepository { get; set; }
        public virtual DbSet<NhapXuatThongTin> NhapXuatThongTinRepository { get; set; }


        public virtual DbSet<NoiDungDonDatHang> NoiDungDonDatHangs { get; set; }
        public virtual DbSet<DieuChuyen> DieuChuyens { get; set; }
        public virtual DbSet<RefreshToken> RefreshTokens { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<NoiDungDieuChuyen> NoiDungDieuChuyens { get; set; }
        public virtual DbSet<DMTinhGia> DMTinhGias { get; set; }
        public virtual DbSet<DMTinhTrang> DMTinhTrangRepository { get; set; }
        public virtual DbSet<DMDonViSuDung> DMDonViSuDungs { get; set; }
        public virtual DbSet<SoDuCongNo> SoDuCongNos { get; set; }
        public virtual DbSet<SoDuHangHoa> SoDuHangHoas { get; set; }
        public virtual DbSet<SoDuLoaiTien> SoDuLoaiTiens { get; set; }
        public virtual DbSet<DMPhongBan> DMPhongBans { get; set; }
        public virtual DbSet<TraCuuNhapXuatAll> TracuuAlls { get; set; }
        public virtual DbSet<DuLieuIn> DuLieuIns { get; set; }
        public virtual DbSet<SoPhaiThu> SoPhaiThus { get; set; }
        public virtual DbSet<TraCuuCongNo> TraCuuCongNos { get; set; }
        public virtual DbSet<SoPhaiTra> SoPhaiTras { get; set; }
        public virtual DbSet<SoPhaiTraTongHop> SoPhaiTraTongHops { get; set; }
        public virtual DbSet<SoQuyTienMat> SoQuyTienMats { get; set; }
        public virtual DbSet<SoTongHopHangHoa> SoTongHopHangHoas { get; set; }
        public virtual DbSet<ViewNhapXuat> ViewNhapXuats { get; set; }
        public virtual DbSet<tblFileAttachment> tblFileAttachments { get; set; }
        public virtual DbSet<InformationClumns> InformationClumnss { get; set; }
        public virtual DbSet<ThongKeDoanhThu> ThongKeDoanhThus { get; set; }
        public virtual DbSet<ImageQRCode> ImageQRCodes { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Name=CRMConnectStrings");
            }
        }

        public float GetSoDuCongNo(int DMKhachHangId, string loai, int id, DateTime date, int mdvsd)
        => throw new InvalidOperationException();



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var methodInfoSoSangChu = typeof(CRMDBContext)
              .GetRuntimeMethod(nameof(SoSangChu), new[] { typeof(float) });

            modelBuilder.HasDbFunction(methodInfoSoSangChu)
                  .HasName("SoSangChu");


            //var methodInfoSoChungTuMax = typeof(CRMDBContext)
            // .GetRuntimeMethod(nameof(GetSoChungTuMax), new[] { typeof(string), typeof(int) });

            //modelBuilder.HasDbFunction(methodInfoSoChungTuMax)
            //      .HasName("GetSoChungTuMax");

            modelBuilder.HasDbFunction(typeof(CRMDBContext).GetMethod(nameof(GetSoChungTuMax)))
                            .HasTranslation(e => SqlFunctionExpression.Create("GetSoChungTuMax", e, typeof(int), null));


            #region table has key
            modelBuilder.Entity<DanhMucChungLoai>(entity =>
            {
                entity.HasKey(e => e.Id);
            });

            modelBuilder.Entity<DanhMucDoDay>(entity =>
            {
                entity.HasKey(e => e.Id);
            });

            modelBuilder.Entity<DanhMucHangHoa>(entity =>
            {
                entity.HasKey(e => e.Id);
            });

            modelBuilder.Entity<DanhMucHinhThucTT>(entity =>
            {
                entity.HasKey(e => e.MaSo);
            });

            modelBuilder.Entity<DanhMucKhuVuc>(entity =>
            {
                entity.HasKey(e => e.Id);
            });

            modelBuilder.Entity<DanhMucKieuSong>(entity =>
            {
                entity.HasKey(e => e.Id);
            });

            modelBuilder.Entity<DanhMucLoaiTon>(entity =>
            {
                entity.HasKey(e => e.Id);
            });

            modelBuilder.Entity<DanhMucMauSac>(entity =>
            {
                entity.HasKey(e => e.Id);
            });

            modelBuilder.Entity<DonDatHang>(entity =>
            {
                entity.HasKey(e => e.Id);
            });

            modelBuilder.Entity<NhapXuat>(entity =>
            {
                entity.HasKey(e => e.Id);
            });

            modelBuilder.Entity<NoiDungNhapXuat>(entity =>
            {
                entity.HasKey(e => e.IdId);
            });

            modelBuilder.Entity<NhapXuatTonCuon>(entity =>
            {
                entity.HasKey(e => e.Id);
            });

            modelBuilder.Entity<NoiDungNhapXuatTonCuon>(entity =>
            {
                entity.HasKey(e => e.IdId);
            });

            modelBuilder.Entity<NhatKy>(entity =>
            {
                entity.HasKey(e => e.Id);
            });

            modelBuilder.Entity<QuyenSuDung>(entity =>
            {
                entity.HasKey(e => e.Id);
            });

            modelBuilder.Entity<DanhMucKhoHang>(entity =>
            {
                entity.HasKey(e => e.Id);
            });

            modelBuilder.Entity<DanhMucKhachHang>(entity =>
            {
                entity.HasKey(e => e.Id);
            });

            modelBuilder.Entity<ThuChi>(entity =>
            {
                entity.HasKey(e => e.Id);
            });
            modelBuilder.Entity<DanhMucHangHoaTonCuon>(entity =>
            {
                entity.HasKey(e => e.Id);
            });
            modelBuilder.Entity<DanhMucNhomKhachHang>(entity =>
            {
                entity.HasKey(e => e.Id);
            });
            modelBuilder.Entity<DanhMucTinhThanh>(entity =>
            {
                entity.HasKey(e => e.Id);
            });
            modelBuilder.Entity<SettingModel>(entity =>
            {
                entity.HasKey(e => e.Id);
            });
            modelBuilder.Entity<NoiDungNhapXuatTraNo>(entity =>
            {
                entity.HasKey(e => e.Id);
            });
            modelBuilder.Entity<NhapXuatThongTin>(entity =>
            {
                entity.HasKey(e => e.Id);
            });
            modelBuilder.Entity<PhieuNhapXuatAll>(entity =>
            {
                entity.ToView("PHIEUNHAPXUATALL");
                entity.HasKey(e => e.IdId);
            });
            #endregion





            modelBuilder.Entity<InformationClumns>(entity =>
            {
                entity.HasNoKey();
            });

            modelBuilder.Entity<SoTongHopHangHoa>(entity =>
            {
                entity.HasKey(e => e.Id);

            });

            modelBuilder.Entity<ThongKeDoanhThu>(entity =>
            {
                entity.HasKey(e => e.Id);

            });
            modelBuilder.Entity<ViewNhapXuat>(entity =>
            {
                entity.HasKey(e => e.Id);

            });
            modelBuilder.Entity<CoQuan>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.ToTable("COQUAN39");
            });
            modelBuilder.Entity<tblFileAttachment>(entity =>
            {
                entity.HasKey(e => e.id);

                entity.ToTable("FileAttachments");
            });

            modelBuilder.Entity<RefreshToken>(entity =>
            {
                entity.HasKey(e => e.TokenId);

                entity.ToTable("RefreshToken");

                entity.Property(e => e.TokenId).HasColumnName("token_id");

                entity.Property(e => e.ExpiryDate)
                    .HasColumnName("expiry_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.Token)
                    .IsRequired()
                    .HasColumnName("token")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.RefreshTokens)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__RefreshTo__user___60FC61CA");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("Role");

                entity.Property(e => e.RoleId).HasColumnName("role_id");

                entity.Property(e => e.RoleDesc)
                    .IsRequired()
                    .HasColumnName("role_desc")
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('New Position - title not formalized yet')");
            });


            //modelBuilder.Entity<User>(entity =>
            //{
            //    entity.HasKey(e => e.UserId)
            //        .HasName("PK_user_id_2")
            //        .IsClustered(false);

            //    entity.ToTable("User");

            //    entity.Property(e => e.UserId).HasColumnName("user_id");

            //    entity.Property(e => e.EmailAddress)
            //        .IsRequired()
            //        .HasColumnName("email_address")
            //        .HasMaxLength(100)
            //        .IsUnicode(false);

            //    entity.Property(e => e.FirstName)
            //        .HasColumnName("first_name")
            //        .HasMaxLength(200)
            //        .IsUnicode(true);

            //    entity.Property(e => e.HireDate)
            //        .HasColumnName("hire_date")
            //        .HasColumnType("datetime")
            //        .HasDefaultValueSql("(getdate())");

            //    entity.Property(e => e.LastName)
            //        .HasColumnName("last_name")
            //        .HasMaxLength(230)
            //        .IsUnicode(true);

            //    entity.Property(e => e.MiddleName)
            //        .HasColumnName("middle_name")
            //        .HasMaxLength(1)
            //        .IsUnicode(true)
            //        .IsFixedLength();

            //    entity.Property(e => e.Password)
            //        .IsRequired()
            //        .HasColumnName("password")
            //        .HasMaxLength(100)
            //        .IsUnicode(false);

            //    entity.Property(e => e.IsActive)
            //       .HasColumnName("is_active")
            //       .HasDefaultValueSql("((0))");


            //    entity.Property(e => e.RoleId)
            //        .HasColumnName("role_id")
            //        .HasDefaultValueSql("((1))");

            //    entity.Property(e => e.DMPhongBanId)
            //        .HasColumnName("DMPhongBanId")
            //        .HasDefaultValueSql("((0))");

            //    entity.Property(e => e.Source)
            //        .IsRequired()
            //        .HasColumnName("source")
            //        .HasMaxLength(100)
            //        .IsUnicode(false);

            //    entity.HasOne(d => d.Role)
            //        .WithMany(p => p.Users)
            //        .HasForeignKey(d => d.RoleId)
            //        .HasConstraintName("FK__User__role_id__6E565CE8");

            //});

            modelBuilder.Entity<ImageQRCode>(entity =>
            {
                entity.HasKey("Id");
                entity.ToTable("ImageQRCodes");
            });

            modelBuilder.Entity<NoiDungDonDatHang>(entity =>
            {

                entity.HasKey("Id");
                entity.ToTable("NoiDungDonDatHangs");
                entity.Ignore(p => p.IsEditing);
            });
            modelBuilder.Entity<DieuChuyen>(entity =>
            {
                entity.HasKey("Id");
                entity.Property(e => e.UserId).HasColumnName("user_id");
                entity.ToTable("DieuChuyens");
            });

            modelBuilder.Entity<NoiDungDieuChuyen>(entity =>
            {
                entity.HasKey("Id");
                entity.ToTable("NoiDungDieuChuyens");
            });


            modelBuilder.Entity<DMTinhGia>(entity =>
            {
                entity.HasKey("Id");
                entity.ToTable("DMTinhGias");
            });
            modelBuilder.Entity<DanhMucLoaiTienModel>(entity =>
            {
                entity.HasKey("Id");
                entity.ToTable("DMLoaiTiens");
            });
            modelBuilder.Entity<DMDonViSuDung>(entity =>
            {
                entity.HasKey("Id");
                entity.ToTable("DMDonViSuDungs");
            });

            modelBuilder.Entity<SoDuHangHoa>(entity =>
            {
                entity.HasKey("Id");
                entity.ToTable("SoDuHangHoas");
                //entity.Property(p => p.ChieuDai).IsRequired(required: false);
                //entity.Property(p => p.KhoRongTon).IsRequired(required: false);
                //entity.Property(p => p.GhiChu).IsRequired(required: false);


            });
            modelBuilder.Entity<SoDuCongNo>(entity =>
            {
                entity.HasKey("Id");
                entity.ToTable("SoDuCongNos");

            });
            modelBuilder.Entity<SoDuLoaiTien>(entity =>
            {
                entity.HasKey("Id");
                entity.ToTable("SoDuLoaiTiens");

            });
            modelBuilder.Entity<DMPhongBan>(entity =>
            {
                entity.HasKey("Id");
                entity.ToTable("DMPhongBans");
            });
            modelBuilder.Entity<TraCuuNhapXuatAll>(entity =>
            {
                entity.ToView("TraCuuNhapXuatAll");
            });

            modelBuilder.Entity<DMQuyenSuDung>(entity =>
            {
                entity.HasKey("Id");
                entity.ToTable("PhanQUyen");
            });
            modelBuilder.Entity<QuyenSuDungModel>(entity =>
            {
                entity.HasKey("Id");
                entity.ToTable("quyensudung");
            });
            modelBuilder.Entity<PhanQuyenBaoCao>(entity =>
            {
                entity.HasKey("Id");
                entity.ToTable("PhanQuyenBaoCao");
            });
            modelBuilder.Entity<DuLieuIn>(entity =>
            {
                entity.HasKey("Id");
                entity.ToTable("DuLieuIns");
            });

            modelBuilder
             .HasDbFunction(typeof(CRMDBContext).GetMethod(nameof(GetSoDuCongNo), new[] { typeof(int), typeof(string), typeof(int), typeof(DateTime), typeof(int) }))
             .HasName("GetSoDuCongNo");


            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

        public string SoSangChu(float so) => throw new InvalidOperationException();
        public int GetSoChungTuMax(string loai, int mdvsd) => throw new InvalidOperationException();


    }
}
