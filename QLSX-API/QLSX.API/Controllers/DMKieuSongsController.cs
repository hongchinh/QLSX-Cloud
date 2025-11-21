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
    public class DMKieuSongsController : ControllerBase
    {
        private readonly CRMDBContext _context;
        private readonly ITenantProvider _tenantProvider;
        private readonly INhatKyService _nhatKyService;
        public DMKieuSongsController(CRMDBContext context, ITenantProvider tenantProvider, INhatKyService nhatKyService)
        {
            _context = context;
            _tenantProvider = tenantProvider;
            _nhatKyService = nhatKyService;
        }
        


        [HttpGet("GetCount")]
        public async Task<ActionResult<ItemCount>> GettCount()
        {
            ItemCount itemCount = new ItemCount();

            itemCount.Count = _context.DanhMucKieuSongRepository.Count();
            return await Task.FromResult(itemCount);
        }

         

        // GET: api/Customers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DanhMucKieuSong>> GetById(int id)
        {
            var item = await _context.DanhMucKieuSongRepository
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
        public async Task<ActionResult<DanhMucKieuSong>> Put(int id, DanhMucKieuSong item)
        {
            if (id != item.Id)
            {
                return new DanhMucKieuSong();
            }

            //item.UpdatedDate = DateTime.Now;
            _context.Entry(item).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();

                // Log Nhat ky
                await _nhatKyService.LogUpdate("DanhMucKieuSong");
            }
            catch (DbUpdateConcurrencyException)
            {
                // Log Nhat ky
                await _nhatKyService.LogError("Update_DanhMucKieuSong", "id : " + id + ";\nitem : " + item.ToString());
                if (!Exists(id))
                {
                    return new DanhMucKieuSong();
                }
                else
                {
                    return new DanhMucKieuSong();
                }
            }

            return item;
        }

        // POST: api/Customers
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost("Create")]
        public async Task<ActionResult<DanhMucKieuSong>> Post(DanhMucKieuSong item)
        {
            //item.CreatedDate = DateTime.Now;
            //item.UpdatedDate = DateTime.Now;
            //item.DMDonViSuDungId = _tenantProvider.TenantId;
            _context.DanhMucKieuSongRepository.Add(item);
            await _context.SaveChangesAsync();

            // Log Nhat ky
            await _nhatKyService.LogCreate("DanhMucKieuSong");
            return item;
        }

        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult<DanhMucKieuSong>> Delete(int id)
        {
            var item = await _context.DanhMucKieuSongRepository
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
            await _nhatKyService.LogDelete("DanhMucKieuSong");
            return item;
        }

        private bool Exists(int id)
        {
            return _context.DanhMucKieuSongRepository.Any(e => e.Id == id);
        }
        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<DanhMucKieuSongModel>>> Get(SearchRequest request)
        {
            return await _context.DanhMucKieuSongRepository
                //.Where(p => p.DMDonViSuDungId == _tenantProvider.TenantId)
                //.Where(x => x.DeletedDate == null)
                .Select(item => new DanhMucKieuSongModel(item))
                .ToListAsync();
        }
        [HttpGet("ExportToExcel")]
        public async Task<ActionResult<GetAllResponse<DanhMucKieuSong>>> ExportToExcel([FromBody] BaseSearchRequest request)
        {
            GetAllResponse<DanhMucKieuSong> outputs = new GetAllResponse<DanhMucKieuSong>();
           // Expression<Func<DanhMucKieuSong, bool>> filter = m => (_tenantProvider.TenantId == 0 || m.DMDonViSuDungId == _tenantProvider.TenantId);
           // if (!string.IsNullOrEmpty(request.Keywords))
           // {
           //     filter = filter.And(x => x.ChiTieu.Contains(request.Keywords) || x.MaSo.Contains(request.Keywords));
           // }
           // Func<IQueryable<DanhMucKieuSong>, IOrderedQueryable<DanhMucKieuSong>> order = null;
           // if (request.SortDirection != QLSX.Shared.Enums.SortDirection.None)
           // {
           //     order = await OrderBy(request.SortLable, request.SortDirection == QLSX.Shared.Enums.SortDirection.Ascending);
           // }

           // IQueryable<DanhMucKieuSong> query = _context.Set<DanhMucKieuSong>();

           // if (filter != null) query = query.Where(filter).Where(x => x.DeletedDate == null);
           // if (order != null) query = order(query);
           // outputs.TotalRecords = await query.CountAsync();
           // outputs.TotalPages = (int)Math.Ceiling(outputs.TotalRecords / (double)request.PageSize);
           // outputs.Page = request.Page;
           // outputs.PageSize = request.PageSize;

           //// query = query.Skip(request.Page * request.PageSize).Take(request.PageSize);
           // outputs.Items = await query.ToListAsync();

            // Log Nhat ky
            await _nhatKyService.LogExportExcel("DanhMucKieuSong");
            return outputs;

        }
        [HttpGet("GetAllPaged")]
        public async Task<ActionResult<GetAllResponse<DanhMucKieuSong>>> GetAllPaged([FromBody] BaseSearchRequest request)
        {
            GetAllResponse<DanhMucKieuSong> outputs = new GetAllResponse<DanhMucKieuSong>();
            //Expression<Func<DanhMucKieuSong, bool>> filter = m => (_tenantProvider.TenantId == 0 || m.DMDonViSuDungId == _tenantProvider.TenantId);
            //if (!string.IsNullOrEmpty(request.Keywords))
            //{
            //    filter = filter.And(x => x.ChiTieu.Contains(request.Keywords) || x.MaSo.Contains(request.Keywords));
            //}
            //Func<IQueryable<DanhMucKieuSong>, IOrderedQueryable<DanhMucKieuSong>> order = null;
            //if (request.SortDirection != QLSX.Shared.Enums.SortDirection.None)
            //{
            //    order = await OrderBy(request.SortLable, request.SortDirection == QLSX.Shared.Enums.SortDirection.Ascending);
            //}

            //IQueryable<DanhMucKieuSong> query = _context.Set<DanhMucKieuSong>();

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
        private async Task<Func<IQueryable<DanhMucKieuSong>, IOrderedQueryable<DanhMucKieuSong>>> OrderBy(string sortBy, bool sortType)
        {
            Func<IQueryable<DanhMucKieuSong>, IOrderedQueryable<DanhMucKieuSong>> myFunc;
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
