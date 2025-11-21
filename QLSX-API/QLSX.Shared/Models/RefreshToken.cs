using System;

namespace QLSX.Shared.Models;

public partial class RefreshToken
{
    public int TokenId { get; set; }

    public int UserId { get; set; }

    public string Token { get; set; }

    public DateTime ExpiryDate { get; set; }

    public virtual UserModel User { get; set; }
}
