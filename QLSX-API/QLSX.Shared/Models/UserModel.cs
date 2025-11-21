using FoolProof.Core;
using QLSX.Shared.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLSX.Shared.Models;

[NotMapped]
public partial class UserModel
{
    public UserModel()
    {
        RefreshTokens = new HashSet<RefreshToken>();
    }

    public UserModel(User user)
    {
        if(user != null)
        {
            Id = user.Id;
            HoTen = user.HoTen;
            MatKhau = user.MatKhau;
            QuyenSuDung = user.QuyenSuDung;
            Quyen = user.Quyen;
            TrangThai = user.TrangThai;
            GhiChu = user.GhiChu;
            EmailAddress = user.EmailAddress;
            Source = user.Source;
            FirstName = user.FirstName;
            MiddleName = user.MiddleName;
            LastName = user.LastName;
            HireDate = user.HireDate;
            IsActive = user.IsActive;
            IdKt = user.IdKt ?? 0;
            DMPhongBanId = user.DMPhongBanId ?? 0;
            MaNhanVienId = user.MaNhanVienId;
            DMDonViSuDungId = user.DMDonViSuDungId ?? 0;
            CreatedDate = user.CreatedDate;
            UpdatedDate = user.UpdatedDate;
            RoleId = user.RoleId;
            DeletedDate = user.DeletedDate;
            CreateBy = user.CreateBy;
            RefreshTokens = new HashSet<RefreshToken>();
        }
        
    }

    public string? HoTen { get; set; }

    public string? MatKhau { get; set; }

    public string? QuyenSuDung { get; set; }

    public int? Quyen { get; set; }

    public bool? TrangThai { get; set; }

    public string? GhiChu { get; set; }

    public int Id { get; set; }

    public string EmailAddress { get; set; }

    public string Source { get; set; }

    public string FirstName { get; set; }

    public string MiddleName { get; set; }

    public string LastName { get; set; }

    public DateTime? HireDate { get; set; }

    public bool? IsActive { get; set; }

    public int IdKt { get; set; }

    public int? MaNhanVienId { get; set; }

    public int? DMDonViSuDungId { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public int? RoleId { get; set; }

    public DateTime? DeletedDate { get; set; }

    public string? CreateBy { get; set; }

    public Role Role { get; set; }

    public DMPhongBan DMPhongBans { get; set; }

    [NotMapped]
    //[RequiredIfFalse("IsMatch", ErrorMessage = "Mật khẩu không giống nhau.")]
    public string ConfirmPassword { get; set; }

    [NotMapped]
    public string AccessToken { get; set; }

    [NotMapped]
    public string RefreshToken { get; set; }

    //[Range(1, int.MaxValue, ErrorMessage = "Bạn phải nhập vào phòng ban")]
    public int DMPhongBanId { get; set; }

    [NotMapped]
    public bool IsAdmin
    {
        get
        {
            return (this.RoleId == 1);
        }
    }
    public virtual ICollection<RefreshToken> RefreshTokens { get; set; }
}
