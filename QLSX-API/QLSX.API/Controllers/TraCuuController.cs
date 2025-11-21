using AutoMapper;
using QLSX.Shared.Constants;
using QLSX.Shared.Models;
using SaleAPI.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SaleAPI.Interfaces;
using System.Net;
using System.Linq.Expressions;
using SaleAPI.Extensions;

namespace SaleAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TraCuuController : ControllerBase
    {
        private readonly CRMDBContext _context;
        private readonly QLSX.Shared.Models.JWTSettings _jwtsettings;
        private readonly IMapper _mapper;
        private readonly ITenantProvider _tenantProvider;
        public IConfiguration _configuration { get; }
        private readonly IWebHostEnvironment _webHostEnvironment;
        public Microsoft.Extensions.Configuration.IConfiguration Configuration { get; }
        public TraCuuController(IWebHostEnvironment webHostEnvironment, IConfiguration configuration,
            CRMDBContext context, IOptions<QLSX.Shared.Models.JWTSettings> jwtsettings, IMapper mapper, ITenantProvider tenantProvider)
        {
            _webHostEnvironment = webHostEnvironment;
            System.Text.Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            Configuration = configuration;
            _context = context;
            _jwtsettings = jwtsettings.Value;
            _mapper = mapper;
            _configuration = configuration;
            _tenantProvider = tenantProvider;
        }

        [HttpGet("hanghoas")]
        public async Task<ActionResult<GetAllResponse<SoTongHopHangHoa>>> SoTongHopHangHoa(TraCuuTonKhoRequest request)
        {
            GetAllResponse<SoTongHopHangHoa> lstApi = new GetAllResponse<SoTongHopHangHoa>();
            try
            {
                var storeExec = string.Format("EXEC dbo.TraCuuHangTonKho @hien={0} ,@date1='{1}', @date2='{2}', @mk={3} , @mdvsd={4} , @tmptblOK='{5}'",
                                               request.Hien,
                                               String.Format("{0:MM/dd/yyyy}", request.TuNgay ?? DateTime.Now),
                                               String.Format("{0:MM/dd/yyyy}", request.DenNgay ?? DateTime.Now),
                                               request.DMKhoHangId,
                                               _tenantProvider.TenantId,
                                               "ZZZTEMPABCZXY");
                //var storeExec = string.Format("EXEC dbo.TraCuuHangTonKho @hien={0}, @date1='{1}', @date2='{2}', @mdvsd={3},@makho ={4},@mahanghoa='{5}',@tenhanghoa='{6}', @donvitinh='{7}', @tmptblOK='{8}'",
                //    request.Hien,
                //    String.Format("{0:MM/dd/yyyy}", request.TuNgay ?? DateTime.Now),
                //    String.Format("{0:MM/dd/yyyy}", request.DenNgay ?? DateTime.Now),
                //    _tenantProvider.TenantId,
                //    request.DMKhoHangId,
                //    request.MaHangHoa,
                //    request.TenHangHoa,
                //    request.DonViTinh,
                //    "ZZZTEMPABCZXY");
                var items = await _context.SoTongHopHangHoas.FromSqlRaw(storeExec).ToListAsync();
                if (request.SortDirection != QLSX.Shared.Enums.SortDirection.None)
                {
                    if (request.SortLable == "MaHangHoa")
                    {
                        if (request.SortDirection == QLSX.Shared.Enums.SortDirection.Ascending) items = items.OrderBy(x => x.MaHangHoa).ToList();
                        else items = items.OrderByDescending(x => x.MaHangHoa).ToList();
                    }
                    else if (request.SortLable == "TenHangHoa")
                    {
                        if (request.SortDirection == QLSX.Shared.Enums.SortDirection.Ascending) items = items.OrderBy(x => x.TenHangHoa).ToList();
                        else items = items.OrderByDescending(x => x.TenHangHoa).ToList();
                    }
                    else if (request.SortLable == "DonViTinh")
                    {
                        if (request.SortDirection == QLSX.Shared.Enums.SortDirection.Ascending) items = items.OrderBy(x => x.DonViTinh).ToList();
                        else items = items.OrderByDescending(x => x.DonViTinh).ToList();
                    }
                }


                lstApi.StatusCode = (int)HttpStatusCode.OK;
                lstApi.Message = ApiResponseMessages.Success;

                lstApi.TotalRecords = items.Count();
                lstApi.TotalPages = (int)Math.Ceiling(lstApi.TotalRecords / (double)request.PageSize);
                lstApi.Page = request.Page;
                lstApi.PageSize = request.PageSize;

                //lstApi.Items = items.Skip(request.Page * request.PageSize).Take(request.PageSize).ToList();
                lstApi.Items = items;

            }
            catch (Exception ex)
            {

                lstApi = new GetAllResponse<SoTongHopHangHoa>()
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError,
                    Message = ex.Message,
                };
            }

            return Ok(lstApi);
        }
        [HttpGet("GetViewDanhSachNhapXuat")]
        public async Task<ActionResult<GetAllResponse<ViewNhapXuat>>> ViewDanhSachNhapXuat(ViewNhapXuatRequest request)
        {
            GetAllResponse<ViewNhapXuat> lstApi = new GetAllResponse<ViewNhapXuat>();
            try
            {
                var storeExec = string.Format("EXEC dbo.GetViewDanhSachNhapXuat @loai='{0}', @date1='{1}', @date2='{2}', @mdvsd={3},@makho ={4},@mahanghoaid='{5}',@tenhanghoa='{6}', @donvitinh='{7}', @tmptblOK='{8}'",
                    request.Loai,
                    request.TuNgay,
                    request.DenNgay,
                    _tenantProvider.TenantId,
                    request.DMKhoHangId,
                    request.DMHangHoaId,
                    request.TenHangHoa,
                    request.DonViTinh,
                    "ZZZTEMPABCZXY");
                var items = await _context.ViewNhapXuats.FromSqlRaw(storeExec).ToListAsync();
                if (request.SortDirection != QLSX.Shared.Enums.SortDirection.None)
                {
                    if (request.SortLable == "SoCT")
                    {
                        if (request.SortDirection == QLSX.Shared.Enums.SortDirection.Ascending) items = items.OrderBy(x => x.SoCT).ToList();
                        else items = items.OrderByDescending(x => x.SoCT).ToList();
                    }
                    else if (request.SortLable == "NgayCT")
                    {
                        if (request.SortDirection == QLSX.Shared.Enums.SortDirection.Ascending) items = items.OrderBy(x => x.NgayCT).ToList();
                        else items = items.OrderByDescending(x => x.NgayCT).ToList();
                    }
                    else if (request.SortLable == "MaDonVi")
                    {
                        if (request.SortDirection == QLSX.Shared.Enums.SortDirection.Ascending) items = items.OrderBy(x => x.MaDonVi).ToList();
                        else items = items.OrderByDescending(x => x.MaDonVi).ToList();
                    }
                    else if (request.SortLable == "TenDonVi")
                    {
                        if (request.SortDirection == QLSX.Shared.Enums.SortDirection.Ascending) items = items.OrderBy(x => x.TenDonVi).ToList();
                        else items = items.OrderByDescending(x => x.TenDonVi).ToList();
                    }
                    else if (request.SortLable == "DiaChi")
                    {
                        if (request.SortDirection == QLSX.Shared.Enums.SortDirection.Ascending) items = items.OrderBy(x => x.DiaChi).ToList();
                        else items = items.OrderByDescending(x => x.DiaChi).ToList();
                    }
                    else if (request.SortLable == "SoTien")
                    {
                        if (request.SortDirection == QLSX.Shared.Enums.SortDirection.Ascending) items = items.OrderBy(x => x.SoTien).ToList();
                        else items = items.OrderByDescending(x => x.SoTien).ToList();
                    }
                    else if (request.SortLable == "DienGiai")
                    {
                        if (request.SortDirection == QLSX.Shared.Enums.SortDirection.Ascending) items = items.OrderBy(x => x.DienGiai).ToList();
                        else items = items.OrderByDescending(x => x.DienGiai).ToList();
                    }
                }

                lstApi.StatusCode = (int)HttpStatusCode.OK;
                lstApi.Message = ApiResponseMessages.Success;

                lstApi.TotalRecords = items.Count();
                lstApi.TotalPages = (int)Math.Ceiling(lstApi.TotalRecords / (double)request.PageSize);
                lstApi.Page = request.Page;
                lstApi.PageSize = request.PageSize;
                lstApi.Items = items;
            }
            catch (Exception ex)
            {

                lstApi = new GetAllResponse<ViewNhapXuat>()
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError,
                    Message = ex.Message,
                };
            }

            return Ok(lstApi);
        }

        [HttpGet("congno")]
        public async Task<ActionResult<GetAllResponse<SoPhaiThuTongHop>>> TraCuuCongNo(TraCuuCongNoRequest request)
        {
            GetAllResponse<SoPhaiThuTongHop> lstApi = new GetAllResponse<SoPhaiThuTongHop>();
            try
            {
                string tungay = "";
                string denngay = "";
                if (request.TuNgay != null)
                {
                    tungay = String.Format("{0:MM/dd/yyyy}", request.TuNgay);
                }
                if (request.DenNgay != null)
                {
                    denngay = String.Format("{0:MM/dd/yyyy}", request.DenNgay);
                }
                var storeExec = string.Format("EXEC dbo.TraCuuSoDuCongNo @hien={0},@loai={1}, @date1='{2}', @date2='{3}', @mdvsd={4},@madonvi ='{5}',@tendonvi='{6}',@diachi='{7}',@dienthoai='{8}', @tmptblOK='{9}'",
                    request.Hien,
                    request.Loai,
                    tungay,
                    denngay,
                    _tenantProvider.TenantId,
                    request.MaDonVi,
                    request.TenDonVi,
                    request.DiaChi,
                    request.DienThoai,
                    "ZZZTEMPABCZXY");
                var items = await _context.SoPhaiThuTongHops.FromSqlRaw(storeExec).ToListAsync();
                if (request.SortDirection != QLSX.Shared.Enums.SortDirection.None)
                {
                    if (request.SortLable == "MaDonVi")
                    {
                        if (request.SortDirection == QLSX.Shared.Enums.SortDirection.Ascending) items = items.OrderBy(x => x.MaDonVi).ToList();
                        else items = items.OrderByDescending(x => x.MaDonVi).ToList();
                    }
                    else if (request.SortLable == "TenDonVi")
                    {
                        if (request.SortDirection == QLSX.Shared.Enums.SortDirection.Ascending) items = items.OrderBy(x => x.TenDonVi).ToList();
                        else items = items.OrderByDescending(x => x.TenDonVi).ToList();
                    }
                    else if (request.SortLable == "DiaChi")
                    {
                        if (request.SortDirection == QLSX.Shared.Enums.SortDirection.Ascending) items = items.OrderBy(x => x.DiaChi).ToList();
                        else items = items.OrderByDescending(x => x.DiaChi).ToList();
                    }

                }


                lstApi.StatusCode = (int)HttpStatusCode.OK;
                lstApi.Message = ApiResponseMessages.Success;

                lstApi.TotalRecords = items.Count();
                lstApi.TotalPages = (int)Math.Ceiling(lstApi.TotalRecords / (double)request.PageSize);
                lstApi.Page = request.Page;
                lstApi.PageSize = request.PageSize;

                //lstApi.Items = items.Skip(request.Page * request.PageSize).Take(request.PageSize).ToList();
                lstApi.Items = items;

            }
            catch (Exception ex)
            {

                lstApi = new GetAllResponse<SoPhaiThuTongHop>()
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError,
                    Message = ex.Message,
                };
            }

            return Ok(lstApi);
        }

        [HttpGet("GetViewDanhSachCongNo")]
        public async Task<ActionResult<GetAllResponse<ViewNhapXuat>>> GetViewDanhSachCongNo(ViewCongNoRequest request)
        {
            GetAllResponse<ViewNhapXuat> lstApi = new GetAllResponse<ViewNhapXuat>();
            try
            {
                var storeExec = string.Format("EXEC dbo.GetViewDanhSachCongNo @loai='{0}', @sodu={1}, @date1='{2}', @date2='{3}', @mdvsd={4},@madonvi ='{5}'",
                    request.Loai,
                    request.sodu,
                    request.TuNgay,
                    request.DenNgay,
                    _tenantProvider.TenantId,
                    request.MaDonVi
                   );
                var items = await _context.ViewNhapXuats.FromSqlRaw(storeExec).ToListAsync();
                if (request.SortDirection != QLSX.Shared.Enums.SortDirection.None)
                {
                    if (request.SortLable == "SoCT")
                    {
                        if (request.SortDirection == QLSX.Shared.Enums.SortDirection.Ascending) items = items.OrderBy(x => x.SoCT).ToList();
                        else items = items.OrderByDescending(x => x.SoCT).ToList();
                    }
                    else if (request.SortLable == "NgayCT")
                    {
                        if (request.SortDirection == QLSX.Shared.Enums.SortDirection.Ascending) items = items.OrderBy(x => x.NgayCT).ToList();
                        else items = items.OrderByDescending(x => x.NgayCT).ToList();
                    }
                    else if (request.SortLable == "MaDonVi")
                    {
                        if (request.SortDirection == QLSX.Shared.Enums.SortDirection.Ascending) items = items.OrderBy(x => x.MaDonVi).ToList();
                        else items = items.OrderByDescending(x => x.MaDonVi).ToList();
                    }
                    else if (request.SortLable == "TenDonVi")
                    {
                        if (request.SortDirection == QLSX.Shared.Enums.SortDirection.Ascending) items = items.OrderBy(x => x.TenDonVi).ToList();
                        else items = items.OrderByDescending(x => x.TenDonVi).ToList();
                    }
                    else if (request.SortLable == "DiaChi")
                    {
                        if (request.SortDirection == QLSX.Shared.Enums.SortDirection.Ascending) items = items.OrderBy(x => x.DiaChi).ToList();
                        else items = items.OrderByDescending(x => x.DiaChi).ToList();
                    }
                    else if (request.SortLable == "SoTien")
                    {
                        if (request.SortDirection == QLSX.Shared.Enums.SortDirection.Ascending) items = items.OrderBy(x => x.SoTien).ToList();
                        else items = items.OrderByDescending(x => x.SoTien).ToList();
                    }
                    else if (request.SortLable == "DienGiai")
                    {
                        if (request.SortDirection == QLSX.Shared.Enums.SortDirection.Ascending) items = items.OrderBy(x => x.DienGiai).ToList();
                        else items = items.OrderByDescending(x => x.DienGiai).ToList();
                    }
                }

                lstApi.StatusCode = (int)HttpStatusCode.OK;
                lstApi.Message = ApiResponseMessages.Success;

                lstApi.TotalRecords = items.Count();
                lstApi.TotalPages = (int)Math.Ceiling(lstApi.TotalRecords / (double)request.PageSize);
                lstApi.Page = request.Page;
                lstApi.PageSize = request.PageSize;

                // lstApi.Items = items.Skip(request.Page * request.PageSize).Take(request.PageSize).ToList();
                lstApi.Items = items;
            }
            catch (Exception ex)
            {

                lstApi = new GetAllResponse<ViewNhapXuat>()
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError,
                    Message = ex.Message,
                };
            }

            return Ok(lstApi);
        }

        // GET: api/NhapXuats
        [HttpGet("TraCuuToanBo")]
        public async Task<ActionResult<GetAllResponse<TraCuuNhapXuatAll>>> TraCuuToanBo([FromBody] NhapXuatSearchRequest request)
        {
            GetAllResponse<TraCuuNhapXuatAll> outputs = new GetAllResponse<TraCuuNhapXuatAll>();
            Expression<Func<TraCuuNhapXuatAll, bool>> filter = m => (1 == 1);
            if (!string.IsNullOrEmpty(request.SoPhieu))
            {
                filter = filter.And(x => x.SoChungTu.Contains(request.SoPhieu));
            }
            if (request.NgayLap_To != null)
            {
                filter = filter.And(x => x.NgayCT <= request.NgayLap_To);
            }
            if (request.NgayLap_From != null)
            {
                filter = filter.And(x => x.NgayCT >= request.NgayLap_From);
            }
            if (!string.IsNullOrEmpty(request.MaDonVi))
            {
                filter = filter.And(x => x.MaDoiTuong.Contains(request.MaDonVi));
            }
            if (!string.IsNullOrEmpty(request.TenDonVi))
            {
                filter = filter.And(x => x.TenDoiTuong.Contains(request.TenDonVi));
            }
            if (!string.IsNullOrEmpty(request.DienGiai))
            {
                filter = filter.And(x => x.DienGiai.Contains(request.DienGiai));
            }
            if (!string.IsNullOrEmpty(request.DiaChi))
            {
                filter = filter.And(x => x.DiaChiDoiTuong.Contains(request.DiaChi));
            }
            if (!string.IsNullOrEmpty(request.Loai))
            {
                filter = filter.And(x => x.Loai.ToLower().Contains(request.Loai.ToLower()));
            }
            if (request.SoTien_From != null)
            {
                filter = filter.And(x => x.SoTien >= request.SoTien_From);
            }
            if (request.SoTien_To != null)
            {
                filter = filter.And(x => x.SoTien <= request.SoTien_To);
            }
            Func<IQueryable<TraCuuNhapXuatAll>, IOrderedQueryable<TraCuuNhapXuatAll>> order = null;
            if (request.SortDirection != QLSX.Shared.Enums.SortDirection.None)
            {
                order = await OrderByTraCuuAll(request.SortLable, request.SortDirection == QLSX.Shared.Enums.SortDirection.Ascending);
            }

            IQueryable<TraCuuNhapXuatAll> query = _context.Set<TraCuuNhapXuatAll>();

            if (filter != null) query = query.Where(filter);
            if (order != null) query = order(query);
            var items = _context.TracuuAlls.FromSqlRaw<TraCuuNhapXuatAll>("select * from TraCuuNhapXuatAll");
            string queryString = query.ToQueryString();
            Console.Write(queryString);
            var totalST = await query.SumAsync(x => x.SoTien);
            outputs.TotalRecords = await query.CountAsync();
            outputs.TotalPages = (int)Math.Ceiling(outputs.TotalRecords / (double)request.PageSize);
            outputs.Page = request.Page;
            outputs.PageSize = request.PageSize;

            query = query.Skip(request.Page * request.PageSize).Take(request.PageSize);
            outputs.Items = await query.ToListAsync();
            outputs.ListData = new List<TraCuuNhapXuatAll>{
                new TraCuuNhapXuatAll { SoTien = totalST  }
            };
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

    }
}
