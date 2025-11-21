using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using QLSX.Shared.Models;
using User = QLSX.Shared.Entities.User;

namespace SaleAPI.Models
{
    public partial class CRMDBContext
    {
        public DMDonViSuDung GetByTenantId(int tenantId, bool ignoreStatus = false)
        {
            return DMDonViSuDungs.AsNoTracking().FirstOrDefault(t => t.Id == tenantId);
        }

        public User GeUserById(int userId, bool ignoreStatus = false)
        {
            return UserRepository.AsNoTracking()/*.Include( x=>x.Role)*/.FirstOrDefault(t => t.Id == userId);
        }
    }
}
