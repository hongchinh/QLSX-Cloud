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
    public class DMChungLoaisController : ControllerBase
    {
        private readonly CRMDBContext _context;
        private readonly ITenantProvider _tenantProvider;
        private readonly INhatKyService _nhatKyService;
        public DMChungLoaisController(CRMDBContext context, ITenantProvider tenantProvider, INhatKyService nhatKyService)
        {
            _context = context;
            _tenantProvider = tenantProvider;
            _nhatKyService = nhatKyService;
        }


        [HttpGet("GetCount")]
        public async Task<ActionResult<ItemCount>> GettCount()
        {
            ItemCount itemCount = new ItemCount();

            itemCount.Count = _context.
                DanhMucChungLoaiRepository
                //.Where(p => p.DMDonViSuDungId == _tenantProvider.TenantId)
                .Count();
            return await Task.FromResult(itemCount);
        }

        // GET: api/Customers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DanhMucChungLoai>> GetById(int id)
        {
            var item = _context.DanhMucChungLoaiRepository
                    //.Where(p => p.DMDonViSuDungId == _tenantProvider.TenantId)
                    .Where(p => p.Id == id).FirstOrDefault(); ;

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
        public async Task<ActionResult<DanhMucChungLoai>> Put(int id, DanhMucChungLoai item)
        {
            if (id != item.Id)
            {
                return new DanhMucChungLoai();
            }

            //item.UpdatedDate = DateTime.Now;
            _context.Entry(item).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();

                // Log Nhat ky
                await _nhatKyService.LogUpdate("DanhMucChungLoaiModel");
            }
            catch (DbUpdateConcurrencyException)
            {
                // Log Nhat ky
                await _nhatKyService.LogError("Update_DanhMucChungLoai", "id : " + id + ";\nitem : " + item.ToString());
                if (!Exists(id))
                {
                    return new DanhMucChungLoai();
                }
                else
                {
                    return new DanhMucChungLoai();
                }
            }

            return item;
        }

        // POST: api/Customers
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost("Create")]
        public async Task<ActionResult<DanhMucChungLoai>> Post(DanhMucChungLoai item)
        {
            //item.CreatedDate = DateTime.Now;
            //item.UpdatedDate = DateTime.Now;
            //item.DMDonViSuDungId = _tenantProvider.TenantId;
            _context.DanhMucChungLoaiRepository.Add(item);
            await _context.SaveChangesAsync();

            // Log Nhat ky
            await _nhatKyService.LogCreate("DanhMucChungLoaiModel");
            return item;
        }

        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult<DanhMucChungLoai>> Delete(int id)
        {
            var item = _context.DanhMucChungLoaiRepository
                //.Where(p => p.DMDonViSuDungId == _tenantProvider.TenantId)
                .FirstOrDefault(p => p.Id == id);
            if (item == null)
            {
                return NotFound();
            }

            item.DeletedDate = DateTime.Now;
            await _context.SaveChangesAsync();

            // Log Nhat ky
            await _nhatKyService.LogDelete("DanhMucChungLoaiModel");
            return item;
        }

        private bool Exists(int id)
        {
            return _context.DanhMucChungLoaiRepository
                //.Where(p => p.DMDonViSuDungId == _tenantProvider.TenantId)
                .Any(e => e.Id == id);
        }

        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<DanhMucChungLoaiModel>>> Get(SearchRequest request)
        {
            var rawData = await _context.DanhMucChungLoaiRepository
                //.Where(p => p.DMDonViSuDungId == _tenantProvider.TenantId)
                //.Where(x => x.DeletedDate == null)
                .ToListAsync();
            return rawData.Select(item => new DanhMucChungLoaiModel(item)).ToList();
        }

        [HttpGet("ExportToExcel")]
        public async Task<ActionResult<GetAllResponse<DanhMucChungLoai>>> ExportToExcel([FromBody] BaseSearchRequest request)
        {
            GetAllResponse<DanhMucChungLoai> outputs = new GetAllResponse<DanhMucChungLoai>();
            //Expression<Func<DanhMucChungLoaiModel, bool>> filter = m => (_tenantProvider.TenantId == 0 || m.DMDonViSuDungId == _tenantProvider.TenantId);
            //if (!string.IsNullOrEmpty(request.Keywords))
            //{
            //    filter = filter.And(x => x.ChiTieu.Contains(request.Keywords) || x.MaSo.Contains(request.Keywords));
            //}
            //Func<IQueryable<DanhMucChungLoaiModel>, IOrderedQueryable<DanhMucChungLoaiModel>> order = null;
            //if (request.SortDirection != QLSX.Shared.Enums.SortDirection.None)
            //{
            //    order = await OrderBy(request.SortLable, request.SortDirection == QLSX.Shared.Enums.SortDirection.Ascending);
            //}

            //IQueryable<DanhMucChungLoaiModel> query = _context.Set<DanhMucChungLoaiModel>();

            //if (filter != null) query = query.Where(filter).Where(x => x.DeletedDate == null);
            //if (order != null) query = order(query);
            //outputs.TotalRecords = await query.CountAsync();
            //outputs.TotalPages = (int)Math.Ceiling(outputs.TotalRecords / (double)request.PageSize);
            //outputs.Page = request.Page;
            //outputs.PageSize = request.PageSize;

            //outputs.Items = await query.ToListAsync();

            // Log Nhat ky
            await _nhatKyService.LogExportExcel("DanhMucChungLoaiModel");
            return outputs;

        }
        [HttpGet("GetAllPaged")]
        public async Task<ActionResult<GetAllResponse<DanhMucChungLoaiModel>>> GetAllPaged([FromBody] BaseSearchRequest request)
        {
            GetAllResponse<DanhMucChungLoaiModel> outputs = new GetAllResponse<DanhMucChungLoaiModel>();
            IQueryable<DanhMucChungLoai> query = _context.Set<DanhMucChungLoai>();
            if (!string.IsNullOrEmpty(request.Keywords))
            {
                query = query.Where(x => x.ChiTieu.Contains(request.Keywords) || x.MaSo.Contains(request.Keywords));
            }
            Func<IQueryable<DanhMucChungLoai>, IOrderedQueryable<DanhMucChungLoai>> order = null;
            if (request.SortDirection != QLSX.Shared.Enums.SortDirection.None)
            {
                order = await OrderBy(request.SortLable, request.SortDirection == QLSX.Shared.Enums.SortDirection.Ascending);
            }

            //if (filter != null) query = query.Where(filter).Where(x => x.DeletedDate == null);
            if (order != null) query = order(query);
            outputs.TotalRecords = await query.CountAsync();
            outputs.TotalPages = (int)Math.Ceiling(outputs.TotalRecords / (double)request.PageSize);
            outputs.Page = request.Page;
            outputs.PageSize = request.PageSize;

            query = query.Skip(request.Page * request.PageSize).Take(request.PageSize);
            var rawData = await query.ToListAsync();
            outputs.Items = rawData.Select(item => new DanhMucChungLoaiModel(item)).ToList();
            return outputs;

        }

        private async Task<Func<IQueryable<DanhMucChungLoai>, IOrderedQueryable<DanhMucChungLoai>>> OrderBy(string sortBy, bool sortType)
        {
            Func<IQueryable<DanhMucChungLoai>, IOrderedQueryable<DanhMucChungLoai>> myFunc;
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
