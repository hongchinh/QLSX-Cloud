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
public class DMKhoanChisController : ControllerBase
{
    private readonly CRMDBContext _context;
    private readonly ITenantProvider _tenantProvider;
    private readonly INhatKyService _nhatKyService;
    public DMKhoanChisController(CRMDBContext context, ITenantProvider tenantProvider, INhatKyService nhatKyService)
    {
        _context = context;
        _tenantProvider = tenantProvider;
        _nhatKyService = nhatKyService;
    }


    [HttpGet("GetCount")]
    public async Task<ActionResult<ItemCount>> GetCount()
    {
        ItemCount itemCount = new ItemCount();

        itemCount.Count = _context.DanhMucKhoanChiRepository/*.Where(p => p.DMDonViSuDungId == _tenantProvider.TenantId)*/.Count();
        return await Task.FromResult(itemCount);
    }

    // GET: api/Customers/5
    [HttpGet("{id}")]
    public async Task<ActionResult<DanhMucKhoanChiModel>> GetById(int id)
    {
        var item = await _context.DanhMucKhoanChiRepository.FirstOrDefaultAsync(p => p.Id == id && p.DeletedDate == null);

        if (item == null)
        {
            return NotFound();
        }
        var result = new DanhMucKhoanChiModel(item);
        return result;
    }

    // PUT: api/Customers/5
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for
    // more details see https://aka.ms/RazorPagesCRUD.
    [HttpPut("Update/{id}")]
    public async Task<ActionResult<DanhMucKhoanChi>> Put(int id, DanhMucKhoanChiModel model)
    {
        if (id != model.Id)
        {
            return new DanhMucKhoanChi();
        }

        var entity = await _context.DanhMucKhoanChiRepository.FirstOrDefaultAsync(item => item.Id == id);
        entity.MaSo = model.MaKhoanChi;
        entity.UpdatedDate = DateTime.Now;
        entity.ChiTieu = model.TenKhoanChi;
        entity.GhiChu = model.GhiChu;
        try
        {
            await _context.SaveChangesAsync();

            // Log Nhat ky
            await _nhatKyService.LogUpdate("DanhMucKhoanChi");
        }
        catch (DbUpdateConcurrencyException)
        {
            // Log Nhat ky
            await _nhatKyService.LogError("Update_DanhMucKhoanChi", "id : " + id + ";\nitem : " + entity.ToString());
            if (!Exists(id))
            {
                return new DanhMucKhoanChi();
            }
            else
            {
                return new DanhMucKhoanChi();
            }
        }

        return entity;
    }

    // POST: api/Customers
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for
    // more details see https://aka.ms/RazorPagesCRUD.
    [HttpPost("Create")]
    public async Task<ActionResult<DanhMucKhoanChi>> Post(DanhMucKhoanChiModel model)
    {
        DanhMucKhoanChi entity = new DanhMucKhoanChi()
        {
            //Stt = stt,
            Id = 0,
            MaSo = model.MaKhoanChi,
            ChiTieu = model.TenKhoanChi,
            GhiChu = model.GhiChu,
            CreatedDate = DateTime.Now,
            UpdatedDate = DateTime.Now,
            //Cap = cap,
            //SHTK = sHTK,
            //BatBuoc = batBuoc,
        };
        _context.DanhMucKhoanChiRepository.Add(entity);
        await _context.SaveChangesAsync();

        // Log Nhat ky
        await _nhatKyService.LogCreate("DanhMucKhoanChi");
        return entity;
    }

    [HttpDelete("Delete/{id}")]
    public async Task<ActionResult<DanhMucKhoanChi>> Delete(int id)
    {
        var item = await _context.DanhMucKhoanChiRepository.FirstOrDefaultAsync(p => p.Id == id && p.DeletedDate == null);
        if (item == null)
        {
            return NotFound();
        }

        item.DeletedDate = DateTime.Now;
        await _context.SaveChangesAsync();

        // Log Nhat ky
        await _nhatKyService.LogDelete("DanhMucKhoanChi");
        return item;
    }

    private bool Exists(int id)
    {
        return _context.DanhMucKhoanChiRepository.Any(e => e.Id == id);
    }

    [HttpGet("getCode/{code}")]
    public async Task<ActionResult<DanhMucKhoanChiModel>> GetByCode(string code)
    {
        if (string.IsNullOrEmpty(code))
        {
            return NotFound();
        }
        var item = await _context.DanhMucKhoanChiRepository.FirstOrDefaultAsync(p => p.MaSo.ToLower() == code.ToLower() && p.DeletedDate == null);

        if (item == null)
        {
            return NotFound();
        }
        return new DanhMucKhoanChiModel(item);
    }

    [HttpGet("")]
    public async Task<ActionResult<IEnumerable<DanhMucKhoanChiModel>>> Get(SearchRequest request)
    {
        return await _context.DanhMucKhoanChiRepository
            .Where(x => x.DeletedDate == null)
            .Select(item => new DanhMucKhoanChiModel(item))
            .ToListAsync();
    }

    [HttpGet("ExportToExcel")]
    public async Task<ActionResult<GetAllResponse<DanhMucKhoanChiModel>>> ExportToExcel([FromBody] BaseSearchRequest request)
    {
        GetAllResponse<DanhMucKhoanChiModel> outputs = await GetData(request, false);

        // Log Nhat ky
        await _nhatKyService.LogExportExcel("DanhMucKhoanChi");
        return outputs;
    }

    [HttpGet("GetAllPaged")]
    public async Task<ActionResult<GetAllResponse<DanhMucKhoanChiModel>>> GetAllPaged([FromBody] BaseSearchRequest request)
    {
        GetAllResponse<DanhMucKhoanChiModel> outputs = await GetData(request, true);
        return outputs;
    }

    private async Task<GetAllResponse<DanhMucKhoanChiModel>> GetData(BaseSearchRequest request, bool isPaging)
    {
        GetAllResponse<DanhMucKhoanChiModel> outputs = new GetAllResponse<DanhMucKhoanChiModel>();
        IQueryable<DanhMucKhoanChi> query = _context.DanhMucKhoanChiRepository.Where(item => item.DeletedDate == null);
        if (!string.IsNullOrEmpty(request.Keywords))
        {
            query = query.Where(item => item.MaSo.Contains(request.Keywords)
                                        || item.ChiTieu.Contains(request.Keywords));
        }

        Func<IQueryable<DanhMucKhoanChi>, IOrderedQueryable<DanhMucKhoanChi>> order = null;
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
        outputs.Items = rawData.Select(item => new DanhMucKhoanChiModel(item)).ToList();
        return outputs;
    }

    private async Task<Func<IQueryable<DanhMucKhoanChi>, IOrderedQueryable<DanhMucKhoanChi>>> OrderBy(string sortBy, bool sortType)
    {
        Func<IQueryable<DanhMucKhoanChi>, IOrderedQueryable<DanhMucKhoanChi>> myFunc;
        if (sortBy == "MaKhoanChi")
        {
            if (sortType) myFunc = source => source.OrderBy(x => x.MaSo);
            else myFunc = source => source.OrderByDescending(x => x.MaSo);
            return myFunc;
        }
        if (sortBy == "TenKhoanChi")
        {
            if (sortType) myFunc = source => source.OrderBy(x => x.ChiTieu);
            else myFunc = source => source.OrderByDescending(x => x.ChiTieu);
            return myFunc;
        }
        return null;
    }
}
