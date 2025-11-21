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
    public class SoDuHangHoaController : ControllerBase
    {
        private readonly CRMDBContext _context;
        private readonly ITenantProvider _tenantProvider;
        private readonly INhatKyService _nhatKyService;
        public SoDuHangHoaController(CRMDBContext context, ITenantProvider tenantProvider, INhatKyService nhatKyService)
        {
            _context = context;
            _tenantProvider = tenantProvider;
            _nhatKyService = nhatKyService;
        }
        // GET: api/GetCustomerTypes
        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<SoDuHangHoa>>> Get(SearchSoDuHangHoaRequest request)
        {
            //await Task.Delay(3000);
            var query = (
             from cus in _context.SoDuHangHoas.Where(x => x.DeletedDate == null && request.DMKhoHangId == request.DMKhoHangId)
              .Where(p => p.DMDonViSuDungId == _tenantProvider.TenantId)
              .Where(x => x.DeletedDate == null)
             select cus);

            if (request.DMKhoHangId > 0) query = query.Where(p => p.DMKhoHangId == request.DMKhoHangId);
            if (!string.IsNullOrEmpty(request.MaHangHoa)) query = query.Where(p => p.MaHangHoa.Contains(request.MaHangHoa));
            if (!string.IsNullOrEmpty(request.TenHangHoa)) query = query.Where(p => p.TenHangHoa.Contains(request.TenHangHoa));
            if (!string.IsNullOrEmpty(request.DonViTinh)) query = query.Where(p => p.DonViTinh.Contains(request.DonViTinh));

            var lst = query.ToList();

            return await Task.FromResult(lst);


        }

        [HttpGet("GetCount")]
        public async Task<ActionResult<ItemCount>> GettCount()
        {
            ItemCount itemCount = new ItemCount();

            itemCount.Count = _context.SoDuHangHoas
                .Where(x => x.DeletedDate == null).Count();
            return await Task.FromResult(itemCount);
        }

        // GET: api/Customers
        [HttpGet("GetByPage")]
        public async Task<ActionResult<IEnumerable<SoDuHangHoa>>> GetByPage(int pageSize, int pageNumber)
        {
            //pageNumber * pageSize -> take 5
            //ItemList = Items.Skip(pageNumber * PageSize).Take(PageSize).ToList();

            List<SoDuHangHoa> list = await _context.SoDuHangHoas.Where(x=>x.DeletedDate == null).ToListAsync();
            list = list.Skip(pageNumber * pageSize).Take(pageSize).ToList();

            return await Task.FromResult(list);
        }

        // GET: api/Customers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SoDuHangHoa>> GetById(int id)
        {
            var item =  _context.SoDuHangHoas.Where(x => x.DeletedDate == null && x.Id == id).FirstOrDefault();

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
        public async Task<IActionResult> Put(int id, SoDuHangHoa item)
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
                await _nhatKyService.LogUpdate("SoDuHangHoa");
            }
            catch (DbUpdateConcurrencyException)
            {
                // Log Nhat ky
                await _nhatKyService.LogError("Update_DMSoDuHangHoa", "id : " + id + ";\nitem : " + item.ToString());
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
        public async Task<IActionResult> UpdateBatch(List<SoDuHangHoa> items)
        {
            int  makho = 0;
            foreach (var item in items)
            {
                if (item.Id != 0)
                {
                    item.UpdatedDate = DateTime.Now;
                    _context.Entry(item).State = EntityState.Modified;
                }
                else
                {
                    item.CreatedDate = DateTime.Now;
                    item.UpdatedDate = DateTime.Now;
                    item.DMDonViSuDungId = _tenantProvider.TenantId;
                    _context.Entry(item).State = EntityState.Added;
                }
                makho = item.DMKhoHangId;
            }

            var idsOfAddresses = items.Select(x => x.Id).ToList();
            var addressesToDelete = await _context
                .SoDuHangHoas
                .Where(x=>x.DMKhoHangId == makho)
                .Where(x => !idsOfAddresses.Contains(x.Id)).Where(x => x.DeletedDate == null)
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
                await _nhatKyService.LogUpdate("Batch_SoDuHangHoa");
            }
            catch (DbUpdateConcurrencyException)
            {

                // Log Nhat ky
                await _nhatKyService.LogError("Update_Batch_SoDuHangHoa", "items : " + items.ToString());
                throw;

            }

            return NoContent();
        }
        // POST: api/Customers
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost("Create")]
        public async Task<ActionResult<SoDuHangHoa>> Post(SoDuHangHoa item)
        {
            item.CreatedDate = DateTime.Now;
            item.UpdatedDate = DateTime.Now;
            item.DMDonViSuDungId = _tenantProvider.TenantId;
            _context.SoDuHangHoas.Add(item);
            await _context.SaveChangesAsync();

            // Log Nhat ky
            await _nhatKyService.LogCreate("SoDuHangHoa");
            return item;
        }

        // POST: api/Customers
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost("CreateBatch")]
        public async Task<ActionResult<List<SoDuHangHoa>>> CreateBatch(List<SoDuHangHoa> items)
        {
            foreach (SoDuHangHoa item in items)
            {
                item.CreatedDate = DateTime.Now;
                item.UpdatedDate = DateTime.Now;
            }
            _context.SoDuHangHoas.AddRange(items);
            await _context.SaveChangesAsync();

            // Log Nhat ky
            await _nhatKyService.LogCreate("Batch_SoDuHangHoa");
            return items;
        }


        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult<SoDuHangHoa>> Delete(int id)
        {
            var item =   _context.SoDuHangHoas.Where(x => x.DeletedDate == null && x.Id == id).FirstOrDefault();
            if (item == null)
            {
                return NotFound();
            }


            item.DeletedDate = DateTime.Now;
            await _context.SaveChangesAsync();

            // Log Nhat ky
            await _nhatKyService.LogDelete("SoDuHangHoa");
            return item;
        }

        private bool Exists(int id)
        {
            return _context.SoDuHangHoas.Where(x => x.DeletedDate == null).Any(e => e.Id == id);
        }
        // GET: api/DMHangHoas
        [HttpGet("GetAllPaged")]
        public async Task<ActionResult<GetAllResponse<SoDuHangHoa>>> GetAllPaged([FromBody] BaseSearchRequest request)
        {
            GetAllResponse<SoDuHangHoa> outputs = new GetAllResponse<SoDuHangHoa>();
            var query = (
               from cus in _context.SoDuHangHoas
               where cus .DeletedDate == null
               select cus);
            Console.WriteLine(query.ToString());
            if (!string.IsNullOrEmpty(request.Keywords)) query = query.Where(x => x.MaHangHoa.ToLower().Contains(request.Keywords.ToLower())
            || x.TenHangHoa.ToLower().Contains(request.Keywords.ToLower()));

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
