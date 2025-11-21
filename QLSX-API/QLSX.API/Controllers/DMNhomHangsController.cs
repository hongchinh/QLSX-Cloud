using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SaleAPI.Models;
using QLSX.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using SaleAPI.Interfaces;
using SaleAPI.Services;
using QLSX.Shared.Entities;
using QLSX.Shared.Models.Request;

namespace SaleAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class DMNhomHangsController : ControllerBase
{
    private readonly CRMDBContext _context;
    private readonly ITenantProvider _tenantProvider;
    private readonly INhatKyService _nhatKyService;
    public DMNhomHangsController(CRMDBContext context, ITenantProvider tenantProvider, INhatKyService nhatKyService)
    {
        _context = context;
        _tenantProvider = tenantProvider;
        _nhatKyService = nhatKyService;
    }

    [HttpGet("GetCount")]
    public async Task<ActionResult<ItemCount>> GettCount()
    {
        ItemCount itemCount = new ItemCount();

        itemCount.Count = _context.DanhMucNhomHangRepository.Count(x => x.DeletedDate == null);
        return await Task.FromResult(itemCount);
    }

    // GET: api/Customers/5
    [HttpGet("{id}")]
    public async Task<ActionResult<DanhMucNhomHangModel>> GetById(int id)
    {
        var item = await _context.DanhMucNhomHangRepository.FirstOrDefaultAsync(p => p.Id == id && p.DeletedDate == null);

        if (item == null)
        {
            return NotFound();
        }
        return new DanhMucNhomHangModel(item);
    }

    [HttpGet("getCode/{code}")]
    public async Task<ActionResult<DanhMucNhomHangModel>> GetByCode(string code)
    {
        if (string.IsNullOrEmpty(code))
        {
            return NotFound();
        }
        var item = await _context.DanhMucNhomHangRepository.FirstOrDefaultAsync(p => p.MaNhomHang.ToLower() == code.ToLower() && p.DeletedDate == null);

        if (item == null)
        {
            return NotFound();
        }
        return new DanhMucNhomHangModel(item);
    }

    // PUT: api/Customers/5
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for
    // more details see https://aka.ms/RazorPagesCRUD.
    [HttpPut("Update/{id}")]
    public async Task<ActionResult<DanhMucNhomHangModel>> Put(int id, DanhMucNhomHangModel model)
    {
        if (id != model.Id)
        {
            return new DanhMucNhomHangModel();
        }

        var entity = await _context.DanhMucNhomHangRepository.FirstOrDefaultAsync(item => item.Id == id);
        entity.MaNhomHang = model.MaNhom;
        entity.TenNhomHang = model.TenNhom;
        entity.UpdatedDate = DateTime.Now;
        entity.GhiChu = model.GhiChu;
        try
        {
            await _context.SaveChangesAsync();

            // Log Nhat ky
            await _nhatKyService.LogUpdate("DanhMucNhomHang");
        }
        catch (DbUpdateConcurrencyException)
        {
            // Log Nhat ky
            await _nhatKyService.LogError("Update_DanhMucNhomHang", "id : " + id + ";\nitem : " + entity.ToString());
            if (!Exists(id))
            {
                return new DanhMucNhomHangModel();
            }
            else
            {
                return new DanhMucNhomHangModel();
            }
        }

        return new DanhMucNhomHangModel(entity);
    }

    // POST: api/Customers
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for
    // more details see https://aka.ms/RazorPagesCRUD.
    [HttpPost("Create")]
    public async Task<ActionResult<DanhMucNhomHangModel>> Post(DanhMucNhomHangModel model)
    {
        //item.DMDonViSuDungId = _tenantProvider.TenantId;
        var entity = new DanhMucNhomHang()
        {
            MaNhomHang = model.MaNhom,
            TenNhomHang = model.TenNhom,
            GhiChu = model.GhiChu,
            KyHieu = model.KyHieu,
            Selected = model.Selected,
            CreatedDate = DateTime.Now,
            UpdatedDate = DateTime.Now,
        };
        _context.DanhMucNhomHangRepository.Add(entity);
        await _context.SaveChangesAsync();

        // Log Nhat ky
        await _nhatKyService.LogCreate("DanhMucNhomHang");
        return new DanhMucNhomHangModel(entity);
    }

    [HttpDelete("Delete/{id}")]
    public async Task<ActionResult<DanhMucNhomHang>> Delete(int id)
    {
        var item = await _context.DanhMucNhomHangRepository.FirstOrDefaultAsync(p => p.Id == id && p.DeletedDate == null);
        if (item == null)
        {
            return NotFound();
        }

        item.DeletedDate = DateTime.Now;
        await _context.SaveChangesAsync();

        // Log Nhat ky
        await _nhatKyService.LogDelete("DanhMucNhomHang");
        return item;
    }

    private bool Exists(int id)
    {
        return _context.DanhMucNhomHangRepository.Any(e => e.Id == id);
    }

    [HttpGet("")]
    public async Task<ActionResult<IEnumerable<DanhMucNhomHangModel>>> Get(SearchRequest request)
    {
        var result = await _context.DanhMucNhomHangRepository
                                   .Where(x => x.DeletedDate == null)
                                   .Select(item => new DanhMucNhomHangModel(item))
                                   .ToListAsync();
        return result;
    }

    [HttpGet("ExportToExcel")]
    public async Task<ActionResult<GetAllResponse<DanhMucNhomHangModel>>> Get([FromBody] BaseSearchRequest request)
    {
        GetAllResponse<DanhMucNhomHangModel> outputs = await GetData(request, true);

        // Log Nhat ky
        await _nhatKyService.LogExportExcel("DanhMucNhomHang");
        return outputs;

    }
    [HttpGet("GetAllPaged")]
    public async Task<ActionResult<GetAllResponse<DanhMucNhomHangModel>>> GetAllPaged([FromBody] BaseSearchRequest request)
    {
        GetAllResponse<DanhMucNhomHangModel> outputs = await GetData(request, true);
        return outputs;
    }

    private async Task<GetAllResponse<DanhMucNhomHangModel>> GetData(BaseSearchRequest request, bool isPaging)
    {
        GetAllResponse<DanhMucNhomHangModel> outputs = new GetAllResponse<DanhMucNhomHangModel>();
        IQueryable<DanhMucNhomHang> query = _context.DanhMucNhomHangRepository.Where(item => item.DeletedDate == null);
        if (!string.IsNullOrEmpty(request.Keywords))
        {
            query = query.Where(x => x.MaNhomHang.Contains(request.Keywords) || x.TenNhomHang.Contains(request.Keywords));
        }
        Func<IQueryable<DanhMucNhomHang>, IOrderedQueryable<DanhMucNhomHang>> order = null;
        if (request.SortDirection != QLSX.Shared.Enums.SortDirection.None)
        {
            order = await OrderBy(request.SortLable, request.SortDirection == QLSX.Shared.Enums.SortDirection.Ascending);
        }

        if (order != null) query = order(query);
        outputs.TotalRecords = await query.CountAsync();
        outputs.TotalPages = (int)Math.Ceiling(outputs.TotalRecords / (double)request.PageSize);
        outputs.Page = request.Page;
        outputs.PageSize = request.PageSize;
        if (isPaging)
        {
            query = query.Skip(request.Page * request.PageSize).Take(request.PageSize);
        }
        var rawData = await query.ToListAsync();
        outputs.Items = rawData.Select(item => new DanhMucNhomHangModel(item)).ToList();
        return outputs;
    }

    private async Task<Func<IQueryable<DanhMucNhomHang>, IOrderedQueryable<DanhMucNhomHang>>> OrderBy(string sortBy, bool sortType)
    {
        Func<IQueryable<DanhMucNhomHang>, IOrderedQueryable<DanhMucNhomHang>> myFunc;
        if (sortBy == "MaNhom")
        {
            if (sortType) myFunc = source => source.OrderBy(x => x.MaNhomHang);
            else myFunc = source => source.OrderByDescending(x => x.MaNhomHang);
            return myFunc;
        }
        if (sortBy == "TenNhom")
        {
            if (sortType) myFunc = source => source.OrderBy(x => x.TenNhomHang);
            else myFunc = source => source.OrderByDescending(x => x.TenNhomHang);
            return myFunc;
        }
        return null;

    }

}
