using System;

namespace QLSX.Shared.Models;

public class BaseModel
{
    public int DMDonViSuDungId { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public DateTime? DeletedDate { get; set; }

    public string? CreateBy { get; set; }
}
