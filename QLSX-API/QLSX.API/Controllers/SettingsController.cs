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
using QLSX.Shared.Entities;
using AngleSharp.Dom;
using AspNetCore.ReportingServices.ReportProcessing.ReportObjectModel;
 

namespace SaleAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SettingsController : ControllerBase
    {
        private readonly CRMDBContext _context;
        private readonly ITenantProvider _tenantProvider;
        private readonly INhatKyService _nhatKyService;
        public SettingsController(CRMDBContext context, ITenantProvider tenantProvider, INhatKyService nhatKyService)
        {
            _context = context;
            _tenantProvider = tenantProvider;
            _nhatKyService = nhatKyService;
        }
        // GET: api/GetCustomerTypes
        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<SettingModel>>> Get(SettingRequest request)
        {
            //await Task.Delay(3000);
            return await _context.SettingRepository
                //.Include(p => p.User)
                .Where(p => p.UserId == request.UserId || request.UserId == 0)
                .Select(item => new SettingModel(item))
                .ToListAsync();
        }


        [HttpGet("GetCount")]
        public async Task<ActionResult<ItemCount>> GettCount()
        {
            ItemCount itemCount = new ItemCount();

            itemCount.Count = _context.SettingRepository.Count();
            return await Task.FromResult(itemCount);
        }

        // GET: api/Customers
        [HttpGet("GetByPage")]
        public async Task<ActionResult<IEnumerable<SettingModel>>> GetByPage(int pageSize, int pageNumber)
        {
            //pageNumber * pageSize -> take 5
            //ItemList = Items.Skip(pageNumber * PageSize).Take(PageSize).ToList();

            List<SettingModel> list = await _context.SettingRepository.Select(item => new SettingModel(item)).ToListAsync();
            list = list.Skip(pageNumber * pageSize).Take(pageSize).ToList();

            return await Task.FromResult(list);
        }

        // GET: api/Customers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SettingModel>> GetById(int id)
        {
            try
            {
                var item = await _context.SettingRepository.FirstOrDefaultAsync(x => x.UserId == id && x.DeletedDate == null);
                if (item == null)
                {
                    var item1 = await _context.SettingRepository.FindAsync(1);
                    QLSX.Shared.Entities.Settings newItem = new QLSX.Shared.Entities.Settings();
                    newItem = item1;
                    newItem.Id = 0;
                    newItem.UserId = id;
                    newItem.CreatedDate = DateTime.Now;
                    newItem.UpdatedDate = DateTime.Now;
                    _context.SettingRepository.Add(newItem);
                    await _context.SaveChangesAsync();

                    // Log Nhat ky
                    await _nhatKyService.LogCreate("SettingModel");
                    return new SettingModel(newItem);
                }
                return new SettingModel(item);
            }
            catch (Exception ex)
            {

                await _nhatKyService.LogError("SettingModel", ex.Message);
            }


            return new SettingModel();
        }

        // PUT: api/Customers/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("Update/{id}")]
        public async Task<IActionResult> Put(int id, SettingModel item)
        {
            if (id != item.Id)
            {
                return BadRequest();
            }
            QLSX.Shared.Entities.Settings setting = await _context.SettingRepository.FindAsync(id);
            if (setting == null)
            {
                return BadRequest();
            }
            setting.IdId = item.IDID;
            setting.MaDonViCapTren = item.MADONVICAPTREN;
            setting.TenDonViCapTren = item.TENDONVICAPTREN;
            setting.MaDonVi = item.MaDonVi;
            setting.TenDonVi = item.TenDonVi;
            setting.DiaChi = item.DiaChi;
            setting.ChucDanhKeToan = item.CHUCDANHKETOAN;
            setting.ChucDanhLapBieu = item.CHUCDANHLAPBIEU;
            setting.ChucDanhThuTruong = item.CHUCDANHTHUTRUONG;
            setting.HoTenThuTruong = item.HOTENTHUTRUONG;
            setting.NgayThangLB = item.NGAYTHANGLB;
            setting.HoTenNguoiLapBieu = item.HOTENNGUOILAPBIEU;
            setting.HoTenKeToan = item.HOTENKETOAN;
            setting.HoTenThuQuy = item.HOTENTHUQUY;
            setting.MaSoThue = item.MASOTHUE;
            setting.SoTaiKhoan = item.SOTAIKHOAN;
            setting.TenNganHang = item.TENNGANHANG;
            setting.DienThoai = item.DienThoai;
            setting.QuanHuyen = item.QUANHUYEN;
            setting.Email = item.Email;
            setting.Fax = item.FAX;
            setting.TinhThanhPho = item.TINHTHANHPHO;
            setting.GhiChu = item.GHICHU;
            setting.WebSite = item.WEBSITE;
            setting.ChucDanhThuKho = item.CHUCDANHTHUKHO;
            setting.HoTenThuKho = item.HOTENTHUKHO;
            setting.NoiDungNghe = item.NOIDUNGNGHE;
            setting.NganhNghe = item.nganhnghe;
            setting.UserId = item.UserId;
            setting.PathImage = item.PathImage;
            setting.PathLogoImage = item.PathLogoImage;
            setting.NgayBatDauSuDung = item.NgayBatDauSuDung;
            setting.LoaiNhapXuat = item.LoaiNhapXuat;
            setting.IsNhapTheoM2 = item.isNhapTheoM2;
            setting.InPhieuSauThemMoi = item.InPhieuSauThemMoi;
            setting.TuDongThuChi = item.TuDongThuChi;
            setting.TuDongNhapXuat = item.TuDongNhapXuat;
            setting.TuDongDonDatHang = item.TuDongDonDatHang;
            setting.TuDongMaHangHoa = item.TuDongMaHangHoa;
            setting.TuDongMaDonVi = item.TuDongMaDonVi;
            setting.TemplateQR = item.TemplateQR;
            setting.UrlQR = item.URLQR;
            setting.BankQR = item.BankQR;
            item.UpdatedDate = DateTime.Now;
            _context.Entry(setting).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();

                // Log Nhat ky
                await _nhatKyService.LogUpdate("SettingModel");
            }
            catch (DbUpdateConcurrencyException)
            {
                // Log Nhat ky
                await _nhatKyService.LogError("Update_DMSetting", "id : " + id + ";\nitem : " + item.ToString());
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

        // POST: api/Customers
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost("Create")]
        public async Task<ActionResult<SettingModel>> Post(SettingModel item)
        {
            QLSX.Shared.Entities.Settings entity = new();
            entity.CreatedDate = DateTime.Now;
            entity.UpdatedDate = DateTime.Now;
            entity.DMDonViSuDungId = _tenantProvider.TenantId;
            _context.SettingRepository.Add(entity);
            await _context.SaveChangesAsync();

            // Log Nhat ky
            await _nhatKyService.LogCreate("SettingModel");
            return item;
        }

        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult<SettingModel>> Delete(int id)
        {
            var item = await _context.SettingRepository.Where(p => p.DMDonViSuDungId == _tenantProvider.TenantId).Where(p => p.Id == id).FirstOrDefaultAsync();
            if (item == null)
            {
                return NotFound();
            }


            item.DeletedDate = DateTime.Now;
            await _context.SaveChangesAsync();

            // Log Nhat ky
            await _nhatKyService.LogDelete("SettingModel");
            return new SettingModel(item);
        }

        private bool Exists(int id)
        {
            return _context.SettingRepository.Any(e => e.Id == id);
        }
        // GET: api/SettingModel
        [HttpGet("GetAllPaged")]
        public async Task<ActionResult<GetAllResponse<SettingModel>>> GetAllPaged([FromBody] SettingRequest request)
        {
            GetAllResponse<SettingModel> outputs = new GetAllResponse<SettingModel>();
            var query = (
                  from cus in _context.SettingRepository
                      //.Include(p => p.User)
                  select cus);
            if (request.UserId > 0)
            {
                query = (
                 from cus in _context.SettingRepository
                 //.Include(p => p.User)
                 .Where(x => x.UserId == request.UserId)
                 select cus);
            }

            Console.WriteLine(query.ToString());
            if (!string.IsNullOrEmpty(request.Keywords)) query = query.Where(x => x.TenDonVi.ToLower().Contains(request.Keywords.ToLower())
            || x.DiaChi.ToLower().Contains(request.Keywords.ToLower()));

            outputs.TotalRecords = await query.CountAsync();
            outputs.TotalPages = (int)Math.Ceiling(outputs.TotalRecords / (double)request.PageSize);
            outputs.Page = request.Page;
            outputs.PageSize = request.PageSize;

            query = query
                .Skip(request.Page * request.PageSize).Take(request.PageSize);
            outputs.Items = await query.Select(item => new SettingModel(item)).ToListAsync();
            return outputs;
        }
    }
}
