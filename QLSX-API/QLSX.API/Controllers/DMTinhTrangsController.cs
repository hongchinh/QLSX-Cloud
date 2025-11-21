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
using static MudBlazor.CategoryTypes;
using QLSX.Shared.Models.Request;

namespace SaleAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DMTinhTrangsController : ControllerBase
    {
        private readonly CRMDBContext _context;
        private readonly ITenantProvider _tenantProvider;
        private readonly INhatKyService _nhatKyService;
        public DMTinhTrangsController(CRMDBContext context, ITenantProvider tenantProvider, INhatKyService nhatKyService)
        {
            _context = context;
            _tenantProvider = tenantProvider;
            _nhatKyService = nhatKyService;
        }
        // GET: api/GetCustomerTypes
        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<DMTinhTrangModel>>> Get(SearchRequest request)
        {
            //await Task.Delay(3000);
            var lst = await _context.DMTinhTrangRepository
            .ToListAsync();

            return lst.Select(x => new DMTinhTrangModel(x)).ToList();
        }


        [HttpGet("GetCount")]
        public async Task<ActionResult<ItemCount>> GettCount()
        {
            ItemCount itemCount = new ItemCount();

            itemCount.Count = _context.DMTinhTrangRepository
                .Count();
            return await Task.FromResult(itemCount);
        }

        // GET: api/Customers
        [HttpGet("GetByPage")]
        public async Task<ActionResult<IEnumerable<DMTinhTrangModel>>> GetByPage(int pageSize, int pageNumber)
        {
            List<DMTinhTrang> list = await _context.DMTinhTrangRepository
                .ToListAsync();
            list = list.Skip(pageNumber * pageSize).Take(pageSize).ToList();

            return list.Select(x => new DMTinhTrangModel(x)).ToList();
        }

        // GET: api/Customers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DMTinhTrangModel>> GetById(int id)
        {
            var item = await _context.DMTinhTrangRepository.Where(p => p.Id == id).FirstOrDefaultAsync();

            if (item == null)
            {
                return NotFound();
            }

            return new DMTinhTrangModel(item);
        }

        // PUT: api/Customers/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("Update/{id}")]
        public async Task<ActionResult<DMTinhTrangModel>> Put(int id, DMTinhTrangModel item)
        {
            if (id != item.Id)
            {
                return new DMTinhTrangModel();
            }

            _context.Entry(item).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();

                // Log Nhat ky
                await _nhatKyService.LogUpdate("DMTinhTrang");
            }
            catch (DbUpdateConcurrencyException)
            {
                // Log Nhat ky
                await _nhatKyService.LogError("Update_DMTinhTrang", "id : " + id + ";\nitem : " + item.ToString());
                if (!Exists(id))
                {
                    return new DMTinhTrangModel();
                }
                else
                {
                    return new DMTinhTrangModel();
                }
            }

            return item;
        }

        // POST: api/Customers
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost("Create")]
        public async Task<ActionResult<DMTinhTrangModel>> Post(DMTinhTrangModel item)
        {
            DMTinhTrang itemNew = new DMTinhTrang();
            itemNew.Stt = item.Stt;
            itemNew.TenTrangThai = item.TenTrangThai;
            itemNew.GhiChu = item.GhiChu;

            _context.DMTinhTrangRepository.Add(itemNew);
            await _context.SaveChangesAsync();

            // Log Nhat ky
            await _nhatKyService.LogCreate("DMTinhTrang");
            return new DMTinhTrangModel(itemNew);
        }

        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult<DMTinhTrangModel>> Delete(int id)
        {
            var item = await _context.DMTinhTrangRepository.Where(p => p.Id == id).FirstOrDefaultAsync();
            if (item == null)
            {
                return NotFound();
            }

            await _context.SaveChangesAsync();

            // Log Nhat ky
            await _nhatKyService.LogDelete("DMTinhTrang");
            return new DMTinhTrangModel(item);
        }

        private bool Exists(int id)
        {
            return _context.DMTinhTrangRepository
                .Any(e => e.Id == id);
        }

    }
}
