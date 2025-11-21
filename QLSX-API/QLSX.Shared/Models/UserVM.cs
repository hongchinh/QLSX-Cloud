using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLSX.Shared.Models
{
    public class UserVM
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string HoTen { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public string Source { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string RoleName { get; set; }
        public int RoleId { get; set; }
        public DateTime? HireDate { get; set; }

        public bool  IsActive { get; set; }

    }
}
