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
    public class DMMauSacsController : ControllerBase
    {
        private readonly CRMDBContext _context;
        private readonly ITenantProvider _tenantProvider;
        private readonly INhatKyService _nhatKyService;
        public DMMauSacsController(CRMDBContext context, ITenantProvider tenantProvider, INhatKyService nhatKyService)
        {
            _context = context;
            _tenantProvider = tenantProvider;
            _nhatKyService = nhatKyService;
        }


        [HttpGet("GetCount")]
        public async Task<ActionResult<ItemCount>> GettCount()
        {
            ItemCount itemCount = new ItemCount();

            itemCount.Count = _context.DanhMucMauSacRepository.Count();
            return await Task.FromResult(itemCount);
        }



        // GET: api/Customers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DanhMucMauSac>> GetById(int id)
        {
            var item = await _context.DanhMucMauSacRepository
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
        public async Task<ActionResult<DanhMucMauSac>> Put(int id, DanhMucMauSacModel model)
        {
            if (id != model.Id)
            {
                return new DanhMucMauSac();
            }

            //item.UpdatedDate = DateTime.Now;
            var entity = _context.DanhMucMauSacRepository.FirstOrDefault(item => item.Id == id);
            entity.MaSo = model.MaSo;
            entity.ChiTieu = model.ChiTieu;
            try
            {
                await _context.SaveChangesAsync();

                // Log Nhat ky
                await _nhatKyService.LogUpdate("DanhMucMauSac");
            }
            catch (DbUpdateConcurrencyException)
            {
                // Log Nhat ky
                await _nhatKyService.LogError("Update_DanhMucMauSac", "id : " + id + ";\nitem : " + model.ToString());
                if (!Exists(id))
                {
                    return new DanhMucMauSac();
                }
                else
                {
                    return new DanhMucMauSac();
                }
            }

            return entity;
        }

        // POST: api/Customers
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost("Create")]
        public async Task<ActionResult<DanhMucMauSac>> Post(DanhMucMauSacModel model)
        {
            //item.CreatedDate = DateTime.Now;
            //item.UpdatedDate = DateTime.Now;
            //item.DMDonViSuDungId = _tenantProvider.TenantId;
            var entity = new DanhMucMauSac()
            {
                MaSo = model.MaSo,
                ChiTieu = model.ChiTieu
            };
            _context.DanhMucMauSacRepository.Add(entity);
            await _context.SaveChangesAsync();

            // Log Nhat ky
            await _nhatKyService.LogCreate("DanhMucMauSac");
            return entity;
        }

        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult<DanhMucMauSac>> Delete(int id)
        {
            var item = await _context.DanhMucMauSacRepository
                /*.Where(p => p.DMDonViSuDungId == _tenantProvider.TenantId)*/.Where(p => p.Id == id).FirstOrDefaultAsync();
            if (item == null)
            {
                return NotFound();
            }

            item.DeletedDate = DateTime.Now;
            await _context.SaveChangesAsync();

            // Log Nhat ky
            await _nhatKyService.LogDelete("DanhMucMauSac");
            return item;
        }

        private bool Exists(int id)
        {
            return _context.DanhMucMauSacRepository.Any(e => e.Id == id);
        }

        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<DanhMucMauSac>>> Get(SearchRequest request)
        {
            return await _context.DanhMucMauSacRepository
                //.Where(p => p.DMDonViSuDungId == _tenantProvider.TenantId)
                //.Where(x => x.DeletedDate == null)
                .ToListAsync();
        }

        [HttpGet("ExportToExcel")]
        public async Task<ActionResult<GetAllResponse<DanhMucMauSac>>> Getd([FromBody] BaseSearchRequest request)
        {

            GetAllResponse<DanhMucMauSac> outputs = new GetAllResponse<DanhMucMauSac>();
            //Expression<Func<DanhMucMauSac, bool>> filter = m => (_tenantProvider.TenantId == 0 || m.DMDonViSuDungId == _tenantProvider.TenantId);
            //if (!string.IsNullOrEmpty(request.Keywords))
            //{
            //    filter = filter.And(x => x.ChiTieu.Contains(request.Keywords) || x.MaSo.Contains(request.Keywords));
            //}
            //Func<IQueryable<DanhMucMauSac>, IOrderedQueryable<DanhMucMauSac>> order = null;
            //if (request.SortDirection != QLSX.Shared.Enums.SortDirection.None)
            //{
            //    order = await OrderBy(request.SortLable, request.SortDirection == QLSX.Shared.Enums.SortDirection.Ascending);
            //}

            //IQueryable<DanhMucMauSac> query = _context.Set<DanhMucMauSac>();

            //if (filter != null) query = query.Where(filter).Where(x => x.DeletedDate == null);
            //if (order != null) query = order(query);
            //outputs.TotalRecords = await query.CountAsync();
            //outputs.TotalPages = (int)Math.Ceiling(outputs.TotalRecords / (double)request.PageSize);
            //outputs.Page = request.Page;
            //outputs.PageSize = request.PageSize;

            ////query = query.Skip(request.Page * request.PageSize).Take(request.PageSize);
            //outputs.Items = await query.ToListAsync();

            // Log Nhat ky
            await _nhatKyService.LogExportExcel("DanhMucMauSac");
            return outputs;

        }
        [HttpGet("GetAllPaged")]
        public async Task<ActionResult<GetAllResponse<DanhMucMauSacModel>>> GetAllPaged([FromBody] BaseSearchRequest request)
        {
            GetAllResponse<DanhMucMauSacModel> outputs = new GetAllResponse<DanhMucMauSacModel>();
            IQueryable<DanhMucMauSac> query = _context.Set<DanhMucMauSac>();
            if (!string.IsNullOrEmpty(request.Keywords))
            {
                query = query.Where(x => x.ChiTieu.Contains(request.Keywords) || x.MaSo.Contains(request.Keywords));
            }
            Func<IQueryable<DanhMucMauSac>, IOrderedQueryable<DanhMucMauSac>> order = null;
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
            outputs.Items = rawData.Select(item => new DanhMucMauSacModel(item)).ToList();
            return outputs;
        }

        private async Task<Func<IQueryable<DanhMucMauSac>, IOrderedQueryable<DanhMucMauSac>>> OrderBy(string sortBy, bool sortType)
        {
            Func<IQueryable<DanhMucMauSac>, IOrderedQueryable<DanhMucMauSac>> myFunc;
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
