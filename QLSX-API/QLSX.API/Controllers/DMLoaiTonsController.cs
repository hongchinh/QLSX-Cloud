using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SaleAPI.Models;
using QLSX.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using System.Linq.Expressions;
using SaleAPI.Extensions;
using SaleAPI.Interfaces;
using SaleAPI.Services;
using QLSX.Shared.Entities;
using QLSX.Shared.Models.Request;

namespace SaleAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DMLoaiTonsController : ControllerBase
    {
        private readonly CRMDBContext _context;
        private readonly ITenantProvider _tenantProvider;
        private readonly INhatKyService _nhatKyService;
        public DMLoaiTonsController(CRMDBContext context, ITenantProvider tenantProvider, INhatKyService nhatKyService)
        {
            _context = context;
            _tenantProvider = tenantProvider;
            _nhatKyService = nhatKyService;
        }

        [HttpGet("GetCount")]
        public async Task<ActionResult<ItemCount>> GettCount()
        {
            ItemCount itemCount = new ItemCount();

            itemCount.Count = _context.DanhMucLoaiTonRepository.Count();
            return await Task.FromResult(itemCount);
        }



        // GET: api/Customers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DanhMucLoaiTonModel>> GetById(int id)
        {
            var item = await _context.DanhMucLoaiTonRepository
                //.Where(p => p.DMDonViSuDungId == _tenantProvider.TenantId)
                .Where(p => p.Id == id && p.DeletedDate == null)
                .FirstOrDefaultAsync();

            if (item == null)
            {
                return NotFound();
            }

            return new DanhMucLoaiTonModel(item);
        }

        // PUT: api/Customers/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("Update/{id}")]
        public async Task<ActionResult<DanhMucLoaiTon>> Put(int id, DanhMucLoaiTon item)
        {
            if (id != item.Id)
            {
                return new DanhMucLoaiTon();
            }

            //item.UpdatedDate = DateTime.Now;
            _context.Entry(item).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();

                // Log Nhat ky
                await _nhatKyService.LogUpdate("DanhMucLoaiTon");
            }
            catch (DbUpdateConcurrencyException)
            {
                // Log Nhat ky
                await _nhatKyService.LogError("Update_DanhMucLoaiTon", "id : " + id + ";\nitem : " + item.ToString());
                if (!Exists(id))
                {
                    return new DanhMucLoaiTon();
                }
                else
                {
                    return new DanhMucLoaiTon();
                }
            }

            return item;
        }

        // POST: api/Customers
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost("Create")]
        public async Task<ActionResult<DanhMucLoaiTon>> Post(DanhMucLoaiTon item)
        {
            //item.CreatedDate = DateTime.Now;
            //item.UpdatedDate = DateTime.Now;
            //item.DMDonViSuDungId = _tenantProvider.TenantId;
            _context.DanhMucLoaiTonRepository.Add(item);
            await _context.SaveChangesAsync();

            // Log Nhat ky
            await _nhatKyService.LogCreate("DanhMucLoaiTon");
            return item;
        }

        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult<DanhMucLoaiTon>> Delete(int id)
        {
            var item = await _context.DanhMucLoaiTonRepository
                //.Where(p => p.DMDonViSuDungId == _tenantProvider.TenantId)
                .Where(p => p.Id == id)
                .FirstOrDefaultAsync();
            if (item == null)
            {
                return NotFound();
            }

            //item.DeletedDate = DateTime.Now;
            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            // Log Nhat ky
            await _nhatKyService.LogDelete("DanhMucLoaiTon");
            return item;
        }

        private bool Exists(int id)
        {
            return _context.DanhMucLoaiTonRepository.Any(e => e.Id == id);
        }
        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<DanhMucLoaiTonModel>>> Get(SearchRequest request)
        {
            return await _context.DanhMucLoaiTonRepository
                //.Where(p => p.DMDonViSuDungId == _tenantProvider.TenantId)
                .Where(x => x.DeletedDate == null)
                .Select(item => new DanhMucLoaiTonModel(item))
                .ToListAsync();
        }
        [HttpGet("ExportToExcel")]
        public async Task<ActionResult<GetAllResponse<DanhMucLoaiTon>>> ExportToExcel([FromBody] BaseSearchRequest request)
        {
            GetAllResponse<DanhMucLoaiTon> outputs = new GetAllResponse<DanhMucLoaiTon>();
            // Expression<Func<DanhMucLoaiTon, bool>> filter = m => (_tenantProvider.TenantId == 0 || m.DMDonViSuDungId == _tenantProvider.TenantId);
            // if (!string.IsNullOrEmpty(request.Keywords))
            // {
            //     filter = filter.And(x => x.ChiTieu.Contains(request.Keywords) || x.MaSo.Contains(request.Keywords));
            // }
            // Func<IQueryable<DanhMucLoaiTon>, IOrderedQueryable<DanhMucLoaiTon>> order = null;
            // if (request.SortDirection != QLSX.Shared.Enums.SortDirection.None)
            // {
            //     order = await OrderBy(request.SortLable, request.SortDirection == QLSX.Shared.Enums.SortDirection.Ascending);
            // }

            // IQueryable<DanhMucLoaiTon> query = _context.Set<DanhMucLoaiTon>();

            // if (filter != null) query = query.Where(filter).Where(x => x.DeletedDate == null);
            // if (order != null) query = order(query);
            // outputs.TotalRecords = await query.CountAsync();
            // outputs.TotalPages = (int)Math.Ceiling(outputs.TotalRecords / (double)request.PageSize);
            // outputs.Page = request.Page;
            // outputs.PageSize = request.PageSize;

            ////query = query.Skip(request.Page * request.PageSize).Take(request.PageSize);
            // outputs.Items = await query.ToListAsync();

            // Log Nhat ky
            await _nhatKyService.LogExportExcel("DanhMucLoaiTon");
            return outputs;

        }
        [HttpGet("GetAllPaged")]
        public async Task<ActionResult<GetAllResponse<DanhMucLoaiTon>>> GetAllPaged([FromBody] BaseSearchRequest request)
        {
            GetAllResponse<DanhMucLoaiTon> outputs = new GetAllResponse<DanhMucLoaiTon>();
            //Expression<Func<DanhMucLoaiTon, bool>> filter = m => (_tenantProvider.TenantId == 0 || m.DMDonViSuDungId == _tenantProvider.TenantId);
            //if (!string.IsNullOrEmpty(request.Keywords))
            //{
            //    filter = filter.And(x => x.ChiTieu.Contains(request.Keywords) || x.MaSo.Contains(request.Keywords));
            //}
            //Func<IQueryable<DanhMucLoaiTon>, IOrderedQueryable<DanhMucLoaiTon>> order = null;
            //if (request.SortDirection != QLSX.Shared.Enums.SortDirection.None)
            //{
            //    order = await OrderBy(request.SortLable, request.SortDirection == QLSX.Shared.Enums.SortDirection.Ascending);
            //}

            //IQueryable<DanhMucLoaiTon> query = _context.Set<DanhMucLoaiTon>();

            //if (filter != null) query = query.Where(filter).Where(x => x.DeletedDate == null);
            //if (order != null) query = order(query);
            //outputs.TotalRecords = await query.CountAsync();
            //outputs.TotalPages = (int)Math.Ceiling(outputs.TotalRecords / (double)request.PageSize);
            //outputs.Page = request.Page;
            //outputs.PageSize = request.PageSize;

            //query = query.Skip(request.Page * request.PageSize).Take(request.PageSize);
            //outputs.Items = await query.ToListAsync();
            return outputs;

        }
        private async Task<Func<IQueryable<DanhMucLoaiTon>, IOrderedQueryable<DanhMucLoaiTon>>> OrderBy(string sortBy, bool sortType)
        {
            Func<IQueryable<DanhMucLoaiTon>, IOrderedQueryable<DanhMucLoaiTon>> myFunc;
            if (sortBy == "MaSo")
            {
                if (sortType) myFunc = source => source.OrderBy(x => x.MaSo);
                else myFunc = source => source.OrderByDescending(x => x.MaSo);
                return myFunc;
            }
            if (sortBy == "ChiTieu")
            {
                if (sortType) myFunc = source => source.OrderBy(x => x.ChiTieu);
                else myFunc = source => source.OrderByDescending(x => x.ChiTieu);
                return myFunc;
            }
            return null;

        }
    }
}
