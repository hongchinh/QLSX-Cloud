using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLSX.Shared.Entities;

[Table("USERS")]
public class User : BaseModel
{
    [Column("ID")]
    public int Id { get; set; }

    [Column("HOTEN")]
    public string? HoTen { get; set; }

    [Column("MATKHAU")]
    public string? MatKhau { get; set; }

    [Column("QUYENSUDUNG")]
    public string? QuyenSuDung { get; set; }

    [Column("QUYEN")]
    public int? Quyen { get; set; }

    [Column("TRANGTHAI")]
    public bool? TrangThai { get; set; }

    [Column("GHICHU")]
    public string? GhiChu { get; set; }

    [Column("USERID")]
    public int? UserId { get; set; }

    [Column("EMAILADDRESS")]
    public string EmailAddress { get; set; }

    [Column("SOURCE")]
    public string Source { get; set; }

    [Column("FIRSTNAME")]
    public string? FirstName { get; set; }

    [Column("MIDDLENAME")]
    public string? MiddleName { get; set; }

    [Column("LASTNAME")]
    public string? LastName { get; set; }

    [Column("HIREDATE")]
    public DateTime? HireDate { get; set; }

    [Column("ISACTIVE")]
    public bool? IsActive { get; set; }

    [Column("IDKT")]
    public int? IdKt { get; set; }

    [Column("DMPHONGBANID")]
    public int? DMPhongBanId { get; set; }

    [Column("MANHANVIENID")]
    public int? MaNhanVienId { get; set; }

    [Column("DMDONVISUDUNGID")]
    public int? DMDonViSuDungId { get; set; }

    [Column("ROLEID")]
    public int? RoleId { get; set; }

    [Column("CREATEBY")]
    public string? CreateBy { get; set; }
}
