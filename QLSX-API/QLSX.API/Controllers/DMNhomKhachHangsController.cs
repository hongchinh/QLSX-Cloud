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
    public class DMNhomKhachHangsController : ControllerBase
    {
        private readonly CRMDBContext _context;
        private readonly ITenantProvider _tenantProvider;
        private readonly INhatKyService _nhatKyService;
        public DMNhomKhachHangsController(CRMDBContext context, ITenantProvider tenantProvider, INhatKyService nhatKyService)
        {
            _context = context;
            _tenantProvider = tenantProvider;
            _nhatKyService = nhatKyService;
        }

        [HttpGet("GetCount")]
        public async Task<ActionResult<ItemCount>> GettCount()
        {
            ItemCount itemCount = new ItemCount();

            itemCount.Count = _context.DanhMucNhomKhachHangRepository
                .Where(x => x.DeletedDate == null)
                .Count();
            return await Task.FromResult(itemCount);
        }


        // GET: api/Customers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DanhMucNhomKhachHangModel>> GetById(int id)
        {
            var item = await _context.DanhMucNhomKhachHangRepository.FirstOrDefaultAsync(p => p.Id == id && p.DeletedDate==null);

            if (item == null)
            {
                return NotFound();
            }

            return new DanhMucNhomKhachHangModel(item);
        }

        [HttpGet("getCode/{code}")]
        public async Task<ActionResult<DanhMucNhomKhachHangModel>> GetByCode(string code)
        {
            if (string.IsNullOrEmpty(code))
            {
                return NotFound();
            }
            var item = await _context.DanhMucNhomKhachHangRepository.FirstOrDefaultAsync(p => p.MaNhom.ToLower() == code.ToLower() && p.DeletedDate == null);

            if (item == null)
            {
                return NotFound();
            }
            return new DanhMucNhomKhachHangModel(item);
        }

        // PUT: api/Customers/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("Update/{id}")]
        public async Task<ActionResult<DanhMucNhomKhachHangModel>> Put(int id, DanhMucNhomKhachHangModel model)
        {
            DanhMucNhomKhachHang entity = await _context.DanhMucNhomKhachHangRepository.FirstOrDefaultAsync(item => item.DeletedDate == null && item.Id == model.Id);
            if (entity == null)
            {
                return new DanhMucNhomKhachHangModel();
            }

            entity.MaNhom = model.MaNhom;
            entity.TenNhom = model.TenNhom;
            entity.GhiChu = model.GhiChu;
            entity.Selected = model.Selected;
            entity.UpdatedDate = DateTime.Now;

            try
            {
                await _context.SaveChangesAsync();

                // Log Nhat ky
                await _nhatKyService.LogUpdate("DMNhomKhachHang");
            }
            catch (DbUpdateConcurrencyException)
            {
                // Log Nhat ky
                await _nhatKyService.LogError("Update_DMNhomKhachHang", "id : " + id + ";\nitem : " + model.ToString());
                if (!Exists(id))
                {
                    return new DanhMucNhomKhachHangModel();
                }
                else
                {
                    return new DanhMucNhomKhachHangModel();
                }
            }

            return model;
        }


        // POST: api/Customers
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost("Create")]
        public async Task<ActionResult<DanhMucNhomKhachHangModel>> Post(DanhMucNhomKhachHangModel model)
        {
            DanhMucNhomKhachHang entity = new();
            entity.Id = 0;
            entity.MaNhom = model.MaNhom;
            entity.TenNhom = model.TenNhom;
            entity.GhiChu = model.GhiChu;
            entity.Selected = model.Selected;
            entity.CreatedDate = DateTime.Now;
            entity.UpdatedDate = DateTime.Now;
            _context.DanhMucNhomKhachHangRepository.Add(entity);
            await _context.SaveChangesAsync();

            // Log Nhat ky
            await _nhatKyService.LogCreate("DMNhomKhachHang");
            return new DanhMucNhomKhachHangModel(entity);
        }

        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult<DanhMucNhomKhachHangModel>> Delete(int id)
        {
            var item = await _context.DanhMucNhomKhachHangRepository.FirstOrDefaultAsync(p => p.Id == id);
            if (item == null)
            {
                return NotFound();
            }

            item.DeletedDate = DateTime.Now;
            await _context.SaveChangesAsync();

            // Log Nhat ky
            await _nhatKyService.LogDelete("DMNhomKhachHang");
            return new DanhMucNhomKhachHangModel(item);
        }

        private bool Exists(int id)
        {
            return _context.DanhMucNhomKhachHangRepository.Any(e => e.Id == id);
        }

        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<DanhMucNhomKhachHangModel>>> Get(SearchRequest request)
        {
            return await _context.DanhMucNhomKhachHangRepository
                .Where(x => x.DeletedDate == null)
                .Select(item => new DanhMucNhomKhachHangModel(item))
                .ToListAsync();
        }

        [HttpGet("ExportToExcel")]
        public async Task<ActionResult<GetAllResponse<DanhMucNhomKhachHangModel>>> Get([FromBody] BaseSearchRequest request)
        {
            GetAllResponse<DanhMucNhomKhachHangModel> outputs = await GetData(request, false);

            // Log Nhat ky
            await _nhatKyService.LogExportExcel("DMNhomKhachHang");
            return outputs;
        }

        [HttpGet("GetAllPaged")]
        public async Task<ActionResult<GetAllResponse<DanhMucNhomKhachHangModel>>> GetAllPaged([FromBody] BaseSearchRequest request)
        {
            GetAllResponse<DanhMucNhomKhachHangModel> outputs = await GetData(request, true);
            return outputs;
        }

        private async Task<GetAllResponse<DanhMucNhomKhachHangModel>> GetData(BaseSearchRequest request, bool isPaging)
        {
            GetAllResponse<DanhMucNhomKhachHangModel> outputs = new GetAllResponse<DanhMucNhomKhachHangModel>();
            Expression<Func<DanhMucNhomKhachHang, bool>> filter = m => (1 == 1);
            if (!string.IsNullOrEmpty(request.Keywords))
            {
                filter = filter.And(x => x.MaNhom.Contains(request.Keywords) || x.TenNhom.Contains(request.Keywords));
            }
            Func<IQueryable<DanhMucNhomKhachHang>, IOrderedQueryable<DanhMucNhomKhachHang>> order = null;
            if (request.SortDirection != QLSX.Shared.Enums.SortDirection.None)
            {
                order = await OrderBy(request.SortLable, request.SortDirection == QLSX.Shared.Enums.SortDirection.Ascending);
            }

            IQueryable<DanhMucNhomKhachHang> query = _context.DanhMucNhomKhachHangRepository.Where(item => item.DeletedDate == null);

            if (filter != null) query = query.Where(filter);
            if (order != null) query = order(query);
            outputs.TotalRecords = await query.CountAsync();
            outputs.TotalPages = (int)Math.Ceiling(outputs.TotalRecords / (double)request.PageSize);
            outputs.Page = request.Page;
            outputs.PageSize = request.PageSize;

            if (isPaging)
            {
                query = query.Skip(request.Page * request.PageSize).Take(request.PageSize);
            }
            outputs.Items = await query.Select(item => new DanhMucNhomKhachHangModel(item)).ToListAsync();
            return outputs;
        }

        private async Task<Func<IQueryable<DanhMucNhomKhachHang>, IOrderedQueryable<DanhMucNhomKhachHang>>> OrderBy(string sortBy, bool sortType)
        {
            Func<IQueryable<DanhMucNhomKhachHang>, IOrderedQueryable<DanhMucNhomKhachHang>> myFunc;
            if (sortBy == "MaNhom")
            {
                if (sortType) myFunc = source => source.OrderBy(x => x.MaNhom);
                else myFunc = source => source.OrderByDescending(x => x.MaNhom);
                return myFunc;
            }
            if (sortBy == "TenNhom")
            {
                if (sortType) myFunc = source => source.OrderBy(x => x.TenNhom);
                else myFunc = source => source.OrderByDescending(x => x.TenNhom);
                return myFunc;
            }
            return null;
        }
    }
}
