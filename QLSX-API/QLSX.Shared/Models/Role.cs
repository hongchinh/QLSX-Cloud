using System;
using System.Collections.Generic;

namespace QLSX.Shared.Models
{
    public partial class Role 
    {
        public Role()
        {
            Users = new HashSet<UserModel>();
        }

        public int RoleId { get; set; }
        public string RoleDesc { get; set; }
        public DateTime? DeletedDate { get; set; }

        public virtual ICollection<UserModel> Users { get; set; }
    }
}
