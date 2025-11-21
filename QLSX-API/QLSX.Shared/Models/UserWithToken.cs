using System.Collections.Generic;

namespace QLSX.Shared.Models
{
    public class UserWithToken : UserModel
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }

        public UserWithToken(UserModel user)
        {
            this.Id = user.Id;
            this.EmailAddress = user.EmailAddress;
            this.FirstName = user.FirstName;
            this.MiddleName = user.MiddleName;
            this.LastName = user.LastName;
            this.HireDate = user.HireDate;
            //this.RoleName = user.RoleName;
            this.RoleId = user.RoleId;
            this.Quyen  = user.Quyen;
            this.RoleId = user.RoleId;
            this.DMDonViSuDungId = user.DMDonViSuDungId;

            this.Id = user.Id;
            this.HoTen = user.HoTen;
            this.MatKhau = user.MatKhau;
            this.QuyenSuDung = user.QuyenSuDung;
            this.Quyen = user.Quyen;
            this.TrangThai = user.TrangThai;
            this.GhiChu = user.GhiChu;
            this.EmailAddress = user.EmailAddress;
            this.Source = user.Source;
            this.FirstName = user.FirstName;
            this.MiddleName = user.MiddleName;
            this.LastName = user.LastName;
            this.HireDate = user.HireDate;
            this.IsActive = user.IsActive;
            this.IdKt = user.IdKt  ;
            this.DMPhongBanId = user.DMPhongBanId  ;
            this.MaNhanVienId = user.MaNhanVienId;
            this.DMDonViSuDungId = user.DMDonViSuDungId ?? 0;
            this.CreatedDate = user.CreatedDate;
            this.UpdatedDate = user.UpdatedDate;
            this.RoleId = user.RoleId;
            this.DeletedDate = user.DeletedDate;
            this.CreateBy = user.CreateBy;
            this.RefreshTokens = new HashSet<RefreshToken>();
        }
    }
}
