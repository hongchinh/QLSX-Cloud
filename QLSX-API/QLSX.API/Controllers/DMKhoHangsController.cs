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

namespace SaleAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DMKhoHangsController : ControllerBase
    {
        private readonly CRMDBContext _context;
        private readonly ITenantProvider _tenantProvider;
        private readonly INhatKyService _nhatKyService;
        public DMKhoHangsController(CRMDBContext context, ITenantProvider tenantProvider, INhatKyService nhatKyService)
        {
            _context = context;
            _tenantProvider = tenantProvider;
            _nhatKyService = nhatKyService;
        }

        [HttpGet("GetCount")]
        public async Task<ActionResult<ItemCount>> GettCount()
        {
            ItemCount itemCount = new ItemCount();

            itemCount.Count = _context.DanhMucKhoHangRepository.Count();
            return await Task.FromResult(itemCount);
        }

        // GET: api/Customers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DanhMucKhoHangModel>> GetById(int id)
        {
            var item = await _context.DanhMucKhoHangRepository
                //.Where(p => p.DMDonViSuDungId == _tenantProvider.TenantId)
                .Where(p => p.Id == id && p.DeletedDate == null)
                .Select(item => new DanhMucKhoHangModel(item))
                .FirstOrDefaultAsync();

            if (item == null)
            {
                return NotFound();
            }

            return item;
        }

        [HttpGet("getCode/{code}")]
        public async Task<ActionResult<DanhMucKhoHangModel>> GetByCode(string code)
        {
            var item = await _context.DanhMucKhoHangRepository.FirstOrDefaultAsync(p => p.MaKho == code && p.DeletedDate == null);

            if (item == null)
            {
                return NotFound();
            }

            return new DanhMucKhoHangModel(item);
        }

        // PUT: api/Customers/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("Update/{id}")]
        public async Task<ActionResult<DanhMucKhoHangModel>> Put(int id, DanhMucKhoHangModel model)
        {
            var entity = await _context.DanhMucKhoHangRepository.FirstOrDefaultAsync(item => item.Id == model.Id);
            if (entity == null)
            {
                return new DanhMucKhoHangModel();
            }
            entity.TenKho = model.TenKho;
            entity.DiaChi = model.DiaChi;
            entity.UpdatedDate = DateTime.Now;
            try
            {
                await _context.SaveChangesAsync();

                // Log Nhat ky
                await _nhatKyService.LogUpdate("DanhMucKhoHangModel");
            }
            catch (DbUpdateConcurrencyException)
            {
                // Log Nhat ky
                await _nhatKyService.LogError("Update_DMKhoHang", "id : " + id + ";\nitem : " + model.ToString());
                if (!Exists(id))
                {
                    return new DanhMucKhoHangModel();
                }
                else
                {
                    return new DanhMucKhoHangModel();
                }
            }

            return model;
        }
        // POST: api/Customers
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost("Create")]
        public async Task<ActionResult<DanhMucKhoHang>> Post(DanhMucKhoHangModel model)
        {
            //model.DMDonViSuDungId = _tenantProvider.TenantId;
            var entity = new DanhMucKhoHang()
            {
                MaKho = model.MaKho,
                TenKho = model.TenKho,
                DiaChi = model.DiaChi,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
            };
            _context.DanhMucKhoHangRepository.Add(entity);
            await _context.SaveChangesAsync();

            // Log Nhat ky
            await _nhatKyService.LogCreate("DanhMucKhoHang");
            return entity;
        }

        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult<DanhMucKhoHang>> Delete(int id)
        {
            var item = await _context.DanhMucKhoHangRepository
                       //.Where(p => p.DMDonViSuDungId == _tenantProvider.TenantId)
                       .FirstOrDefaultAsync(p => p.Id == id);
            if (item == null)
            {
                return NotFound();
            }

            item.DeletedDate = DateTime.Now;
            await _context.SaveChangesAsync();

            // Log Nhat ky
            await _nhatKyService.LogDelete("DanhMucKhoHangModel");
            return item;
        }

        private bool Exists(int id)
        {
            return _context.DanhMucKhoHangRepository.Any(e => e.Id == id);
        }


        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<DanhMucKhoHangModel>>> Get(SearchRequest request)
        {
            return await _context.DanhMucKhoHangRepository
                         //.Where(p => p.DMDonViSuDungId == _tenantProvider.TenantId)
                         .Where(item => item.DeletedDate == null)
                         .Select(item => new DanhMucKhoHangModel(item))
                         .ToListAsync();
        }

        [HttpGet("ExportToExcel")]
        public async Task<ActionResult<GetAllResponse<DanhMucKhoHangModel>>> ExportToExcel([FromBody] BaseSearchRequest request)
        {

            GetAllResponse<DanhMucKhoHangModel> outputs = new GetAllResponse<DanhMucKhoHangModel>();
            //Expression<Func<DanhMucKhoHangModel, bool>> filter = m => (_tenantProvider.TenantId == 0 || m.DMDonViSuDungId == _tenantProvider.TenantId);
            //if (!string.IsNullOrEmpty(request.Keywords))
            //{
            //    filter = filter.And(model => model.MaKho.Contains(request.Keywords) || model.TenKho.Contains(request.Keywords));
            //}
            //Func<IQueryable<DanhMucKhoHangModel>, IOrderedQueryable<DanhMucKhoHangModel>> order = null;
            //if (request.SortDirection != QLSX.Shared.Enums.SortDirection.None)
            //{
            //    order = await OrderBy(request.SortLable, request.SortDirection == QLSX.Shared.Enums.SortDirection.Ascending);
            //}

            //IQueryable<DanhMucKhoHangModel> query = _context.Set<DanhMucKhoHangModel>();

            //if (filter != null) query = query.Where(filter).Where(model => model.DeletedDate == null);
            //if (order != null) query = order(query);
            //outputs.TotalRecords = await query.CountAsync();
            //outputs.TotalPages = (int)Math.Ceiling(outputs.TotalRecords / (double)request.PageSize);
            //outputs.Page = request.Page;
            //outputs.PageSize = request.PageSize;

            ////query = query.Skip(request.Page * request.PageSize).Take(request.PageSize);
            //outputs.Items = await query.ToListAsync();

            //// Log Nhat ky
            //await _nhatKyService.LogExportExcel("DanhMucKhoHangModel");
            return outputs;

        }
        [HttpGet("GetAllPaged")]
        public async Task<ActionResult<GetAllResponse<DanhMucKhoHangModel>>> GetAllPaged([FromBody] BaseSearchRequest request)
        {

            GetAllResponse<DanhMucKhoHangModel> outputs = new GetAllResponse<DanhMucKhoHangModel>();
            IQueryable<DanhMucKhoHang> query = _context.DanhMucKhoHangRepository.Where(item => item.DeletedDate == null);
            if (!string.IsNullOrEmpty(request.Keywords))
            {
                query = query.Where(item => item.MaKho.Contains(request.Keywords) || item.TenKho.Contains(request.Keywords));
            }
            Func<IQueryable<DanhMucKhoHang>, IOrderedQueryable<DanhMucKhoHang>> order = null;
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
            outputs.Items = rawData.Select(item => new DanhMucKhoHangModel(item)).ToList();
            return outputs;

        }
        private async Task<Func<IQueryable<DanhMucKhoHang>, IOrderedQueryable<DanhMucKhoHang>>> OrderBy(string sortBy, bool sortType)
        {
            Func<IQueryable<DanhMucKhoHang>, IOrderedQueryable<DanhMucKhoHang>> myFunc;
            if (sortBy == "MaKho")
            {
                if (sortType) myFunc = source => source.OrderBy(x => x.MaKho);
                else myFunc = source => source.OrderByDescending(x => x.MaKho);
                return myFunc;
            }
            if (sortBy == "TenKho")
            {
                if (sortType) myFunc = source => source.OrderBy(x => x.TenKho);
                else myFunc = source => source.OrderByDescending(x => x.TenKho);
                return myFunc;
            }
            return null;

        }

    }
}
