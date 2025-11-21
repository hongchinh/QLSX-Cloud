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
using System.Linq.Expressions;
using SaleAPI.Extensions;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using QLSX.Shared.Entities;
using static MudBlazor.CategoryTypes;
using QLSX.Shared.Models.Request;

namespace SaleAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DMSoCTsController : ControllerBase
    {
        private readonly CRMDBContext _context;
        private readonly ITenantProvider _tenantProvider;
        private readonly INhatKyService _nhatKyService;
        public Microsoft.Extensions.Configuration.IConfiguration _configuration { get; }

        public DMSoCTsController(CRMDBContext context, ITenantProvider tenantProvider, INhatKyService nhatKyService, Microsoft.Extensions.Configuration.IConfiguration configuration)
        {
            _context = context;
            _tenantProvider = tenantProvider;
            _nhatKyService = nhatKyService;
            _configuration = configuration;
        }
        // GET: api/GetCustomerTypes
        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<DanhMucSoChungTuModel>>> Get(SearchRequest request)
        {
            return await _context.DanhMucSoChungTuRepository
                .Where(x => x.DeletedDate == null)
                .Select(item => new DanhMucSoChungTuModel(item))
                .ToListAsync();
        }

        [HttpGet("GetAllPaged")]
        public async Task<ActionResult<GetAllResponse<DanhMucSoChungTuModel>>> GetAllPaged([FromBody] BaseSearchRequest request)
        {

            GetAllResponse<DanhMucSoChungTuModel> outputs = new GetAllResponse<DanhMucSoChungTuModel>();
            Expression<Func<DanhMucSoChungTu, bool>> filter = m => (1 == 1);
            if (!string.IsNullOrEmpty(request.Keywords))
            {
                filter = filter.And(x => x.KyHieuChungTu.Contains(request.Keywords) || x.LoaiChungTu.Contains(request.Keywords) || x.GhiChu.Contains(request.Keywords));
            }
            Func<IQueryable<DanhMucSoChungTu>, IOrderedQueryable<DanhMucSoChungTu>> order = null;
            if (request.SortDirection != QLSX.Shared.Enums.SortDirection.None)
            {
                order = await OrderBy(request.SortLable, request.SortDirection == QLSX.Shared.Enums.SortDirection.Ascending);
            }

            IQueryable<DanhMucSoChungTu> query = _context.DanhMucSoChungTuRepository;

            if (filter != null) query = query.Where(filter).Where(x => x.DeletedDate == null);
            if (order != null) query = order(query);
            outputs.TotalRecords = await query.CountAsync();
            outputs.TotalPages = (int)Math.Ceiling(outputs.TotalRecords / (double)request.PageSize);
            outputs.Page = request.Page;
            outputs.PageSize = request.PageSize;

            query = query.Skip(request.Page * request.PageSize).Take(request.PageSize);
            var rawQuery = await query.ToListAsync();
            outputs.Items = rawQuery.Select(item => new DanhMucSoChungTuModel(item)).ToList();
            return outputs;

        }
        private async Task<Func<IQueryable<DanhMucSoChungTu>, IOrderedQueryable<DanhMucSoChungTu>>> OrderBy(string sortBy, bool sortType)
        {
            Func<IQueryable<DanhMucSoChungTu>, IOrderedQueryable<DanhMucSoChungTu>> myFunc;
            if (sortBy == "KyHieu")
            {
                if (sortType) myFunc = source => source.OrderBy(x => x.KyHieuChungTu);
                else myFunc = source => source.OrderByDescending(x => x.KyHieuChungTu);
                return myFunc;
            }
            if (sortBy == "LoaiCT")
            {
                if (sortType) myFunc = source => source.OrderBy(x => x.LoaiChungTu);
                else myFunc = source => source.OrderByDescending(x => x.LoaiChungTu);
                return myFunc;
            }
            if (sortBy == "GhiChu")
            {
                if (sortType) myFunc = source => source.OrderBy(x => x.GhiChu);
                else myFunc = source => source.OrderByDescending(x => x.GhiChu);
                return myFunc;
            }
            if (sortBy == "DoDai")
            {
                if (sortType) myFunc = source => source.OrderBy(x => x.GhiChu);
                else myFunc = source => source.OrderByDescending(x => x.GhiChu);
                return myFunc;
            }
            return null;

        }

        [HttpGet("GetCount")]
        public async Task<ActionResult<ItemCount>> GettCount()
        {
            ItemCount itemCount = new ItemCount();

            itemCount.Count = _context.DanhMucSoChungTuRepository.Where(x => x.DeletedDate == null).Count();
            return await Task.FromResult(itemCount);
        }

        // GET: api/Customers
        [HttpGet("GetByPage")]
        public async Task<ActionResult<IEnumerable<DanhMucSoChungTuModel>>> GetByPage(int pageSize, int pageNumber)
        {
            var list = await _context.DanhMucSoChungTuRepository
                .Where(x => x.DeletedDate == null)
                .Select(item => new DanhMucSoChungTuModel(item))
                .ToListAsync();
            list = list.Skip(pageNumber * pageSize).Take(pageSize).ToList();

            return await Task.FromResult(list);
        }

        // GET: api/Customers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DanhMucSoChungTuModel>> GetById(int id)
        {
            var item = await _context.DanhMucSoChungTuRepository/*.Where(p => p.DMDonViSuDungId == _tenantProvider.TenantId)*/.Where(p => p.Id == id).FirstOrDefaultAsync();

            if (item == null)
            {
                return NotFound();
            }

            return new DanhMucSoChungTuModel(item);
        }

        // PUT: api/Customers/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("Update/{id}")]
        public async Task<ActionResult<DanhMucSoChungTu>> Put(int id, DanhMucSoChungTuModel model)
        {
            if (id != model.Id)
            {
                return new DanhMucSoChungTu();
            }

            var entity = await _context.DanhMucSoChungTuRepository.FirstOrDefaultAsync(item => item.Id == id);
            entity.UpdatedDate = DateTime.Now;
            entity.LoaiChungTu = model.LoaiCT;
            entity.DoDai = model.DoDai;
            entity.KyHieuChungTu = model.KyHieu;
            entity.GhiChu = model.GhiChu;
            try
            {
                await _context.SaveChangesAsync();

                // Log Nhat ky
                await _nhatKyService.LogUpdate("DMSoCT");
            }
            catch (DbUpdateConcurrencyException)
            {
                // Log Nhat ky
                await _nhatKyService.LogError("Update_DMDMSoCT", "id : " + id + ";\nitem : " + model.ToString());
                if (!Exists(id))
                {
                    return new DanhMucSoChungTu();
                }
                else
                {
                    return new DanhMucSoChungTu();
                }
            }

            return entity;
        }

        // POST: api/Customers
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost("Create")]
        public async Task<ActionResult<DanhMucSoChungTu>> Post(DanhMucSoChungTuModel model)
        {
            DanhMucSoChungTu entity = new DanhMucSoChungTu();
            entity.LoaiChungTu = model.LoaiCT;
            entity.DoDai = model.DoDai;
            entity.KyHieuChungTu = model.KyHieu;
            entity.GhiChu = model.GhiChu;
            entity.CreatedDate = DateTime.Now;
            entity.UpdatedDate = DateTime.Now;
            _context.DanhMucSoChungTuRepository.Add(entity);
            await _context.SaveChangesAsync();

            // Log Nhat ky
            await _nhatKyService.LogCreate("DMSoCT");
            return entity;
        }

        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult<DanhMucSoChungTu>> Delete(int id)
        {
            var item = await _context.DanhMucSoChungTuRepository.FirstOrDefaultAsync(p => p.Id == id);
            if (item == null)
            {
                return NotFound();
            }

            item.DeletedDate = DateTime.Now;
            await _context.SaveChangesAsync();

            // Log Nhat ky
            await _nhatKyService.LogDelete("DMSoCT");
            return item;
        }

        private bool Exists(int id)
        {
            return _context.DanhMucSoChungTuRepository.Any(e => e.Id == id);
        }

        [HttpGet("getCode/{code}")]
        public async Task<ActionResult<DanhMucSoChungTuModel>> GetByCode(string code)
        {
            if (string.IsNullOrEmpty(code))
            {
                return NotFound();
            }
            var item = await _context.DanhMucSoChungTuRepository.FirstOrDefaultAsync(p => p.LoaiChungTu.ToLower() == code.ToLower() && p.DeletedDate == null);

            if (item == null)
            {
                return NotFound();
            }
            return new DanhMucSoChungTuModel(item);
        }

        [HttpGet("GetSoChungTu")]
        public async Task<ActionResult<string>> GetSoChungTu(GetSoChungTuRequest request)
        {

            int soctnx = 0;
            var item = await _context.DanhMucSoChungTuRepository
                .FirstOrDefaultAsync(x => request.Loai == x.LoaiChungTu);

            if (item == null)
            {
                return "";
            }
            switch (item.LoaiChungTu.ToLower())
            {
                case "nhap" or "xuat" or "nhaptra" or "xuattra":
                    try
                    {
                        soctnx = 0;
                        string StoredProc = "";
                        StoredProc = string.Format("select dbo.GetSoChungTuMax('{0}',{1})", request.Loai, _tenantProvider.TenantId);
                        DataTable dt = new DataTable();
                        string sqlConn = _configuration.GetConnectionString("CRMConnectStrings");
                        using (SqlDataAdapter dataAdapter = new SqlDataAdapter(StoredProc, sqlConn))
                        {
                            // create the DataSet 
                            DataSet dataSet = new DataSet();
                            // fill the DataSet using our DataAdapter 
                            dataAdapter.Fill(dataSet);
                            dt = dataSet.Tables[0];
                        }

                        if (dt.Rows.Count > 0)
                        {
                            soctnx = int.Parse(dt.Rows[0][0].ToString());
                        }
                    }
                    catch (Exception ex)
                    {
                        soctnx = 0;
                    }

                    return item.KyHieuChungTu + (soctnx + 1).ToString().PadLeft(item.DoDai ?? 0, '0');

                case "donnhap" or "donxuat":
                    soctnx = 0;
                    try
                    {
                        string StoredProc = "";
                        StoredProc = string.Format("select dbo.GetSoChungTuMax('{0}',{1})", request.Loai, _tenantProvider.TenantId);
                        DataTable dt = new DataTable();
                        string sqlConn = _configuration.GetConnectionString("CRMConnectStrings");
                        using (SqlDataAdapter dataAdapter = new SqlDataAdapter(StoredProc, sqlConn))
                        {
                            // create the DataSet 
                            DataSet dataSet = new DataSet();
                            // fill the DataSet using our DataAdapter 
                            dataAdapter.Fill(dataSet);
                            dt = dataSet.Tables[0];
                        }

                        if (dt.Rows.Count > 0)
                        {
                            soctnx = int.Parse(dt.Rows[0][0].ToString());
                        }
                    }
                    catch (Exception ex)
                    {
                        soctnx = 0;
                    }
                    return item.KyHieuChungTu + (soctnx + 1).ToString().PadLeft(item.DoDai ?? 0, '0');
                case "dieuchuyen" or "dieuchuyenton":
                    try
                    {
                        soctnx = 0;
                        string StoredProc = "";
                        StoredProc = string.Format("select dbo.GetSoChungTuMax('{0}',{1})", request.Loai, _tenantProvider.TenantId);
                        DataTable dt = new DataTable();
                        string sqlConn = _configuration.GetConnectionString("CRMConnectStrings");
                        using (SqlDataAdapter dataAdapter = new SqlDataAdapter(StoredProc, sqlConn))
                        {
                            // create the DataSet 
                            DataSet dataSet = new DataSet();
                            // fill the DataSet using our DataAdapter 
                            dataAdapter.Fill(dataSet);
                            dt = dataSet.Tables[0];
                        }

                        if (dt.Rows.Count > 0)
                        {
                            soctnx = int.Parse(dt.Rows[0][0].ToString());
                        }
                    }
                    catch (Exception ex)
                    {
                        soctnx = 0;
                    }
                    return item.KyHieuChungTu + (soctnx + 1).ToString().PadLeft(item.DoDai ?? 0, '0');
                case "nhapton" or "xuatton" or "xuattontra" or "nhaptontra":
                    try
                    {
                        soctnx = 0;
                        string StoredProc = "";
                        StoredProc = string.Format("select dbo.GetSoChungTuMax('{0}',{1})", request.Loai, _tenantProvider.TenantId);
                        DataTable dt = new DataTable();
                        string sqlConn = _configuration.GetConnectionString("CRMConnectStrings");
                        using (SqlDataAdapter dataAdapter = new SqlDataAdapter(StoredProc, sqlConn))
                        {
                            // create the DataSet 
                            DataSet dataSet = new DataSet();
                            // fill the DataSet using our DataAdapter 
                            dataAdapter.Fill(dataSet);
                            dt = dataSet.Tables[0];
                        }

                        if (dt.Rows.Count > 0)
                        {
                            soctnx = int.Parse(dt.Rows[0][0].ToString());
                        }
                    }
                    catch (Exception ex)
                    {
                        soctnx = 0;
                    }
                    return item.KyHieuChungTu + (soctnx + 1).ToString().PadLeft(item.DoDai ?? 0, '0');
                case "thutm" or "chitm":
                    try
                    {
                        soctnx = 0;
                        string StoredProc = "";
                        StoredProc = string.Format("select dbo.GetSoChungTuMax('{0}',{1})", request.Loai, _tenantProvider.TenantId);
                        DataTable dt = new DataTable();
                        string sqlConn = _configuration.GetConnectionString("CRMConnectStrings");
                        using (SqlDataAdapter dataAdapter = new SqlDataAdapter(StoredProc, sqlConn))
                        {
                            // create the DataSet 
                            DataSet dataSet = new DataSet();
                            // fill the DataSet using our DataAdapter 
                            dataAdapter.Fill(dataSet);
                            dt = dataSet.Tables[0];
                        }

                        if (dt.Rows.Count > 0)
                        {
                            soctnx = int.Parse(dt.Rows[0][0].ToString());
                        }
                    }
                    catch (Exception ex)
                    {
                        soctnx = 0;
                    }
                    return item.KyHieuChungTu + (soctnx + 1).ToString().PadLeft(item.DoDai ?? 0, '0');
                case "dmhanghoa":
                    var lsthanghoa = await _context.DanhMucHangHoaRepository
                           .Where(x => x.DeletedDate == null)
                           //.Where(p => p.DMDonViSuDungId == _tenantProvider.TenantId)
                           .Where(x => x.MaHangHoa.Length >= item.DoDai)
                          .OrderByDescending(p => p.MaHangHoa)
                          .FirstOrDefaultAsync();
                    var socthh = "";
                    int temphh = 0;
                    if (lsthanghoa != null)
                    {

                        if (lsthanghoa.MaHangHoa.Length >= item.DoDai)
                        {
                            socthh = lsthanghoa.MaHangHoa.Substring(lsthanghoa.MaHangHoa.Length - item.DoDai ?? 0, item.DoDai ?? 0);
                        }
                    }
                    int.TryParse(socthh, out temphh);
                    return item.KyHieuChungTu + (temphh + 1).ToString().PadLeft(item.DoDai ?? 0, '0');

                case "dmhanghoaton":
                    var lsthanghoaton = await _context.DanhMucHangHoaTonCuonRepository
                            .Where(x => x.DeletedDate == null)
                           //.Where(p => p.DMDonViSuDungId == _tenantProvider.TenantId)
                           .Where(x => x.MaHangHoa.Length >= item.DoDai)
                          .OrderByDescending(p => p.MaHangHoa)
                          .FirstOrDefaultAsync();
                    var soctton1 = "";
                    int tempton1 = 0;
                    if (lsthanghoaton != null)
                    {

                        if (lsthanghoaton.MaHangHoa.Length >= item.DoDai)
                        {
                            soctton1 = lsthanghoaton.MaHangHoa.Substring(lsthanghoaton.MaHangHoa.Length - item.DoDai ?? 0, item.DoDai ?? 0);
                        }
                    }
                    int.TryParse(soctton1, out tempton1);
                    return item.KyHieuChungTu + (tempton1 + 1).ToString().PadLeft(item.DoDai ?? 0, '0');

                case "dmkhachhang":
                    var lskh = await _context.DanhMucKhachHangRepository
                            .Where(x => x.DeletedDate == null)
                           //.Where(p => p.DMDonViSuDungId == _tenantProvider.TenantId)
                           .Where(x => x.MaDonVi.Length >= item.DoDai)
                          .OrderByDescending(p => p.MaDonVi)
                          .FirstOrDefaultAsync();
                    var soctkh = "";
                    int tempkh = 0;
                    if (lskh != null)
                    {

                        if (lskh.MaDonVi.Length >= item.DoDai)
                        {
                            soctkh = lskh.MaDonVi.Substring(lskh.MaDonVi.Length - item.DoDai ?? 0, item.DoDai ?? 0);
                        }
                    }
                    int.TryParse(soctkh, out tempkh);
                    return item.KyHieuChungTu + (tempkh + 1).ToString().PadLeft(item.DoDai ?? 0, '0');
                default:
                    return "";
            }
        }
    }

    public class Orders
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public decimal Amount { get; set; }
    }
}
