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
    public class DMLoaiTiensController : ControllerBase
    {
        private readonly CRMDBContext _context;
        private readonly ITenantProvider _tenantProvider;
        private readonly INhatKyService _nhatKyService;
        public DMLoaiTiensController(CRMDBContext context, ITenantProvider tenantProvider, INhatKyService nhatKyService)
        {
            _context = context;
            _tenantProvider = tenantProvider;
            _nhatKyService = nhatKyService;
        }



        [HttpGet("GetCount")]
        public async Task<ActionResult<ItemCount>> GettCount()
        {
            ItemCount itemCount = new ItemCount();

            itemCount.Count = _context.DanhMucLoaiTienRepository
                .Where(x => x.DeletedDate == null)
                .Count();
            return await Task.FromResult(itemCount);
        }

        // GET: api/Customers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DanhMucLoaiTienModel>> GetById(int id)
        {
            var item = await _context.DanhMucLoaiTienRepository
                //.Where(p => p.DMDonViSuDungId == _tenantProvider.TenantId)
                .FirstOrDefaultAsync(p => p.Id == id && p.DeletedDate == null);

            if (item == null)
            {
                return NotFound();
            }

            return new DanhMucLoaiTienModel(item);
        }

        // GET: api/Customers/5
        [HttpGet("code/{code}")]
        public async Task<ActionResult<DanhMucLoaiTienModel>> GetById(string code)
        {
            var item = await _context.DanhMucLoaiTienRepository.FirstOrDefaultAsync(p => p.LoaiTien.ToLower() == code.ToLower() && p.DeletedDate == null);

            if (item == null)
            {
                return NotFound();
            }

            return new DanhMucLoaiTienModel(item);
        }

        // PUT: api/Customers/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("Update/{id}")]
        public async Task<ActionResult<DanhMucLoaiTienModel>> Put(int id, DanhMucLoaiTienModel model)
        {
            var entity = await _context.DanhMucLoaiTienRepository.FirstOrDefaultAsync(item => item.DeletedDate == null && item.Id == model.Id);
            if (entity == null)
            {
                return new DanhMucLoaiTienModel();
            }
            entity.KyHieu = model.KyHieu;
            entity.LoaiTien = model.LoaiTien;
            entity.UpdatedDate = DateTime.Now;

            try
            {
                await _context.SaveChangesAsync();

                // Log Nhat ky
                await _nhatKyService.LogUpdate("DanhMucLoaiTien");
            }
            catch (DbUpdateConcurrencyException)
            {
                // Log Nhat ky
                await _nhatKyService.LogError("Update_DanhMucLoaiTien", "id : " + id + ";\nitem : " + model.ToString());
                if (!Exists(id))
                {
                    return new DanhMucLoaiTienModel();
                }
                else
                {
                    return new DanhMucLoaiTienModel();
                }
            }

            return model;
        }

        // POST: api/Customers
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost("Create")]
        public async Task<ActionResult<DanhMucLoaiTienModel>> Post(DanhMucLoaiTienModel model)
        {
            DanhMucLoaiTien entity = new();
            entity.Id = 0;
            entity.KyHieu = model.KyHieu;
            entity.LoaiTien = model.LoaiTien;
            entity.CreatedDate = DateTime.Now;
            entity.UpdatedDate = DateTime.Now;
            //item.DMDonViSuDungId = _tenantProvider.TenantId;
            _context.DanhMucLoaiTienRepository.Add(entity);
            await _context.SaveChangesAsync();

            // Log Nhat ky
            await _nhatKyService.LogCreate("DanhMucLoaiTien");
            return new DanhMucLoaiTienModel(entity);
        }

        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult<DanhMucLoaiTien>> Delete(int id)
        {
            var item = await _context.DanhMucLoaiTienRepository.FirstOrDefaultAsync(p => p.Id == id && p.DeletedDate == null);
            if (item == null)
            {
                return NotFound();
            }

            item.DeletedDate = DateTime.Now;
            await _context.SaveChangesAsync();

            // Log Nhat ky
            await _nhatKyService.LogDelete("DanhMucLoaiTien");
            return item;
        }

        private bool Exists(int id)
        {
            return _context.DanhMucKhoanChiRepository.Any(e => e.Id == id);
        }

        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<DanhMucLoaiTienModel>>> Get(SearchRequest request)
        {
            try
            {
                var result = await _context.DanhMucLoaiTienRepository
                                       //.Where(p => p.DMDonViSuDungId == _tenantProvider.TenantId)
                                       .Where(x => x.DeletedDate == null)
                                       .Select(item => new DanhMucLoaiTienModel(item))
                                       .ToListAsync();
                return result;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        [HttpGet("ExportToExcel")]
        public async Task<ActionResult<GetAllResponse<DanhMucLoaiTienModel>>> ExportToExcel([FromBody] BaseSearchRequest request)
        {
            GetAllResponse<DanhMucLoaiTienModel> outputs = await GetData(request, false);

            // Log Nhat ky
            await _nhatKyService.LogExportExcel("DanhMucLoaiTien");
            return outputs;

        }
        [HttpGet("GetAllPaged")]
        public async Task<ActionResult<GetAllResponse<DanhMucLoaiTienModel>>> GetAllPaged([FromBody] BaseSearchRequest request)
        {
            GetAllResponse<DanhMucLoaiTienModel> outputs = await GetData(request, true);
            return outputs;
        }

        private async Task<GetAllResponse<DanhMucLoaiTienModel>> GetData(BaseSearchRequest request, bool isPaging)
        {
            GetAllResponse<DanhMucLoaiTienModel> outputs = new GetAllResponse<DanhMucLoaiTienModel>();
            IQueryable<DanhMucLoaiTien> query = _context.DanhMucLoaiTienRepository.Where(item => item.DeletedDate == null);
            if (!string.IsNullOrEmpty(request.Keywords))
            {
                query = query.Where(x => x.LoaiTien.Contains(request.Keywords) || x.KyHieu.Contains(request.Keywords));
            }
            Func<IQueryable<DanhMucLoaiTien>, IOrderedQueryable<DanhMucLoaiTien>> order = null;
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
            outputs.Items = rawData.Select(item => new DanhMucLoaiTienModel(item)).ToList();
            return outputs;
        }

        private async Task<Func<IQueryable<DanhMucLoaiTien>, IOrderedQueryable<DanhMucLoaiTien>>> OrderBy(string sortBy, bool sortType)
        {
            Func<IQueryable<DanhMucLoaiTien>, IOrderedQueryable<DanhMucLoaiTien>> myFunc;
            if (sortBy == "LoaiTien")
            {
                if (sortType) myFunc = source => source.OrderBy(x => x.LoaiTien);
                else myFunc = source => source.OrderByDescending(x => x.LoaiTien);
                return myFunc;
            }
            if (sortBy == "KyHieu")
            {
                if (sortType) myFunc = source => source.OrderBy(x => x.KyHieu);
                else myFunc = source => source.OrderByDescending(x => x.KyHieu);
                return myFunc;
            }
            //if (sortBy == "GhiChu")
            //{
            //    if (sortType) myFunc = source => source.OrderBy(x => x.GhiChu);
            //    else myFunc = source => source.OrderByDescending(x => x.GhiChu);
            //    return myFunc;
            //}
            return null;

        }
    }
}
