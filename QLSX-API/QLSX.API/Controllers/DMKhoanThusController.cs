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
public class DMKhoanThusController : ControllerBase
{
    private readonly CRMDBContext _context;
    private readonly ITenantProvider _tenantProvider;
    private readonly INhatKyService _nhatKyService;
    public DMKhoanThusController(CRMDBContext context, ITenantProvider tenantProvider, INhatKyService nhatKyService)
    {
        _context = context;
        _tenantProvider = tenantProvider;
        _nhatKyService = nhatKyService;
    }



    [HttpGet("GetCount")]
    public async Task<ActionResult<ItemCount>> GettCount()
    {
        ItemCount itemCount = new ItemCount();

        itemCount.Count = _context.DanhMucKhoanThuRepository/*.Where(p => p.DMDonViSuDungId == _tenantProvider.TenantId)*/.Count();
        return await Task.FromResult(itemCount);
    }

    // GET: api/Customers/5
    [HttpGet("{id}")]
    public async Task<ActionResult<DanhMucKhoanThuModel>> GetById(int id)
    {
        var item = await _context.DanhMucKhoanThuRepository.FirstOrDefaultAsync(p => p.Id == id && p.DeletedDate == null);

        if (item == null)
        {
            return NotFound();
        }

        return new DanhMucKhoanThuModel(item);
    }

    [HttpGet("getCode/{code}")]
    public async Task<ActionResult<DanhMucKhoanThuModel>> GetByCode(string code)
    {
        if (string.IsNullOrEmpty(code))
        {
            return NotFound();
        }
        var item = await _context.DanhMucKhoanThuRepository.FirstOrDefaultAsync(p => p.MaSo.ToLower() == code.ToLower() && p.DeletedDate == null);

        if (item == null)
        {
            return NotFound();
        }
        return new DanhMucKhoanThuModel(item);
    }

    // PUT: api/Customers/5
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for
    // more details see https://aka.ms/RazorPagesCRUD.
    [HttpPut("Update/{id}")]
    public async Task<ActionResult<DanhMucKhoanThuModel>> Put(int id, DanhMucKhoanThuModel model)
    {
        if (id != model.Id)
        {
            return new DanhMucKhoanThuModel();
        }

        var entity = await _context.DanhMucKhoanThuRepository.FirstOrDefaultAsync(item => item.Id == id);
        entity.MaSo = model.MaKhoanThu;
        entity.ChiTieu = model.TenKhoanThu;
        entity.GhiChu = model.GhiChu;
        entity.UpdatedDate = DateTime.Now;
        try
        {
            await _context.SaveChangesAsync();

            // Log Nhat ky
            await _nhatKyService.LogUpdate("DanhMucKhoanThu");
        }
        catch (DbUpdateConcurrencyException)
        {
            // Log Nhat ky
            await _nhatKyService.LogError("Update_DanhMucKhoanThu", "id : " + id + ";\nitem : " + model.ToString());
            if (!Exists(id))
            {
                return new DanhMucKhoanThuModel();
            }
            else
            {
                return new DanhMucKhoanThuModel();
            }
        }

        return new DanhMucKhoanThuModel(entity);
    }

    // POST: api/Customers
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for
    // more details see https://aka.ms/RazorPagesCRUD.
    [HttpPost("Create")]
    public async Task<ActionResult<DanhMucKhoanThu>> Post(DanhMucKhoanThuModel model)
    {
        DanhMucKhoanThu entity = new DanhMucKhoanThu()
        {
            //Stt = stt,
            MaSo = model.MaKhoanThu,
            ChiTieu = model.TenKhoanThu,
            GhiChu = model.GhiChu,
            CreatedDate = DateTime.Now,
            UpdatedDate = DateTime.Now,
            //Cap = cap,
            //SHTK = sHTK,
            //BatBuoc = batBuoc,
        };
        _context.DanhMucKhoanThuRepository.Add(entity);
        await _context.SaveChangesAsync();

        // Log Nhat ky
        await _nhatKyService.LogCreate("DanhMucKhoanThu");
        return entity;
    }

    [HttpDelete("Delete/{id}")]
    public async Task<ActionResult<DanhMucKhoanThu>> Delete(int id)
    {
        var item = await _context.DanhMucKhoanThuRepository.FirstOrDefaultAsync(p => p.Id == id && p.DeletedDate == null);
        if (item == null)
        {
            return NotFound();
        }

        item.DeletedDate = DateTime.Now;
        await _context.SaveChangesAsync();

        // Log Nhat ky
        await _nhatKyService.LogDelete("DanhMucKhoanThu");
        return item;
    }

    private bool Exists(int id)
    {
        return _context.DanhMucKhoanThuRepository.Any(e => e.Id == id);
    }

    [HttpGet("")]
    public async Task<ActionResult<IEnumerable<DanhMucKhoanThuModel>>> Get(SearchRequest request)
    {
        var lst = await _context.DanhMucKhoanThuRepository
                             .Where(x => x.DeletedDate == null)
                             .Select(item => new DanhMucKhoanThuModel(item))
                             .ToListAsync();
        return Ok(lst);
    }

    [HttpGet("ExportToExcel")]
    public async Task<ActionResult<GetAllResponse<DanhMucKhoanThuModel>>> ExportToExcel([FromBody] BaseSearchRequest request)
    {
        GetAllResponse<DanhMucKhoanThuModel> outputs = await GetData(request, false);
        // Log Nhat ky
        await _nhatKyService.LogExportExcel("DanhMucKhoanThu");
        return outputs;

    }

    [HttpGet("GetAllPaged")]
    public async Task<ActionResult<GetAllResponse<DanhMucKhoanThuModel>>> GetAllPaged([FromBody] BaseSearchRequest request)
    {
        GetAllResponse<DanhMucKhoanThuModel> outputs = await GetData(request, true);
        return outputs;
    }

    private async Task<GetAllResponse<DanhMucKhoanThuModel>> GetData(BaseSearchRequest request, bool isPaging)
    {
        GetAllResponse<DanhMucKhoanThuModel> outputs = new GetAllResponse<DanhMucKhoanThuModel>();
        IQueryable<DanhMucKhoanThu> query = _context.DanhMucKhoanThuRepository.Where(item => item.DeletedDate == null);

        if (!string.IsNullOrEmpty(request.Keywords))
        {
            query = query.Where(x => x.MaSo.Contains(request.Keywords) || x.ChiTieu.Contains(request.Keywords));
        }
        Func<IQueryable<DanhMucKhoanThu>, IOrderedQueryable<DanhMucKhoanThu>> order = null;
        if (request.SortDirection != QLSX.Shared.Enums.SortDirection.None)
        {
            order = await OrderBy(request.SortLable, request.SortDirection == QLSX.Shared.Enums.SortDirection.Ascending);
        }

        if (order != null) query = order(query);
        outputs.TotalRecords = await query.CountAsync();
        outputs.TotalPages = (int)Math.Ceiling(outputs.TotalRecords / (double)request.PageSize);
        outputs.Page = request.Page;
        outputs.PageSize = request.PageSize;

        query = query.Skip(request.Page * request.PageSize).Take(request.PageSize);
        var rawData = await query.ToListAsync();
        outputs.Items = rawData.Select(item => new DanhMucKhoanThuModel(item)).ToList();
        return outputs;
    }

    private async Task<Func<IQueryable<DanhMucKhoanThu>, IOrderedQueryable<DanhMucKhoanThu>>> OrderBy(string sortBy, bool sortType)
    {
        Func<IQueryable<DanhMucKhoanThu>, IOrderedQueryable<DanhMucKhoanThu>> myFunc;
        if (sortBy == "MaKhoanThu")
        {
            if (sortType) myFunc = source => source.OrderBy(x => x.MaSo);
            else myFunc = source => source.OrderByDescending(x => x.MaSo);
            return myFunc;
        }
        if (sortBy == "TenKhoanThu")
        {
            if (sortType) myFunc = source => source.OrderBy(x => x.ChiTieu);
            else myFunc = source => source.OrderByDescending(x => x.ChiTieu);
            return myFunc;
        }
        return null;
    }
}
