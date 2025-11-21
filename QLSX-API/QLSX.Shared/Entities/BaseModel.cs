using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLSX.Shared.Entities;

public class BaseModel
{
    [Column("CREATEDDATE")]
    public DateTime CreatedDate { get; set; }

    [Column("UPDATEDDATE")]
    public DateTime? UpdatedDate { get; set; }

    [Column("DELETEDDATE")]
    public DateTime? DeletedDate { get; set; }
}
