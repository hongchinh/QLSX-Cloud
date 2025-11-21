using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SaleAPI.Models;
using QLSX.Shared.Models;
using System.Linq.Expressions;
using SaleAPI.Extensions;
using QLSX.Shared.Data.Responses;
using SaleAPI.Interfaces;
using SaleAPI.Services;
using QLSX.Shared.Entities;
using System.Net.WebSockets;
using static MudBlazor.CategoryTypes;

namespace SaleAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhanQuyenController : ControllerBase
    {
        private readonly CRMDBContext _context;
        private readonly ITenantProvider _tenantProvider;
        private readonly INhatKyService _nhatKyService;
        public PhanQuyenController(CRMDBContext context, ITenantProvider tenantProvider, INhatKyService nhatKyService)
        {
            _context = context;
            _tenantProvider = tenantProvider;
            _nhatKyService = nhatKyService;
        }
        // GET: api/Department
        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<QuyenSuDungModel>>> Get()
        {

            var list = await _context.QuyenSuDungRepository
                .Where(x => x.DeletedDate == null)
                .ToListAsync();
            return list.Select(x => new QuyenSuDungModel(x)).ToList();
        }



        [HttpGet("GetCount")]
        public async Task<ActionResult<ItemCount>> GetCount()
        {
            ItemCount itemCount = new ItemCount();

            itemCount.Count = _context.QuyenSuDungRepository
                .Count();
            return await Task.FromResult(itemCount);
        }

        // GET: api/GetByPage
        [HttpGet("GetByPage")]
        public async Task<ActionResult<IEnumerable<QuyenSuDungModel>>> GetByPage(int pageSize, int pageNumber)
        {

            List<QuyenSuDung> list = await _context.QuyenSuDungRepository
                .ToListAsync();
            list = list.Skip(pageNumber * pageSize).Take(pageSize).ToList();

            return await Task.FromResult(list.Select(x => new QuyenSuDungModel(x)).ToList());
        }

        // GET: api/Departments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<List<QuyenSuDungModel>>> GetById(int id)
        {
            var item = await _context.QuyenSuDungRepository.Where(x => x.UserId == id).ToListAsync();

            if (item == null)
            {
                return new List<QuyenSuDungModel>();
            }

            return item.Select(x => new QuyenSuDungModel(x)).ToList();
        }
        // GET: api/Departments/5
        [HttpGet("{userId}/{quyen}")]
        public async Task<ActionResult<QuyenSuDungModel>> GetQuyenByUserId(int userId, string quyen)
        {
            var user = await _context.UserRepository.FindAsync(userId);
            if (user.Quyen == 100)
            {
                return new QuyenSuDungModel() { Selectted = true, Sua = true, Them = true, Xoa = true, XemIn = true };
            }
            var userQuyen = await _context.QuyenSuDungRepository.FirstOrDefaultAsync(x => x.UserId == userId);
            return new QuyenSuDungModel(userQuyen);
        }

        // GET: api/Departments/5
        [HttpGet("/GetAll/{userId}")]
        public async Task<ActionResult<List<QuyenSuDungModel>>> GetAll(int userId)
        {
            var items = await _context.QuyenSuDungRepository
                .Where(x => x.UserId == userId)
                .ToListAsync();
            return await Task.FromResult(items.Select(x => new QuyenSuDungModel(x)).ToList());
        }

        // PUT: api/Departments/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("Update/{id}")]
        public async Task<IActionResult> Put(int id, QuyenSuDungModel item)
        {
            if (id != item.Id)
            {
                return BadRequest();
            }

            _context.Entry(item).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();

                // Log Nhat ky
                await _nhatKyService.LogUpdate("PhanQuyenSuDung");
            }
            catch (DbUpdateConcurrencyException)
            {
                // Log Nhat ky
                await _nhatKyService.LogError("Update_PhanQuyenSuDung", "id : " + id + ";\nitem : " + item.ToString());
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
        // PUT: api/Departments/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("updatelist")]
        public async Task<IActionResult> PutList(List<QuyenSuDungModel> items)
        {
            var lst = await _context.QuyenSuDungRepository.Where(x => items.Select(x => x.Id).ToList().Contains(x.Id)).ToListAsync();

            foreach (var item in lst)
            {
                var newIte = items.FirstOrDefault(x => x.Id == item.Id);
                item.XemIn = newIte.XemIn;
                item.Selectted = newIte.Selectted;
                item.Them = newIte.Them;
                item.Sua = newIte.Sua;
                item.Xoa = newIte.Xoa;
                item.MaSo = newIte.MaSo;
                item.HoTen= newIte.HoTen;
                _context.Entry(item).State = EntityState.Modified;
            }

            try
            {
                await _context.SaveChangesAsync();

                // Log Nhat ky
                await _nhatKyService.LogUpdate("PhanQuyenSuDung");
            }
            catch (DbUpdateConcurrencyException)
            {

                throw;

            }

            return NoContent();
        }
        // PUT: api/Departments/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("updatelistBaoCao")]
        public async Task<IActionResult> PutBaoCaoList(List<PhanQuyenBaoCaoModel> items)
        {
            foreach (var item in items)
            {
                PhanQuyenBaoCao itemNew = await _context.PhanQuyenBaoCaoRepository.FindAsync(item.Id);
                if (itemNew != null)
                {
                    itemNew.UpdatedDate = DateTime.Now;
                    itemNew.HoTen = item.HoTen;
                    itemNew.Selected = item.Selected;
                    itemNew.LoaiBaoCao = item.Loai;
                    itemNew.ReportFiles= item.ReportFile;
                    itemNew.TenBaoCao = item.TenBaoCao;
                    itemNew.UserId = item.UserId;
                    _context.Entry(itemNew).State = EntityState.Modified;
                }

            }

            try
            {
                await _context.SaveChangesAsync();

                // Log Nhat ky
                await _nhatKyService.LogUpdate("PhanQuyenBaoCao");
            }
            catch (DbUpdateConcurrencyException)
            {

                throw;

            }

            return NoContent();
        }
        // POST: api/Customers
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost("Create")]
        public async Task<ActionResult<QuyenSuDungModel>> Post(QuyenSuDungModel item)
        {
            QuyenSuDung itemNew = new QuyenSuDung();

            itemNew.CreatedDate = DateTime.Now;
            itemNew.UpdatedDate = DateTime.Now;
            itemNew.MaSo = item.MaSo;
            itemNew.ChucNang = item.ChucNang;
            itemNew.Them = item.Them;
            itemNew.Xoa = item.Xoa;
            itemNew.Sua = item.Sua;
            itemNew.XemIn = item.XemIn;
            itemNew.UserId = item.UserId;
            _context.QuyenSuDungRepository.Add(itemNew);
            await _context.SaveChangesAsync();

            // Log Nhat ky
            await _nhatKyService.LogCreate("PhanQuyenSuDung");
            return new QuyenSuDungModel(itemNew);
        }

        private bool Exists(int id)
        {
            return _context.QuyenSuDungRepository.Any(e => e.Id == id);
        }

        [HttpGet("GetAllPaged/{userId}")]
        public async Task<ActionResult<GetAllResponse<QuyenSuDungModel>>> GetAllPaged([FromBody] BaseSearchRequest request, int userId)
        {

            GetAllResponse<QuyenSuDungModel> outputs = new GetAllResponse<QuyenSuDungModel>();
            Expression<Func<QuyenSuDung, bool>> filter = m => (1 == 1);
            //if (!string.IsNullOrEmpty(request.Keywords))
            //{
            //    filter = filter.And(x => x..Contains(request.Keywords) || x.TenPhong.Contains(request.Keywords));
            //}
            Func<IQueryable<QuyenSuDung>, IOrderedQueryable<QuyenSuDung>> order = null;
            if (request.SortDirection != QLSX.Shared.Enums.SortDirection.None)
            {
                order = await OrderBy(request.SortLable, request.SortDirection == QLSX.Shared.Enums.SortDirection.Ascending);
            }

            var user = await _context.UserRepository.FindAsync(userId);


            var sql = "INSERT INTO QUYENSUDUNG(USERID,HOTEN,MASO,CHUCNANG,SELECTTED,THEM,SUA,XOA,XEMIN) SELECT " + userId.ToString()
                + ",N'"+ user.HoTen + "',Q.MASO,Q.CHUCNANG,0,0,0,0,0 FROM  PHANQUYEN Q   WHERE Q.MASO NOT IN (SELECT MASO FROM QUYENSUDUNG WHERE USERID =" + userId.ToString() + "  )";

            _context.Database.ExecuteSqlRaw(sql);

            sql = "INSERT INTO PHANQUYENBAOCAO(USERID,HOTEN,LOAIBAOCAO,REPORTFILES,TENBAOCAO,SELECTTED) SELECT " + userId.ToString()
              + ",N'"+ user.HoTen + "',MALOAIBAOCAO,REPORTFILES,TENBAOCAO,0 FROM  BAOCAO B   WHERE B.REPORTFILES NOT IN (SELECT REPORTFILES FROM PHANQUYENBAOCAO WHERE USERID =" + userId + "  )";

            _context.Database.ExecuteSqlRaw(sql);

            IQueryable<QuyenSuDung> query = _context.Set<QuyenSuDung>().Where(x => x.UserId == userId);

            if (filter != null) query = query.Where(filter);
            if (order != null) query = order(query);
            outputs.TotalRecords = await query.CountAsync();
            outputs.TotalPages = (int)Math.Ceiling(outputs.TotalRecords / (double)request.PageSize);
            outputs.Page = request.Page;
            outputs.PageSize = request.PageSize;

            query = query.Skip(request.Page * request.PageSize).Take(request.PageSize);
            outputs.Items = await query.Select (x=> new QuyenSuDungModel(x)).ToListAsync();
            return outputs;

        }
        [HttpGet("GetAllBaoCaoPaged/{userId}")]
        public async Task<ActionResult<GetAllResponse<PhanQuyenBaoCaoModel>>> GetAllBaoCaoPaged([FromBody] BaseSearchRequest request, int userId)
        {

            GetAllResponse<PhanQuyenBaoCaoModel> outputs = new GetAllResponse<PhanQuyenBaoCaoModel>();
            Expression<Func<PhanQuyenBaoCao, bool>> filter = m => (1 == 1);
            //if (!string.IsNullOrEmpty(request.Keywords))
            //{
            //    filter = filter.And(x => x..Contains(request.Keywords) || x.TenPhong.Contains(request.Keywords));
            //}
            Func<IQueryable<PhanQuyenBaoCao>, IOrderedQueryable<PhanQuyenBaoCao>> order = null;
            if (request.SortDirection != QLSX.Shared.Enums.SortDirection.None)
            {
                order = await OrderByBaoCao(request.SortLable, request.SortDirection == QLSX.Shared.Enums.SortDirection.Ascending);
            }

            var user = await _context.UserRepository.FindAsync(userId);
            var sql = "insert into PHANQUYENBAOCAO(UserId,hoten,LOAIBAOCAO,REPORTFILES,TENBAOCAO,SELECTTED ) select " + userId.ToString()
                + ",N'"+ user.HoTen + "',MALOAIBAOCAO,REPORTFILES,TENBAOCAO,0  FROM  BAOCAO WHERE REPORTFILES NOT IN (SELECT REPORTFILES FROM PHANQUYENBAOCAO WHERE USERID =" + userId + "  )";


            _context.Database.ExecuteSqlRaw(sql);

            IQueryable<PhanQuyenBaoCao> query = _context.Set<PhanQuyenBaoCao>().Where(x => x.UserId == userId || userId == 1);

            if (filter != null) query = query.Where(filter).Where(x => x.DeletedDate == null);
            if (order != null) query = order(query);
            outputs.TotalRecords = await query.CountAsync();
            outputs.TotalPages = (int)Math.Ceiling(outputs.TotalRecords / (double)request.PageSize);
            outputs.Page = request.Page;
            outputs.PageSize = request.PageSize;

            query = query.Skip(request.Page * request.PageSize).Take(request.PageSize);
            var lst = await query.ToListAsync();
            outputs.Items = lst.Select(x => new PhanQuyenBaoCaoModel(x)).ToList();
            return outputs;

        }

        private async Task<Func<IQueryable<QuyenSuDung>, IOrderedQueryable<QuyenSuDung>>> OrderBy(string sortBy, bool sortType)
        {
            Func<IQueryable<QuyenSuDung>, IOrderedQueryable<QuyenSuDung>> myFunc;
            //if (sortBy == "MaPhong")
            //{
            //    if (sortType) myFunc = source => source.OrderBy(x => x.te);
            //    else myFunc = source => source.OrderByDescending(x => x.MaPhong);
            //    return myFunc;
            //}
            //if (sortBy == "TenPhong")
            //{
            //    if (sortType) myFunc = source => source.OrderBy(x => x.TenPhong);
            //    else myFunc = source => source.OrderByDescending(x => x.TenPhong);
            //    return myFunc;
            //}
            return null;

        }
        private async Task<Func<IQueryable<PhanQuyenBaoCao>, IOrderedQueryable<PhanQuyenBaoCao>>> OrderByBaoCao(string sortBy, bool sortType)
        {
            Func<IQueryable<PhanQuyenBaoCao>, IOrderedQueryable<PhanQuyenBaoCao>> myFunc;
            //if (sortBy == "MaPhong")
            //{
            //    if (sortType) myFunc = source => source.OrderBy(x => x.te);
            //    else myFunc = source => source.OrderByDescending(x => x.MaPhong);
            //    return myFunc;
            //}
            //if (sortBy == "TenPhong")
            //{
            //    if (sortType) myFunc = source => source.OrderBy(x => x.TenPhong);
            //    else myFunc = source => source.OrderByDescending(x => x.TenPhong);
            //    return myFunc;
            //}
            return null;

        }
    }
}
