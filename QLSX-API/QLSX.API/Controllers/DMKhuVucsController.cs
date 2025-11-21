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
    public class DanhMucKhuVucsController : ControllerBase
    {
        private readonly CRMDBContext _context;
        private readonly ITenantProvider _tenantProvider;
        private readonly INhatKyService _nhatKyService;
        public DanhMucKhuVucsController(CRMDBContext context, ITenantProvider tenantProvider, INhatKyService nhatKyService)
        {
            _context = context;
            _tenantProvider = tenantProvider;
            _nhatKyService = nhatKyService;
        }



        [HttpGet("GetCount")]
        public async Task<ActionResult<ItemCount>> GettCount()
        {
            ItemCount itemCount = new ItemCount();

            itemCount.Count = _context.DanhMucKhuVucRepository.Count();
            return await Task.FromResult(itemCount);
        }



        // GET: api/Customers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DanhMucKhuVuc>> GetById(int id)
        {
            var item = await _context.DanhMucKhuVucRepository
                //.Where(p => p.DMDonViSuDungId == _tenantProvider.TenantId)
                .Where(p => p.Id == id)
                .FirstOrDefaultAsync();

            if (item == null)
            {
                return NotFound();
            }

            return item;
        }

        // PUT: api/Customers/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("Update/{id}")]
        public async Task<ActionResult<DanhMucKhuVuc>> Put(int id, DanhMucKhuVuc item)
        {
            if (id != item.Id)
            {
                return new DanhMucKhuVuc();
            }

            //item.UpdatedDate = DateTime.Now;
            _context.Entry(item).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();

                // Log Nhat ky
                await _nhatKyService.LogUpdate("DanhMucKhuVuc");
            }
            catch (DbUpdateConcurrencyException)
            {
                // Log Nhat ky
                await _nhatKyService.LogError("Update_DanhMucKhuVuc", "id : " + id + ";\nitem : " + item.ToString());
                if (!Exists(id))
                {
                    return new DanhMucKhuVuc();
                }
                else
                {
                    return new DanhMucKhuVuc();
                }
            }

            return item;
        }
        // POST: api/Customers
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost("Create")]
        public async Task<ActionResult<DanhMucKhuVuc>> Post(DanhMucKhuVuc item)
        {
            //item.CreatedDate = DateTime.Now;
            //item.UpdatedDate = DateTime.Now;
            //item.DMDonViSuDungId = _tenantProvider.TenantId;
            _context.DanhMucKhuVucRepository.Add(item);
            await _context.SaveChangesAsync();

            // Log Nhat ky
            await _nhatKyService.LogCreate("DanhMucKhuVuc");
            return item;
        }

        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult<DanhMucKhuVuc>> Delete(int id)
        {
            var item = await _context.DanhMucKhuVucRepository
                //.Where(p => p.DMDonViSuDungId == _tenantProvider.TenantId)
                .Where(p => p.Id == id)
                .FirstOrDefaultAsync();
            if (item == null)
            {
                return NotFound();
            }

            item.DeletedDate = DateTime.Now;
            await _context.SaveChangesAsync();

            // Log Nhat ky
            await _nhatKyService.LogDelete("DanhMucKhuVuc");
            return item;
        }

        private bool Exists(int id)
        {
            return _context.DanhMucKhuVucRepository.Any(e => e.Id == id);
        }
        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<DanhMucKhuVuc>>> Get(SearchRequest request)
        {
            return await _context.DanhMucKhuVucRepository
                //.Where(p => p.DMDonViSuDungId == _tenantProvider.TenantId)
                //.Where(x => x.DeletedDate == null)
                .ToListAsync();
        }
        [HttpGet("ExportToExcel")]
        public async Task<ActionResult<GetAllResponse<DanhMucKhuVuc>>> ExportToExcel([FromBody] BaseSearchRequest request)
        {
            GetAllResponse<DanhMucKhuVuc> outputs = new GetAllResponse<DanhMucKhuVuc>();
            //Expression<Func<DanhMucKhuVuc, bool>> filter = m => (_tenantProvider.TenantId == 0 || m.DMDonViSuDungId == _tenantProvider.TenantId);
            //if (!string.IsNullOrEmpty(request.Keywords))
            //{
            //    filter = filter.And(x => x.TenKhuVuc.Contains(request.Keywords));
            //}
            //Func<IQueryable<DanhMucKhuVuc>, IOrderedQueryable<DanhMucKhuVuc>> order = null;
            //if (request.SortDirection != QLSX.Shared.Enums.SortDirection.None)
            //{
            //    order = await OrderBy(request.SortLable, request.SortDirection == QLSX.Shared.Enums.SortDirection.Ascending);
            //}

            //IQueryable<DanhMucKhuVuc> query = _context.Set<DanhMucKhuVuc>();

            //if (filter != null) query = query.Where(filter);
            //if (order != null) query = order(query);
            //outputs.TotalRecords = await query.CountAsync();
            //outputs.TotalPages = (int)Math.Ceiling(outputs.TotalRecords / (double)request.PageSize);
            //outputs.Page = request.Page;
            //outputs.PageSize = request.PageSize;

            ////query = query.Skip(request.Page * request.PageSize).Take(request.PageSize);
            //outputs.Items = await query.ToListAsync();

            // Log Nhat ky
            await _nhatKyService.LogExportExcel("DanhMucKhuVuc");
            return outputs;

        }
        [HttpGet("GetAllPaged")]
        public async Task<ActionResult<GetAllResponse<DanhMucKhuVuc>>> GetAllPaged([FromBody] BaseSearchRequest request)
        {
            GetAllResponse<DanhMucKhuVuc> outputs = new GetAllResponse<DanhMucKhuVuc>();
            //Expression<Func<DanhMucKhuVuc, bool>> filter = m => (_tenantProvider.TenantId == 0 || m.DMDonViSuDungId == _tenantProvider.TenantId);
            //if (!string.IsNullOrEmpty(request.Keywords))
            //{
            //    filter = filter.And(x => x.TenKhuVuc.Contains(request.Keywords));
            //}
            //Func<IQueryable<DanhMucKhuVuc>, IOrderedQueryable<DanhMucKhuVuc>> order = null;
            //if (request.SortDirection != QLSX.Shared.Enums.SortDirection.None)
            //{
            //    order = await OrderBy(request.SortLable, request.SortDirection == QLSX.Shared.Enums.SortDirection.Ascending);
            //}

            //IQueryable<DanhMucKhuVuc> query = _context.Set<DanhMucKhuVuc>();

            //if (filter != null) query = query.Where(filter);
            //if (order != null) query = order(query);
            //outputs.TotalRecords = await query.CountAsync();
            //outputs.TotalPages = (int)Math.Ceiling(outputs.TotalRecords / (double)request.PageSize);
            //outputs.Page = request.Page;
            //outputs.PageSize = request.PageSize;

            //query = query.Skip(request.Page * request.PageSize).Take(request.PageSize);
            //outputs.Items = await query.ToListAsync();
            return outputs;

        }
        private async Task<Func<IQueryable<DanhMucKhuVuc>, IOrderedQueryable<DanhMucKhuVuc>>> OrderBy(string sortBy, bool sortType)
        {
            Func<IQueryable<DanhMucKhuVuc>, IOrderedQueryable<DanhMucKhuVuc>> myFunc;
            if (sortBy == "TenKhuVuc")
            {
                if (sortType) myFunc = source => source.OrderBy(x => x.TenKhuVuc);
                else myFunc = source => source.OrderByDescending(x => x.TenKhuVuc);
                return myFunc;
            }

            return null;

        }
    }
}
