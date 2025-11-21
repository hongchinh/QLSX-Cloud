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
    public class DMDoDaysController : ControllerBase
    {
        private readonly CRMDBContext _context;
        private readonly ITenantProvider _tenantProvider;
        private readonly INhatKyService _nhatKyService;
        public DMDoDaysController(CRMDBContext context, ITenantProvider tenantProvider, INhatKyService nhatKyService)
        {
            _context = context;
            _tenantProvider = tenantProvider;
            _nhatKyService = nhatKyService;
        }



        [HttpGet("GetCount")]
        public async Task<ActionResult<ItemCount>> GettCount()
        {
            ItemCount itemCount = new ItemCount();

            itemCount.Count = _context.DanhMucDoDayRepository
                //.Where(p => p.DMDonViSuDungId == _tenantProvider.TenantId)
                .Count();
            return await Task.FromResult(itemCount);
        }

        // GET: api/Customers
        [HttpGet("GetByPage")]
        public async Task<ActionResult<IEnumerable<DanhMucDoDayModel>>> GetByPage(int pageSize, int pageNumber)
        {
            List<DanhMucDoDay> list = await _context.DanhMucDoDayRepository
                //.Where(x => x.DeletedDate == null)
                 //.Where(p => p.DMDonViSuDungId == _tenantProvider.TenantId)
                .ToListAsync();
            var result = list.Skip(pageNumber * pageSize).Take(pageSize).Select(item => new DanhMucDoDayModel(item)).ToList();

            return await Task.FromResult(result);
        }

        // GET: api/Customers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DanhMucDoDay>> GetById(int id)
        {
            var item = await _context.DanhMucDoDayRepository/*.Where(p => p.DMDonViSuDungId == _tenantProvider.TenantId)*/
                 .Where(p => p.Id == id).FirstOrDefaultAsync();

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
        public async Task<ActionResult<DanhMucDoDay>> Put(int id, DanhMucDoDay item)
        {
            if (id != item.Id)
            {
                return new DanhMucDoDay();
            }

            //item.UpdatedDate = DateTime.Now;
            _context.Entry(item).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();

                // Log Nhat ky
                await _nhatKyService.LogUpdate("DanhMucDoDay");
            }
            catch (DbUpdateConcurrencyException)
            {
                // Log Nhat ky
                await _nhatKyService.LogError("Update_DanhMucDoDay", "id : " + id + ";\nitem : " + item.ToString());
                if (!Exists(id))
                {
                    return new DanhMucDoDay();
                }
                else
                {
                    return new DanhMucDoDay();
                }
            }

            return item;
        }

        // POST: api/Customers
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost("Create")]
        public async Task<ActionResult<DanhMucDoDay>> Post(DanhMucDoDay item)
        {
            //item.CreatedDate = DateTime.Now;
            //item.UpdatedDate = DateTime.Now;
            //item.DMDonViSuDungId = _tenantProvider.TenantId;
            _context.DanhMucDoDayRepository.Add(item);
            await _context.SaveChangesAsync();

            // Log Nhat ky
            await _nhatKyService.LogCreate("DanhMucDoDay");
            return item;
        }

        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult<DanhMucDoDay>> Delete(int id)
        {
            var item = await _context.DanhMucDoDayRepository
                 //.Where(p => p.DMDonViSuDungId == _tenantProvider.TenantId)
                 .FirstOrDefaultAsync(p => p.Id == id);
            if (item == null)
            {
                return NotFound();
            }

            item.DeletedDate = DateTime.Now;
            await _context.SaveChangesAsync();

            // Log Nhat ky
            await _nhatKyService.LogDelete("DanhMucDoDay");
            return item;
        }

        private bool Exists(int id)
        {
            return _context.DanhMucDoDayRepository.Any(e => e.Id == id);
        }

        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<DanhMucDoDayModel>>> Get(SearchRequest request)
        {
            return await _context.DanhMucDoDayRepository
                //.Where(p => p.DMDonViSuDungId == _tenantProvider.TenantId)
                //.Where(x => x.DeletedDate == null)
                .Select(item => new DanhMucDoDayModel(item))
                .ToListAsync();
        }

        [HttpGet("ExportToExcel")]
        public async Task<ActionResult<GetAllResponse<DanhMucDoDay>>> ExportToExcel([FromBody] BaseSearchRequest request)
        {
            GetAllResponse<DanhMucDoDay> outputs = new GetAllResponse<DanhMucDoDay>();
            //Expression<Func<DanhMucDoDay, bool>> filter = m => (_tenantProvider.TenantId == 0 || m.DMDonViSuDungId == _tenantProvider.TenantId);
            //if (!string.IsNullOrEmpty(request.Keywords))
            //{
            //    filter = filter.And(x => x.ChiTieu.Contains(request.Keywords) || x.MaSo.Contains(request.Keywords));
            //}
            //Func<IQueryable<DanhMucDoDay>, IOrderedQueryable<DanhMucDoDay>> order = null;
            //if (request.SortDirection != QLSX.Shared.Enums.SortDirection.None)
            //{
            //    order = await OrderBy(request.SortLable, request.SortDirection == QLSX.Shared.Enums.SortDirection.Ascending);
            //}

            //IQueryable<DanhMucDoDay> query = _context.Set<DanhMucDoDay>();

            //if (filter != null) query = query.Where(filter).Where(x => x.DeletedDate == null);
            //if (order != null) query = order(query);
            //outputs.TotalRecords = await query.CountAsync();
            //outputs.TotalPages = (int)Math.Ceiling(outputs.TotalRecords / (double)request.PageSize);
            //outputs.Page = request.Page;
            //outputs.PageSize = request.PageSize;

            ////query = query.Skip(request.Page * request.PageSize).Take(request.PageSize);
            //outputs.Items = await query.ToListAsync();

            // Log Nhat ky
            await _nhatKyService.LogExportExcel("DanhMucDoDay");
            return outputs;

        }
        [HttpGet("GetAllPaged")]
        public async Task<ActionResult<GetAllResponse<DanhMucDoDay>>> GetAllPaged([FromBody] BaseSearchRequest request)
        {
            GetAllResponse<DanhMucDoDay> outputs = new GetAllResponse<DanhMucDoDay>();
            //Expression<Func<DanhMucDoDay, bool>> filter = m => (_tenantProvider.TenantId == 0 || m.DMDonViSuDungId == _tenantProvider.TenantId);
            //if (!string.IsNullOrEmpty(request.Keywords))
            //{
            //    filter = filter.And(x => x.ChiTieu.Contains(request.Keywords) || x.MaSo.Contains(request.Keywords));
            //}
            //Func<IQueryable<DanhMucDoDay>, IOrderedQueryable<DanhMucDoDay>> order = null;
            //if (request.SortDirection != QLSX.Shared.Enums.SortDirection.None)
            //{
            //    order = await OrderBy(request.SortLable, request.SortDirection == QLSX.Shared.Enums.SortDirection.Ascending);
            //}

            //IQueryable<DanhMucDoDay> query = _context.Set<DanhMucDoDay>();

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
        private async Task<Func<IQueryable<DanhMucDoDay>, IOrderedQueryable<DanhMucDoDay>>> OrderBy(string sortBy, bool sortType)
        {
            Func<IQueryable<DanhMucDoDay>, IOrderedQueryable<DanhMucDoDay>> myFunc;
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
