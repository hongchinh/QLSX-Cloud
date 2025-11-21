using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SaleAPI.Models;
using QLSX.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using QLSX.Shared.Data.Requests.NhapXuat;
using QLSX.Shared.Data.Requests.ThuChi;
using QLSX.Shared.Data.Responses.ThuChi;
using System.Data;
using QLSX.Shared.Data.Responses;
using Microsoft.Extensions.Configuration;
using AutoMapper;
using System.Data.SqlClient;
using QLSX.Shared.Ultils;
using QLSX.Shared.Constants;
using QLSX.Shared.DTOs;
using System.Linq.Expressions;
using SaleAPI.Extensions;
using SaleAPI.Interfaces;
using SaleAPI.Services;
using MudBlazor;
using Sale.API.Extensions;
using QLSX.Shared.Entities;
using NuGet.Versioning;
using QLSX.Shared.Models.Request;

namespace SaleAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ThuChisController : ControllerBase
    {
        private readonly CRMDBContext _context;
        private readonly IMapper _mapper;
        private readonly ITenantProvider _tenantProvider;
        private readonly INhatKyService _nhatKyService;
        //private readonly IHubContext<AppSignalR> _hubContext;
        public IConfiguration _configuration { get; }
        public ThuChisController(CRMDBContext context, IMapper mapper, IConfiguration configuration, ITenantProvider tenantProvider, INhatKyService nhatKyService
            //, IHubContext<AppSignalR> hubContext
            )
        {
            this._context = context;
            _mapper = mapper;
            _configuration = configuration;
            _tenantProvider = tenantProvider;
            _nhatKyService = nhatKyService;
            //_hubContext = hubContext; 
        }

        // GET: api/GetCustomerTypes
        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<ThuChiModel>>> Get(SearchRequest request)
        {
            //await Task.Delay(3000);
            return await _context.ThuChiRepository
                //.Include(x => x.DMLoaiTiens)
                //.Where(p => p.DMDonViSuDungId == _tenantProvider.TenantId)
                .Where(x => x.DeletedDate == null)
                .Select(item => new ThuChiModel(item))
                .ToListAsync();
        }


        [HttpGet("GetCount")]
        public async Task<ActionResult<ItemCount>> GettCount()
        {
            ItemCount itemCount = new ItemCount();

            itemCount.Count = _context.ThuChiRepository
                .Where(x => x.DeletedDate == null)
                .Count();
            return await Task.FromResult(itemCount);
        }



        [HttpGet("GetList")]
        public async Task<ActionResult<IEnumerable<ThuChiModel>>> GetList(QLSX.Shared.Models.ThuChiSearchRequest request)
        {
            return await _context.ThuChiRepository
                .Where(p => request.Loai.ToLower() == p.Loai.ToLower())
                .Select(item => new ThuChiModel(item))
                .ToListAsync();
        }

        // GET: api/Customers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ThuChiNavigatorResponse>> GetById(int id)
        {
            var tenantID = _tenantProvider.TenantId;
            var thuchi = await _context.ThuChiRepository
               //.Include(x => x.DMLoaiTiens)
               .Where(x => x.Loai.ToLower() == "thu")
               //.Where(x => x.DMDonViSuDungId == tenantID)
               .Where(x => x.DeletedDate == null)
               .Where(x => x.Id == id)
               .FirstOrDefaultAsync();
            if (thuchi == null)
            {
                return new ThuChiNavigatorResponse { Total = 0 };
            }

            var countAll = await _context.ThuChiRepository
                  .Where(x => x.DeletedDate == null)
                  .Where(x => x.Loai == "thu").CountAsync();
            var newthuchi = _mapper.Map<ThuChiNavigatorResponse>(thuchi);
            newthuchi.Total = countAll;

            return newthuchi;
        }

        [HttpGet("GetPhieuThuIndex")]
        public async Task<ActionResult<ThuChiNavigatorResponse>> GetPhieuThuIndex(QLSX.Shared.Models.ThuChiSearchRequest request)
        {

            var thuchi = await _context.ThuChiRepository
                //.Include(x => x.DMLoaiTiens)
                .Where(x => x.Loai.ToLower() == "thu")
                //.Where(x => x.DMDonViSuDungId == _tenantProvider.TenantId)
                .Where(x => x.DeletedDate == null)
                .OrderBy(x => x.NgayCT)
                .OrderBy(x => x.Id)
                .Skip(request.Index - 1).Take(1)
                .FirstOrDefaultAsync();
            if (thuchi == null)
            {
                return new ThuChiNavigatorResponse { Total = 0 };
            }

            var countAll = await _context.ThuChiRepository
                  .Where(x => x.DeletedDate == null)
                  .Where(x => x.Loai == "thu").CountAsync();
            var newthuchi = _mapper.Map<ThuChiNavigatorResponse>(thuchi);
            newthuchi.Total = countAll;

            return newthuchi;
        }
        [HttpGet("GetPhieuChiIndex")]
        public async Task<ActionResult<ThuChiNavigatorResponse>> GetPhieuChiIndex(QLSX.Shared.Models.ThuChiSearchRequest request)
        {

            var thuchi = await _context.ThuChiRepository
                //.Include(x => x.DMLoaiTiens)
                .Where(x => x.Loai.ToLower() == "chi")
                //.Where(x => x.DMDonViSuDungId == _tenantProvider.TenantId)
                .Where(x => x.DeletedDate == null)
                .OrderBy(x => x.NgayCT)
                .OrderBy(x => x.Id)
                .Skip(request.Index - 1).Take(1)
                .FirstOrDefaultAsync();
            if (thuchi == null)
            {
                return new ThuChiNavigatorResponse { Total = 0 };
            }

            var countAll = await _context.ThuChiRepository
                .Where(x => x.DeletedDate == null)
                .Where(x => x.Loai == "chi").CountAsync();
            var newthuchi = _mapper.Map<ThuChiNavigatorResponse>(thuchi);
            newthuchi.Total = countAll;

            return newthuchi;
        }
        // PUT: api/Customers/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("Update/{id}")]
        public async Task<ActionResult<ThuChiModel>> Put(int id, ThuChiModel model)
        {
            if (id != model.Id)
            {
                return new ThuChiModel();
            }

            ThuChi enity = await _context.ThuChiRepository.FirstOrDefaultAsync(item => item.Id == id);
            enity.Stt = model.Stt;
            enity.Loai = model.Loai;
            enity.Phieu = model.Phieu;
            enity.MaDoiTuong = model.MaDoiTuong;
            enity.TenDoiTuong = model.TenDoiTuong;
            enity.DiaChi = model.DiaChi;
            enity.SoDonHang = model.MaDonHang;
            enity.NgayCT = model.NgayCT;
            enity.NgayThanhToan = model.NgayHoanThanh;
            enity.SoChungTu = model.SoPhieu;
            enity.SoTienVND = model.SoTien;
            enity.DienGiai = model.DienGiai;
            enity.GhiChu = model.GhiChu;
            enity.LoaiTien = model.LoaiTien;
            enity.MaKhoanChi = model.MaKhoanChi;
            enity.MaKhoanThu = model.MaKhoanThu;
            enity.UpdatedDate = DateTime.Now;
            await _context.SaveChangesAsync();

            try
            {
                await _context.SaveChangesAsync();

                // Log Nhat ky
                await _nhatKyService.LogUpdate("ThuChiRepository");
            }
            catch (DbUpdateConcurrencyException)
            {
                // Log Nhat ky
                await _nhatKyService.LogError("Update_ThuChi", "id : " + id + ";\nitem : " + model.ToString());
                if (!Exists(id))
                {
                    return new ThuChiModel();
                }
                else
                {
                    throw;
                }
            }

            return model;
        }

        // POST: api/Customers
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost("Create")]
        public async Task<ActionResult<ThuChi>> Post(ThuChiModel model)
        {
            ThuChi enity = new ThuChi();
            enity.Stt = model.Stt;
            enity.Loai = model.Loai;
            enity.Phieu = model.Phieu;
            enity.MaDoiTuong = model.MaDoiTuong;
            enity.TenDoiTuong = model.TenDoiTuong;
            enity.DiaChi = model.DiaChi;
            enity.SoDonHang = model.MaDonHang;
            enity.NgayCT = model.NgayCT;
            enity.NgayThanhToan = model.NgayHoanThanh;
            enity.SoChungTu = model.SoPhieu;
            enity.SoTienVND = model.SoTien;
            enity.DienGiai = model.DienGiai;
            enity.GhiChu = model.GhiChu;
            enity.LoaiTien = model.LoaiTien;
            enity.MaKhoanChi = model.MaKhoanChi;
            enity.MaKhoanThu = model.MaKhoanThu;
            enity.CreatedDate = DateTime.Now;
            enity.UpdatedDate = DateTime.Now;
            var newItem = _context.ThuChiRepository.Add(enity);
            await _context.SaveChangesAsync();

            // Log Nhat ky
            await _nhatKyService.LogCreate("ThuChiRepository");
            string jsonMessage = Newtonsoft.Json.JsonConvert.SerializeObject(enity);
            //await _hubContext.Clients.All.SendAsync("SendObject", jsonMessage);
            return newItem.Entity;
        }

        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult<ThuChi>> Delete(int id)
        {
            var item = await _context.ThuChiRepository.FirstOrDefaultAsync(p => p.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            item.DeletedDate = DateTime.Now;
            await _context.SaveChangesAsync();

            // Log Nhat ky
            await _nhatKyService.LogDelete("ThuChiRepository");
            return item;
        }

        private bool Exists(int id)
        {
            return _context.DanhMucKhoanChiRepository.Any(e => e.Id == id);
        }


        [HttpGet("ExportToExcel")]
        public async Task<ActionResult<GetAllResponse<ThuChiModel>>> ExportToExcel([FromBody] ThuChiSearchRequest request)
        {
            GetAllResponse<ThuChiModel> outputs = await GetAllData(request, false);
            // Log Nhat ky
            await _nhatKyService.LogExportExcel("ThuChiRepository");
            return outputs;
        }

        [HttpGet("GetAllPaged")]
        public async Task<ActionResult<GetAllResponse<ThuChiModel>>> GetAllPaged([FromBody] ThuChiSearchRequest request)
        {
            GetAllResponse<ThuChiModel> outputs = await GetAllData(request, true);
            return outputs;
        }

        private async Task<GetAllResponse<ThuChiModel>> GetAllData(ThuChiSearchRequest request, bool isPaging)
        {
            GetAllResponse<ThuChiModel> outputs = new GetAllResponse<ThuChiModel>();
            ICollection<FilterDefinition<ThuChi>> filter1 = request.Filter;
            Expression<Func<ThuChi, bool>> filter = m => (1 == 1);
            if (!string.IsNullOrEmpty(request.MaKhoanChi))
            {
                filter = filter.And(x => x.MaKhoanChi == request.MaKhoanChi);
            }
            if (!string.IsNullOrEmpty(request.MaKhoanThu))
            {
                filter = filter.And(x => x.MaKhoanThu == request.MaKhoanThu);
            }
            if (!string.IsNullOrEmpty(request.Loai))
            {
                filter = filter.And(x => x.Loai.Equals(request.Loai));
            }
            if (!string.IsNullOrEmpty(request.SoPhieu))
            {
                filter = filter.And(x => x.SoChungTu.Contains(request.SoPhieu));
            }
            if (!string.IsNullOrEmpty(request.MaDonHang))
            {
                filter = filter.And(x => x.SoDonHang.Contains(request.MaDonHang));
            }
            if (!string.IsNullOrEmpty(request.MaDonVi))
            {
                filter = filter.And(x => x.MaDoiTuong.Contains(request.MaDonVi));
            }
            if (!string.IsNullOrEmpty(request.TenDonVi))
            {
                filter = filter.And(x => x.TenDoiTuong.Contains(request.TenDonVi));
            }
            if (!string.IsNullOrEmpty(request.DiaChi))
            {
                filter = filter.And(x => x.DiaChi.Contains(request.DiaChi));
            }

            if (request.NgayLap_From.HasValue)
            {
                filter = filter.And(x => x.NgayCT >= request.NgayLap_From);
            }
            if (request.NgayLap_To.HasValue)
            {
                filter = filter.And(x => x.NgayCT <= request.NgayLap_To);
            }

            if (request.SoTien_To.HasValue)
            {
                filter = filter.And(x => x.SoTienVND <= request.SoTien_To);
            }
            if (request.SoTien_From.HasValue)
            {
                filter = filter.And(x => x.SoTienVND >= request.SoTien_From);
            }
            Func<IQueryable<ThuChi>, IOrderedQueryable<ThuChi>> order = null;
            if (request.SortDirection != QLSX.Shared.Enums.SortDirection.None)
            {
                order = await OrderBy(request.SortLable, request.SortDirection == QLSX.Shared.Enums.SortDirection.Ascending);
            }

            IQueryable<ThuChi> query = _context.Set<ThuChi>();

            FilterBuider<ThuChi> filterBuider;
            if (filter1 != null)
            {
                foreach (var f in filter1)
                {
                    var dataType = typeof(ThuChi).GetProperty(f.Field).PropertyType;
                    if (dataType == typeof(DateTime?) || dataType == typeof(DateTime))
                    {
                        var fter = GetFilterDateTime(filter, f.Operator, (DateTime)f.Value);
                        query = (IQueryable<ThuChi>)query.Where(fter);

                    }
                    else
                    {
                        filterBuider = new FilterBuider<ThuChi>(f);
                        var filterFunc = filterBuider.GetFilter;
                        query = (IQueryable<ThuChi>)query.Where(filterFunc);
                    }

                }
            }

            if (filter != null) query = query.Where(filter).Where(x => x.DeletedDate == null);
            if (order != null) query = order(query);
            outputs.TotalRecords = await query.CountAsync();
            outputs.TotalPages = (int)Math.Ceiling(outputs.TotalRecords / (double)request.PageSize);
            outputs.Page = request.Page;
            outputs.PageSize = request.PageSize;

            if (isPaging)
            {
                query = query.Skip(request.Page * request.PageSize).Take(request.PageSize);
            }
            var rawData = await query.ToListAsync();

            var loaiTienIdList = rawData.Where(item => item.LoaiTien != null).Select(item => item.LoaiTien).Distinct().ToList();
            var danhMucLoaiTienList = _context.DanhMucLoaiTienRepository.Where(item => item.DeletedDate == null && loaiTienIdList.Contains(item.Id.ToString())).ToList();
            outputs.Items = rawData.Select(item => new ThuChiModel(item, danhMucLoaiTienList)).ToList();

            return outputs;
        }


        [HttpGet("TimKiemNhanh")]
        public async Task<ActionResult<GetAllResponse<NavigatorResponse>>> TimKiemNhanh([FromBody] ThuChiSearchRequest request)
        {
            GetAllResponse<NavigatorResponse> outputs = new GetAllResponse<NavigatorResponse>();
            Expression<Func<ThuChi, bool>> filter = m => (1 == 1);
            //if (request.DMKhoanChiId > 0)
            //{
            //    filter = filter.And(x => x.DMKhoanChiId == request.DMKhoanChiId);
            //}
            //if (request.DMKhoanThuId > 0)
            //{
            //    filter = filter.And(x => x.DMKhoanThuId == request.DMKhoanThuId);
            //}
            if (!string.IsNullOrEmpty(request.Loai))
            {
                filter = filter.And(x => x.Loai.Equals(request.Loai));
            }
            if (!string.IsNullOrEmpty(request.SoPhieu))
            {
                filter = filter.And(x => x.SoChungTu.Contains(request.SoPhieu));
            }
            if (!string.IsNullOrEmpty(request.MaDonHang))
            {
                filter = filter.And(x => x.SoChungTu.Contains(request.SoPhieu));
            }

            if (!string.IsNullOrEmpty(request.MaDonVi))
            {
                filter = filter.And(x => x.MaDoiTuong.Contains(request.MaDonVi));
            }
            if (!string.IsNullOrEmpty(request.TenDonVi))
            {
                filter = filter.And(x => x.TenDoiTuong.Contains(request.TenDonVi));
            }
            if (!string.IsNullOrEmpty(request.DiaChi))
            {
                filter = filter.And(x => x.DiaChi.Contains(request.DiaChi));
            }

            if (request.NgayLap_From.HasValue)
            {
                filter = filter.And(x => x.NgayCT >= request.NgayLap_From);
            }
            if (request.NgayLap_To.HasValue)
            {
                filter = filter.And(x => x.NgayCT <= request.NgayLap_To);
            }

            if (request.SoTien_To.HasValue)
            {
                filter = filter.And(x => x.SoTienVND >= request.SoTien_To);
            }
            if (request.SoTien_From.HasValue)
            {
                filter = filter.And(x => x.SoTienVND <= request.SoTien_From);
            }

            if (request.SoTien_To.HasValue)
            {
                filter = filter.And(x => x.SoTienUSD >= request.SoTien_To);
            }
            if (request.SoTien_From.HasValue)
            {
                filter = filter.And(x => x.SoTienUSD <= request.SoTien_From);
            }

            IQueryable<ThuChi> query = _context.Set<ThuChi>();
            //.Include(x => x.DMLoaiTiens);
            if (filter != null) query = query.Where(filter).Where(x => x.DeletedDate == null);
            outputs.TotalRecords = await query.CountAsync();
            outputs.TotalPages = (int)Math.Ceiling(outputs.TotalRecords / (double)request.PageSize);
            outputs.Page = request.Page;
            outputs.PageSize = request.PageSize;

            var items = await query.ToListAsync();

            var lst = items.Select((x, index) => new NavigatorResponse
            {
                Index = index,
                Id = x.Id
            }).ToList();
            outputs.Items = lst;
            return outputs;
        }
        private Expression<Func<ThuChi, bool>> GetFilterDateTime(Expression<Func<ThuChi, bool>> filter, string Operator, DateTime Value)
        {
            return Operator switch
            {
                FilterOperator.DateTime.Is when null != Value =>
                    filter = filter.And(x => x.NgayCT == Value),
                FilterOperator.DateTime.IsNot when null != Value =>
                   filter = filter.And(x => x.NgayCT != Value),

                FilterOperator.DateTime.After when null != Value =>
                   filter = filter.And(x => x.NgayCT > Value),

                FilterOperator.DateTime.OnOrAfter when null != Value =>
                   filter = filter.And(x => x.NgayCT >= Value),

                FilterOperator.DateTime.Before when null != Value =>
                    filter = filter.And(x => x.NgayCT < Value),

                FilterOperator.DateTime.OnOrBefore when null != Value =>
                    filter = filter.And(x => x.NgayCT <= Value),

                FilterOperator.DateTime.Empty => filter = filter.And(x => x.NgayCT == null),
                FilterOperator.DateTime.NotEmpty => filter = filter.And(x => x.NgayCT != null),

                _ => filter = filter.And(x => 1 == 1)
            };



            return filter;
        }
        // GET: api/DMHangHoas
        [HttpGet("GetHistoryPaged")]
        public async Task<ActionResult<GetAllResponse<TraCuuNhapXuatAll>>> GetHistoryPaged([FromBody] ThuChiSearchRequest request)
        {
            GetAllResponse<TraCuuNhapXuatAll> outputs = new GetAllResponse<TraCuuNhapXuatAll>();
            Expression<Func<TraCuuNhapXuatAll, bool>> filter = m => (1 == 1);
            if (!string.IsNullOrEmpty(request.MaDonVi))
            {
                filter = filter.And(x => x.MaDoiTuong == request.MaDonVi);
            }

            Func<IQueryable<TraCuuNhapXuatAll>, IOrderedQueryable<TraCuuNhapXuatAll>> order = null;
            if (request.SortDirection != QLSX.Shared.Enums.SortDirection.None)
            {
                order = await OrderByTraCuuAll(request.SortLable, request.SortDirection == QLSX.Shared.Enums.SortDirection.Ascending);
            }

            IQueryable<TraCuuNhapXuatAll> query = _context.Set<TraCuuNhapXuatAll>();

            if (filter != null) query = query.Where(filter);
            if (order != null) query = order(query);
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

        private async Task<Func<IQueryable<ThuChi>, IOrderedQueryable<ThuChi>>> OrderBy(string sortBy, bool sortType)
        {
            Func<IQueryable<ThuChi>, IOrderedQueryable<ThuChi>> myFunc;
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
            if (sortBy == "SoPhieu")
            {
                if (sortType) myFunc = source => source.OrderBy(x => x.SoChungTu);
                else myFunc = source => source.OrderByDescending(x => x.SoChungTu);
                return myFunc;
            }
            if (sortBy == "MaDoiTuong")
            {
                if (sortType) myFunc = source => source.OrderBy(x => x.MaDoiTuong);
                else myFunc = source => source.OrderByDescending(x => x.MaDoiTuong);
                return myFunc;
            }
            if (sortBy == "TenDoiTuong")
            {
                if (sortType) myFunc = source => source.OrderBy(x => x.TenDoiTuong);
                else myFunc = source => source.OrderByDescending(x => x.TenDoiTuong);
                return myFunc;
            }
            if (sortBy == "DiaChi")
            {
                if (sortType) myFunc = source => source.OrderBy(x => x.DiaChi);
                else myFunc = source => source.OrderByDescending(x => x.DiaChi);
                return myFunc;
            }
            if (sortBy == "SoTien")
            {
                if (sortType) myFunc = source => source.OrderBy(x => x.SoTienVND);
                else myFunc = source => source.OrderByDescending(x => x.SoTienVND);
                return myFunc;
            }
            //if (sortBy == "KyHieu")
            //{
            //    if (sortType) myFunc = source => source.OrderBy(x => x.DMLoaiTiens.KyHieu);
            //    else myFunc = source => source.OrderByDescending(x => x.DMLoaiTiens.KyHieu);
            //    return myFunc;
            //}

            return null;

        }

        [HttpPost("InPhieuThu")]
        public async Task<ActionResult<ReportResponseBase<InPhieuThuChiResponse>>> InPhieuThu(InPhieuThuRequest request)
        {
            string StoredProc = "EXEC InPhieuThuChi  @id = " + request.Id.ToString() + ", @mdvsd = " + _tenantProvider.TenantId.ToString();
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            string connectionString = _configuration.GetConnectionString("CRMConnectStrings");
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = new SqlCommand(StoredProc, connection);
                adapter.Fill(ds);
            }
            var thongtin = await _context.CoQuanRepository.FirstAsync();
            var ttres = _mapper.Map<CoQuan, CoQuanResponse>(thongtin);
            var lst = ConvertDatatableToList.ConvertToList<InPhieuThuChiResponse>(ds.Tables[0]);
            var lstApi = new ReportResponseBase<InPhieuThuChiResponse>()
            {
                StatusCode = ApiResponseCodes.Success,
                Message = ApiResponseMessages.Success,
                ListData = lst,
                ThongTin = ttres
            };
            return Ok(lstApi);
        }
        [HttpPost("InPhieuChi")]
        public async Task<ActionResult<ReportResponseBase<InPhieuThuChiResponse>>> InPhieuChi(InPhieuChiRequest request)
        {
            string StoredProc = "EXEC InPhieuThuChi @id = " + request.Id.ToString() + ", @mdvsd = " + _tenantProvider.TenantId.ToString();
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            string connectionString = _configuration.GetConnectionString("CRMConnectStrings");
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = new SqlCommand(StoredProc, connection);
                adapter.Fill(ds);
            }
            var thongtin = await _context.CoQuanRepository.FirstAsync();
            var ttres = _mapper.Map<CoQuan, CoQuanResponse>(thongtin);
            var lst = ConvertDatatableToList.ConvertToList<InPhieuThuChiResponse>(ds.Tables[0]);
            var lstApi = new ReportResponseBase<InPhieuThuChiResponse>()
            {
                StatusCode = ApiResponseCodes.Success,
                Message = ApiResponseMessages.Success,
                ListData = lst,
                ThongTin = ttres
            };
            return Ok(lstApi);
        }

        // GET: api/DMHangHoas
        [HttpGet("ChuyenSoSangChu/{id}")]
        public async Task<ActionResult<string>> ChuyenSoSangChu(float id)
        {
            var query = await _context.DanhMucKhoanThuRepository.Select(x => _context.SoSangChu(id)).FirstOrDefaultAsync();
            return Ok(query);
        }

        [HttpGet("GetThuChiById")]
        public async Task<ActionResult<ThuChiNavigatorResponse>> GetThuChiById(ThuChiSearchRequest request)
        {

            var thuchi = await _context.ThuChiRepository
                                       .FirstOrDefaultAsync(x => x.Loai.ToLower() == request.Loai.ToLower()
                                                                 && x.Id == request.Id);
            if (thuchi == null)
            {
                return new ThuChiNavigatorResponse { Total = 0 };
            }
            var newthuchi = _mapper.Map<ThuChiNavigatorResponse>(new ThuChiModel(thuchi));
            return newthuchi;
        }

        [HttpGet("GetAllThuChiIDs")]
        public async Task<ActionResult<List<int>>> GetAllThuChiIDs(NhapXuatSearchRequest request)
        {
            var lst = _context.ThuChiRepository
                //.Include(x => x.DMLoaiTiens)
                //.Where(p => p.DMDonViSuDungId == _tenantProvider.TenantId)
                .Where(x => x.DeletedDate == null)
                .Where(x => x.Loai == request.Loai)
                .OrderBy(x => x.NgayCT)
                .GroupBy(nx => nx.Id)
                .Select(gr => new { Id = gr.Key });
            return lst.Select(x => x.Id).ToList();
        }

        [HttpGet("GetIdLastest/{loai}")]
        public async Task<ActionResult<int>> GetIdLastest(string loai)
        {
            try
            {
                var lst = _context.ThuChiRepository
                 //.Include(x => x.DMLoaiTiens)
                 //.Where(p => p.DMDonViSuDungId == _tenantProvider.TenantId)
                 .Where(x => x.DeletedDate == null)
                 .Where(x => x.Loai == loai)
                 .OrderBy(x => x.NgayCT)
                 .GroupBy(nx => nx.Id)
                 .Select(gr => new { Id = gr.Key }).ToList();
                return lst.Select(x => x.Id).Last();
            }
            catch (Exception)
            {

                return 0;
            }
        }
    }
}
