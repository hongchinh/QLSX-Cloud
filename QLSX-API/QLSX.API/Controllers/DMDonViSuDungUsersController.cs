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

namespace SaleAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DMDonViSuDungUsersController : ControllerBase
    {
        private readonly CRMDBContext _context;

        public DMDonViSuDungUsersController(CRMDBContext context)
        {
            _context = context;
        }
        // GET: api/Permissions
        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<DMDonViSuDungUser>>> Get()
        {
            List<DMDonViSuDungUser> lst = new List<DMDonViSuDungUser>();
            var data = await _context.DMDonViSuDungs.ToListAsync();
            foreach (DMDonViSuDung item in data)
            {
                DMDonViSuDungUser vm = new DMDonViSuDungUser();
                vm.Id = item.Id;
                vm.TenDonVi = item.TenDonVi;
               // List<User> user = await _context.Users.Where(p => p.DMDonViSuDungId == item.Id).ToListAsync();
                //vm.Users.AddRange(user);
                lst.Add(vm);
            }
            return lst;

        }
        
        //// GET: api/GetPermission
        //[HttpGet("GetPermission")]
        //public async Task<ActionResult<IEnumerable<UserVM>>> GetPermission(PermissionVM request)
        //{
        //    if (request.type == "R")
        //    {
        //        var query =
        //            from ue in _context.Users
        //            join ty in _context.PermissionRegions on ue.UserId equals ty.UserId into cus_ty
        //            from ty in cus_ty.DefaultIfEmpty()

        //            join rol in _context.Roles on ue.RoleId equals rol.RoleId into ue_rol
        //            from rol in ue_rol.DefaultIfEmpty()

        //            where ty.RegionId  == request.Id
        //            select new { ue, ty, rol };
        //        var data = await query.Select(x => new UserVM()
        //        {
        //            UserId = x.ue.UserId,
        //            FirstName = x.ue.FirstName,
        //            EmailAddress = x.ue.EmailAddress,
        //            RoleId = x.ue.RoleId,
        //            RoleName = x.rol.RoleDesc,
        //            IsActive = x.ue.IsActive
        //        }).ToListAsync();
        //        return data;
        //      }
        //    else
        //    {
        //        var query =
        //           from ue in _context.Users
        //           join ty in _context.PermissionDepartments on ue.UserId equals ty.UserId into cus_ty
        //           from ty in cus_ty.DefaultIfEmpty()

        //           join rol in _context.Roles on ue.RoleId equals rol.RoleId into ue_rol
        //           from rol in ue_rol.DefaultIfEmpty()
        //           where ty.DepartmentId == request.Id

        //           select new { ue, ty, rol };
        //        var data = await query.Select(x => new UserVM()
        //        {
        //            UserId = x.ue.UserId,
        //            FirstName = x.ue.FirstName,
        //            EmailAddress = x.ue.EmailAddress,
        //            RoleId = x.ue.RoleId,
        //            RoleName = x.rol.RoleDesc,
        //            IsActive = x.ue.IsActive
        //        }).ToListAsync();
        //        return data;
        //    }


        //}
        //[HttpGet("GetCount")]
        //public async Task<ActionResult<ItemCount>> GetCount()
        //{
        //    ItemCount itemCount = new ItemCount();

        //    itemCount.Count = _context.PermissionRegions.Count();
        //    return await Task.FromResult(itemCount);
        //}

        //// GET: api/PermissionRegion/GetByPage
        //[HttpGet("GetByPage")]
        //public async Task<ActionResult<IEnumerable<PermissionRegion>>> GetByPage(int pageSize, int pageNumber)
        //{

        //    List<PermissionRegion> list = await _context.PermissionRegions.ToListAsync();
        //    list = list.Skip(pageNumber * pageSize).Take(pageSize).ToList();

        //    return await Task.FromResult(list);
        //}

        //// GET: api/PermissionRegion/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<PermissionRegion>> GetById(int id)
        //{
        //    var item = await _context.PermissionRegions.Where(p => p.DMDonViSuDungId == _tenantProvider.TenantId).Where(p => p.Id == id).FirstOrDefaultAsync();

        //    if (item == null)
        //    {
        //        return NotFound();
        //    }

        //    return item;
        //}

        //// PUT: api/PermissionRegions/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for
        //// more details see https://aka.ms/RazorPagesCRUD.
        //[HttpPut("Update/{id}")]
        //public async Task<IActionResult> Put(int id, PermissionRegion item)
        //{
        //    _context.Entry(item).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {

        //        throw;

        //    }

        //    return NoContent();
        //}

        //// POST: api/PermissionDepartments
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for
        //// more details see https://aka.ms/RazorPagesCRUD.
        //[HttpPost("Create")]
        //public async Task<ActionResult<PermissionRegion>> Post(PermissionRegion item)
        //{
        //    _context.PermissionRegions.Add(item);
        //    await _context.SaveChangesAsync();

        //    return item;
        //}

        //// DELETE: api/PermissionDepartments/delete/5
        //[HttpDelete("Delete/{id}")]
        //public async Task<ActionResult<PermissionRegion>> Delete(int id)
        //{
        //    var item = await _context.PermissionRegions.Where(p => p.DMDonViSuDungId == _tenantProvider.TenantId).Where(p => p.Id == id).FirstOrDefaultAsync();
        //    if (item == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.PermissionRegions.Remove(item);
        //    await _context.SaveChangesAsync();

        //    return item;
        //}



    }
}
