using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SaleAPI.Models;
using QLSX.Shared.Models;
using SaleAPI.Interfaces;
using SaleAPI.Services;
using BaoCao = QLSX.Shared.Entities.BaoCao;

namespace SaleAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class BaoCaosController : ControllerBase
    {
        private readonly CRMDBContext _context;
        private readonly ITenantProvider _tenantProvider;
        private readonly INhatKyService _nhatKyService;
        public BaoCaosController(CRMDBContext context, ITenantProvider tenantProvider, INhatKyService nhatKyService)
        {
            _context = context;
            _tenantProvider = tenantProvider;
            _nhatKyService = nhatKyService;
        }
        // GET: api/GetCustomerTypes
        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<BaoCao>>> Get(BaoCaoRequest request)
        {

            //var user = await _context.UserRepository.FindAsync(_tenantProvider.UserId);
            //var role = await _context.Roles.FindAsync(user.RoleId);
            //if (role.RoleDesc == "Admin")
            //{
            //    return await _context.BaoCaoRepository
            //   //.Where(p => p.DMDonViSuDungId == _tenantProvider.TenantId)
            //   .Where(p => p.Loai == request.Loai || string.IsNullOrEmpty(request.Loai))
            //   .Where(x => x.Selected == true)
            //   //.Where(x => x.DeletedDate == null)
            //   //.OrderBy(x => x.OrderById)
            //   .ToListAsync();
            //}
            //else
            //{
            var result = await _context.BaoCaoRepository
                      .Where(p => p.MaLoaiBaoCao == request.Loai || string.IsNullOrEmpty(request.Loai))
                      .Where(p => p.Selected == true)
                      .OrderBy(p => p.Stt).ToListAsync();

            return result;
            //}

        }


        [HttpGet("GetCount")]
        public async Task<ActionResult<ItemCount>> GetCount()
        {
            ItemCount itemCount = new ItemCount();
            itemCount.Count = await _context.BaoCaoRepository
              //.Where(p => p.DMDonViSuDungId == _tenantProvider.TenantId)
              /*  .Where(X => X.DeletedDate == null)*/.CountAsync();
            return await Task.FromResult(itemCount);
        }

        // GET: api/Customers
        [HttpGet("GetByPage")]
        public async Task<ActionResult<IEnumerable<BaoCao>>> GetByPage(int pageSize, int pageNumber)
        {
            List<BaoCao> list = await _context.BaoCaoRepository
                //.Where(p => p.DMDonViSuDungId == _tenantProvider.TenantId)
                /*.Where(X => X.DeletedDate == null)*/.ToListAsync();
            list = list.Skip(pageNumber * pageSize).Take(pageSize).ToList();

            return await Task.FromResult(list);
        }

        // GET: api/Customers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BaoCao>> GetById(int id)
        {
            var item = _context.BaoCaoRepository
                //.Where(p => p.DMDonViSuDungId == _tenantProvider.TenantId)
                .Where(p => p.Id == id).FirstOrDefault();

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
        public async Task<IActionResult> Put(int id, BaoCao item)
        {
            if (id != item.Id)
            {
                return BadRequest();
            }

            //item.UpdatedDate = DateTime.Now;
            _context.Entry(item).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();

                // Log Nhat ky
                await _nhatKyService.LogUpdate("BaoCao");
            }
            catch (DbUpdateConcurrencyException)
            {
                // Log Nhat ky
                await _nhatKyService.LogError("Update_BaoCao", "id : " + id + ";\nitem : " + item.ToString());
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

        // POST: api/Customers
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost("Create")]
        public async Task<ActionResult<BaoCao>> Post(BaoCao item)
        {
            //item.CreatedDate = DateTime.Now;
            //item.UpdatedDate = DateTime.Now;
            //item.DMDonViSuDungId = _tenantProvider.TenantId;
            _context.BaoCaoRepository.Add(item);
            await _context.SaveChangesAsync();

            // Log Nhat ky
            await _nhatKyService.LogCreate("BaoCao");
            return item;
        }

        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult<BaoCao>> Delete(int id)
        {
            var item = _context.BaoCaoRepository
                //.Where(p => p.DMDonViSuDungId == _tenantProvider.TenantId)
                .Where(p => p.Id == id).FirstOrDefault();

            if (item == null)
            {
                return NotFound();
            }

            //item.DeletedDate = DateTime.Now;
            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            // Log Nhat ky
            await _nhatKyService.LogDelete("BaoCao");
            return item;
        }

        private bool Exists(int id)
        {
            return _context.BaoCaoRepository.Any(e => e.Id == id);
        }


        // POST: api/DMHangHoas
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost("UpdateListPrint/{loai}")]
        public async Task<ActionResult<bool>> UpdateListPrint(List<int> items, string loai)
        {
            try
            {
                foreach (int item in items)
                {
                    DuLieuIn duLieuIn = new DuLieuIn();
                    duLieuIn.CreatedDate = DateTime.Now;
                    duLieuIn.UpdatedDate = DateTime.Now;
                    duLieuIn.DMDonViSuDungId = _tenantProvider.TenantId;
                    duLieuIn.UserId = _tenantProvider.UserId;
                    duLieuIn.IdMaSo = item;
                    duLieuIn.Loai = loai;
                    duLieuIn.Id = Guid.NewGuid();
                    _context.DuLieuIns.Add(duLieuIn);
                }

                await _context.SaveChangesAsync();

                // Log Nhat ky
                await _nhatKyService.LogCreate("DuLieuIns");
                return true;
            }
            catch (Exception ex)
            {
                await _nhatKyService.LogError("DuLieuIn", ex.Message);
                return false;
            }
        }

        // POST: api/DMHangHoas
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost("ClearListPrint")]
        public async Task<ActionResult<bool>> ClearListPrint()
        {
            try
            {
                var items = _context.DuLieuIns
                    .Where(x => x.DMDonViSuDungId == _tenantProvider.TenantId)
                    .Where(x => x.UserId == _tenantProvider.UserId);
                _context.DuLieuIns.RemoveRange(items);
                await _context.SaveChangesAsync();

                // Log Nhat ky
                await _nhatKyService.LogDelete("DuLieuIns");
                return true;
            }
            catch (Exception ex)
            {
                await _nhatKyService.LogError("DuLieuIn", ex.Message);
                return false;
            }
        }
    }
}
