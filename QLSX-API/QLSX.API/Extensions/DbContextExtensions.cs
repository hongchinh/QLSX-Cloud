using System;
using System.Linq;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SaleAPI.Interfaces;
using QLSX.Shared.Entities;

namespace SaleAPI.Extensions
{
    public static class DbContextExtensions
    {
        /// <summary>
        /// Tự động set TenantId cho các entity có MaDonViSuDung (string) hoặc DMDonViSuDungId (int)
        /// </summary>
        public static void SetTenantIdForEntities(this DbContext context, ITenantProvider tenantProvider)
        {
            if (tenantProvider == null || tenantProvider.TenantId == 0)
                return;

            var tenantId = tenantProvider.TenantId;
            var tenantIdString = tenantId.ToString();

            var entries = context.ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

            foreach (var entry in entries)
            {
                var entity = entry.Entity;
                var entityType = entity.GetType();

                // Set MaDonViSuDung (string) - cho các entity như ThuChi, DonDatHang, NhapXuat, etc.
                var maDonViSuDungProperty = entityType.GetProperty("MaDonViSuDung", BindingFlags.Public | BindingFlags.Instance);
                if (maDonViSuDungProperty != null && maDonViSuDungProperty.PropertyType == typeof(string))
                {
                    // Chỉ set nếu chưa có giá trị hoặc đang là Added
                    if (entry.State == EntityState.Added || string.IsNullOrEmpty(maDonViSuDungProperty.GetValue(entity) as string))
                    {
                        maDonViSuDungProperty.SetValue(entity, tenantIdString);
                    }
                }

                // Set DMDonViSuDungId (int) - cho các entity như SoDuCongNo, SoDuHangHoa, SoDuLoaiTien, User, Setting
                var dmDonViSuDungIdProperty = entityType.GetProperty("DMDonViSuDungId", BindingFlags.Public | BindingFlags.Instance);
                if (dmDonViSuDungIdProperty != null && 
                    (dmDonViSuDungIdProperty.PropertyType == typeof(int) || dmDonViSuDungIdProperty.PropertyType == typeof(int?)))
                {
                    // Chỉ set nếu chưa có giá trị hoặc đang là Added
                    if (entry.State == EntityState.Added)
                    {
                        var currentValue = dmDonViSuDungIdProperty.GetValue(entity);
                        if (currentValue == null || 
                            (dmDonViSuDungIdProperty.PropertyType == typeof(int) && (int)currentValue == 0) ||
                            (dmDonViSuDungIdProperty.PropertyType == typeof(int?) && ((int?)currentValue == null || (int?)currentValue == 0)))
                        {
                            if (dmDonViSuDungIdProperty.PropertyType == typeof(int?))
                            {
                                dmDonViSuDungIdProperty.SetValue(entity, (int?)tenantId);
                            }
                            else
                            {
                                dmDonViSuDungIdProperty.SetValue(entity, tenantId);
                            }
                        }
                    }
                }
            }
        }
    }
}

