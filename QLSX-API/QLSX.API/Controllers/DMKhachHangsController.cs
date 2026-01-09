using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SaleAPI.Models;
using QLSX.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;
using System.Linq.Expressions;
using SaleAPI.Extensions;
using SaleAPI.Interfaces;
using SaleAPI.Services;
using Sale.API.Extensions;
using MudBlazor;
using QLSX.Shared.Entities;
using QLSX.Shared.Models.Request;

namespace SaleAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DMKhachHangsController : ControllerBase
    {
        private readonly CRMDBContext _context;
        private readonly MHDBContext _context1;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly INhatKyService _nhatKyService;
        private readonly ITenantProvider _tenantProvider;

        public Microsoft.Extensions.Configuration.IConfiguration Configuration { get; }

        public DMKhachHangsController(CRMDBContext context,
            Microsoft.Extensions.Configuration.IConfiguration configuration,
            IWebHostEnvironment webHostEnvironment,
            INhatKyService nhatKyService,
            ITenantProvider tenantProvider,
            MHDBContext context1)
        {
            _context = context;
            Configuration = configuration;
            _webHostEnvironment = webHostEnvironment;
            _nhatKyService = nhatKyService;
            _tenantProvider = tenantProvider;
            _context1 = context1;
        }

        [HttpGet("GetCount")]
        public async Task<ActionResult<ItemCount>> GettCount()
        {
            ItemCount itemCount = new ItemCount();

            itemCount.Count = _context.DanhMucKhachHangRepository./*Where(p => p.DMDonViSuDungId == _tenantProvider.TenantId).*/Count();
            return await Task.FromResult(itemCount);
        }

        // GET: api/Customers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DanhMucKhachHangModel>> GetById(int id)
        {
            var item = await _context.DanhMucKhachHangRepository
                //.Where(p => p.DMDonViSuDungId == _tenantProvider.TenantId)
                .Where(x => x.Id == id && x.DeletedDate == null)
                .Select(item => new DanhMucKhachHangModel(item))
                .FirstOrDefaultAsync();

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
        public async Task<ActionResult<DanhMucKhachHangModel>> Put(int id, DanhMucKhachHangModel model)
        {
            var entity = await _context.DanhMucKhachHangRepository.FirstOrDefaultAsync(item => item.DeletedDate == null && item.Id == id);
            if (entity == null)
            {
                return new DanhMucKhachHangModel();
            }
            entity.MaDonVi = model.MaDonVi;
            entity.TenDonVi = model.TenDonVi;
            entity.DienThoai = model.DienThoai;
            entity.DiaChi = model.DiaChi;
            entity.SoTaiKhoan = model.SoTaiKhoan;
            entity.NoiMoTaiKhoan = model.TenNganHang;
            entity.MaSoThue = model.MaSoThue;
            entity.Website = model.Website;
            entity.HanMucDuNo = (decimal?)model.HanMucDuNo;
            entity.MaNhom = model.MaNhom;
            entity.MaTinh = model.MaTinh;
            entity.UpdatedDate = DateTime.Now;
            try
            {
                await _context.SaveChangesAsync();

                // Log Nhat ky
                await _nhatKyService.LogUpdate("DMKhachHang");
            }
            catch (DbUpdateConcurrencyException)
            {
                // Log Nhat ky
                await _nhatKyService.LogError("Update_DMKhachHang", "id : " + id + ";\nitem : " + model.ToString());
                if (!Exists(id))
                {
                    return new DanhMucKhachHangModel();
                }
                else
                {
                    return new DanhMucKhachHangModel();
                }
            }

            return model;
        }
        // POST: api/Customers
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost("Create")]
        public async Task<ActionResult<DanhMucKhachHang>> Post(DanhMucKhachHangModel model)
        {
            DanhMucKhachHang entity = new();
            entity.MaDonVi = model.MaDonVi;
            entity.TenDonVi = model.TenDonVi;
            entity.DienThoai = model.DienThoai;
            entity.DiaChi = model.DiaChi;
            entity.MaTinh = model.MaTinh;
            entity.MaNhom = model.MaNhom;
            entity.SoTaiKhoan = model.SoTaiKhoan;
            entity.NoiMoTaiKhoan = model.TenNganHang;
            entity.MaSoThue = model.MaSoThue;
            entity.Website = model.Website;
            entity.HanMucDuNo = (decimal?)model.HanMucDuNo;
            entity.CreatedDate = DateTime.Now;
            entity.UpdatedDate = DateTime.Now;
            //model.DMDonViSuDungId = _tenantProvider.TenantId;
            _context.DanhMucKhachHangRepository.Add(entity);
            
            // Tự động set TenantId cho entity
            _context.SetTenantIdForEntities(_tenantProvider);
            
            await _context.SaveChangesAsync();

            // Log Nhat ky
            await _nhatKyService.LogCreate("DMKhachHang");

            return entity;
        }

        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult<DanhMucKhachHang>> Delete(int id)
        {
            var item = await _context.DanhMucKhachHangRepository
                 //.Where(p => p.DMDonViSuDungId == _tenantProvider.TenantId)
                 .FirstOrDefaultAsync(p => p.Id == id);
            if (item == null)
            {
                return NotFound();
            }

            item.DeletedDate = DateTime.Now;
            await _context.SaveChangesAsync();

            // Log Nhat ky
            await _nhatKyService.LogDelete("DMKhachHang");
            return item;
        }

        private bool Exists(int id)
        {
            return _context.DanhMucKhachHangRepository.Any(e => e.Id == id);
        }

        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<DanhMucKhachHangModel>>> Get(SearchRequest request)
        {
            return await _context.DanhMucKhachHangRepository
                //.Include(p => p.DanhMucNhomKhachHangRepository)
                //.Include(p => p.DanhMucTinhThanhRepository)
                //.Where(p => p.DMDonViSuDungId == _tenantProvider.TenantId)
                .Where(x => x.DeletedDate == null)
                .Select(item => new DanhMucKhachHangModel(item))
                .ToListAsync();
        }

        [HttpGet("ExportToExcel")]
        public async Task<ActionResult<GetAllResponse<DanhMucKhachHangModel>>> ExportToExcel([FromBody] KhachHangSearchRequest request)
        {
            GetAllResponse<DanhMucKhachHangModel> outputs = await GetData(request, false);

            // Log Nhat Ky
            await _nhatKyService.LogExportExcel("DMKhachHang");
            return outputs;
        }

        [HttpGet("GetAllPaged")]
        public async Task<ActionResult<GetAllResponse<DanhMucKhachHangModel>>> GetAllPaged([FromBody] KhachHangSearchRequest request)
        {
            GetAllResponse<DanhMucKhachHangModel> outputs = await GetData(request, true);
            return outputs;
        }

        private async Task<GetAllResponse<DanhMucKhachHangModel>> GetData(KhachHangSearchRequest request, bool isPaging)
        {
            GetAllResponse<DanhMucKhachHangModel> outputs = new GetAllResponse<DanhMucKhachHangModel>();
            Expression<Func<DanhMucKhachHang, bool>> filter = m => (m.DeletedDate == null);

            if (!string.IsNullOrEmpty(request.MaTinh))
            {
                filter = filter.And(x => x.MaTinh == request.MaTinh);
            }
            if (!string.IsNullOrEmpty(request.MaNhom))
            {
                filter = filter.And(x => x.MaNhom == request.MaNhom);
            }
            if (!string.IsNullOrEmpty(request.MaDonVi))
            {
                filter = filter.And(x => x.MaDonVi.Contains(request.MaDonVi));
            }
            if (!string.IsNullOrEmpty(request.TenDonVi))
            {
                filter = filter.And(x => x.TenDonVi.Contains(request.TenDonVi));
            }
            if (!string.IsNullOrEmpty(request.DiaChi))
            {
                filter = filter.And(x => x.DiaChi.Contains(request.DiaChi));
            }
            if (!string.IsNullOrEmpty(request.DienThoai))
            {
                filter = filter.And(x => x.DienThoai.Contains(request.DienThoai));
            }
            Func<IQueryable<DanhMucKhachHang>, IOrderedQueryable<DanhMucKhachHang>> order = null;
            if (request.SortDirection != QLSX.Shared.Enums.SortDirection.None)
            {
                order = await OrderBy(request.SortLable, request.SortDirection == QLSX.Shared.Enums.SortDirection.Ascending);
            }

            IQueryable<DanhMucKhachHang> query = _context.DanhMucKhachHangRepository.Where(item => item.DeletedDate == null);

            ICollection<FilterDefinition<DanhMucKhachHang>> filter1 = request.Filter;
            FilterBuider<DanhMucKhachHang> filterBuider;
            if (filter1 != null)
            {
                foreach (var f in filter1)
                {
                    if (f.Field == "TenNhom")
                    {
                        var f1 = GetFilterTenNhom(f);
                        query = query.Where(f1);

                    }
                    else
                    {
                        filterBuider = new FilterBuider<DanhMucKhachHang>(f);
                        var filterFunc = filterBuider.GetFilter;
                        query = query.Where(filterFunc);
                    }

                }
            }

            if (filter != null) query = query.Where(filter);
            if (order != null) query = order(query);
            outputs.TotalRecords = await query.CountAsync();
            outputs.TotalPages = (int)Math.Ceiling(outputs.TotalRecords / (double)request.PageSize);
            outputs.Page = request.Page;
            outputs.PageSize = request.PageSize;

            if (isPaging)
            {
                query = query.Skip(request.Page * request.PageSize).Take(request.PageSize);
            }

            var rawData = await (from khachHang in query
                                 join nhomKhach in _context.DanhMucNhomKhachHangRepository.Where(item => item.DeletedDate == null)
                                 on khachHang.MaNhom equals nhomKhach.MaNhom into nhomKhachLeftQuery
                                 from nhomKhachLeft in nhomKhachLeftQuery.DefaultIfEmpty()
                                 join tinhThanh in _context.DanhMucTinhThanhRepository.Where(item => item.DeletedDate == null)
                                 on khachHang.MaTinh equals tinhThanh.MaKhuVuc into tinhThanhLeftQuery
                                 from tinhThanhLeft in tinhThanhLeftQuery.DefaultIfEmpty()
                                 select new DanhMucKhachHangModel
                                 (
                                     khachHang,
                                     nhomKhachLeft,
                                     tinhThanhLeft
                                 )).ToListAsync();
            outputs.Items = rawData;
            return outputs;
        }

        private Expression<Func<DanhMucKhachHang, bool>> GetFilterTenNhom(FilterDefinition<DanhMucKhachHang> f)
        {
            Expression<Func<DanhMucKhachHang, bool>> filter = m => (1 == 1);
            var Operator = f.Operator;
            return filter;
            //return Operator switch
            //{
            //    FilterOperator.String.Contains when f.Value != null =>
            //       filter.And(x => x.DanhMucNhomKhachHangRepository.TenNhom.Contains(f.Value.ToString())),
            //    FilterOperator.String.NotContains when f.Value != null =>
            //      filter.And(x => !x.DanhMucNhomKhachHangRepository.TenNhom.Contains(f.Value.ToString())),
            //    FilterOperator.String.NotContains when f.Value != null =>
            //       filter.And(x => x.DanhMucNhomKhachHangRepository.TenNhom.Contains(f.Value.ToString())),
            //    FilterOperator.String.Equal when f.Value != null =>
            //       filter.And(x => x.DanhMucNhomKhachHangRepository.TenNhom.Equals(f.Value.ToString())),
            //    FilterOperator.String.NotEqual when f.Value != null =>
            //      filter.And(x => !x.DanhMucNhomKhachHangRepository.TenNhom.Equals(f.Value.ToString())),
            //    FilterOperator.String.StartsWith when f.Value != null =>
            //       filter.And(x => x.DanhMucNhomKhachHangRepository.TenNhom.StartsWith(f.Value.ToString())),
            //    FilterOperator.String.EndsWith when f.Value != null =>
            //       filter.And(x => x.DanhMucNhomKhachHangRepository.TenNhom.EndsWith(f.Value.ToString())),
            //    FilterOperator.String.Empty =>
            //       filter.And(x => string.IsNullOrEmpty(x.DanhMucNhomKhachHangRepository.TenNhom)),
            //    _ => filter.And(x => 1 == 1),
            //};
        }
        private async Task<Func<IQueryable<DanhMucKhachHang>, IOrderedQueryable<DanhMucKhachHang>>> OrderBy(string sortBy, bool sortType)
        {
            Func<IQueryable<DanhMucKhachHang>, IOrderedQueryable<DanhMucKhachHang>> myFunc;
            if (sortBy == "MaDonVi")
            {
                if (sortType) myFunc = source => source.OrderBy(x => x.MaDonVi);
                else myFunc = source => source.OrderByDescending(x => x.MaDonVi);
                return myFunc;
            }
            if (sortBy == "TenDonVi")
            {
                if (sortType) myFunc = source => source.OrderBy(x => x.TenDonVi);
                else myFunc = source => source.OrderByDescending(x => x.TenDonVi);
                return myFunc;
            }
            if (sortBy == "DiaChi")
            {
                if (sortType) myFunc = source => source.OrderBy(x => x.DiaChi);
                else myFunc = source => source.OrderByDescending(x => x.DiaChi);
                return myFunc;
            }
            if (sortBy == "DienThoai")
            {
                if (sortType) myFunc = source => source.OrderBy(x => x.DienThoai);
                else myFunc = source => source.OrderByDescending(x => x.DienThoai);
                return myFunc;
            }
            //if (sortBy == "TenNhom")
            //{
            //    if (sortType) myFunc = source => source.OrderBy(x => x.DanhMucNhomKhachHangRepository.TenNhom);
            //    else myFunc = source => source.OrderByDescending(x => x.DanhMucNhomKhachHangRepository.TenNhom);
            //    return myFunc;
            //}
            return null;
        }

        // GET: api/DMHangHoas
        [HttpGet("GetAllPagedDialog")]
        public async Task<ActionResult<GetAllResponse<DanhMucKhachHangModel>>> GetAllPagedDialog([FromBody] KhachHangSearchRequest request)
        {
            GetAllResponse<DanhMucKhachHangModel> outputs = new GetAllResponse<DanhMucKhachHangModel>();

            var query =
               from cus in _context.DanhMucKhachHangRepository
               where /*cus.DMDonViSuDungId == _tenantProvider.TenantId &&*/ cus.DeletedDate == null
               select cus;

            if (!string.IsNullOrEmpty(request.SearchText)) query = query.Where(x => x.MaDonVi.ToLower().Contains(request.SearchText.ToLower())
            || x.TenDonVi.ToLower().Contains(request.SearchText.ToLower())
            || x.DiaChi.ToLower().Contains(request.SearchText.ToLower())
            || x.DienThoai.ToLower().Contains(request.SearchText.ToLower())
            //|| x.DienThoai1.ToLower().Contains(request.SearchText.ToLower())
            );

            outputs.TotalRecords = await query.CountAsync();
            outputs.TotalPages = (int)Math.Ceiling(outputs.TotalRecords / (double)request.PageSize);
            outputs.Page = request.Page;
            outputs.PageSize = request.PageSize;

            query = query.Skip(request.Page * request.PageSize).Take(request.PageSize);
            var rawData = await query.ToListAsync();
            outputs.Items = rawData.Select(item => new DanhMucKhachHangModel(item)).ToList();
            return outputs;
        }

        // GET: api/DMHangHoas/5
        [HttpGet("getCode/{code}")]
        public async Task<ActionResult<DanhMucKhachHangModel>> GetByCode(string code)
        {
            if (string.IsNullOrEmpty(code))
            {
                return NotFound();
            }
            var item = await _context.DanhMucKhachHangRepository.FirstOrDefaultAsync(x => x.DeletedDate == null && x.MaDonVi.ToLower() == code.ToLower());

            if (item == null)
            {
                return NotFound();
            }

            return new DanhMucKhachHangModel(item);
        }
        // GET: api/DMHangHoas/5
        [HttpGet("GetSoDuCongNoById")]
        public async Task<ActionResult<double>> GetSoDuCongNoById(GetSoDuCongNoRequest request)
        {
            string sql = "SELECT  [dbo].[GetSoDuCongNo]('" + request.Loai + "','" + request.Ngay?.ToString("MM/dd/yyyy") + "',N'" + request.MaKhachHang + "',N'" + request.DMDonViSuDungId + "',N'" + request.lPhieu + "'," + request.Id + ")";
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            string connectionString = Configuration.GetConnectionString("Ketoan");
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = new SqlCommand(sql, connection);
                adapter.Fill(ds);
            }
            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    return (double)ds.Tables[0].Rows[0][0];
                }
            }
            return 0;



        }

    }
}
