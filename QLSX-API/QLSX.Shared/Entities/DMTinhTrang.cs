using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLSX.Shared.Entities;

[Table("DANHMUCTRANGTHAIDONHANG")]
public class DMTinhTrang
{
    [Column("ID")]
    public int Id { get; set; }

    [Column("STT")]
    public int? Stt { get; set; }
    
    [Column("TENTRANGTHAI")]
    public string? TenTrangThai { get; set; }
    
    [Column("GHICHU")]
    public string? GhiChu { get; set; }

}

