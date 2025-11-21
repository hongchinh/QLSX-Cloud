using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SaleAPI.Models;
using QLSX.Shared.Models;
using System.Linq.Expressions;
using SaleAPI.Extensions;
using SaleAPI.Interfaces;
using SaleAPI.Services;
using QLSX.Shared.Models.Request;

namespace SaleAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DMPhongBansController : ControllerBase
    {
        private readonly CRMDBContext _context;
        private readonly ITenantProvider _tenantProvider;
        private readonly INhatKyService _nhatKyService;
        public DMPhongBansController(CRMDBContext context, ITenantProvider tenantProvider, INhatKyService nhatKyService)
        {
            _context = context;
            _tenantProvider = tenantProvider;
            _nhatKyService = nhatKyService;
        }
        

        

        [HttpGet("GetCount")]
        public async Task<ActionResult<ItemCount>> GetCount()
        {
            ItemCount itemCount = new ItemCount();

            itemCount.Count = _context.DMPhongBans
                .Where(x => x.DeletedDate == null)
                .Count();
            return await Task.FromResult(itemCount);
        }

        // GET: api/Departments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DMPhongBan>> GetById(int id)
        {
            var item = await _context.DMPhongBans.Where(p => p.DMDonViSuDungId == _tenantProvider.TenantId).Where(p => p.Id == id).FirstOrDefaultAsync();

            if (item == null)
            {
                return NotFound();
            }

            return item;
        }

        // PUT: api/Departments/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("Update/{id}")]
        public async Task<ActionResult<DMPhongBan>> Put(int id, DMPhongBan item)
        {
            if (id != item.Id)
            {
                return new DMPhongBan();
            }

            item.UpdatedDate = DateTime.Now;
            _context.Entry(item).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();

                // Log Nhat ky
                await _nhatKyService.LogUpdate("DMPhongBan");
            }
            catch (DbUpdateConcurrencyException)
            {
                // Log Nhat ky
                await _nhatKyService.LogError("Update_DMPhongBan", "id : " + id + ";\nitem : " + item.ToString());
                if (!Exists(id))
                {
                    return new DMPhongBan();
                }
                else
                {
                    return new DMPhongBan();
                }
            }

            return item;
        }

        // POST: api/Customers
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost("Create")]
        public async Task<ActionResult<DMPhongBan>> Post(DMPhongBan item)
        {
            item.CreatedDate = DateTime.Now;
            item.UpdatedDate = DateTime.Now;
            item.DMDonViSuDungId = _tenantProvider.TenantId;
            _context.DMPhongBans.Add(item);
            await _context.SaveChangesAsync();

            // Log Nhat ky
            await _nhatKyService.LogCreate("DMPhongBan");
            return item;
        }

        // DELETE: api/Departments/delete/5
        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult<DMPhongBan>> Delete(int id)
        {
            var item = await _context.DMPhongBans.Where(p => p.DMDonViSuDungId == _tenantProvider.TenantId).Where(p => p.Id == id).FirstOrDefaultAsync();
            if (item == null)
            {
                return NotFound();
            }

            item.DeletedDate = DateTime.Now;
            await _context.SaveChangesAsync();

            // Log Nhat ky
            await _nhatKyService.LogDelete("DMPhongBan");
            return item;
        }

        private bool  Exists(int id)
        {
            return _context.DMPhongBans.Any(e => e.Id  == id);
        }

        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<DMPhongBan>>> Get(SearchRequest request)
        {
            return await _context.DMPhongBans
                .Where(p => p.DMDonViSuDungId == _tenantProvider.TenantId)
                .Where(x => x.DeletedDate == null)
                .ToListAsync();
        }
        [HttpGet("ExportToExcel")]
        public async Task<ActionResult<GetAllResponse<DMPhongBan>>> Get([FromBody] BaseSearchRequest request)
        {

            GetAllResponse<DMPhongBan> outputs = new GetAllResponse<DMPhongBan>();
            Expression<Func<DMPhongBan, bool>> filter = m => (_tenantProvider.TenantId == 0 || m.DMDonViSuDungId == _tenantProvider.TenantId);
            if (!string.IsNullOrEmpty(request.Keywords))
            {
                filter = filter.And(x => x.MaPhong.Contains(request.Keywords) || x.TenPhong.Contains(request.Keywords));
            }
            Func<IQueryable<DMPhongBan>, IOrderedQueryable<DMPhongBan>> order = null;
            if (request.SortDirection != QLSX.Shared.Enums.SortDirection.None)
            {
                order = await OrderBy(request.SortLable, request.SortDirection == QLSX.Shared.Enums.SortDirection.Ascending);
            }

            IQueryable<DMPhongBan> query = _context.Set<DMPhongBan>();
            
            if (filter != null) query = query.Where(filter).Where(x => x.DeletedDate == null);
            if (order != null) query = order(query);
            outputs.TotalRecords = await query.CountAsync();
            outputs.TotalPages = (int)Math.Ceiling(outputs.TotalRecords / (double)request.PageSize);
            outputs.Page = request.Page;
            outputs.PageSize = request.PageSize;

            //query = query.Skip(request.Page * request.PageSize).Take(request.PageSize);
            outputs.Items = await query.ToListAsync();
            return outputs;
            
        }
        [HttpGet("GetAllPaged")]
        public async Task<ActionResult<GetAllResponse<DMPhongBan>>> GetAllPaged([FromBody] BaseSearchRequest request)
        {

            GetAllResponse<DMPhongBan> outputs = new GetAllResponse<DMPhongBan>();
            Expression<Func<DMPhongBan, bool>> filter = m => (_tenantProvider.TenantId == 0 || m.DMDonViSuDungId == _tenantProvider.TenantId);
            if (!string.IsNullOrEmpty(request.Keywords))
            {
                filter = filter.And(x => x.MaPhong.Contains(request.Keywords) || x.TenPhong.Contains(request.Keywords));
            }
            Func<IQueryable<DMPhongBan>, IOrderedQueryable<DMPhongBan>> order = null;
            if (request.SortDirection != QLSX.Shared.Enums.SortDirection.None)
            {
                order = await OrderBy(request.SortLable, request.SortDirection == QLSX.Shared.Enums.SortDirection.Ascending);
            }

            IQueryable<DMPhongBan> query = _context.Set<DMPhongBan>();

            if (filter != null) query = query.Where(filter).Where(x => x.DeletedDate == null);
            if (order != null) query = order(query);
            outputs.TotalRecords = await query.CountAsync();
            outputs.TotalPages = (int)Math.Ceiling(outputs.TotalRecords / (double)request.PageSize);
            outputs.Page = request.Page;
            outputs.PageSize = request.PageSize;

            query = query.Skip(request.Page * request.PageSize).Take(request.PageSize);
            outputs.Items = await query.ToListAsync();
            return outputs;

        }
        private async Task<Func<IQueryable<DMPhongBan>, IOrderedQueryable<DMPhongBan>>> OrderBy(string sortBy, bool sortType)
        {
            Func<IQueryable<DMPhongBan>, IOrderedQueryable<DMPhongBan>> myFunc;
            if (sortBy == "MaPhong")
            {
                if (sortType) myFunc = source => source.OrderBy(x => x.MaPhong);
                else myFunc = source => source.OrderByDescending(x => x.MaPhong);
                return myFunc;
            }
            if (sortBy == "TenPhong")
            {
                if (sortType) myFunc = source => source.OrderBy(x => x.TenPhong);
                else myFunc = source => source.OrderByDescending(x => x.TenPhong);
                return myFunc;
            }
            return null;

        }
    }
}
