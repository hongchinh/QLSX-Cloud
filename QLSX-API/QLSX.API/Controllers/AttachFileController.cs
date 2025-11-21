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
    public class AttachFileController : ControllerBase
    {
        private readonly CRMDBContext _context;
        private readonly ITenantProvider _tenantProvider;
        private readonly INhatKyService _nhatKyService;
        public AttachFileController(CRMDBContext context, ITenantProvider tenantProvider, INhatKyService nhatKyService)
        {
            _context = context;
            _tenantProvider = tenantProvider;
            _nhatKyService = nhatKyService;
        }
        // GET: api/GetCustomerTypes
        [HttpGet("")]
        public async Task<ActionResult<List<tblFileAttachment>>> Get()
        {
            var lst =  await _context.tblFileAttachments
                //.Where(p => p.DMDonViSuDungId == _tenantProvider.TenantId)
                //.Where(x => x.DeletedDate == null)
                .ToListAsync();
            return lst;
        }


        [HttpGet("GetCount")]
        public async Task<ActionResult<ItemCount>> GettCount()
        {
            ItemCount itemCount = new ItemCount();

            itemCount.Count = _context.tblFileAttachments
                .Where(x => x.DeletedDate == null)
                .Count();
            return await Task.FromResult(itemCount);
        }

        // GET: api/Customers
        [HttpGet("GetByPage")]
        public async Task<ActionResult<IEnumerable<tblFileAttachment>>> GetByPage(int pageSize, int pageNumber)
        {
            List<tblFileAttachment> list = await _context.tblFileAttachments
                //.Where(x => x.DeletedDate == null)
                .ToListAsync();
            list = list.Skip(pageNumber * pageSize).Take(pageSize).ToList();

            return await Task.FromResult(list);
        }

        // GET: api/Customers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<tblFileAttachment>> GetById(int id)
        {
            var item = await _context.tblFileAttachments.Where(p => p.DMDonViSuDungId == _tenantProvider.TenantId).Where(p => p.id == id).FirstOrDefaultAsync();

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
        public async Task<ActionResult<tblFileAttachment>> Put(int id, tblFileAttachment item)
        {
            if (id != item.id)
            {
                return new tblFileAttachment();
            }

            item.UpdatedDate = DateTime.Now;
            _context.Entry(item).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();

                // Log Nhat ky
                await _nhatKyService.LogUpdate("tblFileAttachment");
            }
            catch (DbUpdateConcurrencyException)
            {
                // Log Nhat ky
                await _nhatKyService.LogError("Update_tblFileAttachments", "id : " + id + ";\nitem : " + item.ToString());
                if (!Exists(id))
                {
                    return new tblFileAttachment();
                }
                else
                {
                    return new tblFileAttachment();
                }
            }

            return item;
        }

        // POST: api/Customers
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost("Create")]
        public async Task<ActionResult<tblFileAttachment>> Post(tblFileAttachment item)
        {
            item.CreatedDate = DateTime.Now;
            item.UpdatedDate = DateTime.Now;
            item.DMDonViSuDungId = _tenantProvider.TenantId;
            _context.tblFileAttachments.Add(item);
            await _context.SaveChangesAsync();

            // Log Nhat ky
            await _nhatKyService.LogCreate("tblFileAttachment");
            return item;
        }

        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult<tblFileAttachment>> Delete(int id)
        {
            var item = await _context.tblFileAttachments.Where(p => p.DMDonViSuDungId == _tenantProvider.TenantId).Where(p => p.id == id).FirstOrDefaultAsync();
            if (item == null)
            {
                return NotFound();
            }

            item.DeletedDate = DateTime.Now;
            _context.Entry(item).State = EntityState.Deleted;
            await _context.SaveChangesAsync();

            // Log Nhat ky
            await _nhatKyService.LogDelete("tblFileAttachmens");
            return item;
        }

        private bool Exists(int id)
        {
            return _context.tblFileAttachments.Where(x => x.DeletedDate == null).Any(e => e.id == id);
        }

    }
}
