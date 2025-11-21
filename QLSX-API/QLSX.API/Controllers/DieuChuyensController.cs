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
using QLSX.Shared.Entities;
using MudBlazor;
using Sale.API.Extensions;
using System.Reflection;
using static MudBlazor.CategoryTypes;
using QLSX.Shared.Models.Request;

namespace SaleAPI.Controllers
{
    [ApiController]
    [Route("api/DieuChuyens")]
    [Authorize]
    public class DieuChuyensController : ControllerBase
    {
        private readonly CRMDBContext _context;
        private readonly IMapper _mapper;
        private readonly ITenantProvider _tenantProvider;
        private readonly INhatKyService _nhatKyService;
        public IConfiguration _configuration { get; }
        public DieuChuyensController(CRMDBContext context, IMapper mapper, IConfiguration configuration, ITenantProvider tenantProvider, INhatKyService nhatKyService)
        {
            this._context = context;
            _mapper = mapper;
            _configuration = configuration;
            _tenantProvider = tenantProvider;
            _nhatKyService = nhatKyService;
        }



        // GET: api/GetCustomerTypes
        [HttpGet("GetList")]
        public async Task<ActionResult<IEnumerable<NhapXuatModel>>> GetList(QLSX.Shared.Models.DieuChuyenSearchRequest request)
        {
            return await _context.NhapXuatRepository
                .Where(p => /*p.DMDonViSuDungId == _tenantProvider.TenantId &&*/ request.Loai == p.Loai)
                .Where(x => x.DeletedDate == null)
                .Select(item => new NhapXuatModel(item))
                .ToListAsync();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<DieuChuyenNavigatorResponse>> GetById(int id)
        {
            var nhapxuat = await _context.NhapXuatRepository
                .Where(x => x.DeletedDate == null)
                //.Where(p => p.DMDonViSuDungId == _tenantProvider.TenantId)
                //.Include(x => x.NoiDungDieuChuyens)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (nhapxuat == null)
            {
                return new DieuChuyenNavigatorResponse();
            }
            var newnhapxuat = _mapper.Map<DieuChuyenNavigatorResponse>(nhapxuat);

            return newnhapxuat;
        }
        [HttpGet("index/dieuchuyen/{id}")]
        public async Task<ActionResult<DieuChuyenNavigatorResponse>> GetNhapByIndex(QLSX.Shared.Models.DieuChuyenSearchRequest request)
        {
            var nhapxuat = await _context.NhapXuatRepository
                //.Include(x => x.NoiDungDieuChuyens)
                //.ThenInclude(x => x.DMHangHoa)
                .OrderBy(x => x.NgayCT)
                .Where(x => x.Loai == "dieuchuyen")
                .Where(x => x.DeletedDate == null)
                //.Where(p => p.DMDonViSuDungId == _tenantProvider.TenantId)
                .Skip(request.Index - 1).Take(1)
                .FirstOrDefaultAsync();
            if (nhapxuat == null)
            {
                return new DieuChuyenNavigatorResponse { Total = 0 };
            }

            var countAll = await _context.NhapXuatRepository
                .Where(x => x.DeletedDate == null)
                //.Include(x => x.NoiDungDieuChuyens)
                .Where(x => x.Loai == "nhap")
                //.Where(p => p.DMDonViSuDungId == _tenantProvider.TenantId)
                .GroupBy(nx => nx.Id)
                .Select(gr => new { id = gr.Key }).CountAsync();

            var newnhapxuat = _mapper.Map<DieuChuyenNavigatorResponse>(nhapxuat);
            newnhapxuat.Total = countAll;

            return newnhapxuat;
        }


        [HttpPut("Update/{id}")]
        public async Task<ActionResult<NhapXuatModel>> Put(DieuChuyen nhapxuat)
        {
            try
            {
                nhapxuat.UpdatedDate = DateTime.Now;
                _context.Entry(nhapxuat).State = EntityState.Modified;

                foreach (var item in nhapxuat.NoiDungDieuChuyens)
                {
                    if (item.Id != 0)
                    {
                        _context.Entry(item).State = EntityState.Modified;
                    }
                    else
                    {
                        _context.Entry(item).State = EntityState.Added;
                    }
                }

                var idsOfAddresses = nhapxuat.NoiDungDieuChuyens.Select(x => x.Id).ToList();
                var addressesToDelete = await _context
                    .NoiDungDieuChuyens
                    .Where(x => !idsOfAddresses.Contains(x.Id) && x.DieuChuyenId == nhapxuat.Id)
                     .Where(p => p.DMDonViSuDungId == _tenantProvider.TenantId)
                    .ToListAsync();

                foreach (var item in addressesToDelete)
                {
                    item.DeletedDate = DateTime.Now;
                    _context.Entry(item).State = EntityState.Added;
                };

                await _context.SaveChangesAsync();

                // Log Nhat ky
                await _nhatKyService.LogUpdate("DieuChuyen");
                return new NhapXuatModel();
            }
            catch (Exception ex)
            {

                // Log Nhat ky
                await _nhatKyService.LogError("Update_DieuChuyen", "item : " + nhapxuat.ToString());
                return new NhapXuatModel();
            }

        }

        [HttpPost("Create")]
        public async Task<ActionResult<NhapXuatModel>> Post([FromBody] DieuChuyen item)
        {
            try
            {
                item.CreatedDate = DateTime.Now;
                item.UpdatedDate = DateTime.Now;
                item.DMDonViSuDungId = _tenantProvider.TenantId;
                //var newItem = _context.NhapXuatRepository.Add(item);
                foreach (NoiDungDieuChuyen ite in item.NoiDungDieuChuyens)
                {
                    ite.DMKhoHangId = item.DMKhoHangId;
                    ite.CreatedDate = DateTime.Now;
                }
                _context.NoiDungDieuChuyens.AddRange(item.NoiDungDieuChuyens);

                await _context.SaveChangesAsync();

                // Log Nhat ky
                await _nhatKyService.LogCreate("DieuChuyen");
                return new NhapXuatModel();
            }
            catch (Exception ex)
            {
                await _nhatKyService.LogError("DieuChuyen", ex.Message);
                throw ex;
            }

        }

        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<NhapXuatModel>>> Get(SearchRequest request)
        {
            return await _context.NhapXuatRepository
                //.Include(x => x.NoiDungDieuChuyens)
                //.Where(p => p.DMDonViSuDungId == _tenantProvider.TenantId)
                .Where(x => x.DeletedDate == null)
                .Select(item => new NhapXuatModel(item))
                .ToListAsync();
        }

        [HttpGet("ExportToExcel")]
        public async Task<ActionResult<GetAllResponse<NhapXuatModel>>> ExportToExcel([FromBody] DieuChuyenSearchRequest request)
        {
            GetAllResponse<NhapXuatModel> outputs = await GetData(request, true);

            // Log Nhat ky
            await _nhatKyService.LogExportExcel("DieuChuyen");
            return outputs;
        }

        [HttpGet("GetAllPaged")]
        public async Task<ActionResult<GetAllResponse<NhapXuatModel>>> GetAllPaged([FromBody] DieuChuyenSearchRequest request)
        {
            GetAllResponse<NhapXuatModel> outputs = await GetData(request, true);
            return outputs;
        }

        private async Task<GetAllResponse<NhapXuatModel>> GetData(DieuChuyenSearchRequest request, bool isPaging)
        {
            GetAllResponse<NhapXuatModel> outputs = new();

            Expression<Func<NhapXuat, bool>> filter = m => (1 == 1);
            if (!string.IsNullOrEmpty(request.MaKhoHang))
            {
                filter = filter.And(x => x.MaKho == request.MaKhoHang);
            }
            if (request.DMLoaiTienId > 0)
            {
                filter = filter.And(x => x.LoaiTien == request.DMLoaiTienId.ToString());
            }
            if (!string.IsNullOrEmpty(request.Loai))
            {
                filter = filter.And(x => x.Loai.Equals(request.Loai));
            }
            //if (!string.IsNullOrEmpty(request.MaHangHoa))
            //{
            //    filter = filter.And(x => x.NoiDungDieuChuyens.Any(x => x.MaHangHoa.Contains(request.MaHangHoa)));
            //}
            //if (!string.IsNullOrEmpty(request.TenHangHoa))
            //{
            //    filter = filter.And(x => x.NoiDungDieuChuyens.Any(x => x.TenHangHoa.Contains(request.TenHangHoa)));
            //}
            //if (!string.IsNullOrEmpty(request.DonViTinh))
            //{
            //    filter = filter.And(x => x.NoiDungDieuChuyens.Any(x => x.DonViTinh.Contains(request.DonViTinh)));
            //}

            Func<IQueryable<NhapXuat>, IOrderedQueryable<NhapXuat>> order = null;
            if (request.SortDirection != QLSX.Shared.Enums.SortDirection.None)
            {
                order = await OrderBy(request.SortLable, request.SortDirection == QLSX.Shared.Enums.SortDirection.Ascending);
            }

            IQueryable<NhapXuat> query = _context.NhapXuatRepository.Where(item => item.DeletedDate == null && item.Loai.ToLower() == "dieuchuyen");

            if (filter != null) query = query.Where(filter);
            if (order != null) query = order(query);
            outputs.SumSoTien2 = await query.SumAsync(x => x.SoTienTT) ?? 0;
            outputs.TotalRecords = await query.CountAsync();
            outputs.TotalPages = (int)Math.Ceiling(outputs.TotalRecords / (double)request.PageSize);
            if (outputs.TotalRecords <= request.PageSize)
            {
                request.Page = 0;
            }
            outputs.Page = request.Page;
            outputs.PageSize = request.PageSize;
            if (isPaging)
            {
                query = query.Skip(request.Page * request.PageSize).Take(request.PageSize);
            }
            try
            {
                var joinQuery = (from nhapXuat in query
                                 join noiDung in _context.NoiDungNhapXuatRepository.Where(item => item.DeletedDate == null)
                                 on nhapXuat.LoaiPhieu equals noiDung.LoaiPhieu into noiDungQueryLeft
                                 from noiDungLeft in noiDungQueryLeft.DefaultIfEmpty()
                                 join khoHang in _context.DanhMucKhoHangRepository.Where(item => item.DeletedDate == null)
                                 on nhapXuat.MaKho equals khoHang.MaKho into khoHangQueryLeft
                                 from khoHangLeft in khoHangQueryLeft.DefaultIfEmpty()
                                 join loaiTien in _context.DanhMucLoaiTienRepository.Where(item => item.DeletedDate == null)
                                 on nhapXuat.LoaiTien equals loaiTien.Id.ToString() into loaiTienQueryLeft
                                 from loaiTienLeft in loaiTienQueryLeft.DefaultIfEmpty()
                                 select new
                                 {
                                     nhapXuat,
                                     noiDungLeft,
                                     khoHangLeft,
                                     loaiTienLeft
                                 }).ToList();
                var resultData = joinQuery.GroupBy(item => new { item.nhapXuat })
                                          .Select(item => new NhapXuatModel(
                                                 item.Key.nhapXuat,
                                                 item.Where(item => item.noiDungLeft != null).Select(item => item.noiDungLeft).ToList(),
                                                 item.Select(item => item.khoHangLeft)?.FirstOrDefault() ?? new(),
                                                 item.Select(item => item.loaiTienLeft)?.FirstOrDefault() ?? new()))
                                          .ToList();

                List<NoiDungNhapXuatModel> noiDungNhapXuatList = new();
                foreach (var item in resultData)
                {
                    noiDungNhapXuatList.AddRange(item.NoiDungNhapXuats);
                }
                outputs.Items = resultData;
            }
            catch (Exception ex)
            {

                throw;
            }
            return outputs;
        }

        // GET: api/DieuChuyens
        [HttpGet("GetAllPagedOnTraCuuAll")]
        public async Task<ActionResult<GetAllResponse<TraCuuNhapXuatAll>>> GetAllPagedOnTraCuuAll([FromBody] DieuChuyenSearchRequest request)
        {
            GetAllResponse<TraCuuNhapXuatAll> outputs = new GetAllResponse<TraCuuNhapXuatAll>();
            Expression<Func<TraCuuNhapXuatAll, bool>> filter = m => (1==1);
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

        private async Task<Func<IQueryable<NhapXuat>, IOrderedQueryable<NhapXuat>>> OrderBy(string sortBy, bool sortType)
        {
            Func<IQueryable<NhapXuat>, IOrderedQueryable<NhapXuat>> myFunc;
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
                if (sortType) myFunc = source => source.OrderBy(x => x.MaDonViSuDung);
                else myFunc = source => source.OrderByDescending(x => x.MaDonViSuDung);
                return myFunc;
            }
            if (sortBy == "TenDonVi")
            {
                if (sortType) myFunc = source => source.OrderBy(x => x.TenDonViSuDung);
                else myFunc = source => source.OrderByDescending(x => x.TenDonViSuDung);
                return myFunc;
            }
            if (sortBy == "DiaChi")
            {
                if (sortType) myFunc = source => source.OrderBy(x => x.DiaChiDoiTuong);
                else myFunc = source => source.OrderByDescending(x => x.DiaChiDoiTuong);
                return myFunc;
            }
            //if (sortBy == "TongSoTien")
            //{
            //    if (sortType) myFunc = source => source.OrderBy(x => x.NoiDungDieuChuyens.Sum(s => s.SoTien));
            //    else myFunc = source => source.OrderByDescending(x => x.NoiDungDieuChuyens.Sum(s => s.SoTien));
            //    return myFunc;
            //}
            //if (sortBy == "KyHieu")
            //{
            //    if (sortType) myFunc = source => source.OrderBy(x => x.DMLoaiTiens.KyHieu);
            //    else myFunc = source => source.OrderByDescending(x => x.DMLoaiTiens.KyHieu);
            //    return myFunc;
            //}
            if (sortBy == "SoTienTT")
            {
                if (sortType) myFunc = source => source.OrderBy(x => x.SoTienTT);
                else myFunc = source => source.OrderByDescending(x => x.SoTienTT);
                return myFunc;
            }
            //if (sortBy == "TenKho")
            //{
            //    if (sortType) myFunc = source => source.OrderBy(x => x.DMKhoHang.TenKho);
            //    else myFunc = source => source.OrderByDescending(x => x.DMKhoHang.TenKho);
            //    return myFunc;
            //}
            return null;

        }

        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult<NhapXuat>> Delete(int id)
        {
            var entity = await _context.NhapXuatRepository/*.Where(p => p.DMDonViSuDungId == _tenantProvider.TenantId)*/.FirstOrDefaultAsync(p => p.Id == id);
            if (entity == null)
            {
                return NotFound();
            }

            entity.DeletedDate = DateTime.Now;
            var noidungs = await _context.NoiDungNhapXuatRepository.Where(p => p.LoaiPhieu == entity.LoaiPhieu).ToListAsync();
            foreach (var item in noidungs)
            {
                item.DeletedDate = DateTime.Now;
            }
            await _context.SaveChangesAsync();

            // Log Nhat ky
            await _nhatKyService.LogDeleteNX(new NhapXuatModel(entity));
            return entity;
        }

        [HttpGet("TimKiemNhanh")]
        public async Task<ActionResult<GetAllResponse<NavigatorResponse>>> TimKiemNhanh([FromBody] DieuChuyenSearchRequest request)
        {
            var tenant = _tenantProvider.GetTenant();
            GetAllResponse<NavigatorResponse> outputs = new GetAllResponse<NavigatorResponse>();
            Expression<Func<NhapXuat, bool>> filter = m => 1 == 1;

            if (!string.IsNullOrEmpty(request.Loai))
            {
                filter = filter.And(x => x.Loai.Equals(request.Loai));
            }
            if (!string.IsNullOrEmpty(request.SoPhieu))
            {
                filter = filter.And(x => x.SoChungTu.Contains(request.SoPhieu));
            }

            if (!string.IsNullOrEmpty(request.MaDonVi))
            {
                filter = filter.And(x => x.MaDonViSuDung.Contains(request.MaDonVi));
            }
            if (!string.IsNullOrEmpty(request.TenDonVi))
            {
                filter = filter.And(x => x.TenDonViSuDung.Contains(request.TenDonVi));
            }
            if (!string.IsNullOrEmpty(request.DiaChi))
            {
                filter = filter.And(x => x.DiaChiDoiTuong.Contains(request.DiaChi));
            }

            //if (!string.IsNullOrEmpty(request.MaHangHoa))
            //{
            //    filter = filter.And(x => x.NoiDungDieuChuyens.Any(x => x.MaHangHoa.Contains(request.MaHangHoa)));
            //}
            //if (!string.IsNullOrEmpty(request.TenHangHoa))
            //{
            //    filter = filter.And(x => x.NoiDungDieuChuyens.Any(x => x.TenHangHoa.Contains(request.TenHangHoa)));
            //}
            //if (!string.IsNullOrEmpty(request.DonViTinh))
            //{
            //    filter = filter.And(x => x.NoiDungDieuChuyens.Any(x => x.DonViTinh.Contains(request.DonViTinh)));
            //}

            IQueryable<NhapXuat> query = _context.Set<NhapXuat>();
            //.Include(x => x.NoiDungDieuChuyens);
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

        [HttpGet("GetAllDieuChuyenIDs")]
        public async Task<ActionResult<List<int>>> GetAllDieuChuyenIDs(DieuChuyenSearchRequest request)
        {
            var lst = _context.NhapXuatRepository
                //.Include(x => x.NoiDungDieuChuyens)
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
                var lst = _context.NhapXuatRepository
                //.Include(x => x.NoiDungDieuChuyens)
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


