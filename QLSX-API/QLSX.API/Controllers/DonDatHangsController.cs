using AutoMapper;
using QLSX.Shared.Data.Responses;
using QLSX.Shared.DTOs;
using QLSX.Shared.Models;
using SaleAPI.Extensions;
using SaleAPI.Interfaces;
using SaleAPI.Models;
using SaleAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DonDatHang = QLSX.Shared.Entities.DonDatHang;
using QLSX.Shared.Models.Request;
using System.Data.SqlClient;
using Microsoft.AspNetCore.SignalR;
using System.Text.Json;
using Sale.API.SignalR;
using AngleSharp.Dom;
using QLSX.Shared.Constants;
using System.Dynamic;
using QLSX.Shared.Entities;
using AngleSharp.Io;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace SaleAPI.Controllers
{
    [ApiController]
    [Route("api/DonDatHangs")]
    [Authorize]
    public class DonDatHangsController : ControllerBase
    {
        private readonly CRMDBContext _context;
        private readonly IMapper _mapper;
        private readonly ITenantProvider _tenantProvider;
        private readonly INhatKyService _nhatKyService;
        private readonly IHubContext<AppSignalR> _hubContext;
        public IConfiguration _configuration { get; }
        public DonDatHangsController(CRMDBContext context, IMapper mapper, IConfiguration configuration, ITenantProvider tenantProvider, INhatKyService nhatKyService, IHubContext<AppSignalR> hubContext)
        {
            this._context = context;
            _mapper = mapper;
            _configuration = configuration;
            _tenantProvider = tenantProvider;
            _nhatKyService = nhatKyService;
            _hubContext = hubContext;
        }



        [HttpGet("{id}")]
        public async Task<ActionResult<DonDatHangNavigatorResponse>> GetById(int id)
        {
            var nhapxuat = await _context.DonDatHangRepository
                                         //.Where(x => x.DeletedDate == null)
                                         //.Where(p => p.DMDonViSuDungId == _tenantProvider.TenantId)
                                         //.Include(x => x.NoiDungDonDatHangs)
                                         .FirstOrDefaultAsync(x => x.Id == id);
            if (nhapxuat == null)
            {
                return new DonDatHangNavigatorResponse();
            }
            var newnhapxuat = _mapper.Map<DonDatHangNavigatorResponse>(nhapxuat);

            return newnhapxuat;
        }

        //[HttpGet("getbyCode/{soct}")]
        //public async Task<ActionResult<DonDatHangNavigatorResponse>> GetByCode(string soct)
        //{
        //    var nhapxuat = await _context.DonDatHangRepository
        //        .Where(x => x.DeletedDate == null)
        //        .Where(p => p.DMDonViSuDungId == _tenantProvider.TenantId)
        //        .Include(x => x.NoiDungDonDatHangs)
        //        .FirstOrDefaultAsync(x => x.SoCT == soct);
        //    if (nhapxuat == null)
        //    {
        //        return new DonDatHangNavigatorResponse();
        //    }
        //    var newnhapxuat = _mapper.Map<DonDatHangNavigatorResponse>(nhapxuat);

        //    return newnhapxuat;
        //}

        //[HttpGet("index/nhap/{id}")]
        //public async Task<ActionResult<DonDatHangNavigatorResponse>> GetNhapByIndex(DonDatHangSearchRequest request)
        //{
        //    var nhapxuat = await _context.DonDatHangRepository
        //        .Include(x => x.NoiDungDonDatHangs)
        //        .ThenInclude(x => x.DMHangHoa)
        //        .OrderBy(x => x.NgayCT)
        //        .Where(x => x.Loai == "nhap")
        //        .Where(p => p.DMDonViSuDungId == _tenantProvider.TenantId)
        //        .Where(x => x.DeletedDate == null)
        //        .Skip(request.Index - 1).Take(1)
        //        .FirstOrDefaultAsync();
        //    if (nhapxuat == null)
        //    {
        //        return new DonDatHangNavigatorResponse { Total = 0 };
        //    }

        //    var countAll = await _context.DonDatHangRepository
        //        .Where(x => x.DeletedDate == null)
        //        .Include(x => x.NoiDungDonDatHangs)
        //        .Where(x => x.Loai == "nhap")
        //        .GroupBy(nx => nx.Id)
        //        .Select(gr => new { id = gr.Key }).CountAsync();

        //    var newnhapxuat = _mapper.Map<DonDatHangNavigatorResponse>(nhapxuat);
        //    newnhapxuat.Total = countAll;

        //    return newnhapxuat;
        //}

        //[HttpGet("index/xuat/{id}")]
        //public async Task<ActionResult<DonDatHangNavigatorResponse>> GetXuatByIndex(QLSX.Shared.Models.DonDatHangSearchRequest request)
        //{
        //    var nhapxuat = await _context.DonDatHangRepository
        //        .Include(x => x.NoiDungDonDatHangs)
        //         .Where(x => x.Loai == "xuat")
        //         .Where(x => x.DeletedDate == null)
        //         .Where(p => p.DMDonViSuDungId == _tenantProvider.TenantId)
        //         .OrderBy(x => x.NgayCT)
        //        .Skip(request.Index - 1).Take(1)
        //        .FirstOrDefaultAsync();
        //    if (nhapxuat == null)
        //    {
        //        return new DonDatHangNavigatorResponse { Total = 0 };
        //    }

        //    var countAll = await _context.DonDatHangRepository
        //         .Where(x => x.Loai == "xuat")
        //         .Where(x => x.DeletedDate == null)
        //        .Include(x => x.NoiDungDonDatHangs)
        //        .GroupBy(nx => nx.Id)
        //        .Select(gr => new { id = gr.Key }).CountAsync();

        //    var newnhapxuat = _mapper.Map<DonDatHangNavigatorResponse>(nhapxuat);
        //    newnhapxuat.Total = countAll;

        //    return newnhapxuat;
        //}

        [HttpPut("Update/{id}")]
        public async Task<ActionResult<DonDatHang>> Put(DonDatHang nhapxuat)
        {
            try
            {
                //nhapxuat.UpdatedDate = DateTime.Now;
                _context.Entry(nhapxuat).State = EntityState.Modified;

                //foreach (var item in nhapxuat.NoiDungDonDatHangs)
                //{
                //    if (item.Id != 0)
                //    {
                //        _context.Entry(item).State = EntityState.Modified;
                //    }
                //    else
                //    {
                //        _context.Entry(item).State = EntityState.Added;
                //    }
                //}

                //var idsOfAddresses = nhapxuat.NoiDungDonDatHangs.Select(x => x.Id).ToList();
                //var addressesToDelete = await _context
                //    .NoiDungDonDatHangs
                //    .Where(x => !idsOfAddresses.Contains(x.Id) && x.DonDatHangId == nhapxuat.Id)
                //    .ToListAsync();

                //foreach (var item in addressesToDelete)
                //{
                //    item.DeletedDate = DateTime.Now;
                //    _context.Entry(item).State = EntityState.Added;
                //};

                await _context.SaveChangesAsync();

                // Log Nhat ky
                await _nhatKyService.LogUpdate("DonDatHang");
                return nhapxuat;
            }
            catch (Exception)
            {

                // Log Nhat ky
                await _nhatKyService.LogError("Update_DMDonDatHang", "item : " + nhapxuat.ToString());
                return new DonDatHang();
            }

        }

        [HttpPost("Create")]
        public async Task<ActionResult<DonDatHang>> Post([FromBody] DonDatHang item)
        {
            try
            {
                //item.CreatedDate = DateTime.Now;
                //item.UpdatedDate = DateTime.Now;
                //item.DMDonViSuDungId = _tenantProvider.TenantId;
                var newItem = _context.DonDatHangRepository.Add(item);
                //foreach (NoiDungDonDatHang ite in item.NoiDungDonDatHangs)
                //{
                //    ite.MaKhoHang = item.MaKhoHang;
                //    ite.CreatedDate = DateTime.Now;
                //}
                //_context.NoiDungDonDatHangs.AddRange(item.NoiDungDonDatHangs);

                await _context.SaveChangesAsync();

                // Log Nhat ky
                await _nhatKyService.LogCreate("DonDatHang");
                return newItem.Entity;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<DonDatHang>>> Get()
        {
            return await _context.DonDatHangRepository
                //.Include(x => x.NoiDungDonDatHangs)
                //.Include(x => x.User)
                //.Include(x => x.DanhMucKhoHangModel)
                //.Include(x => x.DMLoaiTiens)
                //.Where(p => p.DMDonViSuDungId == _tenantProvider.TenantId)
                //.Where(x => x.DeletedDate == null)
                .ToListAsync();
        }

        [HttpGet("ExportToExcel")]
        public async Task<ActionResult<GetDonHangResponse<PhieuNhapXuatAllModel>>> ExportToExcel([FromBody] DonHangRequest request)
        {
            GetDonHangResponse<PhieuNhapXuatAllModel> outputs = await GetData(request);

            // Log Nhat ky
            await _nhatKyService.LogExportExcel("DonDatHang");
            return outputs;
        }

        [HttpGet("GetAllPaged")]
        public async Task<ActionResult<GetDonHangResponse<PhieuNhapXuatAllModel>>> GetAllPaged([FromBody] DonHangRequest request)
        {
            GetDonHangResponse<PhieuNhapXuatAllModel> outputs = await GetData(request);
            return outputs;
        }

        private async Task<GetDonHangResponse<PhieuNhapXuatAllModel>> GetData(DonHangRequest request)
        {
            GetDonHangResponse<PhieuNhapXuatAllModel> result = new();
            if (request.NgayCTFrom == null || request.NgayCTTo == null)
            {
                return result;
            }

            var query = _context.PhieuNhapXuatAllRepository.Where(item => item.DeletedDate == null
                                                                          && item.NgayCT >= request.NgayCTFrom
                                                                          && item.NgayCT <= request.NgayCTTo
                                                                          && item.Loai.ToLower().Equals("donhang"));

            if (request.TrangThaiDonHang >= 1 && request.TrangThaiDonHang <= 5)
            {
                query = query.Where(item => item.TrangThaiDetail == request.TrangThaiDonHang);
            }
            if (!string.IsNullOrEmpty(request.SoCT))
            {
                query = query.Where(item => item.SoChungTu.Contains(request.SoCT));
            }
            if (!string.IsNullOrEmpty(request.DienGiai))
            {
                query = query.Where(item => item.DienGiai.Contains(request.DienGiai));
            }
            if (!string.IsNullOrEmpty(request.MaDonVi))
            {
                query = query.Where(item => item.MaDoiTuong.Contains(request.MaDonVi));
            }
            if (!string.IsNullOrEmpty(request.TenDonVi))
            {
                query = query.Where(item => item.TenDoiTuong.Contains(request.TenDonVi));
            }
            if (!string.IsNullOrEmpty(request.DiaChi))
            {
                query = query.Where(item => item.DiaChiDoiTuong.Contains(request.DiaChi));
            }
            if (!string.IsNullOrEmpty(request.NguoiQL))
            {
                query = query.Where(item => item.TenQuanLy.Contains(request.NguoiQL));
            }
            if (request.ThoiGianGiaoHang != null)
            {
                string thoiGianGiaoHangString = (request.ThoiGianGiaoHang ?? DateTime.Now).ToString("dd/MM/yyyy");
                query = query.Where(item => item.ThoiGianGiao.Contains(thoiGianGiaoHangString));
            }
            if (!string.IsNullOrEmpty(request.DiaDiem))
            {
                query = query.Where(item => item.DiaDiem.Contains(request.DiaDiem));
            }
            if (!string.IsNullOrEmpty(request.MaHangHoa))
            {
                query = query.Where(item => item.MaHangHoa.Contains(request.MaHangHoa));
            }
            if (!string.IsNullOrEmpty(request.TenHangHoa))
            {
                query = query.Where(item => item.TenHangHoa.Contains(request.TenHangHoa));
            }
            if (!string.IsNullOrEmpty(request.SoPhieuLSX))
            {
                query = query.Where(item => item.SoPhieuLSX.Contains(request.SoPhieuLSX));
            }
            if (!string.IsNullOrEmpty(request.SoPhieuXK))
            {
                query = query.Where(item => item.SoPhieuXuat.Contains(request.SoPhieuXK));
            }

            var data = await query.Select(item => new PhieuNhapXuatAllModel(item)).ToListAsync();

            if (!string.IsNullOrEmpty(request.NgayCT))
            {
                data = data.Where(item => item.NgayCTDisplay.StartsWith(request.NgayCT)).ToList();
            }
            if (!string.IsNullOrEmpty(request.NgayLSX))
            {
                data = data.Where(item => item.NgayPhieuLSXDisplay.StartsWith(request.NgayLSX)).ToList();
            }
            if (!string.IsNullOrEmpty(request.NgayXK))
            {
                data = data.Where(item => item.NgayXuatKhoDisplay.StartsWith(request.NgayXK)).ToList();
            }

            for (int i = 0; i < data.Count; i++)
            {
                data[i].Stt = (i + 1).ToString();
            }
            result.Items = data;
            result.TotalRows = result.Items.Count;
            result.TotalM2 = result.Items.Sum(item => item.TongDienTich) ?? 0;
            result.TotalSoTien = result.Items.Sum(item => item.SoTien) ?? 0;
            result.TotalMd = result.Items.Sum(item => item.TongChieuDai) ?? 0;
            return result;
        }

        // GET: api/DonDatHangs
        [HttpGet("GetAllPagedOnTraCuuAll")]
        public async Task<ActionResult<GetAllResponse<TraCuuNhapXuatAll>>> GetAllPagedOnTraCuuAll([FromBody] DonDatHangSearchRequest request)
        {
            GetAllResponse<TraCuuNhapXuatAll> outputs = new GetAllResponse<TraCuuNhapXuatAll>();
            Expression<Func<TraCuuNhapXuatAll, bool>> filter = m => (1 == 1);
            if (!string.IsNullOrEmpty(request.MaDonVi))
            {
                filter = filter.And(x => x.MaDoiTuong == request.MaDonVi);
            }
            if (!string.IsNullOrEmpty(request.Loai))
            {
                filter = filter.And(x => x.Loai == request.Loai);
            }

            Func<IQueryable<TraCuuNhapXuatAll>, IOrderedQueryable<TraCuuNhapXuatAll>> order = null;
            if (request.SortDirection != QLSX.Shared.Enums.SortDirection.None)
            {
                order = await OrderByTraCuuAll(request.SortLable, request.SortDirection == QLSX.Shared.Enums.SortDirection.Ascending);
            }

            IQueryable<TraCuuNhapXuatAll> query = _context.Set<TraCuuNhapXuatAll>();

            if (filter != null) query = query.Where(filter);
            if (order != null) query = order(query);
            Console.Write("AAAAAAAAA");
            Console.Write(query.ToQueryString());

            outputs.TotalRecords = await query.CountAsync();
            outputs.TotalPages = (int)Math.Ceiling(outputs.TotalRecords / (double)request.PageSize);
            outputs.Page = request.Page;
            outputs.PageSize = request.PageSize;

            query = query.Skip(request.Page * request.PageSize).Take(request.PageSize);
            outputs.Items = await query.ToListAsync();
            return outputs;
        }
        private async Task<Func<IQueryable<TraCuuNhapXuatAll>, IOrderedQueryable<TraCuuNhapXuatAll>>> OrderByTraCuuAll(string sortBy, bool sortType)
        {
            Func<IQueryable<TraCuuNhapXuatAll>, IOrderedQueryable<TraCuuNhapXuatAll>> myFunc;
            if (sortBy == "Loai")
            {
                if (sortType) myFunc = source => source.OrderBy(x => x.Loai);
                else myFunc = source => source.OrderByDescending(x => x.Loai);
                return myFunc;
            }
            if (sortBy == "NgayCT")
            {
                if (sortType) myFunc = source => source.OrderBy(x => x.NgayCT);
                else myFunc = source => source.OrderByDescending(x => x.NgayCT);
                return myFunc;
            }
            if (sortBy == "SoCT")
            {
                if (sortType) myFunc = source => source.OrderBy(x => x.SoChungTu);
                else myFunc = source => source.OrderByDescending(x => x.SoChungTu);
                return myFunc;
            }
            if (sortBy == "MaDonVi")
            {
                if (sortType) myFunc = source => source.OrderBy(x => x.MaDoiTuong);
                else myFunc = source => source.OrderByDescending(x => x.MaDoiTuong);
                return myFunc;
            }
            if (sortBy == "TenDonVi")
            {
                if (sortType) myFunc = source => source.OrderBy(x => x.TenDoiTuong);
                else myFunc = source => source.OrderByDescending(x => x.TenDoiTuong);
                return myFunc;
            }
            if (sortBy == "DiaChi")
            {
                if (sortType) myFunc = source => source.OrderBy(x => x.DiaChiDoiTuong);
                else myFunc = source => source.OrderByDescending(x => x.DiaChiDoiTuong);
                return myFunc;
            }
            if (sortBy == "SoTien")
            {
                if (sortType) myFunc = source => source.OrderBy(x => x.SoTien);
                else myFunc = source => source.OrderByDescending(x => x.SoTien);
                return myFunc;
            }


            return null;

        }

        private async Task<Func<IQueryable<DonDatHang>, IOrderedQueryable<DonDatHang>>> OrderBy(string sortBy, bool sortType)
        {
            Func<IQueryable<DonDatHang>, IOrderedQueryable<DonDatHang>> myFunc;
            if (sortBy == "Loai")
            {
                if (sortType) myFunc = source => source.OrderBy(x => x.Loai);
                else myFunc = source => source.OrderByDescending(x => x.Loai);
                return myFunc;
            }
            if (sortBy == "NgayCT")
            {
                if (sortType) myFunc = source => source.OrderBy(x => x.NgayCT);
                else myFunc = source => source.OrderByDescending(x => x.NgayCT);
                return myFunc;
            }
            //if (sortBy == "SoCT")
            //{
            //    if (sortType) myFunc = source => source.OrderBy(x => x.SoCT);
            //    else myFunc = source => source.OrderByDescending(x => x.SoCT);
            //    return myFunc;
            //}
            //if (sortBy == "MaDonVi")
            //{
            //    if (sortType) myFunc = source => source.OrderBy(x => x.MaDonVi);
            //    else myFunc = source => source.OrderByDescending(x => x.MaDonVi);
            //    return myFunc;
            //}
            //if (sortBy == "TenDonVi")
            //{
            //    if (sortType) myFunc = source => source.OrderBy(x => x.TenDonVi);
            //    else myFunc = source => source.OrderByDescending(x => x.TenDonVi);
            //    return myFunc;
            //}
            //if (sortBy == "DiaChi")
            //{
            //    if (sortType) myFunc = source => source.OrderBy(x => x.DiaChi);
            //    else myFunc = source => source.OrderByDescending(x => x.DiaChi);
            //    return myFunc;
            //}
            //if (sortBy == "TongSoTien")
            //{
            //    if (sortType) myFunc = source => source.OrderBy(x => x.NoiDungDonDatHangs.Sum(s => s.SoTien));
            //    else myFunc = source => source.OrderByDescending(x => x.NoiDungDonDatHangs.Sum(s => s.SoTien));
            //    return myFunc;
            //}
            //if (sortBy == "KyHieu")
            //{
            //    if (sortType) myFunc = source => source.OrderBy(x => x.DMLoaiTiens.KyHieu);
            //    else myFunc = source => source.OrderByDescending(x => x.DMLoaiTiens.KyHieu);
            //    return myFunc;
            //}
            //if (sortBy == "SoTienTT")
            //{
            //    if (sortType) myFunc = source => source.OrderBy(x => x.SoTienTT);
            //    else myFunc = source => source.OrderByDescending(x => x.SoTienTT);
            //    return myFunc;
            //}
            //if (sortBy == "TenKho")
            //{
            //    if (sortType) myFunc = source => source.OrderBy(x => x.DanhMucKhoHangModel.TenKho);
            //    else myFunc = source => source.OrderByDescending(x => x.DanhMucKhoHangModel.TenKho);
            //    return myFunc;
            //}
            return null;

        }

        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult<DonDatHang>> Delete(int id)
        {
            var item = await _context.DonDatHangRepository/*.Where(p => p.DMDonViSuDungId == _tenantProvider.TenantId)*/.Where(p => p.Id == id).FirstOrDefaultAsync();
            if (item == null)
            {
                return NotFound();
            }

            item.DeletedDate = DateTime.Now;
            var noidungs = await _context.NoiDungDonDatHangs.Where(p => p.DonDatHangId == id).ToListAsync();
            foreach (var itm in noidungs)
            {
                itm.DeletedDate = DateTime.Now;
            }
            await _context.SaveChangesAsync();

            // Log Nhat ky
            await _nhatKyService.LogDelete("DonDatHang");
            return item;
        }

        [HttpGet("TimKiemNhanh")]
        public async Task<ActionResult<GetAllResponse<NavigatorResponse>>> TimKiemNhanh([FromBody] DonDatHangSearchRequest request)
        {
            var tenant = _tenantProvider.GetTenant();
            GetAllResponse<NavigatorResponse> outputs = new GetAllResponse<NavigatorResponse>();
            //Expression<Func<DonDatHang, bool>> filter = m => (_tenantProvider.TenantId == 0 || m.DMDonViSuDungId == _tenantProvider.TenantId);

            //if (!string.IsNullOrEmpty(request.Loai))
            //{
            //    filter = filter.And(x => x.Loai.Equals(request.Loai));
            //}
            //if (!string.IsNullOrEmpty(request.SoPhieuLSX))
            //{
            //    filter = filter.And(x => x.SoCT.Contains(request.SoPhieuLSX));
            //}

            //if (!string.IsNullOrEmpty(request.MaDonVi))
            //{
            //    filter = filter.And(x => x.MaDonVi.Contains(request.MaDonVi));
            //}
            //if (!string.IsNullOrEmpty(request.TenDonVi))
            //{
            //    filter = filter.And(x => x.TenDonVi.Contains(request.TenDonVi));
            //}
            //if (!string.IsNullOrEmpty(request.DiaChi))
            //{
            //    filter = filter.And(x => x.DiaChi.Contains(request.DiaChi));
            //}

            //if (!string.IsNullOrEmpty(request.MaHangHoa))
            //{
            //    filter = filter.And(x => x.NoiDungDonDatHangs.Any(x => x.MaHangHoa.Contains(request.MaHangHoa)));
            //}
            //if (!string.IsNullOrEmpty(request.TenHangHoa))
            //{
            //    filter = filter.And(x => x.NoiDungDonDatHangs.Any(x => x.TenHangHoa.Contains(request.TenHangHoa)));
            //}
            //if (!string.IsNullOrEmpty(request.DonViTinh))
            //{
            //    filter = filter.And(x => x.NoiDungDonDatHangs.Any(x => x.DonViTinh.Contains(request.DonViTinh)));
            //}

            //IQueryable<DonDatHang> query = _context.Set<DonDatHang>().Include(x => x.NoiDungDonDatHangs)
            //  .Include(x => x.User)
            //  .Include(x => x.DanhMucKhoHangModel)
            //  .Include(x => x.DMLoaiTiens);
            //if (filter != null) query = query.Where(filter).Where(x => x.DeletedDate == null);
            //outputs.TotalRecords = await query.CountAsync();
            //outputs.TotalPages = (int)Math.Ceiling(outputs.TotalRecords / (double)request.PageSize);
            //outputs.Page = request.Page;
            //outputs.PageSize = request.PageSize;

            //var items = await query.ToListAsync();

            //var lst = items.Select((x, index) => new NavigatorResponse
            //{
            //    Index = index,
            //    Id = x.Id
            //}).ToList();
            //outputs.Items = lst;
            return outputs;
        }

        [HttpGet("GetAllDonDatHangIDs")]
        public async Task<ActionResult<List<int>>> GetAllDonDatHangIDs(DonDatHangSearchRequest request)
        {
            var lst = _context.DonDatHangRepository
                //.Include(x => x.NoiDungDonDatHangs)
                //.Include(x => x.User)
                //.Include(x => x.DanhMucKhoHangModel)
                //.Include(x => x.DMLoaiTiens)
                //.Where(p => p.DMDonViSuDungId == _tenantProvider.TenantId)
                //.Where(x => x.DeletedDate == null)
                .Where(x => x.Loai == request.Loai)
                .OrderBy(x => x.NgayCT)
                .GroupBy(nx => nx.Id)
                .Select(gr => new { Id = gr.Key });
            return await lst.Select(x => x.Id).ToListAsync();
        }

        [HttpGet("GetIdLastest/{loai}")]
        public async Task<ActionResult<int>> GetLastest(string loai)
        {
            try
            {
                var lst = await _context.DonDatHangRepository
               //.Include(x => x.NoiDungDonDatHangs)
               //.Include(x => x.User)
               //.Include(x => x.DanhMucKhoHangModel)
               //.Include(x => x.DMLoaiTiens)
               //.Where(p => p.DMDonViSuDungId == _tenantProvider.TenantId)
               //.Where(x => x.DeletedDate == null)
               .Where(x => x.Loai == loai)
               .OrderBy(x => x.NgayCT)
               .GroupBy(nx => nx.Id)
               .Select(gr => new { Id = gr.Key }).ToListAsync();
                return lst.Select(x => x.Id).Last();
            }
            catch (Exception)
            {

                return 0;
            }
        }

        [HttpPost("UpdateTrangThaiDonHang")]
        public async Task<IActionResult> UpdateTrangThaiDonHang([FromBody] UpdateTrangThaiRequest request)
        {
            if (request == null || !request.IdIdList.Any())
            {
                return BadRequest("Invalid request");
            }

            try
            {
                string sqlConn = _configuration.GetConnectionString("CRMConnectStrings");
                using (var connection = new SqlConnection(sqlConn))
                {
                    await connection.OpenAsync();

                    string idids = string.Join(",", request.IdIdList);
                    string storedProc = string.Format(
                        "exec dbo.UpdateTrangThaiDonHang_Next {0}, '{1}', '{2}', '{3}'",
                            request.TrangThai,
                            string.Join(",", request.IdIdList),
                            request.Ngay.ToString("yyyy/MM/dd HH:mm:ss.fff"),
                            request.Spx ?? string.Empty);

                    using (SqlDataAdapter dataAdapter = new SqlDataAdapter(storedProc, connection))
                    {
                        // Create the DataSet 
                        DataSet dataSet = new DataSet();
                        // Fill the DataSet using our DataAdapter 
                        dataAdapter.Fill(dataSet);
                    }

                    // Send Socket to Blazer page
                    await SendSocket(SignalRKey.HuyDonHangSocketKey, request.IdIdList);
                }

                return Ok();
            }
            catch (Exception ex)
            {
                // Log the exception (not shown here)
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost("GetSoChungTuDonHang")]
        public async Task<IActionResult> GetSoChungTuDonHang([FromBody] GetSoChungTuDonHangRequest request)
        {
            if (request == null)
            {
                return BadRequest("Invalid request");
            }

            try
            {
                string idids = string.Join(",", request.IdIdList);
                string sqlConn = _configuration.GetConnectionString("CRMConnectStrings");
                using (var connection = new SqlConnection(sqlConn))
                {
                    await connection.OpenAsync();

                    string storedProc = string.Format(
                        "exec dbo.GetSoChungTuDonHang '{0}', '{1}', {2}, '{3}'",
                        idids,
                        request.HoTen,
                        request.SoTien,
                        request.NgayCT.ToString("yyyy-MM-dd HH:mm:ss"));

                    using (SqlCommand command = new SqlCommand(storedProc, connection))
                    {
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            if (reader.HasRows)
                            {
                                var results = new List<dynamic>();
                                while (await reader.ReadAsync())
                                {
                                    var row = new ExpandoObject() as IDictionary<string, object>;
                                    for (int i = 0; i < reader.FieldCount; i++)
                                    {
                                        row.Add(reader.GetName(i), reader[i]);
                                    }
                                    results.Add(row);
                                }
                                var firstResult = results.FirstOrDefault();
                                return Ok(firstResult);
                            }
                            else
                            {
                                return NotFound("No records found");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the exception (not shown here)
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost("SaveNoiDungDonHangTraNo")]
        public async Task<ActionResult<bool>> SaveNoiDungDonHangTraNo([FromBody] SaveNoiDungDonHangTraNoRequest request)
        {
            // Add new NoiDungDonHangTraNo
            var noiDungTraNoEntityList = request.NoiDungTraNoList.Select(model => ConvertToNoiDungNhapXuatTraNo(model)).ToList();
            await _context.NoiDungNhapXuatTraNoRepository.AddRangeAsync(noiDungTraNoEntityList);

            return await _context.SaveChangesAsync() > 0;
        }

        [HttpPost("SaveNhapXuatThongTin")]
        public async Task<ActionResult<bool>> SaveNhapXuatThongTin([FromBody] NhapXuatThongTinModel model)
        {
            var nhapXuatThongTinEntity = await _context.NhapXuatThongTinRepository.FirstOrDefaultAsync(item => item.SoCt == model.SoCt);
            if (nhapXuatThongTinEntity == null)
            {
                nhapXuatThongTinEntity = ConvertToNhapXuatThongTin(model);
            }
            else
            {
                nhapXuatThongTinEntity.LoaiPhieu = model.LoaiPhieu;
                nhapXuatThongTinEntity.SoTienGiam = model.SoTienGiam;
                nhapXuatThongTinEntity.SoTienCK = model.SoTienCK;
                nhapXuatThongTinEntity.SoTienVc = model.SoTienVc;
                nhapXuatThongTinEntity.TyLeVat = model.TyLeVat;
                nhapXuatThongTinEntity.SoTienVat = model.SoTienVat;
                nhapXuatThongTinEntity.SoTienTT = model.SoTienTT;
                nhapXuatThongTinEntity.SoTien = model.SoTien;
                nhapXuatThongTinEntity.NgayCt = model.NgayCt;
                nhapXuatThongTinEntity.TongCong = model.TongCong;
                nhapXuatThongTinEntity.UpdatedDate = DateTime.Now;
                nhapXuatThongTinEntity.GhiChu = model.GhiChu;
                nhapXuatThongTinEntity.UpdateBy = model.UpdateBy;
                nhapXuatThongTinEntity.IdId = model.IdId;
            }
            _context.NhapXuatThongTinRepository.Update(nhapXuatThongTinEntity);

            return await _context.SaveChangesAsync() > 0;
        }

        [HttpPost("CancelPXK")]
        public async Task<ActionResult<bool>> CancelPXK([FromBody] CancelPXKRequest request)
        {
            var nhapXuatThongTinEntity = await _context.NhapXuatThongTinRepository.FirstOrDefaultAsync(item => item.SoCt == request.SoCt);
            if (nhapXuatThongTinEntity == null)
            {
                return false;
            }
            nhapXuatThongTinEntity.SoCt = nhapXuatThongTinEntity.SoCt + "-" + nhapXuatThongTinEntity.Id;
            nhapXuatThongTinEntity.UpdatedDate = DateTime.Now;
            nhapXuatThongTinEntity.GhiChu = "Xóa PXK";

            return await _context.SaveChangesAsync() > 0;
        }

        [HttpPost("ReloadSocketPage")]
        public async Task<ActionResult> ReloadSocketPage([FromBody] ReloadSocketPageRequest request)
        {
            // Send Socket to Blazer page
            await SendSocket(SignalRKey.UpdateDonHangSocketKey, request.IdIdList);
            return Ok();
        }

        private NhapXuatThongTin ConvertToNhapXuatThongTin(NhapXuatThongTinModel model)
        {
            return new NhapXuatThongTin
            {
                Id = model.Id,
                LoaiPhieu = model.LoaiPhieu,
                SoTienGiam = model.SoTienGiam,
                SoTienCK = model.SoTienCK,
                SoTienVc = model.SoTienVc,
                TyLeVat = model.TyLeVat,
                SoTienVat = model.SoTienVat,
                SoTienTT = model.SoTienTT,
                SoTien = model.SoTien,
                NgayCt = model.NgayCt,
                SoCt = model.SoCt,
                TongCong = model.TongCong,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                GhiChu = model.GhiChu,
                CreateBy = model.CreateBy,
                UpdateBy = model.UpdateBy,
                IdId = model.IdId
            };
        }

        private NoiDungNhapXuatTraNo ConvertToNoiDungNhapXuatTraNo(NoiDungNhapXuatTraNoModel model)
        {
            return new NoiDungNhapXuatTraNo
            {
                IdId = model.IdId,
                LoaiPhieu = model.LoaiPhieu,
                SHTK = model.SHTK,
                MaHangHoa = model.MaHangHoa,
                TenHangHoa = model.TenHangHoa,
                SoLuongTon = model.SoLuongTon,
                SoLuong = model.SoLuong,
                DonGia = model.DonGia,
                DonViTinh = model.DonViTinh,
                SoTien = model.SoTien,
                SoTienUSD = model.SoTienUSD,
                DonGiaUSD = model.DonGiaUSD,
                MaDonVi = model.MaDonVi,
                TenDonVi = model.TenDonVi,
                MaDonVi1 = model.MaDonVi1,
                TenDonVi1 = model.TenDonVi1,
                HanSuDung = model.HanSuDung,
                KetChuyen1 = model.KetChuyen1,
                GiaVon = model.GiaVon,
                GiaVonUSD = model.GiaVonUSD,
                MaPhanBo = model.MaPhanBo,
                TenPhanBo = model.TenPhanBo,
                Comment = model.Comment,
                CapNhatGiaVon = model.CapNhatGiaVon,
                TyLeChietKhau = model.TyLeChietKhau,
                SoTienChietKhau = model.SoTienChietKhau,
                SoTienXuat = model.SoTienXuat,
                DonGiaXuat = model.DonGiaXuat,
                ChenhLech = model.ChenhLech,
                XuatXu = model.XuatXu,
                QuyCach = model.QuyCach,
                MaNhom = model.MaNhom,
                MaLoai = model.MaLoai,
                TenNhom = model.TenNhom,
                TenLoai = model.TenLoai,
                PhuongThuc = model.PhuongThuc,
                DienGiai = model.DienGiai,
                TongChieuDai = model.TongChieuDai,
                TongDienTich = model.TongDienTich,
                KhoRongTon = model.KhoRongTon,
                ChieuDai = model.ChieuDai,
                MaHangHoa1 = model.MaHangHoa1,
                TenHangHoa1 = model.TenHangHoa1,
                DonViTinh1 = model.DonViTinh1,
                SoLuong1 = model.SoLuong1,
                ThueNK = model.ThueNK,
                ThueVAT = model.ThueVAT,
                TyLePhiVanChuyen = model.TyLePhiVanChuyen,
                SoTienPhiVanChuyen = model.SoTienPhiVanChuyen,
                TyLeKhuyenMai = model.TyLeKhuyenMai,
                SoTienKhuyenMai = model.SoTienKhuyenMai,
                TongCong = model.TongCong,
                TinhChat = model.TinhChat,
                Kieu = model.Kieu,
                SoTienVND = model.SoTienVND,
                POLY = model.POLY,
                MDI = model.MDI,
                MaHoaChatPoly = model.MaHoaChatPoly,
                MaHoaChatMDI = model.MaHoaChatMDI,
                MaDonViNhan = model.MaDonViNhan,
                TenDonViNhan = model.TenDonViNhan,
                NuocSanXuat = model.NuocSanXuat,
                TyLeCK = model.TyLeCK,
                SoTienOK = model.SoTienOk,
                DonGiaBan = model.DonGiaBan,
                SoTienBan = model.SoTienBan,
                MaKho = model.MaKho,
                ThoiGianTao = model.ThoiGianTao,
                SoLuongTra = model.SoLuongTra,
                LoHang = model.LoHang,
                MaBarCode = model.MaBarCode,
                DonGiaBanLe = model.DonGiaBanLe,
                SoTienBanLe = model.SoTienBanLe,
                TyLeVAT = model.TyLeVAT,
                SoTienVAT = model.SoTienVAT,
                LoaiTon = model.LoaiTon,
                MauSac = model.MauSac,
                DoDay = model.DoDay,
                KieuSong = model.KieuSong,
                ChungLoai = model.ChungLoai,
                MaLoaiTon = model.MaLoaiTon,
                MaMauSac = model.MaMauSac,
                MaDoDay = model.MaDoDay,
                MaKieuSong = model.MaKieuSong,
                MaChungLoai = model.MaChungLoai,
                TyTrong = model.TyTrong,
                DonGiaDVT1 = model.DonGiaDVT1,
                SoLuongDVT1 = model.SoLuongDVT1,
                SoTienDVT1 = model.SoTienDVT1,
                TyLeCkNV = model.TyLeCkNV,
                SoTienCkNv = model.SoTienCkNv,
                SoPhieuLSX = model.SoPhieuLSX,
                NgayPhieuLSX = model.NgayPhieuLSX,
                TrangThaiDetail = model.TrangThaiDetail,
                SoPhieuXuat = model.SoPhieuXuat,
                NgayXuatKho = model.NgayXuatKho,
                NgayXacNhan = model.NgayXacNhan,
                NhapXuatId = model.NhapXuatId,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now
            };
        }

        /// <summary>
        /// Send socket when loai is donhang
        /// </summary>
        /// <param name="nhapXuat"></param>
        /// <returns></returns>
        private async Task SendSocket(string signalRKey, List<int> idIdList)
        {

            var query = _context.PhieuNhapXuatAllRepository.Where(item => idIdList.Contains(item.IdId));
            var resultData = await query.Select(item => new PhieuNhapXuatAllModel(item)).ToListAsync();
            if (resultData.Any())
            {
                string jsonMessage = JsonSerializer.Serialize(resultData);
                await _hubContext.Clients.All.SendAsync(signalRKey, jsonMessage);
            }
        }

        [HttpPost("CheckTemplateReport")]
        public async Task<ActionResult<TemplateReportResult>> CheckTemplateReport([FromBody] CheckTemplateRequest request)
        {
            string StoredProc = "";
            var listIdId = request.ListIdId;
            StoredProc = string.Format("EXEC dbo.CheckIsTemplateReport @idids = '{0}'", string.Join(",", listIdId));

            DataTable dt = new DataTable();
            string sqlConn = _configuration.GetConnectionString("CRMConnectStrings");
            using (SqlConnection connection = new SqlConnection(sqlConn))
            {
                SqlCommand cmd = new SqlCommand(StoredProc, connection);
                cmd.CommandType = CommandType.Text;
                SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                DataSet dataSet = new DataSet();
                try
                {
                    connection.Open();
                    dataAdapter.Fill(dataSet);
                    dt = dataSet.Tables[0];
                    TemplateReportResult result = new TemplateReportResult();

                    if (dt.Rows.Count > 0)
                    {
                        result.UPDATE_STATUS = dt.Rows[0]["UPDATE_STATUS"].ToString();
                        result.IsMau_CuaCuon = dt.Rows[0]["IsMau_CuaCuon"] == DBNull.Value ? (bool?)null : Convert.ToBoolean(dt.Rows[0]["IsMau_CuaCuon"]);
                        result.IsMau_CuaXep = dt.Rows[0]["IsMau_CuaXep"] == DBNull.Value ? (bool?)null : Convert.ToBoolean(dt.Rows[0]["IsMau_CuaXep"]);
                        result.IsMau_CuaNhom = dt.Rows[0]["IsMau_CuaNhom"] == DBNull.Value ? (bool?)null : Convert.ToBoolean(dt.Rows[0]["IsMau_CuaNhom"]);
                        result.IsMau_Nano = dt.Rows[0]["IsMau_Nano"] == DBNull.Value ? (bool?)null : Convert.ToBoolean(dt.Rows[0]["IsMau_Nano"]);
                        result.IsMau_Tran36 = dt.Rows[0]["IsMau_Tran36"] == DBNull.Value ? (bool?)null : Convert.ToBoolean(dt.Rows[0]["IsMau_Tran36"]);
                        result.IsMau_Panel = dt.Rows[0]["IsMau_Panel"] == DBNull.Value ? (bool?)null : Convert.ToBoolean(dt.Rows[0]["IsMau_Panel"]);
                        result.IsMau_Tranh = dt.Rows[0]["IsMau_Tranh"] == DBNull.Value ? (bool?)null : Convert.ToBoolean(dt.Rows[0]["IsMau_Tranh"]);
                        result.IsMau_ThanTre = dt.Rows[0]["IsMau_ThanTre"] == DBNull.Value ? (bool?)null : Convert.ToBoolean(dt.Rows[0]["IsMau_ThanTre"]);
                        result.LstCuaCuon = dt.Rows[0]["LstCuaCuon"] == DBNull.Value ? null : dt.Rows[0]["LstCuaCuon"].ToString();
                        result.LstCuaXep = dt.Rows[0]["LstCuaXep"] == DBNull.Value ? null : dt.Rows[0]["LstCuaXep"].ToString();
                        result.LstCuaNhom = dt.Rows[0]["LstCuaNhom"] == DBNull.Value ? null : dt.Rows[0]["LstCuaNhom"].ToString();
                        result.LstNhuaNaNo = dt.Rows[0]["LstNhuaNaNo"] == DBNull.Value ? null : dt.Rows[0]["LstNhuaNaNo"].ToString();
                        result.LstTran36 = dt.Rows[0]["LstTran36"] == DBNull.Value ? null : dt.Rows[0]["LstTran36"].ToString();
                        result.LstPanel = dt.Rows[0]["LstPanel"] == DBNull.Value ? null : dt.Rows[0]["LstPanel"].ToString();
                        result.LstTranh = dt.Rows[0]["LstTranh"] == DBNull.Value ? null : dt.Rows[0]["LstTranh"].ToString();
                        result.LstThanTre = dt.Rows[0]["LstThanTre"] == DBNull.Value ? null : dt.Rows[0]["LstThanTre"].ToString();
                        result.LstIDID_All = dt.Rows[0]["LstIDID_All"] == DBNull.Value ? null : dt.Rows[0]["LstIDID_All"].ToString();
                    }
                    else
                    {
                        Console.WriteLine("Không có dữ liệu trả về từ stored procedure.");
                    }
                    return result;

                }
                catch (Exception ex)
                {
                    Console.WriteLine("Lỗi khi thực thi stored procedure: " + ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
            return default;
        }
    }
}


