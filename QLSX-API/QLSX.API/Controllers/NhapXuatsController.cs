using AutoMapper;
using QLSX.Shared.Constants;
using QLSX.Shared.Data.Requests.NhapXuat;
using QLSX.Shared.Data.Responses;
using QLSX.Shared.Data.Responses.NhapXuat;
using QLSX.Shared.DTOs;
using QLSX.Shared.Models;
using QLSX.Shared.Ultils;
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
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MudBlazor;
using Sale.API.Extensions;
using QLSX.Shared.Entities;
using AngleSharp.Io;
using Microsoft.AspNetCore.SignalR;
using Sale.API.SignalR;
using System.Text.Json;
using QLSX.Shared.Models.Request;

namespace SaleAPI.Controllers;

[ApiController]
[Route("api/NhapXuats")]
[Authorize]
public class NhapXuatsController : ControllerBase
{
    private readonly CRMDBContext _context;
    private readonly IMapper _mapper;
    private readonly ITenantProvider _tenantProvider;
    private readonly INhatKyService _nhatKyService;
    private readonly IImageUtilsServices _imageUtilsServices;
    private readonly IHubContext<AppSignalR> _hubContext;

    public IConfiguration _configuration { get; }
    public NhapXuatsController(CRMDBContext context, IMapper mapper, IConfiguration configuration, ITenantProvider tenantProvider, INhatKyService nhatKyService, IImageUtilsServices imageUtilsServices, IHubContext<AppSignalR> hubContext)
    {
        this._context = context;
        _mapper = mapper;
        _configuration = configuration;
        _tenantProvider = tenantProvider;
        _nhatKyService = nhatKyService;
        _imageUtilsServices = imageUtilsServices;
        _hubContext = hubContext;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<NhapXuatNavigatorResponse>> GetById(int id)
    {
        try
        {
            var nhapXuat = await _context.NhapXuatRepository.FirstOrDefaultAsync(x => x.DeletedDate == null && x.Id == id);
            //.Where(p => p.DMDonViSuDungId == _tenantProvider.TenantId)
            if (nhapXuat == null)
            {
                return new NhapXuatNavigatorResponse();
            }

            var noiDungNhapXuatList = _context.NoiDungNhapXuatRepository.Where(item => item.DeletedDate == null && item.LoaiPhieu == nhapXuat.LoaiPhieu).ToList();
            var khoHang = _context.DanhMucKhoHangRepository.FirstOrDefault(item => item.MaKho == nhapXuat.MaKho && item.DeletedDate == null);
            var loaiTien = _context.DanhMucLoaiTienRepository.FirstOrDefault(item => item.Id.ToString() == nhapXuat.LoaiTien && item.DeletedDate == null);

            var nx = new NhapXuatModel(nhapXuat, noiDungNhapXuatList, khoHang, loaiTien);
            var newnhapxuat = _mapper.Map<NhapXuatNavigatorResponse>(nx);

            return newnhapxuat;
        }
        catch (Exception ex)
        {

            throw;
        }
    }

    [HttpGet("GetBySoChungTu")]
    public async Task<ActionResult<NhapXuatNavigatorResponse>> GetBySoChungTu(QLSX.Shared.Models.NhapXuatSearchRequest request)
    {
        var nhapXuat = await _context.NhapXuatRepository.FirstOrDefaultAsync(x => x.DeletedDate == null && x.SoChungTu == request.SoPhieu && x.Loai == request.Loai);
        if (nhapXuat == null || nhapXuat.Id == 0)
        {
            return new NhapXuatNavigatorResponse();
        }
        var nx = new NhapXuatModel(nhapXuat);
        var newnhapxuat = _mapper.Map<NhapXuatNavigatorResponse>(nx);

        return newnhapxuat;
    }

    [HttpGet("index/nhap/{id}")]
    public async Task<ActionResult<NhapXuatNavigatorResponse>> GetNhapByIndex(NhapXuatSearchRequest request)
    {
        var nhapxuat = await _context.NhapXuatRepository
            //.Include(x => x.NoiDungNhapXuats)
            //.ThenInclude(x => x.DMHangHoa)
            .OrderBy(x => x.NgayCT)
            .Where(x => x.Loai == "nhap")
            //.Where(p => p.DMDonViSuDungId == _tenantProvider.TenantId)
            .Where(x => x.DeletedDate == null)
            .Skip(request.Index - 1).Take(1)
            .FirstOrDefaultAsync();
        if (nhapxuat == null)
        {
            return new NhapXuatNavigatorResponse { Total = 0 };
        }

        var countAll = await _context.NhapXuatRepository
            .Where(x => x.DeletedDate == null)
            //.Include(x => x.NoiDungNhapXuats)
            .Where(x => x.Loai == "nhap")
            .GroupBy(nx => nx.Id)
            .Select(gr => new { id = gr.Key }).CountAsync();

        var newnhapxuat = _mapper.Map<NhapXuatNavigatorResponse>(nhapxuat);
        newnhapxuat.Total = countAll;

        return newnhapxuat;
    }

    [HttpGet("index/xuat/{id}")]
    public async Task<ActionResult<NhapXuatNavigatorResponse>> GetXuatByIndex(QLSX.Shared.Models.NhapXuatSearchRequest request)
    {
        var nhapxuat = await _context.NhapXuatRepository
             //.Include(x => x.NoiDungNhapXuats)
             .Where(x => x.Loai == "xuat")
             .Where(x => x.DeletedDate == null)
             //.Where(p => p.DMDonViSuDungId == _tenantProvider.TenantId)
             .OrderBy(x => x.NgayCT)
            .Skip(request.Index - 1).Take(1)
            .FirstOrDefaultAsync();
        if (nhapxuat == null)
        {
            return new NhapXuatNavigatorResponse { Total = 0 };
        }

        var countAll = await _context.NhapXuatRepository
             .Where(x => x.Loai == "xuat")
            .Where(x => x.DeletedDate == null)
            //.Include(x => x.NoiDungNhapXuats)
            .GroupBy(nx => nx.Id)
            .Select(gr => new { id = gr.Key }).CountAsync();

        var newnhapxuat = _mapper.Map<NhapXuatNavigatorResponse>(nhapxuat);
        newnhapxuat.Total = countAll;

        return newnhapxuat;
    }

    [HttpPut("update/{id}")]
    public async Task<ActionResult<NhapXuatModel>> Put(NhapXuatModel model)
    {
        try
        {
            NhapXuat entity = _context.NhapXuatRepository.FirstOrDefault(item => item.Id == model.Id && item.DeletedDate == null);
            if (entity == null)
            {
                return new NhapXuatModel();
            }
            var loaiTien = _context.DanhMucLoaiTienRepository.FirstOrDefault(item => item.Id == model.DMLoaiTienId);
            entity.Loai = model.Loai;
            entity.NgayCT = model.NgayCT;
            entity.SoChungTu = model.SoChungTu;
            entity.MaDoiTuong = model.MaDonVi;
            entity.TenDoiTuong = model.TenDonVi;
            entity.DiaChiDoiTuong = model.DiaChi;
            entity.MaLyDo = model.MaQuanLy;
            entity.NgayGiao = model.NgayGiao;
            entity.NoiGiaoHang = model.NoiGiao;
            entity.MaDonViSuDung = model.MaDonHang;
            entity.HinhThucTT = model.DMHinhThucTTId.ToString();
            entity.TyLeVAT = model.TyleVAT;
            entity.DienGiai = model.DienGiai;
            entity.MaKho = model.MaKho;
            entity.SoTienTT = model.SoTienTT;
            entity.HinhThucGiaoHang = model.PhuongTien;
            entity.LoaiTien = model.LoaiTien;
            entity.TongCong = model.NoiDungNhapXuats?.Sum(x => x.SoTien);
            entity.HanThanhToan = model.NgayHenThanhToan;
            entity.UpdatedDate = DateTime.Now;
            entity.SoTienCK = model.SoTienCK;
            entity.SoTienVAT = model.SoTienVAT;
            entity.SoTienVC = model.SoTienVC;
            entity.SoTienGiam = model.SoTienGiam;
            entity.TrangThai = model.TrangThai;
            var noiDungNhapXuatDBList = _context.NoiDungNhapXuatRepository.Where(item => item.DeletedDate == null && item.LoaiPhieu == entity.LoaiPhieu).ToList();
            foreach (var noiDungModelItem in model.NoiDungNhapXuats)
            {
                var noiDungNhapXuatEntity = noiDungNhapXuatDBList.FirstOrDefault(item => item.IdId == noiDungModelItem.Id);
                if (noiDungNhapXuatEntity == null)
                {
                    if (noiDungModelItem.Id > 0)
                    {
                        continue;
                    }
                    noiDungNhapXuatEntity = new NoiDungNhapXuat();
                    noiDungNhapXuatEntity.LoaiPhieu = entity.LoaiPhieu;
                    noiDungNhapXuatEntity.CreatedDate = DateTime.Now;
                    noiDungNhapXuatEntity.IdId = 0;
                }
                noiDungNhapXuatEntity.MaHangHoa = noiDungModelItem.MaHangHoa;
                noiDungNhapXuatEntity.TenHangHoa = noiDungModelItem.TenHangHoa;
                noiDungNhapXuatEntity.DonViTinh = noiDungModelItem.DonViTinh;
                noiDungNhapXuatEntity.SoLuong = noiDungModelItem.SoLuong;
                noiDungNhapXuatEntity.DonGia = noiDungModelItem.DonGia;
                noiDungNhapXuatEntity.SoTien = noiDungModelItem.SoTien;
                noiDungNhapXuatEntity.KhoRongTon = noiDungModelItem.KhoRongTon;
                noiDungNhapXuatEntity.ChieuDai = noiDungModelItem.ChieuDai;
                noiDungNhapXuatEntity.TongChieuDai = noiDungModelItem.TongChieuDai;
                noiDungNhapXuatEntity.TongDienTich = noiDungModelItem.TongDienTich;
                noiDungNhapXuatEntity.DienGiai = noiDungModelItem.GhiChu;
                noiDungNhapXuatEntity.SoLuongTon = noiDungModelItem.SoLuongTon;
                noiDungNhapXuatEntity.TyLeCkNv = noiDungModelItem.DonGiaHoaHong;
                noiDungNhapXuatEntity.SoTienCkNv = noiDungModelItem.SoTienHoaHong;
                noiDungNhapXuatEntity.UpdatedDate = DateTime.Now;

                noiDungNhapXuatEntity.MaNhom = noiDungModelItem.MaNhom;
                noiDungNhapXuatEntity.TenNhom = noiDungModelItem.TenNhom;
                noiDungNhapXuatEntity.KieuSong = noiDungModelItem.KieuSong;
                noiDungNhapXuatEntity.MaKieuSong = noiDungModelItem.MaKieuSong;
                noiDungNhapXuatEntity.LoaiTon = noiDungModelItem.LoaiTon;
                noiDungNhapXuatEntity.MaLoaiTon = noiDungModelItem.MaLoaiTon;
                noiDungNhapXuatEntity.DoDay = noiDungModelItem.DoDay;
                noiDungNhapXuatEntity.ChungLoai = noiDungModelItem.ChungLoai;
                noiDungNhapXuatEntity.MaChungLoai = noiDungModelItem.MaChungLoai;
                noiDungNhapXuatEntity.MauSac = noiDungModelItem.MauSac;
                noiDungNhapXuatEntity.MaMauSac = noiDungModelItem.MaMauSac;
                noiDungNhapXuatEntity.NhapXuatId = entity.Id;
                noiDungNhapXuatEntity.TrangThaiDetail = noiDungModelItem.TrangThaiDetail;
                if (noiDungNhapXuatEntity.IdId == 0)
                {
                    _context.NoiDungNhapXuatRepository.AddRange(noiDungNhapXuatEntity);
                }
            }

            var idsOfAddresses = model.NoiDungNhapXuats.Select(x => x.Id).ToList();
            var addressesToDelete = await _context
                                    .NoiDungNhapXuatRepository
                                    .Where(x => !idsOfAddresses.Contains(x.IdId) && x.LoaiPhieu == entity.LoaiPhieu)
                                    .ToListAsync();

            foreach (var item in addressesToDelete)
            {
                item.DeletedDate = DateTime.Now;
            }
            await _context.SaveChangesAsync();

            ////File attach
            //foreach (var entity in entity.tblFileAttachments)
            //{
            //    if (entity.id != 0)
            //    {
            //        _context.Entry(entity).State = EntityState.Modified;
            //    }
            //    else
            //    {
            //        _context.Entry(entity).State = EntityState.Added;
            //    }
            //}

            //var idsOfAddresses1 = entity.tblFileAttachments.Select(x => x.id).ToList();
            //var addressesToDelete1 = await _context
            //    .tblFileAttachments
            //    .Where(x => !idsOfAddresses1.Contains(x.id) && x.IdPhieu == entity.Id)
            //    .ToListAsync();

            //foreach (var entity in addressesToDelete1)
            //{
            //    entity.DeletedDate = DateTime.Now;
            //    _context.Entry(entity).State = EntityState.Added;
            //}

            await _context.SaveChangesAsync();

            //save image
            //var itemImage = await _imageUtilsServices.CreateImageBarcode(_tenantProvider.TenantId, _tenantProvider.UserId, entity.Loai, entity.Id, entity.SoTien ?? 0, "Thanh toán đơn hàng " + entity.SoCT + " - " + entity.MaDonVi);

            // Log Nhat ky
            var itemNX = await _context.NhapXuatRepository.FindAsync(model.Id);
            await _nhatKyService.LogUpdateNX(new NhapXuatModel(entity), new NhapXuatModel(itemNX));

            // Send Socket to Blazer page
            if (entity.Loai.ToLower().Equals("donhang"))
            {
                var idIdList = _context.NoiDungNhapXuatRepository.Where(item => item.DeletedDate == null
                                                                                && item.LoaiPhieu == entity.LoaiPhieu)
                                                                 .Select(item => item.IdId)
                                                                 .Distinct()
                                                                 .ToList();
                await SendSocket(SignalRKey.UpdateDonHangSocketKey, idIdList);
            }
            return model;
        }
        catch (Exception ex)
        {

            // Log Nhat ky
            await _nhatKyService.LogError("Update_NhapXuat", "entity : " + model.ToString() + ex.Message);
            return new NhapXuatModel();
        }
    }

    [HttpPost("Create")]
    public async Task<ActionResult<NhapXuatModel>> Post([FromBody] NhapXuatModel model)
    {
        try
        {
            var loaiPhieu = Guid.NewGuid().ToString();
            var loaiTien = _context.DanhMucLoaiTienRepository.FirstOrDefault(item => item.Id == model.DMLoaiTienId);
            NhapXuat entity = new();
            entity.LoaiPhieu = loaiPhieu;
            entity.Loai = model.Loai;
            entity.NgayCT = model.NgayCT;
            entity.SoChungTu = model.SoChungTu;
            entity.MaDoiTuong = model.MaDonVi;
            entity.TenDoiTuong = model.TenDonVi;
            entity.DiaChiDoiTuong = model.DiaChi;
            entity.MaLyDo = model.MaLyDo;
            entity.TenLyDo = model.TenLyDo;
            entity.NgayGiao = model.NgayGiao;
            entity.NoiGiaoHang = model.NoiGiao;
            entity.MaDonViSuDung = model.MaDonHang;
            entity.HinhThucTT = model.DMHinhThucTTId.ToString();
            entity.TyLeVAT = model.TyleVAT;
            entity.DienGiai = model.DienGiai;
            entity.MaKho = model.MaKho;
            entity.LoaiTien = model.LoaiTien;
            entity.SoTienTT = model.SoTienTT;
            entity.HinhThucGiaoHang = model.PhuongTien;
            entity.TongCong = model.NoiDungNhapXuats?.Sum(x => x.SoTien);
            entity.HanThanhToan = model.NgayHenThanhToan;
            entity.CreatedDate = DateTime.Now;
            entity.UpdatedDate = DateTime.Now;
            entity.SoTienCK = model.SoTienCK;
            entity.SoTienVAT = model.SoTienVAT;
            entity.SoTienVC = model.SoTienVC;
            entity.SoTienGiam = model.SoTienGiam;
            entity.TrangThai = model.TrangThai;
            var newItem = _context.NhapXuatRepository.Add(entity);

            await _context.SaveChangesAsync();

            List<NoiDungNhapXuat> noiDungNhapXuatAddList = new();
            foreach (var ite in model.NoiDungNhapXuats)
            {
                NoiDungNhapXuat noiDungNhapXuat = new();
                noiDungNhapXuat.LoaiPhieu = entity.LoaiPhieu;
                noiDungNhapXuat.NhapXuatId = entity.Id;
                noiDungNhapXuat.MaHangHoa = ite.MaHangHoa;
                noiDungNhapXuat.TenHangHoa = ite.TenHangHoa;
                noiDungNhapXuat.DonViTinh = ite.DonViTinh;
                noiDungNhapXuat.SoLuong = ite.SoLuong;
                noiDungNhapXuat.DonGia = ite.DonGia;
                noiDungNhapXuat.SoTien = ite.SoTien;
                noiDungNhapXuat.KhoRongTon = ite.KhoRongTon;
                noiDungNhapXuat.ChieuDai = ite.ChieuDai;
                noiDungNhapXuat.TongChieuDai = ite.TongChieuDai;
                noiDungNhapXuat.TongDienTich = ite.TongDienTich;
                noiDungNhapXuat.DienGiai = ite.GhiChu;
                noiDungNhapXuat.SoLuongTon = ite.SoLuongTon;
                noiDungNhapXuat.TyLeCkNv = ite.DonGiaHoaHong;
                noiDungNhapXuat.SoTienCkNv = ite.SoTienHoaHong;
                noiDungNhapXuat.CreatedDate = DateTime.Now;
                noiDungNhapXuat.UpdatedDate = DateTime.Now;
                noiDungNhapXuat.MaNhom = ite.MaNhom;
                noiDungNhapXuat.TenNhom = ite.TenNhom;
                noiDungNhapXuat.KieuSong = ite.KieuSong;
                noiDungNhapXuat.MaKieuSong = ite.MaKieuSong;
                noiDungNhapXuat.LoaiTon = ite.LoaiTon;
                noiDungNhapXuat.MaLoaiTon = ite.MaLoaiTon;
                noiDungNhapXuat.DoDay = ite.DoDay;
                noiDungNhapXuat.ChungLoai = ite.ChungLoai;
                noiDungNhapXuat.MaChungLoai = ite.MaChungLoai;
                noiDungNhapXuat.MauSac = ite.MauSac;
                noiDungNhapXuat.MaMauSac = ite.MaMauSac;
                noiDungNhapXuat.TrangThaiDetail = ite.TrangThaiDetail;
                noiDungNhapXuatAddList.Add(noiDungNhapXuat);
            }
            _context.NoiDungNhapXuatRepository.AddRange(noiDungNhapXuatAddList);
            await _context.SaveChangesAsync();

            //foreach (tblFileAttachment noiDungModelItem in entity.tblFileAttachments)
            //{
            //    noiDungModelItem.DMDonViSuDungId = _tenantProvider.TenantId;
            //    noiDungModelItem.CreatedDate = DateTime.Now;
            //    noiDungModelItem.UpdatedDate = DateTime.Now;
            //    noiDungModelItem.UserId = _tenantProvider.UserId;
            //    noiDungModelItem.Loai = entity.Loai;
            //    noiDungModelItem.IdPhieu = entity.Id;
            //}
            //_context.tblFileAttachments.AddRange(entity.tblFileAttachments);
            await _context.SaveChangesAsync();
            //save image
            //var itemImage = await _imageUtilsServices.CreateImageBarcode(_tenantProvider.TenantId, _tenantProvider.UserId, entity.Loai, entity.Id, entity.SoTien ?? 0, "Thanh toán đơn hàng " + entity.SoCT + " - " + entity.MaDonVi);

            var result = new NhapXuatModel(entity);

            // Log Nhat ky
            await _nhatKyService.LogCreateNX(model);

            // Send Socket to Blazer page
            if (entity.Loai.ToLower().Equals("donhang"))
            {
                var idIdList = _context.NoiDungNhapXuatRepository.Where(item => item.DeletedDate == null
                                                                                && item.LoaiPhieu == entity.LoaiPhieu)
                                                                 .Select(item => item.IdId)
                                                                 .Distinct()
                                                                 .ToList();
                await SendSocket(SignalRKey.CreateDonHangSocketKey, idIdList);
            }
            return result;
        }
        catch (Exception ex)
        {

            throw ex;
        }
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

    [HttpGet("")]
    public async Task<ActionResult<IEnumerable<NhapXuat>>> Get(SearchRequest request)
    {
        return await _context.NhapXuatRepository
            //.Include(x => x.NoiDungNhapXuats)
            //.Include(x => x.User)
            //.Include(x => x.DanhMucKhoHangModel)
            //.Include(x => x.DMLoaiTiens)
            //.Where(p => p.DMDonViSuDungId == _tenantProvider.TenantId)
            .Where(x => x.DeletedDate == null)
            .ToListAsync();
    }

    [HttpGet("ExportToExcel")]
    public async Task<ActionResult<GetAllResponse<NhapXuatModel>>> ExportToExcel([FromBody] NhapXuatSearchRequest request)
    {
        GetAllResponse<NhapXuatModel> outputs = await GetData(request, false);

        // Log Nhat ky
        await _nhatKyService.LogExportExcel("NhapXuat");
        return outputs;
    }

    [HttpGet("GetAllPaged")]
    public async Task<ActionResult<GetAllResponse<NhapXuatModel>>> GetAllPaged([FromBody] NhapXuatSearchRequest request)
    {
        GetAllResponse<NhapXuatModel> outputs = await GetData(request, true);
        return outputs;
    }

    private async Task<GetAllResponse<NhapXuatModel>> GetData(NhapXuatSearchRequest request, bool isPaging)
    {
        GetAllResponse<NhapXuatModel> outputs = new GetAllResponse<NhapXuatModel>();
        ICollection<FilterDefinition<NhapXuat>> filter1 = request.Filter;

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
            filter = filter.And(x => x.Loai.ToLower().Equals(request.Loai.ToLower()));
        }
        if (!string.IsNullOrEmpty(request.SoPhieu))
        {
            filter = filter.And(x => x.SoChungTu.Contains(request.SoPhieu));
        }
        if (request.NgayLap_From != null)
        {
            filter = filter.And(x => x.NgayCT >= request.NgayLap_From);
        }
        if (request.NgayLap_To != null)
        {
            filter = filter.And(x => x.NgayCT <= request.NgayLap_To);
        }

        //đơn vị
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
            filter = filter.And(x => x.DiaChiDoiTuong.Contains(request.DiaChi));
        }
        //if (!string.IsNullOrEmpty(request.DienThoai))
        //{
        //    filter = filter.And(x => x.DienThoai.Contains(request.DienThoai));
        //}
        if (!string.IsNullOrEmpty(request.TenKho))
        {
            var maKhoList = _context.DanhMucKhoHangRepository.Where(item => item.DeletedDate == null && item.TenKho.Contains(request.TenKho)).Select(item => item.MaKho).Distinct().ToList();
            if (maKhoList.Any())
            {
                filter = filter.And(x => maKhoList.Contains(x.MaKho));
            }
        }

        // hàng hóa
        if (!string.IsNullOrEmpty(request.MaHangHoa))
        {
            var loaiPhieuList = _context.NoiDungNhapXuatRepository.Where(item => item.MaHangHoa.Contains(request.MaHangHoa) && item.DeletedDate == null).Select(item => item.LoaiPhieu).Distinct().ToList();
            if (loaiPhieuList.Any())
            {
                filter = filter.And(x => loaiPhieuList.Contains(x.LoaiPhieu));
            }
        }
        if (!string.IsNullOrEmpty(request.TenHangHoa))
        {
            var loaiPhieuList = _context.NoiDungNhapXuatRepository.Where(item => item.TenHangHoa.Contains(request.TenHangHoa) && item.DeletedDate == null).Select(item => item.LoaiPhieu).Distinct().ToList();
            if (loaiPhieuList.Any())
            {
                filter = filter.And(x => loaiPhieuList.Contains(x.LoaiPhieu));
            }
        }
        if (!string.IsNullOrEmpty(request.DonViTinh))
        {
            var loaiPhieuList = _context.NoiDungNhapXuatRepository.Where(item => item.DonViTinh.Contains(request.DonViTinh) && item.DeletedDate == null).Select(item => item.LoaiPhieu).Distinct().ToList();
            if (loaiPhieuList.Any())
            {
                filter = filter.And(x => loaiPhieuList.Contains(x.LoaiPhieu));
            }
        }

        if (request.SoTienTT_From != null && request.SoTienTT_From > 0)
        {
            filter = filter.And(x => x.SoTienTT >= request.SoTienTT_From);
        }
        if (request.SoTienTT_To != null && request.SoTienTT_To > 0)
        {
            filter = filter.And(x => x.SoTienTT <= request.SoTienTT_To);
        }

        if ((request.SoTien_From != null && request.SoTien_From > 0) || (request.SoTien_To != null && request.SoTien_To > 0))
        {
            var groupSoTien = _context.NoiDungNhapXuatRepository.Where(item => item.DeletedDate == null)
                              .GroupBy(item => item.LoaiPhieu)
                              .Select(item => new { item.Key, item.Sum(x => x.SoTien).Value })
                              .ToList();

            if (request.SoTien_From != null && request.SoTien_From > 0)
            {
                groupSoTien = groupSoTien.Where(item => item.Value >= request.SoTien_From).ToList();
            }

            if (request.SoTien_To != null && request.SoTien_To > 0)
            {
                groupSoTien = groupSoTien.Where(item => item.Value <= request.SoTien_To).ToList();
            }
            var loaiPhieuList = groupSoTien.Select(item => item.Key).Distinct().ToList();

            filter = filter.And(x => loaiPhieuList.Contains(x.LoaiPhieu));
        }

        Func<IQueryable<NhapXuat>, IOrderedQueryable<NhapXuat>> order = null;
        if (request.SortDirection != QLSX.Shared.Enums.SortDirection.None)
        {
            order = await OrderBy(request.SortLable, request.SortDirection == QLSX.Shared.Enums.SortDirection.Ascending);
        }
        Expression<Func<NhapXuat, bool>> expression;

        IQueryable<NhapXuat> query = _context.NhapXuatRepository.Where(x => x.DeletedDate == null);

        switch (request.QueryType)
        {
            case 2:
                query = query.Where(item => item.Loai.ToLower() == "dieuchuyen");
                break;
        }
        FilterBuider<NhapXuat> filterBuider;
        if (filter1 != null)
        {
            foreach (var f in filter1)
            {
                var dataType = typeof(NhapXuat).GetProperty(f.Field).PropertyType;
                if (dataType == typeof(DateTime?) || dataType == typeof(DateTime))
                {
                    var fter = GetFilterDateTime(filter, f.Operator, (DateTime)f.Value);
                    query = (IQueryable<NhapXuat>)query.Where(fter);

                }
                else
                {
                    filterBuider = new FilterBuider<NhapXuat>(f);
                    var filterFunc = filterBuider.GetFilter;
                    query = (IQueryable<NhapXuat>)query.Where(filterFunc);
                }

            }
        }
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
                                             item.Select(item => item.khoHangLeft)?.FirstOrDefault(),
                                             item.Select(item => item.loaiTienLeft)?.FirstOrDefault()))
                                      .ToList();

            List<NoiDungNhapXuatModel> noiDungNhapXuatList = new();
            foreach (var item in resultData)
            {
                noiDungNhapXuatList.AddRange(item.NoiDungNhapXuats);
            }
            outputs.Items = resultData;
            outputs.SumSoTien1 = (double)noiDungNhapXuatList.Sum(x => x.SoTien);
        }
        catch (Exception ex)
        {

            throw;
        }
        return outputs;
    }

    private Expression<Func<NhapXuat, bool>> GetFilterDateTime(Expression<Func<NhapXuat, bool>> filter, string Operator, DateTime Value)
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

    // GET: api/NhapXuats
    [HttpGet("GetAllPagedOnTraCuuAll")]
    public async Task<ActionResult<GetAllResponse<TraCuuNhapXuatAll>>> GetAllPagedOnTraCuuAll([FromBody] NhapXuatSearchRequest request)
    {
        GetAllResponse<TraCuuNhapXuatAll> outputs = new GetAllResponse<TraCuuNhapXuatAll>();
        //Expression<Func<TraCuuNhapXuatAll, bool>> filter = m => (_tenantProvider.TenantId == 0 || m.DMDonViSuDungId == _tenantProvider.TenantId);
        //if (!string.IsNullOrEmpty(request.MaDonVi))
        //{
        //    filter = filter.And(x => x.MaDonVi == request.MaDonVi);
        //}
        //if (!string.IsNullOrEmpty(request.Loai))
        //{
        //    filter = filter.And(x => x.Loai == request.Loai);
        //}

        //Func<IQueryable<TraCuuNhapXuatAll>, IOrderedQueryable<TraCuuNhapXuatAll>> order = null;
        //if (request.SortDirection != QLSX.Shared.Enums.SortDirection.None)
        //{
        //    order = await OrderByTraCuuAll(request.SortLable, request.SortDirection == QLSX.Shared.Enums.SortDirection.Ascending);
        //}

        //IQueryable<TraCuuNhapXuatAll> query = _context.Set<TraCuuNhapXuatAll>();

        //if (filter != null) query = query.Where(filter);
        //if (order != null) query = order(query);
        //Console.Write("AAAAAAAAA");
        //Console.Write(query.ToQueryString());

        //outputs.TotalRecords = await query.CountAsync();
        //outputs.TotalPages = (int)Math.Ceiling(outputs.TotalRecords / (double)request.PageSize);
        //outputs.Page = request.Page;
        //outputs.PageSize = request.PageSize;

        //query = query.Skip(request.Page * request.PageSize).Take(request.PageSize);
        //outputs.Items = await query.ToListAsync();
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
        if (sortBy == "SoChungTu")
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
        if (sortBy == "TongCong")
        {
            if (sortType) myFunc = source => source.OrderBy(x => x.TongCong);
            else myFunc = source => source.OrderByDescending(x => x.TongCong);
            return myFunc;
        }

        if (sortBy == "SoTienTT")
        {
            if (sortType) myFunc = source => source.OrderBy(x => x.SoTienTT);
            else myFunc = source => source.OrderByDescending(x => x.SoTienTT);
            return myFunc;
        }
        if (sortBy == "TenKho")
        {
            if (sortType) myFunc = source => source.OrderBy(x => x.TenKho);
            else myFunc = source => source.OrderByDescending(x => x.TenKho);
            return myFunc;
        }
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
        var noidungs = await _context.NoiDungNhapXuatRepository.Where(p => p.LoaiPhieu == entity.LoaiPhieu && p.DeletedDate == null).ToListAsync();
        foreach (var item in noidungs)
        {
            item.DeletedDate = DateTime.Now;
        }
        await _context.SaveChangesAsync();

        // Log Nhat ky
        await _nhatKyService.LogDeleteNX(new NhapXuatModel(entity));

        // Send Socket to Blazer page
        if (entity.Loai.ToLower().Equals("donhang"))
        {
            var idIdList = noidungs.Select(item => item.IdId).Distinct().ToList();
            await SendSocket(SignalRKey.DeleteDonHangSocketKey, idIdList);
        }
        return entity;
    }

    [HttpPost("InPhieuNhap")]
    public async Task<ActionResult<ReportResponseBase<InPhieuNhapXuatResponse>>> InPhieuNhap(InPhieuNhapRequest request)
    {
        string StoredProc = "EXEC InPhieuNhap @id = " + request.Id.ToString() + ", @mdvsd = " + _tenantProvider.TenantId.ToString();
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
        var lst = ConvertDatatableToList.ConvertToList<InPhieuNhapXuatResponse>(ds.Tables[0]);
        var lstApi = new ReportResponseBase<InPhieuNhapXuatResponse>()
        {
            StatusCode = ApiResponseCodes.Success,
            Message = ApiResponseMessages.Success,
            ListData = lst,
            ThongTin = ttres
        };
        return Ok(lstApi);

    }

    [HttpPost("InPhieuXuat")]
    public async Task<ActionResult<ReportResponseBase<InPhieuNhapXuatResponse>>> InPhieuXuat(InPhieuXuatRequest request)
    {
        string StoredProc = "EXEC InPhieuXuat @id = " + request.Id.ToString() + ", @mdvsd = " + _tenantProvider.TenantId.ToString();
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
        var lst = ConvertDatatableToList.ConvertToList<InPhieuNhapXuatResponse>(ds.Tables[0]);
        var lstApi = new ReportResponseBase<InPhieuNhapXuatResponse>()
        {
            StatusCode = ApiResponseCodes.Success,
            Message = ApiResponseMessages.Success,
            ListData = lst,
            ThongTin = ttres
        };
        return Ok(lstApi);

    }

    [HttpGet("TimKiemNhanh")]
    public async Task<ActionResult<GetAllResponse<NavigatorResponse>>> TimKiemNhanh([FromBody] NhapXuatSearchRequest request)
    {
        GetAllResponse<NavigatorResponse> outputs = new GetAllResponse<NavigatorResponse>();
        var data = await GetData(request, false);

        var lst = data.Items?.Select((x, index) => new NavigatorResponse
        {
            Index = index,
            Id = x.Id
        }).ToList();
        outputs.Items = lst;
        return outputs;
    }

    [HttpGet("GetAllNhapXuatIDs")]
    public async Task<ActionResult<List<int>>> GetAllNhapXuatIDs(NhapXuatSearchRequest request)
    {
        var lst = _context.NhapXuatRepository
            //.Include(x => x.NoiDungNhapXuats)
            //.Include(x => x.User)
            //.Include(x => x.DanhMucKhoHangModel)
            //.Include(x => x.DMLoaiTiens)
            //.Where(p => p.DMDonViSuDungId == _tenantProvider.TenantId)
            .Where(x => x.DeletedDate == null)
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
            var lst = await _context.NhapXuatRepository
           //.Include(x => x.NoiDungNhapXuats)
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

    [HttpGet("GetImage")]
    public async Task<ActionResult<int>> GetImage(string loai)
    {
        var item = await _imageUtilsServices.CreateImageBarcode(_tenantProvider.TenantId, _tenantProvider.UserId, "nhap", 1, 20000, "wewwrwe");
        return 3;
    }

    [HttpGet("GetDonHangByMaDonVi")]
    public async Task<ActionResult<List<NhapXuatModel>>> GetDonHangByMaDonVi(string maDoiTuong)
    {
        var result = await _context.NhapXuatRepository
                                   .Where(x => x.MaDoiTuong == maDoiTuong
                                               && x.Loai.ToUpper() == "DONHANG"
                                               && x.DeletedDate == null)
                                   .OrderBy(x => x.NgayCT)
                                   .Select(item => new NhapXuatModel(item))
                                   .ToListAsync();
        return result;
    }

    [HttpGet("GetHangHoaByLoaiPhieu")]
    public async Task<ActionResult<List<NoiDungNhapXuatModel>>> GetHangHoaByLoaiPhieu(string loaiPhieu)
    {
        var result = await _context.NoiDungNhapXuatRepository
                                   .Where(x => x.LoaiPhieu == loaiPhieu
                                               && x.DeletedDate == null)
                                   .Select(item => new NoiDungNhapXuatModel(item))
                                   .ToListAsync();
        for (int i = 0; i < result.Count; i++)
        {
            result[i].Stt = (i + 1).ToString();
        }
        return result;
    }
}


