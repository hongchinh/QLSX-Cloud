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
    public class SoDuCongNoController : ControllerBase
    {
        private readonly CRMDBContext _context;
        private readonly ITenantProvider _tenantProvider;
        private readonly INhatKyService _nhatKyService;
        public SoDuCongNoController(CRMDBContext context, ITenantProvider tenantProvider, INhatKyService nhatKyService)
        {
            _context = context;
            _tenantProvider = tenantProvider;
            _nhatKyService = nhatKyService;
        }
        // GET: api/GetCustomerTypes
        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<SoDuCongNo>>> Get(SearchSoDuCongNoRequest request)
        {
            var query = (
             from cus in _context.SoDuCongNos
              .Where(x => x.Loai == request.Loai || string.IsNullOrEmpty(request.Loai))
              .Where(p => p.DMDonViSuDungId == _tenantProvider.TenantId)
              .Where(x => x.DeletedDate == null)
             select cus);

            if (!string.IsNullOrEmpty(request.Loai)) query = query.Where(p => p.Loai.ToLower().Equals(request.Loai.ToLower()));
            if (!string.IsNullOrEmpty(request.MaDonVi)) query = query.Where(p => p.MaDonVi.Contains(request.MaDonVi));
            if (!string.IsNullOrEmpty(request.TenDonVi)) query = query.Where(p => p.TenDonVi.Contains(request.TenDonVi));
            if (!string.IsNullOrEmpty(request.DiaChi)) query = query.Where(p => p.DiaChi.Contains(request.DiaChi));
            if (!string.IsNullOrEmpty(request.DienThoai)) query = query.Where(p => p.DienThoai.Contains(request.DienThoai));

            return await query
                .ToListAsync();


        }

        [HttpGet("GetCount")]
        public async Task<ActionResult<ItemCount>> GettCount()
        {
            ItemCount itemCount = new ItemCount();

            itemCount.Count = _context.SoDuCongNos
                .Where(x => x.DeletedDate == null)
                .Count();
            return await Task.FromResult(itemCount);
        }

        // GET: api/Customers
        [HttpGet("GetByPage")]
        public async Task<ActionResult<IEnumerable<SoDuCongNo>>> GetByPage(int pageSize, int pageNumber)
        {
            //pageNumber * pageSize -> take 5
            //ItemList = Items.Skip(pageNumber * PageSize).Take(PageSize).ToList();

            List<SoDuCongNo> list = await _context.SoDuCongNos
                .Where(x => x.DeletedDate == null)
                .ToListAsync();
            list = list.Skip(pageNumber * pageSize).Take(pageSize).ToList();

            return await Task.FromResult(list);
        }

        // GET: api/Customers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SoDuCongNo>> GetById(int id)
        {
            var item = await _context.SoDuCongNos.Where(p => p.DMDonViSuDungId == _tenantProvider.TenantId).Where(p => p.Id == id).FirstOrDefaultAsync();

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
        public async Task<IActionResult> Put(int id, SoDuCongNo item)
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
                await _nhatKyService.LogUpdate("SoDuCongNo");
            }
            catch (DbUpdateConcurrencyException)
            {
                // Log Nhat ky
                await _nhatKyService.LogError("Update_DMSoDuCongNo", "id : " + id + ";\nitem : " + item.ToString());
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
        public async Task<IActionResult> UpdateBatch(List<SoDuCongNo> items)
        {
            string loai = "";
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
                    _context.Entry(item).State = EntityState.Added;
                }
                loai = item.Loai;
            }

            var idsOfAddresses = items.Select(x => x.Id).ToList();
            var addressesToDelete = await _context
                .SoDuCongNos
                .Where(x => !idsOfAddresses.Contains(x.Id))
                .Where(x => x.Loai == loai) 
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
                await _nhatKyService.LogUpdate("Batch_SoDuCongNo");
            }
            catch (DbUpdateConcurrencyException)
            {

                // Log Nhat ky
                await _nhatKyService.LogError("Update_Batch_DMSoDuCongNo", "items : " + items.ToString());
                throw;

            }

            return NoContent();
        }
        // POST: api/Customers
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost("Create")]
        public async Task<ActionResult<SoDuCongNo>> Post(SoDuCongNo item)
        {
            item.CreatedDate = DateTime.Now;
            item.UpdatedDate = DateTime.Now;
            item.DMDonViSuDungId = _tenantProvider.TenantId;
            _context.SoDuCongNos.Add(item);
            await _context.SaveChangesAsync();

            // Log Nhat ky
            await _nhatKyService.LogCreate("SoDuCongNo");
            return item;
        }

        // POST: api/Customers
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost("CreateBatch")]
        public async Task<ActionResult<List<SoDuCongNo>>> CreateBatch(List<SoDuCongNo> items)
        {
            foreach (SoDuCongNo item in items)
            {
                item.CreatedDate = DateTime.Now;
                item.UpdatedDate = DateTime.Now;
            }
            _context.SoDuCongNos.AddRange(items);
            await _context.SaveChangesAsync();

            // Log Nhat ky
            await _nhatKyService.LogCreate("Batch_SoDuCongNo");
            return items;
        }


        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult<SoDuCongNo>> Delete(int id)
        {
            var item = await _context.SoDuCongNos.Where(p => p.DMDonViSuDungId == _tenantProvider.TenantId).Where(p => p.Id == id).FirstOrDefaultAsync();
            if (item == null)
            {
                return NotFound();
            }


            item.DeletedDate = DateTime.Now;
            await _context.SaveChangesAsync();

            // Log Nhat ky
            await _nhatKyService.LogDelete("SoDuCongNo");
            return item;
        }

        private bool Exists(int id)
        {
            return _context.SoDuCongNos.Any(e => e.Id == id);
        }
        // GET: api/DMHangHoas
        [HttpGet("GetAllPaged")]
        public async Task<ActionResult<GetAllResponse<SoDuCongNo>>> GetAllPaged([FromBody] BaseSearchRequest request)
        {
            GetAllResponse<SoDuCongNo> outputs = new GetAllResponse<SoDuCongNo>();
            var query = (
               from cus in _context.SoDuCongNos
               where cus.DeletedDate == null
               select cus);
            Console.WriteLine(query.ToString());
            if (!string.IsNullOrEmpty(request.Keywords)) query = query.Where(x => x.MaDonVi.ToLower().Contains(request.Keywords.ToLower())
            || x.TenDonVi.ToLower().Contains(request.Keywords.ToLower()));

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
