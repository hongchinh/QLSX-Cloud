using AutoMapper;
using QLSX.Shared.Data.Responses;
using QLSX.Shared.DTOs;
using QLSX.Shared.Entities;
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
using static MudBlazor.CategoryTypes;
using AngleSharp.Dom;
using QLSX.Shared.Models.Request;

namespace SaleAPI.Controllers
{
    [ApiController]
    [Route("api/NhapXuatTonCuons")]
    [Authorize]
    public class NhapXuatTonCuonsController : ControllerBase
    {
        private readonly CRMDBContext _context;
        private readonly IMapper _mapper;
        private readonly ITenantProvider _tenantProvider;
        private readonly INhatKyService _nhatKyService;
        public IConfiguration _configuration { get; }
        public NhapXuatTonCuonsController(CRMDBContext context, IMapper mapper, IConfiguration configuration, ITenantProvider tenantProvider, INhatKyService nhatKyService)
        {
            this._context = context;
            _mapper = mapper;
            _configuration = configuration;
            _tenantProvider = tenantProvider;
            _nhatKyService = nhatKyService;
        }

        // GET: api/GetCustomerTypes
        [HttpGet("GetList")]
        public async Task<ActionResult<IEnumerable<NhapXuatTonCuonModel>>> GetList(QLSX.Shared.Models.NhapXuatTonCuonSearchRequest request)
        {
            return await _context.NhapXuatTonCuonRepository
                .Where(p => /*p.DMDonViSuDungId == _tenantProvider.TenantId &&*/ request.Loai == p.Loai)
                .Where(x => x.DeletedDate == null)
                .Select(item => new NhapXuatTonCuonModel(item, new(), new(), new()))
                .ToListAsync();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<NhapXuatTonCuonNavigatorResponse>> GetById(int id)
        {
            var nhapxuat = await _context.NhapXuatTonCuonRepository
                                         .FirstOrDefaultAsync(x => x.DeletedDate == null && x.Id == id);
            if (nhapxuat == null)
            {
                return new NhapXuatTonCuonNavigatorResponse();
            }
            var noiDungNhapXuat = _context.NoiDungNhapXuatTonCuonRepository.Where(item => item.LoaiPhieu == nhapxuat.LoaiPhieu && item.DeletedDate == null).ToList();
            var newnhapxuat = _mapper.Map<NhapXuatTonCuonNavigatorResponse>(new NhapXuatTonCuonModel(nhapxuat, noiDungNhapXuat, new(), new()));

            return newnhapxuat;
        }

        [HttpGet("index/nhapton/{id}")]
        public async Task<ActionResult<NhapXuatTonCuonNavigatorResponse>> GetNhapByIndex(NhapXuatTonCuonSearchRequest request)
        {
            var nhapxuat = await _context.NhapXuatTonCuonRepository
                //.Include(x => x.NoiDungNhapXuatTonCuons)
                //.ThenInclude(x => x.DMHangHoa)
                .OrderBy(x => x.NgayCT)
                .Where(x => x.Loai == "nhapton")
                .Where(x => x.DeletedDate == null)
                .Skip(request.Index - 1).Take(1)
                .FirstOrDefaultAsync();
            if (nhapxuat == null)
            {
                return new NhapXuatTonCuonNavigatorResponse { Total = 0 };
            }

            var countAll = await _context.NhapXuatTonCuonRepository
                //.Where(x => x.DeletedDate == null)
                //.Include(x => x.NoiDungNhapXuatTonCuons)
                .Where(x => x.Loai == "nhapton")
                .GroupBy(nx => nx.Id)
                .Select(gr => new { id = gr.Key }).CountAsync();

            var newnhapxuat = _mapper.Map<NhapXuatTonCuonNavigatorResponse>(nhapxuat);
            newnhapxuat.Total = countAll;

            return newnhapxuat;
        }
        [HttpGet("index/xuatton/{id}")]
        public async Task<ActionResult<NhapXuatTonCuonNavigatorResponse>> GetXuatByIndex(QLSX.Shared.Models.NhapXuatTonCuonSearchRequest request)
        {
            var nhapxuat = await _context.NhapXuatRepository
                 //.Include(x => x.NoiDungNhapXuatTonCuons)
                 .Where(x => x.Loai == "xuatton")
                 .Where(x => x.DeletedDate == null)
                 .OrderBy(x => x.NgayCT)
                .Skip(request.Index - 1).Take(1)
                .FirstOrDefaultAsync();
            if (nhapxuat == null)
            {
                return new NhapXuatTonCuonNavigatorResponse { Total = 0 };
            }

            var countAll = await _context.NhapXuatRepository
                 .Where(x => x.Loai == "xuatton")
                .Where(x => x.DeletedDate == null)
                //.Include(x => x.NoiDungNhapXuatTonCuons)
                .GroupBy(nx => nx.Id)
                .Select(gr => new { id = gr.Key }).CountAsync();

            var newnhapxuat = _mapper.Map<NhapXuatTonCuonNavigatorResponse>(nhapxuat);
            newnhapxuat.Total = countAll;

            return newnhapxuat;
        }

        [HttpPut("update/{id}")]
        public async Task<ActionResult<NhapXuatTonCuonModel>> Put(NhapXuatTonCuonModel model)
        {
            try
            {
                NhapXuatTonCuon entity = _context.NhapXuatTonCuonRepository.FirstOrDefault(item => item.Id == model.Id && item.DeletedDate == null);
                if (entity == null)
                {
                    return new NhapXuatTonCuonModel();
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
                entity.TongCong = model.NoiDungNhapXuatTonCuons?.Sum(x => x.SoTien);
                entity.HanThanhToan = model.NgayHenThanhToan;
                entity.UpdatedDate = DateTime.Now;

                var noiDungNhapXuatDBList = _context.NoiDungNhapXuatTonCuonRepository.Where(item => item.DeletedDate == null && item.LoaiPhieu == entity.LoaiPhieu).ToList();
                foreach (var noiDungModelItem in model.NoiDungNhapXuatTonCuons)
                {
                    var noiDungNhapXuatEntity = noiDungNhapXuatDBList.FirstOrDefault(item => item.IdId == noiDungModelItem.Id);
                    if (noiDungNhapXuatEntity == null)
                    {
                        if (noiDungModelItem.Id > 0)
                        {
                            continue;
                        }
                        noiDungNhapXuatEntity = new NoiDungNhapXuatTonCuon();
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
                    noiDungNhapXuatEntity.NhapXuatTonCuonId = entity.Id;
                    if (noiDungNhapXuatEntity.IdId == 0)
                    {
                        _context.NoiDungNhapXuatTonCuonRepository.AddRange(noiDungNhapXuatEntity);
                    }
                }

                var idsOfAddresses = model.NoiDungNhapXuatTonCuons.Select(x => x.Id).ToList();
                var addressesToDelete = await _context
                                        .NoiDungNhapXuatTonCuonRepository
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
                //await _nhatKyService.LogUpdateNX(entity, new NhapXuatModel(itemNX));
                return model;
            }
            catch (Exception ex)
            {

                // Log Nhat ky
                await _nhatKyService.LogError("Update_NhapXuatTonCuon", "entity : " + model.ToString() + ex.Message);
                return new NhapXuatTonCuonModel();
            }
        }

        [HttpPost("Create")]
        public async Task<ActionResult<NhapXuatTonCuonModel>> Post([FromBody] NhapXuatTonCuonModel model)
        {
            try
            {
                var loaiPhieu = Guid.NewGuid().ToString();
                var loaiTien = _context.DanhMucLoaiTienRepository.FirstOrDefault(item => item.Id == model.DMLoaiTienId);
                NhapXuatTonCuon entity = new();
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
                entity.TongCong = model.NoiDungNhapXuatTonCuons?.Sum(x => x.SoTien);
                entity.HanThanhToan = model.NgayHenThanhToan;
                entity.CreatedDate = DateTime.Now;
                entity.UpdatedDate = DateTime.Now;
                entity.UserName = _tenantProvider.GetUser().EmailAddress;

                var newItem = _context.NhapXuatTonCuonRepository.Add(entity);
                _context.SetTenantIdForEntities(_tenantProvider);


                await _context.SaveChangesAsync();

                List<NoiDungNhapXuatTonCuon> noiDungNhapXuatAddList = new();
                foreach (var ite in model.NoiDungNhapXuatTonCuons)
                {
                    NoiDungNhapXuatTonCuon noiDungNhapXuatTonCuon = new();
                    noiDungNhapXuatTonCuon.LoaiPhieu = entity.LoaiPhieu;
                    noiDungNhapXuatTonCuon.NhapXuatTonCuonId = entity.Id;
                    noiDungNhapXuatTonCuon.MaHangHoa = ite.MaHangHoa;
                    noiDungNhapXuatTonCuon.TenHangHoa = ite.TenHangHoa;
                    noiDungNhapXuatTonCuon.DonViTinh = ite.DonViTinh;
                    noiDungNhapXuatTonCuon.SoLuong = ite.SoLuong;
                    noiDungNhapXuatTonCuon.DonGia = ite.DonGia;
                    noiDungNhapXuatTonCuon.SoTien = ite.SoTien;
                    noiDungNhapXuatTonCuon.KhoRongTon = ite.KhoRongTon;
                    noiDungNhapXuatTonCuon.ChieuDai = ite.ChieuDai;
                    noiDungNhapXuatTonCuon.TongChieuDai = ite.TongChieuDai;
                    noiDungNhapXuatTonCuon.TongDienTich = ite.TongDienTich;
                    noiDungNhapXuatTonCuon.DienGiai = ite.GhiChu;
                    noiDungNhapXuatTonCuon.SoLuongTon = ite.SoLuongTon;
                    noiDungNhapXuatTonCuon.TyLeCkNv = ite.DonGiaHoaHong;
                    noiDungNhapXuatTonCuon.SoTienCkNv = ite.SoTienHoaHong;
                    noiDungNhapXuatTonCuon.CreatedDate = DateTime.Now;
                    noiDungNhapXuatTonCuon.UpdatedDate = DateTime.Now;
                    noiDungNhapXuatTonCuon.MaNhom = ite.MaNhom;
                    noiDungNhapXuatTonCuon.TenNhom = ite.TenNhom;
                    noiDungNhapXuatTonCuon.KieuSong = ite.KieuSong;
                    noiDungNhapXuatTonCuon.MaKieuSong = ite.MaKieuSong;
                    noiDungNhapXuatTonCuon.LoaiTon = ite.LoaiTon;
                    noiDungNhapXuatTonCuon.MaLoaiTon = ite.MaLoaiTon;
                    noiDungNhapXuatTonCuon.DoDay = ite.DoDay;
                    noiDungNhapXuatTonCuon.ChungLoai = ite.ChungLoai;
                    noiDungNhapXuatTonCuon.MaChungLoai = ite.MaChungLoai;
                    noiDungNhapXuatTonCuon.MauSac = ite.MauSac;
                    noiDungNhapXuatTonCuon.MaMauSac = ite.MaMauSac;
                    noiDungNhapXuatAddList.Add(noiDungNhapXuatTonCuon);
                }
                _context.NoiDungNhapXuatTonCuonRepository.AddRange(noiDungNhapXuatAddList);
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

                var result = new NhapXuatTonCuonModel(entity);

                // Log Nhat ky
                await _nhatKyService.LogCreate("NhapXuatTonCuon");
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<NhapXuatTonCuonModel>>> Get(SearchRequest request)
        {
            return await _context.NhapXuatTonCuonRepository
                //.Include(x => x.NoiDungNhapXuatTonCuons)
                //.Include(x => x.User)
                //.Include(x => x.DMKhoHang)
                //.Include(x => x.DMLoaiTiens)
                //.Where(p => p.DMDonViSuDungId == _tenantProvider.TenantId)
                .Where(x => x.DeletedDate == null)
                .Select(item => new NhapXuatTonCuonModel(item))
                .ToListAsync();
        }

        [HttpGet("ExportToExcel")]
        public async Task<ActionResult<GetAllResponse<NhapXuatTonCuonModel>>> ExportToExcel([FromBody] NhapXuatTonCuonSearchRequest request)
        {
            GetAllResponse<NhapXuatTonCuonModel> outputs = await GetData(request, false);

            // Log Nhat ky
            await _nhatKyService.LogExportExcel("NhapXuat");
            return outputs;
        }
        [HttpGet("GetAllPaged")]
        public async Task<ActionResult<GetAllResponse<NhapXuatTonCuonModel>>> GetAllPaged([FromBody] NhapXuatTonCuonSearchRequest request)
        {
            GetAllResponse<NhapXuatTonCuonModel> outputs = await GetData(request, true);
            return outputs;
        }

        private async Task<GetAllResponse<NhapXuatTonCuonModel>> GetData(NhapXuatTonCuonSearchRequest request, bool isPaging)
        {
            GetAllResponse<NhapXuatTonCuonModel> outputs = new GetAllResponse<NhapXuatTonCuonModel>();

            Expression<Func<NhapXuatTonCuon, bool>> filter = m => 1 == 1;
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
                var loaiPhieuList = _context.NoiDungNhapXuatTonCuonRepository.Where(item => item.MaHangHoa.Contains(request.MaHangHoa) && item.DeletedDate == null).Select(item => item.LoaiPhieu).Distinct().ToList();
                if (loaiPhieuList.Any())
                {
                    filter = filter.And(x => loaiPhieuList.Contains(x.LoaiPhieu));
                }
            }
            if (!string.IsNullOrEmpty(request.TenHangHoa))
            {
                var loaiPhieuList = _context.NoiDungNhapXuatTonCuonRepository.Where(item => item.TenHangHoa.Contains(request.TenHangHoa) && item.DeletedDate == null).Select(item => item.LoaiPhieu).Distinct().ToList();
                if (loaiPhieuList.Any())
                {
                    filter = filter.And(x => loaiPhieuList.Contains(x.LoaiPhieu));
                }
            }
            if (!string.IsNullOrEmpty(request.DonViTinh))
            {
                var loaiPhieuList = _context.NoiDungNhapXuatTonCuonRepository.Where(item => item.DonViTinh.Contains(request.DonViTinh) && item.DeletedDate == null).Select(item => item.LoaiPhieu).Distinct().ToList();
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
                var groupSoTien = _context.NoiDungNhapXuatTonCuonRepository.Where(item => item.DeletedDate == null)
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

            Func<IQueryable<NhapXuatTonCuon>, IOrderedQueryable<NhapXuatTonCuon>> order = null;
            if (request.SortDirection != QLSX.Shared.Enums.SortDirection.None)
            {
                order = await OrderBy(request.SortLable, request.SortDirection == QLSX.Shared.Enums.SortDirection.Ascending);
            }

            IQueryable<NhapXuatTonCuon> query = _context.NhapXuatTonCuonRepository.Where(item => item.DeletedDate == null);
            //.Include(x => x.User)
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
            try
            {
                var joinQuery = (from nhapXuat in query
                                 join noiDung in _context.NoiDungNhapXuatTonCuonRepository.Where(item => item.DeletedDate == null)
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

                var resultData = joinQuery.GroupBy(item => item.nhapXuat)
                                          .Select(item => new NhapXuatTonCuonModel(
                                                              item.Key,
                                                              item.Where(item => item.noiDungLeft != null).Select(item => item.noiDungLeft).ToList(),
                                                              item.Select(item => item.khoHangLeft)?.FirstOrDefault(),
                                                              item.Select(item => item.loaiTienLeft)?.FirstOrDefault()))
                                          .ToList();

                outputs.Items = resultData;
                return outputs;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        // GET: api/NhapXuats
        [HttpGet("GetAllPagedOnTraCuuAll")]
        public async Task<ActionResult<GetAllResponse<TraCuuNhapXuatAll>>> GetAllPagedOnTraCuuAll([FromBody] NhapXuatTonCuonSearchRequest request)
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

        private async Task<Func<IQueryable<NhapXuatTonCuon>, IOrderedQueryable<NhapXuatTonCuon>>> OrderBy(string sortBy, bool sortType)
        {
            Func<IQueryable<NhapXuatTonCuon>, IOrderedQueryable<NhapXuatTonCuon>> myFunc;
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
        public async Task<ActionResult<NhapXuatTonCuonModel>> Delete(int id)
        {
            var item = await _context.NhapXuatTonCuonRepository/*Where(p => p.DMDonViSuDungId == _tenantProvider.TenantId)*/
                            .FirstOrDefaultAsync(p => p.Id == id);
            if (item == null)
            {
                return NotFound();
            }

            item.DeletedDate = DateTime.Now;
            var noidungs = await _context.NoiDungNhapXuatTonCuonRepository.Where(p => p.LoaiPhieu == item.LoaiPhieu && p.DeletedDate == null).ToListAsync();
            foreach (var itm in noidungs)
            {
                itm.DeletedDate = DateTime.Now;
            }
            await _context.SaveChangesAsync();

            // Log Nhat ky
            await _nhatKyService.LogDelete("NhapXuatTonCuon");
            return new NhapXuatTonCuonModel(item);
        }

        [HttpGet("TimKiemNhanh")]
        public async Task<ActionResult<GetAllResponse<NavigatorResponse>>> TimKiemNhanh([FromBody] NhapXuatTonCuonSearchRequest request)
        {
            var tenant = _tenantProvider.GetTenant();
            GetAllResponse<NavigatorResponse> outputs = new GetAllResponse<NavigatorResponse>();
            Expression<Func<NhapXuatTonCuonModel, bool>> filter = m => (_tenantProvider.TenantId == 0 || m.DMDonViSuDungId == _tenantProvider.TenantId);

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

            if (!string.IsNullOrEmpty(request.MaHangHoa))
            {
                filter = filter.And(x => x.NoiDungNhapXuatTonCuons.Any(x => x.MaHangHoa.Contains(request.MaHangHoa)));
            }
            if (!string.IsNullOrEmpty(request.TenHangHoa))
            {
                filter = filter.And(x => x.NoiDungNhapXuatTonCuons.Any(x => x.TenHangHoa.Contains(request.TenHangHoa)));
            }
            if (!string.IsNullOrEmpty(request.DonViTinh))
            {
                filter = filter.And(x => x.NoiDungNhapXuatTonCuons.Any(x => x.DonViTinh.Contains(request.DonViTinh)));
            }

            IQueryable<NhapXuatTonCuonModel> query = _context.Set<NhapXuatTonCuonModel>().Include(x => x.NoiDungNhapXuatTonCuons)
              .Include(x => x.User)
              .Include(x => x.DMKhoHang)
              .Include(x => x.DMLoaiTiens);
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

        [HttpGet("GetAllNhapXuatIDs")]
        public async Task<ActionResult<List<int>>> GetAllNhapXuatIDs(NhapXuatTonCuonSearchRequest request)
        {
            var lst = _context.NhapXuatTonCuonRepository
                //.Include(x => x.NoiDungNhapXuatTonCuons)
                //.Include(x => x.User)
                //.Include(x => x.DMKhoHang)
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
                var lst = _context.NhapXuatTonCuonRepository
                //.Include(x => x.NoiDungNhapXuatTonCuons)
                //.Include(x => x.User)
                //.Include(x => x.DMKhoHang)
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


