using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SaleAPI.Models;
using QLSX.Shared.Models;
using QLSX.Shared.Ultils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Caching.Memory;
using SaleAPI.Services;
using System.Linq.Expressions;
using SaleAPI.Extensions;
using SaleAPI.Interfaces;
using Sale.API.Extensions;
using MudBlazor;
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
    public class DMHangHoasController : ControllerBase
    {
        private readonly CRMDBContext _context;
        private IMemoryCache _cache;
        private readonly ITenantProvider _tenantProvider;
        private readonly INhatKyService _nhatKyService;
        public Microsoft.Extensions.Configuration.IConfiguration _configuration { get; }

        private MemoryCacheEntryOptions cacheOptions = new MemoryCacheEntryOptions()
        {
            AbsoluteExpiration = DateTime.Now.AddMinutes(Contants.CACHE_EXPIRATION_DAY)
        };

        public DMHangHoasController(CRMDBContext context, IMemoryCache cache,
            ITenantProvider tenantProvider, INhatKyService nhatKyService, Microsoft.Extensions.Configuration.IConfiguration configuration)
        {
            _context = context;
            _cache = cache;
            _tenantProvider = tenantProvider;
            _nhatKyService = nhatKyService;
            _configuration = configuration;
        }


        [HttpGet("GetCount")]
        public async Task<ActionResult<ItemCount>> GetCount()
        {
            ItemCount itemCount = new ItemCount();

            itemCount.Count = _context.DanhMucHangHoaRepository/*.Where(x => x.DMDonViSuDungId == _tenantProvider.TenantId)*/.Count();
            return await Task.FromResult(itemCount);
        }



        // GET: api/DanhMucHangHoas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DanhMucHangHoaModel>> GetById(int id)
        {
            var item = await _context.DanhMucHangHoaRepository
                //.Where(x => x.DMDonViSuDungId == _tenantProvider.TenantId)
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

            if (item == null)
            {
                return NotFound();
            }

            return new DanhMucHangHoaModel(item);
        }
        // GET: api/DanhMucHangHoas/5
        [HttpGet("getCode/{code}")]
        public async Task<ActionResult<DanhMucHangHoaModel>> GetByCode(string code)
        {
            if (string.IsNullOrEmpty(code))
            {
                return NotFound();
            }
            var item = await _context.DanhMucHangHoaRepository.FirstOrDefaultAsync(x => x.MaHangHoa.ToLower() == code.ToLower() && x.DeletedDate == null);

            if (item == null)
            {
                return NotFound();
            }

            return new DanhMucHangHoaModel(item);
        }
        // GET: api/DanhMucHangHoas/5
        [HttpGet("getLoaiGiaByCodes")]
        public async Task<ActionResult<DanhMucHangHoaModel>> GetLoaiGiaByCodes(List<string> codes)
        {
            var items = await _context.DanhMucHangHoaRepository
                .Where(x => codes.Contains(x.MaHangHoa) && x.DeletedDate == null)
                //.Where(x => x.DMDonViSuDungId == _tenantProvider.TenantId)
                /*.OrderByDescending(x => x.DMTinhGiaId)*/.FirstOrDefaultAsync();

            if (items == null)
            {
                return NotFound();
            }
            return new DanhMucHangHoaModel(items);
        }

        // PUT: api/DanhMucHangHoas/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("Update/{id}")]
        public async Task<ActionResult<DanhMucHangHoaModel>> Put(int id, DanhMucHangHoaModel model)
        {
            DanhMucHangHoa entity = _context.DanhMucHangHoaRepository.FirstOrDefault(item => item.Id == model.Id && item.DeletedDate == null);
            if (entity == null)
            {
                return new DanhMucHangHoaModel();
            }

            entity.MaHangHoa = model.MaHangHoa;
            entity.TenHangHoa = model.TenHangHoa;
            entity.DonViTinh = model.DonViTinh;
            entity.GiaNhap = (decimal)model.GiaNhap;
            entity.GiaXuat = (decimal)model.GiaXuat;
            entity.DonGia = (decimal)model.DonGia;
            entity.TyTrong = model.TyTrong;
            entity.KhoRongTon = model.KhoRongTon;
            var nhomHang = await _context.DanhMucNhomHangRepository.FirstOrDefaultAsync(item => item.MaNhomHang == model.MaNhomHang && item.DeletedDate == null);
            var mauSac = await _context.DanhMucMauSacRepository.FirstOrDefaultAsync(item => item.MaSo == model.MaMauSac && item.DeletedDate == null);
            var doDay = await _context.DanhMucDoDayRepository.FirstOrDefaultAsync(item => item.MaSo == model.MaDoDay && item.DeletedDate == null);
            var loaiTon = await _context.DanhMucLoaiTonRepository.FirstOrDefaultAsync(item => item.MaSo == model.MaLoaiTon && item.DeletedDate == null);
            var chungLoai = await _context.DanhMucChungLoaiRepository.FirstOrDefaultAsync(item => item.MaSo == model.MaChungLoai && item.DeletedDate == null);
            var kieuSong = await _context.DanhMucKieuSongRepository.FirstOrDefaultAsync(item => item.MaSo == model.MaKieuSong && item.DeletedDate == null);
            entity.TenNhomHang = nhomHang?.TenNhomHang;
            entity.MauSac = mauSac?.ChiTieu;
            entity.DoDay = doDay?.ChiTieu;
            entity.LoaiTon = loaiTon?.ChiTieu;
            entity.ChungLoai = chungLoai?.ChiTieu;
            entity.KieuSong = kieuSong?.ChiTieu;
            entity.MaNhomHang = model.MaNhomHang;
            entity.MaMauSac = model.MaMauSac;
            entity.MaDoDay = model.MaDoDay;
            entity.MaLoaiTon = model.MaLoaiTon;
            entity.MaChungLoai = model.MaChungLoai;
            entity.MaKieuSong = model.MaKieuSong;

            entity.UpdatedDate = DateTime.Now;

            try
            {
                await _context.SaveChangesAsync();

                // Log Nhat ky
                await _nhatKyService.LogUpdate("DanhMucHangHoa");
            }
            catch (DbUpdateConcurrencyException)
            {
                // Log Nhat ky
                await _nhatKyService.LogError("Update_DanhMucHangHoa", "id : " + id + ";\nitem : " + model.ToString());
                if (!Exists(id))
                {
                    return new DanhMucHangHoaModel();
                }
                else
                {
                    return new DanhMucHangHoaModel();
                }
            }

            return new DanhMucHangHoaModel(entity);
        }

        // POST: api/DanhMucHangHoas
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost("Create")]
        public async Task<ActionResult<DanhMucHangHoaModel>> Post(DanhMucHangHoaModel model)
        {
            try
            {
                var itemCheck = await _context.DanhMucHangHoaRepository.AnyAsync(x => x.MaHangHoa == model.MaHangHoa && x.DeletedDate == null);
                if (itemCheck)
                {
                    model.Id = -1;
                    return model;
                }
                DanhMucHangHoa entity = new();

                entity.Id = 0;
                entity.MaHangHoa = model.MaHangHoa;
                entity.TenHangHoa = model.TenHangHoa;
                entity.DonViTinh = model.DonViTinh;
                entity.GiaNhap = (decimal)model.GiaNhap;
                entity.GiaXuat = (decimal)model.GiaXuat;
                entity.DonGia = (decimal)model.DonGia;
                entity.TyTrong = model.TyTrong;
                entity.KhoRongTon = model.KhoRongTon;
                var nhomHang = await _context.DanhMucNhomHangRepository.FirstOrDefaultAsync(item => item.MaNhomHang == model.MaNhomHang && item.DeletedDate == null);
                var mauSac = await _context.DanhMucMauSacRepository.FirstOrDefaultAsync(item => item.MaSo == model.MaMauSac && item.DeletedDate == null);
                var doDay = await _context.DanhMucDoDayRepository.FirstOrDefaultAsync(item => item.MaSo == model.MaDoDay && item.DeletedDate == null);
                var loaiTon = await _context.DanhMucLoaiTonRepository.FirstOrDefaultAsync(item => item.MaSo == model.MaLoaiTon && item.DeletedDate == null);
                var chungLoai = await _context.DanhMucChungLoaiRepository.FirstOrDefaultAsync(item => item.MaSo == model.MaChungLoai && item.DeletedDate == null);
                var kieuSong = await _context.DanhMucKieuSongRepository.FirstOrDefaultAsync(item => item.MaSo == model.MaKieuSong && item.DeletedDate == null);
                entity.TenNhomHang = nhomHang?.TenNhomHang;
                entity.MauSac = mauSac?.ChiTieu;
                entity.DoDay = doDay?.ChiTieu;
                entity.LoaiTon = loaiTon?.ChiTieu;
                entity.ChungLoai = chungLoai?.ChiTieu;
                entity.KieuSong = kieuSong?.ChiTieu;
                entity.MaNhomHang = model.MaNhomHang;
                entity.MaMauSac = model.MaMauSac;
                entity.MaDoDay = model.MaDoDay;
                entity.MaLoaiTon = model.MaLoaiTon;
                entity.MaChungLoai = model.MaChungLoai;
                entity.MaKieuSong = model.MaKieuSong;

                entity.CreatedDate = DateTime.Now;
                entity.UpdatedDate = DateTime.Now;
                //item.DMDonViSuDungId = _tenantProvider.TenantId;
                _context.DanhMucHangHoaRepository.Add(entity);
                await _context.SaveChangesAsync();

                // Log Nhat ky
                await _nhatKyService.LogCreate("DanhMucHangHoa");
                _cache.Remove(Contants.CACHE_HANGHOA_KEY);
                return new DanhMucHangHoaModel(entity);
            }
                catch (Exception e)
            {
                throw e;
            }
        }

        // DELETE: api/DanhMucHangHoas/delete/5
        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult<DanhMucHangHoaModel>> Delete(int id)
        {
            var item = await _context.DanhMucHangHoaRepository
                .FirstOrDefaultAsync(x => x.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            item.DeletedDate = DateTime.Now;
            await _context.SaveChangesAsync();

            // Log Nhat ky
            await _nhatKyService.LogDelete("DanhMucHangHoa");
            _cache.Remove(Contants.CACHE_HANGHOA_KEY);
            return new DanhMucHangHoaModel(item);
        }
        private bool Exists(int id)
        {
            return _context.DanhMucHangHoaRepository/*.Where(x => x.DMDonViSuDungId == _tenantProvider.TenantId)*/.Any(e => e.Id == id);
        }

        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<DanhMucHangHoaModel>>> Get(SearchRequest request)
        {
            var lst = await _context.DanhMucHangHoaRepository
                .Where(x => x.DeletedDate == null)
                .ToListAsync();
            return lst.Select(x => new DanhMucHangHoaModel(x)).ToList();
        }

        [HttpGet("ExportToExcel")]
        public async Task<ActionResult<GetAllResponse<DanhMucHangHoaModel>>> Get([FromBody] HangHoaSearchRequest request)
        {
            GetAllResponse<DanhMucHangHoaModel> outputs = await GetData(request, false);

            // Log Nhat ky
            await _nhatKyService.LogExportExcel("DanhMucHangHoa");
            return outputs;
        }

        // GET: api/DanhMucHangHoas
        [HttpGet("GetAllPaged")]
        public async Task<ActionResult<GetAllResponse<DanhMucHangHoaModel>>> GetAllPaged([FromBody] HangHoaSearchRequest request)
        {
            GetAllResponse<DanhMucHangHoaModel> outputs = await GetData(request, true);
            return outputs;
        }

        private async Task<GetAllResponse<DanhMucHangHoaModel>> GetData(HangHoaSearchRequest request, bool isPaging)
        {
            GetAllResponse<DanhMucHangHoaModel> outputs = new GetAllResponse<DanhMucHangHoaModel>();
            Expression<Func<DanhMucHangHoa, bool>> filter = m => (1 == 1);

            if (!string.IsNullOrEmpty(request.MaNhom))
            {
                filter = filter.And(x => x.MaNhomHang == request.MaNhom);

            }
            if (!string.IsNullOrEmpty(request.MaMauSac))
            {

                filter = filter.And(x => x.MaMauSac == request.MaMauSac);

            }
            if (!string.IsNullOrEmpty(request.MaDoDay))
            {

                filter = filter.And(x => x.MaDoDay == request.MaDoDay);

            }
            if (!string.IsNullOrEmpty(request.MaChungLoai))
            {

                filter = filter.And(x => x.MaChungLoai == request.MaChungLoai);

            }
            if (!string.IsNullOrEmpty(request.MaLoaiTon))
            {

                filter = filter.And(x => x.MaLoaiTon == request.MaLoaiTon);

            }
            if (!string.IsNullOrEmpty(request.MaHangHoa))
            {
                filter = filter.And(x => x.MaHangHoa.Contains(request.MaHangHoa));
            }
            if (!string.IsNullOrEmpty(request.TenHangHoa))
            {
                filter = filter.And(x => x.TenHangHoa.Contains(request.TenHangHoa));
            }
            if (!string.IsNullOrEmpty(request.DonViTinh))
            {
                filter = filter.And(x => x.DonViTinh.Contains(request.DonViTinh));
            }

            Func<IQueryable<DanhMucHangHoa>, IOrderedQueryable<DanhMucHangHoa>> order = null;
            if (request.SortDirection != QLSX.Shared.Enums.SortDirection.None)
            {
                order = await OrderBy(request.SortLable, request.SortDirection == QLSX.Shared.Enums.SortDirection.Ascending);
            }

            IQueryable<DanhMucHangHoa> query = _context.Set<DanhMucHangHoa>();

            //ICollection<FilterDefinition<DanhMucHangHoa>> filter1 = request.Filter;
            //FilterBuider<DanhMucHangHoa> filterBuider;
            //if (filter1 != null)
            //{
            //    foreach (var f in filter1)
            //    {
            //        if (f.Field == "TenNhomHang")
            //        {
            //            var f1 = GetFilterTenNhom(f);
            //            query = query.Where(f1);
            //        }
            //        else if (f.Field == "TenChungLoai")
            //        {
            //            var f1 = GetFilterTenChungLoai(f);
            //            query = query.Where(f1);
            //        }
            //        else if (f.Field == "TenMauSac")
            //        {
            //            var f1 = GetFilterTenMauSac(f);
            //            query = query.Where(f1);
            //        }
            //        else if (f.Field == "TenDoDay")
            //        {
            //            var f1 = GetFilterTenDoDay(f);
            //            query = query.Where(f1);
            //        }
            //        else if (f.Field == "TenKieuSong")
            //        {
            //            var f1 = GetFilterTenKieuSong(f);
            //            query = query.Where(f1);
            //        }
            //        else if (f.Field == "TenLoaiTon")
            //        {
            //            var f1 = GetFilterTenLoaiTon(f);
            //            query = query.Where(f1);
            //        }
            //        else
            //        {
            //            filterBuider = new FilterBuider<DanhMucHangHoa>(f);
            //            var filterFunc = filterBuider.GetFilter;
            //            query = query.Where(filterFunc);
            //        }

            //    }
            //}
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

            var lst = await query.ToListAsync();
            outputs.Items = lst.Select(x => new DanhMucHangHoaModel(x)).ToList();
            return outputs;
        }

        private Expression<Func<DanhMucHangHoa, bool>> GetFilterTenNhom(FilterDefinition<DanhMucHangHoa> f)
        {
            Expression<Func<DanhMucHangHoa, bool>> filter = m => (1 == 1);
            var Operator = f.Operator;
            return null;
            //return Operator switch
            //{
            //    FilterOperator.String.Contains when f.Value != null =>
            //       filter.And(x => x.DMNhomHangs.TenNhom.Contains(f.Value.ToString())),
            //    FilterOperator.String.NotContains when f.Value != null =>
            //      filter.And(x => !x.DMNhomHangs.TenNhom.Contains(f.Value.ToString())),
            //    FilterOperator.String.NotContains when f.Value != null =>
            //       filter.And(x => x.DMNhomHangs.TenNhom.Contains(f.Value.ToString())),
            //    FilterOperator.String.Equal when f.Value != null =>
            //       filter.And(x => x.DMNhomHangs.TenNhom.Equals(f.Value.ToString())),
            //    FilterOperator.String.NotEqual when f.Value != null =>
            //      filter.And(x => !x.DMNhomHangs.TenNhom.Equals(f.Value.ToString())),
            //    FilterOperator.String.StartsWith when f.Value != null =>
            //       filter.And(x => x.DMNhomHangs.TenNhom.StartsWith(f.Value.ToString())),
            //    FilterOperator.String.EndsWith when f.Value != null =>
            //       filter.And(x => x.DMNhomHangs.TenNhom.EndsWith(f.Value.ToString())),
            //    FilterOperator.String.Empty =>
            //       filter.And(x => string.IsNullOrEmpty(x.DMNhomHangs.TenNhom)),
            //    _ => filter.And(x => 1 == 1),
            //};
        }
        private Expression<Func<DanhMucHangHoa, bool>> GetFilterTenChungLoai(FilterDefinition<DanhMucHangHoa> f)
        {
            Expression<Func<DanhMucHangHoa, bool>> filter = m => (1 == 1);
            var Operator = f.Operator;
            return null;
            //return Operator switch
            //{
            //    FilterOperator.String.Contains when f.Value != null =>
            //       filter.And(x => x.DMChungLoais.ChiTieu.Contains(f.Value.ToString())),
            //    FilterOperator.String.NotContains when f.Value != null =>
            //      filter.And(x => !x.DMChungLoais.ChiTieu.Contains(f.Value.ToString())),
            //    FilterOperator.String.NotContains when f.Value != null =>
            //       filter.And(x => x.DMChungLoais.ChiTieu.Contains(f.Value.ToString())),
            //    FilterOperator.String.Equal when f.Value != null =>
            //       filter.And(x => x.DMChungLoais.ChiTieu.Equals(f.Value.ToString())),
            //    FilterOperator.String.NotEqual when f.Value != null =>
            //      filter.And(x => !x.DMChungLoais.ChiTieu.Equals(f.Value.ToString())),
            //    FilterOperator.String.StartsWith when f.Value != null =>
            //       filter.And(x => x.DMChungLoais.ChiTieu.StartsWith(f.Value.ToString())),
            //    FilterOperator.String.EndsWith when f.Value != null =>
            //       filter.And(x => x.DMChungLoais.ChiTieu.EndsWith(f.Value.ToString())),
            //    FilterOperator.String.Empty =>
            //       filter.And(x => string.IsNullOrEmpty(x.DMChungLoais.ChiTieu)),
            //    _ => filter.And(x => 1 == 1),
            //};
        }
        private Expression<Func<DanhMucHangHoa, bool>> GetFilterTenMauSac(FilterDefinition<DanhMucHangHoa> f)
        {
            Expression<Func<DanhMucHangHoa, bool>> filter = m => (1 == 1);
            var Operator = f.Operator;
            return null;
            //return Operator switch
            //{
            //    FilterOperator.String.Contains when f.Value != null =>
            //       filter.And(x => x.DMMauSacs.ChiTieu.Contains(f.Value.ToString())),
            //    FilterOperator.String.NotContains when f.Value != null =>
            //      filter.And(x => !x.DMMauSacs.ChiTieu.Contains(f.Value.ToString())),
            //    FilterOperator.String.NotContains when f.Value != null =>
            //       filter.And(x => x.DMMauSacs.ChiTieu.Contains(f.Value.ToString())),
            //    FilterOperator.String.Equal when f.Value != null =>
            //       filter.And(x => x.DMMauSacs.ChiTieu.Equals(f.Value.ToString())),
            //    FilterOperator.String.NotEqual when f.Value != null =>
            //      filter.And(x => !x.DMMauSacs.ChiTieu.Equals(f.Value.ToString())),
            //    FilterOperator.String.StartsWith when f.Value != null =>
            //       filter.And(x => x.DMMauSacs.ChiTieu.StartsWith(f.Value.ToString())),
            //    FilterOperator.String.EndsWith when f.Value != null =>
            //       filter.And(x => x.DMMauSacs.ChiTieu.EndsWith(f.Value.ToString())),
            //    FilterOperator.String.Empty =>
            //       filter.And(x => string.IsNullOrEmpty(x.DMMauSacs.ChiTieu)),
            //    _ => filter.And(x => 1 == 1),
            //};
        }
        private Expression<Func<DanhMucHangHoa, bool>> GetFilterTenDoDay(FilterDefinition<DanhMucHangHoa> f)
        {
            Expression<Func<DanhMucHangHoa, bool>> filter = m => (1 == 1);
            var Operator = f.Operator;
            return null;

            //return Operator switch
            //{
            //    FilterOperator.String.Contains when f.Value != null =>
            //       filter.And(x => x.DMDoDays.ChiTieu.Contains(f.Value.ToString())),
            //    FilterOperator.String.NotContains when f.Value != null =>
            //      filter.And(x => !x.DMDoDays.ChiTieu.Contains(f.Value.ToString())),
            //    FilterOperator.String.NotContains when f.Value != null =>
            //       filter.And(x => x.DMDoDays.ChiTieu.Contains(f.Value.ToString())),
            //    FilterOperator.String.Equal when f.Value != null =>
            //       filter.And(x => x.DMDoDays.ChiTieu.Equals(f.Value.ToString())),
            //    FilterOperator.String.NotEqual when f.Value != null =>
            //      filter.And(x => !x.DMDoDays.ChiTieu.Equals(f.Value.ToString())),
            //    FilterOperator.String.StartsWith when f.Value != null =>
            //       filter.And(x => x.DMDoDays.ChiTieu.StartsWith(f.Value.ToString())),
            //    FilterOperator.String.EndsWith when f.Value != null =>
            //       filter.And(x => x.DMDoDays.ChiTieu.EndsWith(f.Value.ToString())),
            //    FilterOperator.String.Empty =>
            //       filter.And(x => string.IsNullOrEmpty(x.DMDoDays.ChiTieu)),
            //    _ => filter.And(x => 1 == 1),
            //};
        }
        private Expression<Func<DanhMucHangHoa, bool>> GetFilterTenKieuSong(FilterDefinition<DanhMucHangHoa> f)
        {
            Expression<Func<DanhMucHangHoa, bool>> filter = m => (1 == 1);
            var Operator = f.Operator;
            return null;
            //return Operator switch
            //{
            //    FilterOperator.String.Contains when f.Value != null =>
            //       filter.And(x => x.DMKieuSongs.ChiTieu.Contains(f.Value.ToString())),
            //    FilterOperator.String.NotContains when f.Value != null =>
            //      filter.And(x => !x.DMKieuSongs.ChiTieu.Contains(f.Value.ToString())),
            //    FilterOperator.String.NotContains when f.Value != null =>
            //       filter.And(x => x.DMKieuSongs.ChiTieu.Contains(f.Value.ToString())),
            //    FilterOperator.String.Equal when f.Value != null =>
            //       filter.And(x => x.DMKieuSongs.ChiTieu.Equals(f.Value.ToString())),
            //    FilterOperator.String.NotEqual when f.Value != null =>
            //      filter.And(x => !x.DMKieuSongs.ChiTieu.Equals(f.Value.ToString())),
            //    FilterOperator.String.StartsWith when f.Value != null =>
            //       filter.And(x => x.DMKieuSongs.ChiTieu.StartsWith(f.Value.ToString())),
            //    FilterOperator.String.EndsWith when f.Value != null =>
            //       filter.And(x => x.DMKieuSongs.ChiTieu.EndsWith(f.Value.ToString())),
            //    FilterOperator.String.Empty =>
            //       filter.And(x => string.IsNullOrEmpty(x.DMKieuSongs.ChiTieu)),
            //    _ => filter.And(x => 1 == 1),
            //};
        }
        private Expression<Func<DanhMucHangHoa, bool>> GetFilterTenLoaiTon(FilterDefinition<DanhMucHangHoa> f)
        {
            Expression<Func<DanhMucHangHoa, bool>> filter = m => (1 == 1);
            var Operator = f.Operator;
            return null;
            //return Operator switch
            //{
            //    FilterOperator.String.Contains when f.Value != null =>
            //       filter.And(x => x.DMLoaiTons.ChiTieu.Contains(f.Value.ToString())),
            //    FilterOperator.String.NotContains when f.Value != null =>
            //      filter.And(x => !x.DMLoaiTons.ChiTieu.Contains(f.Value.ToString())),
            //    FilterOperator.String.NotContains when f.Value != null =>
            //       filter.And(x => x.DMLoaiTons.ChiTieu.Contains(f.Value.ToString())),
            //    FilterOperator.String.Equal when f.Value != null =>
            //       filter.And(x => x.DMLoaiTons.ChiTieu.Equals(f.Value.ToString())),
            //    FilterOperator.String.NotEqual when f.Value != null =>
            //      filter.And(x => !x.DMLoaiTons.ChiTieu.Equals(f.Value.ToString())),
            //    FilterOperator.String.StartsWith when f.Value != null =>
            //       filter.And(x => x.DMLoaiTons.ChiTieu.StartsWith(f.Value.ToString())),
            //    FilterOperator.String.EndsWith when f.Value != null =>
            //       filter.And(x => x.DMLoaiTons.ChiTieu.EndsWith(f.Value.ToString())),
            //    FilterOperator.String.Empty =>
            //       filter.And(x => string.IsNullOrEmpty(x.DMLoaiTons.ChiTieu)),
            //    _ => filter.And(x => 1 == 1),
            //};
        }
        private async Task<Func<IQueryable<DanhMucHangHoa>, IOrderedQueryable<DanhMucHangHoa>>> OrderBy(string sortBy, bool sortType)
        {
            Func<IQueryable<DanhMucHangHoa>, IOrderedQueryable<DanhMucHangHoa>> myFunc;
            if (sortBy == "MaHangHoa")
            {
                if (sortType) myFunc = source => source.OrderBy(x => x.MaHangHoa);
                else myFunc = source => source.OrderByDescending(x => x.MaHangHoa);
                return myFunc;
            }
            if (sortBy == "TenHangHoa")
            {
                if (sortType) myFunc = source => source.OrderBy(x => x.TenHangHoa);
                else myFunc = source => source.OrderByDescending(x => x.TenHangHoa);
                return myFunc;
            }
            if (sortBy == "DonViTinh")
            {
                if (sortType) myFunc = source => source.OrderBy(x => x.DonViTinh);
                else myFunc = source => source.OrderByDescending(x => x.DonViTinh);
                return myFunc;
            }
            if (sortBy == "GiaNhap")
            {
                if (sortType) myFunc = source => source.OrderBy(x => x.GiaNhap);
                else myFunc = source => source.OrderByDescending(x => x.GiaNhap);
                return myFunc;
            }
            if (sortBy == "GiaXuat")
            {
                if (sortType) myFunc = source => source.OrderBy(x => x.GiaXuat);
                else myFunc = source => source.OrderByDescending(x => x.GiaXuat);
                return myFunc;
            }
            //if (sortBy == "TenNhom")
            //{
            //    if (sortType) myFunc = source => source.OrderBy(x => x.DMNhomHangs.TenNhom);
            //    else myFunc = source => source.OrderByDescending(x => x.DMNhomHangs.TenNhom);
            //    return myFunc;
            //}
            //if (sortBy == "TenChungLoai")
            //{
            //    if (sortType) myFunc = source => source.OrderBy(x => x.DMChungLoais.ChiTieu);
            //    else myFunc = source => source.OrderByDescending(x => x.DMChungLoais.ChiTieu);
            //    return myFunc;
            //}
            //if (sortBy == "TenMauSac")
            //{
            //    if (sortType) myFunc = source => source.OrderBy(x => x.DMMauSacs.ChiTieu);
            //    else myFunc = source => source.OrderByDescending(x => x.DMMauSacs.ChiTieu);
            //    return myFunc;
            //}
            //if (sortBy == "TenDoDay")
            //{
            //    if (sortType) myFunc = source => source.OrderBy(x => x.DMDoDays.ChiTieu);
            //    else myFunc = source => source.OrderByDescending(x => x.DMDoDays.ChiTieu);
            //    return myFunc;
            //}
            //if (sortBy == "TenKieuSong")
            //{
            //    if (sortType) myFunc = source => source.OrderBy(x => x.DMKieuSongs.ChiTieu);
            //    else myFunc = source => source.OrderByDescending(x => x.DMKieuSongs.ChiTieu);
            //    return myFunc;
            //}
            //if (sortBy == "TenLoaiTon")
            //{
            //    if (sortType) myFunc = source => source.OrderBy(x => x.DMLoaiTons.ChiTieu);
            //    else myFunc = source => source.OrderByDescending(x => x.DMLoaiTons.ChiTieu);
            //    return myFunc;
            //}
            return null;

        }

        // GET: api/DanhMucHangHoas
        [HttpGet("GetAllPagedDialog")]
        public async Task<ActionResult<GetAllResponse<DanhMucHangHoaModel>>> GetAllPagedDialog([FromBody] HangHoaSearchRequest request)
        {
            GetAllResponse<DanhMucHangHoaModel> outputs = new GetAllResponse<DanhMucHangHoaModel>();

            var query = (
               from cus in _context.DanhMucHangHoaRepository
                   //where cus.DMDonViSuDungId == _tenantProvider.TenantId && cus.DeletedDate == null
               select cus);

            if (!string.IsNullOrEmpty(request.SearchText))
                query = query.Where(x =>
                x.MaHangHoa.Contains(request.SearchText)
            || x.TenHangHoa.Contains(request.SearchText)
            || x.DonViTinh.Contains(request.SearchText)
            );
            Console.WriteLine(query.ToQueryString());

            outputs.TotalRecords = await query.CountAsync();
            outputs.TotalPages = (int)Math.Ceiling(outputs.TotalRecords / (double)request.PageSize);
            outputs.Page = request.Page;
            outputs.PageSize = request.PageSize;
            //var item = await query.ToListAsync();
            query = query
                .Skip(request.Page * request.PageSize).Take(request.PageSize);
            var lst = await query.ToListAsync();
            outputs.Items = lst.Select(x => new DanhMucHangHoaModel(x)).ToList();
            return outputs;
        }

        // GET: api/DanhMucHangHoas/5
        [HttpGet("GetSoDuHangHoaByCode")]
        public async Task<ActionResult<double>> GetSoDuHangHoaByCode(GetSoDuHangHoaRequest request)
        {
            try
            {
                string StoredProc = "";
                StoredProc = string.Format("exec dbo.GetSoDuHangHoa {0}, '{1}', '{2}', '{3}'", request.MaKho, request.MaHangHoa, request.Ngay.ToString("MM/dd/yyyy"), _tenantProvider.TenantId);

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
                    return double.Parse(dt.Rows[0]["SoLuong"].ToString());
                }
            }
            catch (Exception ex)
            {
                return 0;
            }
            return 0;
        }

        // POST: api/DanhMucHangHoas/GetSoDuHangHoaBatch
        [HttpPost("GetSoDuHangHoaBatch")]
        public async Task<ActionResult<Dictionary<string, double>>> GetSoDuHangHoaBatch(GetSoDuHangHoaBatchRequest request)
        {
            var result = new Dictionary<string, double>();
            
            try
            {
                if (request.MaHangHoas == null || !request.MaHangHoas.Any())
                {
                    return result;
                }

                // Loại bỏ trùng lặp và null/empty
                var distinctMaHangHoas = request.MaHangHoas
                    .Where(x => !string.IsNullOrWhiteSpace(x))
                    .Distinct()
                    .ToList();

                if (!distinctMaHangHoas.Any())
                {
                    return result;
                }

                string sqlConn = _configuration.GetConnectionString("CRMConnectStrings");
                
                // Tạo XML từ danh sách mã hàng hóa
                var xmlBuilder = new System.Text.StringBuilder();
                xmlBuilder.Append("<items>");
                foreach (var maHangHoa in distinctMaHangHoas)
                {
                    // Escape XML special characters
                    var escapedMaHangHoa = maHangHoa.Replace("&", "&amp;")
                                                   .Replace("<", "&lt;")
                                                   .Replace(">", "&gt;")
                                                   .Replace("\"", "&quot;")
                                                   .Replace("'", "&apos;");
                    xmlBuilder.Append($"<item>{escapedMaHangHoa}</item>");
                }
                xmlBuilder.Append("</items>");
                string maHangHoasXML = xmlBuilder.ToString();

                // Gọi stored procedure batch (nếu có) hoặc fallback về cách cũ
                string storedProc;
                bool useBatchProc = true; // Set to true sau khi đã tạo stored procedure batch trong DB
                
                if (useBatchProc)
                {
                    // Sử dụng stored procedure batch mới
                    storedProc = string.Format(
                        "exec dbo.GetSoDuHangHoaBatch '{0}', '{1}', '{2}', {3}",
                        request.MaKho ?? "",
                        maHangHoasXML.Replace("'", "''"), // Escape single quotes
                        request.Ngay.ToString("MM/dd/yyyy"),
                        _tenantProvider.TenantId
                    );

                    DataTable dt = new DataTable();
                    using (SqlDataAdapter dataAdapter = new SqlDataAdapter(storedProc, sqlConn))
                    {
                        DataSet dataSet = new DataSet();
                        dataAdapter.Fill(dataSet);
                        dt = dataSet.Tables[0];
                    }

                    // Parse kết quả từ stored procedure batch
                    foreach (DataRow row in dt.Rows)
                    {
                        if (row["MaHangHoa"] != DBNull.Value && row["SoLuong"] != DBNull.Value)
                        {
                            string maHangHoa = row["MaHangHoa"].ToString();
                            double soLuong = double.Parse(row["SoLuong"].ToString());
                            result[maHangHoa] = soLuong;
                        }
                    }

                    // Đảm bảo tất cả mã hàng hóa đều có kết quả (set 0 nếu không có)
                    foreach (var maHangHoa in distinctMaHangHoas)
                    {
                        if (!result.ContainsKey(maHangHoa))
                        {
                            result[maHangHoa] = 0;
                        }
                    }
                }
                else
                {
                    // Fallback: Gọi stored procedure cho từng hàng hóa (tối ưu hơn cách cũ bằng cách dùng connection pooling)
                    using (SqlConnection connection = new SqlConnection(sqlConn))
                    {
                        await connection.OpenAsync();
                        
                        foreach (var maHangHoa in distinctMaHangHoas)
                        {
                            try
                            {
                                storedProc = string.Format(
                                    "exec dbo.GetSoDuHangHoa {0}, '{1}', '{2}', {3}",
                                    request.MaKho ?? "NULL",
                                    maHangHoa.Replace("'", "''"), // Escape single quotes
                                    request.Ngay.ToString("MM/dd/yyyy"),
                                    _tenantProvider.TenantId
                                );

                                using (SqlCommand command = new SqlCommand(storedProc, connection))
                                {
                                    command.CommandTimeout = 30;
                                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                                    {
                                        if (await reader.ReadAsync() && reader["SoLuong"] != DBNull.Value)
                                        {
                                            result[maHangHoa] = reader.GetDouble(reader.GetOrdinal("SoLuong"));
                                        }
                                        else
                                        {
                                            result[maHangHoa] = 0;
                                        }
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                // Nếu một hàng hóa lỗi, vẫn tiếp tục với các hàng hóa khác
                                result[maHangHoa] = 0;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Trả về kết quả đã tính được, không throw exception
                return result;
            }
            
            return result;
        }
    }
}
