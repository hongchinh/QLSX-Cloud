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
    public class DMHinhThucTTsController : ControllerBase
    {
        private readonly CRMDBContext _context;
        private readonly ITenantProvider _tenantProvider;
        private readonly INhatKyService _nhatKyService;
        public DMHinhThucTTsController(CRMDBContext context, ITenantProvider tenantProvider, INhatKyService nhatKyService)
        {
            _context = context;
            _tenantProvider = tenantProvider;
            _nhatKyService = nhatKyService;
        }



        [HttpGet("GetCount")]
        public async Task<ActionResult<ItemCount>> GettCount()
        {
            ItemCount itemCount = new ItemCount();

            itemCount.Count = _context.DanhMucHinhThucTTRepository.Count();
            return await Task.FromResult(itemCount);
        }

        // GET: api/Customers
        [HttpGet("GetByPage")]
        public async Task<ActionResult<IEnumerable<DanhMucHinhThucTTModel>>> GetByPage(int pageSize, int pageNumber)
        {
            var list = await _context.DanhMucHinhThucTTRepository
                                     .Where(x => x.DeletedDate == null)
                                     .Skip(pageNumber * pageSize).Take(pageSize)
                                     .Select(item => new DanhMucHinhThucTTModel(item))
                                     .ToListAsync();
            return await Task.FromResult(list);
        }

        // GET: api/Customers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DanhMucHinhThucTTModel>> GetById(int id)
        {
            var item = await _context.DanhMucHinhThucTTRepository
                                     .FirstOrDefaultAsync(p => p.Id == id && p.DeletedDate == null);

            if (item == null)
            {
                return NotFound();
            }

            return new DanhMucHinhThucTTModel(item);
        }

        // PUT: api/Customers/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("Update/{id}")]
        public async Task<ActionResult<DanhMucHinhThucTT>> Put(int id, DanhMucHinhThucTT item)
        {
            if (id != item.Id)
            {
                return new DanhMucHinhThucTT();
            }

            //item.UpdatedDate = DateTime.Now;
            _context.Entry(item).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();

                // Log Nhat ky
                await _nhatKyService.LogUpdate("DanhMucHinhThucTT");
            }
            catch (DbUpdateConcurrencyException)
            {
                // Log Nhat ky
                await _nhatKyService.LogError("Update_DanhMucHinhThucTT", "id : " + id + ";\nitem : " + item.ToString());
                if (!Exists(id))
                {
                    return new DanhMucHinhThucTT();
                }
                else
                {
                    return new DanhMucHinhThucTT();
                }
            }

            return item;
        }

        // POST: api/Customers
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost("Create")]
        public async Task<ActionResult<DanhMucHinhThucTT>> Post(DanhMucHinhThucTT item)
        {
            //item.CreatedDate = DateTime.Now;
            //item.UpdatedDate = DateTime.Now;
            //item.DMDonViSuDungId = _tenantProvider.TenantId;
            _context.DanhMucHinhThucTTRepository.Add(item);
            await _context.SaveChangesAsync();

            // Log Nhat ky
            await _nhatKyService.LogCreate("DanhMucHinhThucTT");
            return item;
        }

        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult<DanhMucHinhThucTT>> Delete(int id)
        {
            var item = await _context.DanhMucHinhThucTTRepository
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
            await _nhatKyService.LogDelete("DanhMucHinhThucTT");
            return item;
        }

        private bool Exists(int id)
        {
            return _context.DanhMucHinhThucTTRepository.Any(e => e.Id == id);
        }


        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<DanhMucHinhThucTTModel>>> Get(SearchRequest request)
        {
            return await _context.DanhMucHinhThucTTRepository
                .Where(item => item.DeletedDate == null)
                .Select(item => new DanhMucHinhThucTTModel(item))
                .ToListAsync();
        }
        [HttpGet("ExportToExcel")]
        public async Task<ActionResult<GetAllResponse<DanhMucHinhThucTT>>> ExportToExcel([FromBody] BaseSearchRequest request)
        {
            GetAllResponse<DanhMucHinhThucTT> outputs = new GetAllResponse<DanhMucHinhThucTT>();
            // Expression<Func<DanhMucHinhThucTT, bool>> filter = m => (_tenantProvider.TenantId == 0 || m.DMDonViSuDungId == _tenantProvider.TenantId);
            // if (!string.IsNullOrEmpty(request.Keywords))
            // {
            //     filter = filter.And(x => x.TenHinhThuc.Contains(request.Keywords));
            // }
            // Func<IQueryable<DanhMucHinhThucTT>, IOrderedQueryable<DanhMucHinhThucTT>> order = null;
            // if (request.SortDirection != QLSX.Shared.Enums.SortDirection.None)
            // {
            //     order = await OrderBy(request.SortLable, request.SortDirection == QLSX.Shared.Enums.SortDirection.Ascending);
            // }

            // IQueryable<DanhMucHinhThucTT> query = _context.Set<DanhMucHinhThucTT>();

            // if (filter != null) query = query.Where(filter).Where(x => x.DeletedDate == null);
            // if (order != null) query = order(query);
            // outputs.TotalRecords = await query.CountAsync();
            // outputs.TotalPages = (int)Math.Ceiling(outputs.TotalRecords / (double)request.PageSize);
            // outputs.Page = request.Page;
            // outputs.PageSize = request.PageSize;

            //// query = query.Skip(request.Page * request.PageSize).Take(request.PageSize);
            // outputs.Items = await query.ToListAsync();

            // Log Nhat ky
            await _nhatKyService.LogExportExcel("DanhMucHinhThucTT");
            return outputs;

        }
        [HttpGet("GetAllPaged")]
        public async Task<ActionResult<GetAllResponse<DanhMucHinhThucTT>>> GetAllPaged([FromBody] BaseSearchRequest request)
        {
            GetAllResponse<DanhMucHinhThucTT> outputs = new GetAllResponse<DanhMucHinhThucTT>();
            //Expression<Func<DanhMucHinhThucTT, bool>> filter = m => (_tenantProvider.TenantId == 0 || m.DMDonViSuDungId == _tenantProvider.TenantId);
            //if (!string.IsNullOrEmpty(request.Keywords))
            //{
            //    filter = filter.And(x => x.TenHinhThuc.Contains(request.Keywords));
            //}
            //Func<IQueryable<DanhMucHinhThucTT>, IOrderedQueryable<DanhMucHinhThucTT>> order = null;
            //if (request.SortDirection != QLSX.Shared.Enums.SortDirection.None)
            //{
            //    order = await OrderBy(request.SortLable, request.SortDirection == QLSX.Shared.Enums.SortDirection.Ascending);
            //}

            //IQueryable<DanhMucHinhThucTT> query = _context.Set<DanhMucHinhThucTT>();

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

        private async Task<Func<IQueryable<DanhMucHinhThucTT>, IOrderedQueryable<DanhMucHinhThucTT>>> OrderBy(string sortBy, bool sortType)
        {
            Func<IQueryable<DanhMucHinhThucTT>, IOrderedQueryable<DanhMucHinhThucTT>> myFunc;
            //if (sortBy == "TenHinhThuc")
            //{
            //    if (sortType) myFunc = source => source.OrderBy(x => x.TenHinhThuc);
            //    else myFunc = source => source.OrderByDescending(x => x.TenHinhThuc);
            //    return myFunc;
            //}

            return null;

        }
    }
}
