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
using SaleAPI.Interfaces;
using SaleAPI.Services;
using QLSX.Shared.Entities;
using QLSX.Shared.Models.Request;

namespace SaleAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DMTinhThanhsController : ControllerBase
    {
        private readonly CRMDBContext _context;
        private readonly ITenantProvider _tenantProvider;
        private readonly INhatKyService _nhatKyService;
        public DMTinhThanhsController(CRMDBContext context, ITenantProvider tenantProvider, INhatKyService nhatKyService)
        {
            _context = context;
            _tenantProvider = tenantProvider;
            _nhatKyService = nhatKyService;
        }
        // GET: api/GetCustomerTypes
        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<DanhMucTinhThanhModel>>> Get(SearchRequest request)
        {
            //await Task.Delay(3000);
            return await _context.DanhMucTinhThanhRepository.Where(item => item.DeletedDate == null)
                .OrderBy(x => x.MaKhuVuc)
                .Select(item => new DanhMucTinhThanhModel(item))
                .ToListAsync();
        }


        [HttpGet("GetCount")]
        public async Task<ActionResult<ItemCount>> GettCount()
        {
            ItemCount itemCount = new ItemCount();

            itemCount.Count = _context.DanhMucTinhThanhRepository
                .Where(x => x.DeletedDate == null)
                .Count();
            return await Task.FromResult(itemCount);
        }

        // GET: api/Customers
        [HttpGet("GetByPage")]
        public async Task<ActionResult<IEnumerable<DanhMucTinhThanhModel>>> GetByPage(int pageSize, int pageNumber)
        {
            //pageNumber * pageSize -> take 5
            //ItemList = Items.Skip(pageNumber * PageSize).Take(PageSize).ToList();

            List<DanhMucTinhThanhModel> list = await _context.DanhMucTinhThanhRepository
                .Where(x => x.DeletedDate == null)
                .Select(item => new DanhMucTinhThanhModel(item))
                .ToListAsync();
            list = list.Skip(pageNumber * pageSize).Take(pageSize).ToList();

            return await Task.FromResult(list);
        }

        // GET: api/Customers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DanhMucTinhThanhModel>> GetById(int id)
        {
            var item = await _context.DanhMucTinhThanhRepository.FirstOrDefaultAsync(p => p.Id == id);

            if (item == null)
            {
                return NotFound();
            }

            return new DanhMucTinhThanhModel(item);
        }

        // PUT: api/Customers/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("Update/{id}")]
        public async Task<ActionResult<DanhMucTinhThanhModel>> Put(int id, DanhMucTinhThanhModel model)
        {
            DanhMucTinhThanh entity = await _context.DanhMucTinhThanhRepository.FirstOrDefaultAsync(item => item.Id == model.Id && item.DeletedDate == null);
            if (entity != null)
            {
                return new DanhMucTinhThanhModel();
            }
            entity.MaKhuVuc = model.MaTinh;
            entity.TenKhuVuc = model.TenTinh;
            entity.UpdatedDate = DateTime.Now;

            try
            {
                await _context.SaveChangesAsync();

                // Log Nhat ky
                await _nhatKyService.LogUpdate("DMTinhThanh");
            }
            catch (DbUpdateConcurrencyException)
            {
                // Log Nhat ky
                await _nhatKyService.LogError("Update_DMTinhThanh", "id : " + id + ";\nitem : " + model.ToString());
                if (!Exists(id))
                {
                    return new DanhMucTinhThanhModel();
                }
                else
                {
                    return new DanhMucTinhThanhModel();
                }
            }

            return new DanhMucTinhThanhModel(entity);
        }

        // POST: api/Customers
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost("Create")]
        public async Task<ActionResult<DanhMucTinhThanhModel>> Post(DanhMucTinhThanhModel model)
        {
            DanhMucTinhThanh entity = new();
            entity.Id = 0;
            entity.MaKhuVuc = model.MaTinh;
            entity.TenKhuVuc = model.TenTinh;
            entity.CreatedDate = DateTime.Now;
            entity.UpdatedDate = DateTime.Now;
            _context.DanhMucTinhThanhRepository.Add(entity);
            await _context.SaveChangesAsync();

            // Log Nhat ky
            await _nhatKyService.LogCreate("DMTinhThanh");
            return new DanhMucTinhThanhModel(entity);
        }

        // DELETE: api/Customer/delete/5
        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult<DanhMucTinhThanhModel>> Delete(int id)
        {
            var item = await _context.DanhMucTinhThanhRepository.FirstOrDefaultAsync(p => p.Id == id);
            if (item == null)
            {
                return NotFound();
            }

            item.DeletedDate = DateTime.Now;
            await _context.SaveChangesAsync();

            // Log Nhat ky
            await _nhatKyService.LogDelete("DMTinhThanh");
            return new DanhMucTinhThanhModel(item);
        }

        private bool Exists(int id)
        {
            return _context.DanhMucTinhThanhRepository.Where(x => x.DeletedDate == null).Any(e => e.Id == id);
        }

    }
}
