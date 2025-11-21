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

namespace SaleAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SoDuLoaiTienController : ControllerBase
    {
        private readonly CRMDBContext _context;
        private readonly ITenantProvider _tenantProvider;
        private readonly INhatKyService _nhatKyService;
        public SoDuLoaiTienController(CRMDBContext context, ITenantProvider tenantProvider, INhatKyService nhatKyService)
        {
            _context = context;
            _tenantProvider = tenantProvider;
            _nhatKyService = nhatKyService;
        }
        // GET: api/GetCustomerTypes
        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<SoDuLoaiTien>>> Get(SearchSoDuLoaiTienRequest request)
        {
            //await Task.Delay(3000);
            var query = (
             from cus in _context.SoDuLoaiTiens
              .Where(p => p.DMDonViSuDungId == _tenantProvider.TenantId)
              .Where(x => x.DeletedDate == null)
             select cus);
            
            if (!string.IsNullOrEmpty(request.KyHieu)) query = query.Where(p => p.KyHieu.ToLower().Contains(request.KyHieu.ToLower()));
            if (!string.IsNullOrEmpty(request.TenLoaiTien)) query = query.Where(p => p.TenLoaiTien.ToLower().Contains(request.TenLoaiTien.ToLower()));
                       return await query
                .ToListAsync();


        }

        [HttpGet("GetCount")]
        public async Task<ActionResult<ItemCount>> GettCount()
        {
            ItemCount itemCount = new ItemCount();

            itemCount.Count = _context.SoDuLoaiTiens
                .Where(x => x.DeletedDate == null)
                .Count();
            return await Task.FromResult(itemCount);
        }

        // GET: api/Customers
        [HttpGet("GetByPage")]
        public async Task<ActionResult<IEnumerable<SoDuLoaiTien>>> GetByPage(int pageSize, int pageNumber)
        {
            //pageNumber * pageSize -> take 5
            //ItemList = Items.Skip(pageNumber * PageSize).Take(PageSize).ToList();

            List<SoDuLoaiTien> list = await _context.SoDuLoaiTiens
                .Where(x => x.DeletedDate == null)
                .ToListAsync();
            list = list.Skip(pageNumber * pageSize).Take(pageSize).ToList();

            return await Task.FromResult(list);
        }

        // GET: api/Customers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SoDuLoaiTien>> GetById(int id)
        {
            var item = await _context.SoDuLoaiTiens.Where(p => p.DMDonViSuDungId == _tenantProvider.TenantId).Where(p => p.Id == id).FirstOrDefaultAsync();

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
        public async Task<IActionResult> Put(int id, SoDuLoaiTien item)
        {
            if (id != item.Id)
            {
                return BadRequest();
            }

            item.UpdatedDate = DateTime.Now;
            _context.Entry(item).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();

                // Log Nhat ky
                await _nhatKyService.LogUpdate("SoDuLoaiTien");
            }
            catch (DbUpdateConcurrencyException)
            {
                // Log Nhat ky
                await _nhatKyService.LogError("Update_DMSoDuLoaiTien", "id : " + id + ";\nitem : " + item.ToString());
                if (!Exists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }
        // PUT: api/Customers/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("UpdateBatch")]
        public async Task<IActionResult> UpdateBatch(List<SoDuLoaiTien> items)
        {

            foreach (var item in items)
            {
                if (item.Id != 0)
                {
                    _context.Entry(item).State = EntityState.Modified;
                }
                else
                {
                    item.CreatedDate = DateTime.Now;
                    item.UpdatedDate = DateTime.Now;
                    item.DMDonViSuDungId = _tenantProvider.TenantId;
                    // _context.Entry(item).State = EntityState.Added;
                    _context.SoDuLoaiTiens.Add(item);
                }
            }

            var idsOfAddresses = items.Select(x => x.Id).ToList();
            var addressesToDelete = await _context
                .SoDuLoaiTiens
                .Where(x => !idsOfAddresses.Contains(x.Id))
                .ToListAsync();

           
            foreach (var item in addressesToDelete)
            {
                item.DeletedDate = DateTime.Now;
                _context.Entry(item).State = EntityState.Modified;
            }
            try
            {
                await _context.SaveChangesAsync();

                // Log Nhat ky
                await _nhatKyService.LogUpdate("Batch_SoDuLoaiTien");
            }
            catch (DbUpdateConcurrencyException ex)
            {

                // Log Nhat ky
                await _nhatKyService.LogError("Update_Batch_DMSoDuLoaiTien", "items : " + items.ToString());
                throw;

            }

            return NoContent();
        }
        // POST: api/Customers
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost("Create")]
        public async Task<ActionResult<SoDuLoaiTien>> Post(SoDuLoaiTien item)
        {
            item.CreatedDate = DateTime.Now;
            item.UpdatedDate = DateTime.Now;
            item.DMDonViSuDungId = _tenantProvider.TenantId;
            _context.SoDuLoaiTiens.Add(item);
            await _context.SaveChangesAsync();

            // Log Nhat ky
            await _nhatKyService.LogCreate("SoDuLoaiTien");
            return item;
        }

        // POST: api/Customers
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost("CreateBatch")]
        public async Task<ActionResult<List<SoDuLoaiTien>>> CreateBatch(List<SoDuLoaiTien> items)
        {
            foreach (SoDuLoaiTien item in items)
            {
                item.CreatedDate = DateTime.Now;
                item.UpdatedDate = DateTime.Now;
            }
            _context.SoDuLoaiTiens.AddRange(items);
            await _context.SaveChangesAsync();

            // Log Nhat ky
            await _nhatKyService.LogCreate("Batch_SoDuLoaiTien");
            return items;
        }


        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult<SoDuLoaiTien>> Delete(int id)
        {
            var item = await _context.SoDuLoaiTiens.Where(p => p.DMDonViSuDungId == _tenantProvider.TenantId).Where(p => p.Id == id).FirstOrDefaultAsync();
            if (item == null)
            {
                return NotFound();
            }


            item.DeletedDate = DateTime.Now;
            await _context.SaveChangesAsync();

            // Log Nhat ky
            await _nhatKyService.LogDelete("SoDuLoaiTien");
            return item;
        }

        private bool Exists(int id)
        {
            return _context.SoDuLoaiTiens.Any(e => e.Id == id);
        }
        // GET: api/DMHangHoas
        [HttpGet("GetAllPaged")]
        public async Task<ActionResult<GetAllResponse<SoDuLoaiTien>>> GetAllPaged([FromBody] BaseSearchRequest request)
        {
            GetAllResponse<SoDuLoaiTien> outputs = new GetAllResponse<SoDuLoaiTien>();
            var query = (
               from cus in _context.SoDuLoaiTiens
               where cus.DeletedDate == null
               select cus);
            Console.WriteLine(query.ToString());
            if (!string.IsNullOrEmpty(request.Keywords)) query = query.Where(x => x.TenLoaiTien.ToLower().Contains(request.Keywords.ToLower()));

            outputs.TotalRecords = await query.CountAsync();
            outputs.TotalPages = (int)Math.Ceiling(outputs.TotalRecords / (double)request.PageSize);
            outputs.Page = request.Page;
            outputs.PageSize = request.PageSize;
            //var item = await query.ToListAsync();
            query = query
                .Skip(request.Page * request.PageSize).Take(request.PageSize);
            outputs.Items = await query.ToListAsync();
            return outputs;
        }
    }
}
