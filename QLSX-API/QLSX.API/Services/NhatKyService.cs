using Newtonsoft.Json;
using QLSX.Shared.Models;
using SaleAPI.Interfaces;
using SaleAPI.Models;
using System;
using System.Threading.Tasks;
using NhatKy = QLSX.Shared.Entities.NhatKy;

namespace SaleAPI.Services;

public class NhatKyService : INhatKyService
{
    private readonly CRMDBContext _context;
    private readonly ITenantProvider _tenantProvider;
    public NhatKyService(CRMDBContext context, ITenantProvider tenantProvider)
    {
        _context = context;
        _tenantProvider = tenantProvider;
    }

    public Task<bool> LogDelete(string TableName)
    {
        try
        {
            NhatKy nhatKy = new NhatKy();
            //nhatKy.UserId = _tenantProvider.TenantId;
            //nhatKy.TenantId = _tenantProvider.GetTenant().Id;
            nhatKy.CreatedDate = DateTime.Now;
            nhatKy.ChucNang = "DeleteItem_" + TableName;
            _context.NhatKyRepository.Add(nhatKy);
            _context.SaveChanges();
            return Task.FromResult(true);
        }
        catch
        {
            return Task.FromResult(false);
        }
    }

    public Task<bool> LogExportExcel(string TableName)
    {
        try
        {
            NhatKy nhatKy = new NhatKy();
            //nhatKy.UserId = _tenantProvider.TenantId;
            //nhatKy.TenantId = _tenantProvider.GetTenant().Id;
            nhatKy.CreatedDate = DateTime.Now;
            nhatKy.ChucNang = "ExportExcel_" + TableName;
            _context.NhatKyRepository.Add(nhatKy);
            _context.SaveChanges();
            return Task.FromResult(true);
        }
        catch
        {
            return Task.FromResult(false);
        }
    }

    public Task<bool> LogCreate(string TableName)
    {
        try
        {
            NhatKy nhatKy = new NhatKy();
            //nhatKy.UserId = _tenantProvider.TenantId;
            //nhatKy.TenantId = _tenantProvider.GetTenant().Id;
            nhatKy.CreatedDate = DateTime.Now;
            nhatKy.ChucNang = "InsertItem_" + TableName;
            _context.NhatKyRepository.Add(nhatKy);
            _context.SaveChanges();
            return Task.FromResult(true);
        }
        catch
        {
            return Task.FromResult(false);
        }
    }

    public Task<bool> LogUpdate(string TableName)
    {
        try
        {
            NhatKy nhatKy = new NhatKy();
            //nhatKy.UserId = _tenantProvider.TenantId;
            //nhatKy.TenantId = _tenantProvider.GetTenant().Id;
            nhatKy.ChucNang = "UpdateItem_" + TableName;
            nhatKy.CreatedDate = DateTime.Now;
            _context.NhatKyRepository.Add(nhatKy);
            _context.SaveChanges();
            return Task.FromResult(true);
        }
        catch
        {
            return Task.FromResult(false);
        }
    }

    public Task<bool> LogError(string TableName, string Error)
    {
        try
        {
            NhatKy nhatKy = new NhatKy();
            //nhatKy.UserId = _tenantProvider.TenantId;
            //nhatKy.TenantId = _tenantProvider.GetTenant().Id;
            nhatKy.ChucNang = "Error_" + TableName;
            //nhatKy.Error = Error;
            nhatKy.CreatedDate = DateTime.Now;
            _context.NhatKyRepository.Add(nhatKy);
            _context.SaveChanges();
            return Task.FromResult(true);
        }
        catch
        {
            return Task.FromResult(false);
        }
    }

    public Task<bool> LogUpdateNX(NhapXuatModel itemCu, NhapXuatModel itemMoi)
    {
        try
        {
            NhatKy nhatKy = new NhatKy();
            //nhatKy.UserId = _tenantProvider.TenantId;
            //nhatKy.TenantId = _tenantProvider.GetTenant().Id;
            nhatKy.ChucNang = "UpdateItem_NhapXuat";
            //nhatKy.IdPhieu = itemCu.Id;
            //nhatKy.NoiDungCu = JsonConvert.SerializeObject(itemCu);
            //nhatKy.NoiDungMoi = JsonConvert.SerializeObject(itemMoi);
            nhatKy.CreatedDate = DateTime.Now;
            _context.NhatKyRepository.Add(nhatKy);
            _context.SaveChanges();
            return Task.FromResult(true);
        }
        catch
        {
            return Task.FromResult(false);
        }
    }

    public Task<bool> LogCreateNX(NhapXuatModel item)
    {
        try
        {
            NhatKy nhatKy = new NhatKy();
            //nhatKy.UserId = _tenantProvider.TenantId;
            //nhatKy.TenantId = _tenantProvider.GetTenant().Id;
            nhatKy.ChucNang = "CreateItem_NhapXuat";
            //nhatKy.IdPhieu = item.Id;
            //nhatKy.NoiDungCu = "";
            //nhatKy.NoiDungMoi = JsonConvert.SerializeObject(item);
            nhatKy.CreatedDate = DateTime.Now;
            _context.NhatKyRepository.Add(nhatKy);
            _context.SaveChanges();
            return Task.FromResult(true);
        }
        catch
        {
            return Task.FromResult(false);
        }
    }
    public Task<bool> LogDeleteNX(NhapXuatModel item)
    {
        try
        {
            NhatKy nhatKy = new NhatKy();
            //nhatKy.UserId = _tenantProvider.TenantId;
            //nhatKy.TenantId = _tenantProvider.GetTenant().Id;
            nhatKy.ChucNang = "DeleteItem_NhapXuat";
            //nhatKy.IdPhieu = item.Id;
            //nhatKy.NoiDungCu = "";
            //nhatKy.NoiDungMoi = JsonConvert.SerializeObject(item);
            nhatKy.CreatedDate = DateTime.Now;
            _context.NhatKyRepository.Add(nhatKy);
            _context.SaveChanges();
            return Task.FromResult(true);
        }
        catch
        {
            return Task.FromResult(false);
        }
    }
}
