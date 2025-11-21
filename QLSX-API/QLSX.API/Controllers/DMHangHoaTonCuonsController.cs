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
using MudBlazor;
using QLSX.Shared.Entities;
using QLSX.Shared.Models.Request;

namespace SaleAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DMHangHoaTonCuonsController : ControllerBase
    {
        private readonly CRMDBContext _context;
        private IMemoryCache _cache;
        private readonly ITenantProvider _tenantProvider;
        private readonly INhatKyService _nhatKyService;
        private MemoryCacheEntryOptions cacheOptions = new MemoryCacheEntryOptions()
        {
            AbsoluteExpiration = DateTime.Now.AddMinutes(Contants.CACHE_EXPIRATION_DAY)
        };

        public DMHangHoaTonCuonsController(CRMDBContext context, IMemoryCache cache, ITenantProvider tenantProvider, INhatKyService nhatKyService)
        {
            _context = context;
            _cache = cache;
            _tenantProvider = tenantProvider;
            _nhatKyService = nhatKyService;
        }

        [HttpGet("GetCount")]
        public async Task<ActionResult<ItemCount>> GetCount()
        {
            ItemCount itemCount = new ItemCount();

            itemCount.Count = _context.DanhMucHangHoaRepository/*.Where(p => p.DMDonViSuDungId == _tenantProvider.TenantId)*/
                  .Count();
            return await Task.FromResult(itemCount);
        }


        // GET: api/DMHangHoas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DanhMucHangHoaTonCuonModel>> GetById(int id)
        {
            var item = await _context.DanhMucHangHoaTonCuonRepository
                //.Where(p => p.DMDonViSuDungId == _tenantProvider.TenantId)
                .FirstOrDefaultAsync(p => p.Id == id && p.DeletedDate == null);

            if (item == null)
            {
                return NotFound();
            }

            return new DanhMucHangHoaTonCuonModel(item);
        }
        // GET: api/DMHangHoas/5
        [HttpGet("getCode/{code}")]
        public async Task<ActionResult<DanhMucHangHoaTonCuonModel>> GetByCode(string code)
        {
            if (string.IsNullOrEmpty(code))
            {
                return NotFound();
            }

            var item = await _context.DanhMucHangHoaTonCuonRepository.FirstOrDefaultAsync(x => x.DeletedDate == null && x.MaHangHoa.ToLower() == code.ToLower());
            if (item == null)
            {
                return NotFound();
            }

            return new DanhMucHangHoaTonCuonModel(item);
        }
        // GET: api/DMHangHoas/5
        [HttpGet("getLoaiGiaByCodes")]
        public async Task<ActionResult<DanhMucHangHoaTonCuonModel>> GetLoaiGiaByCodes(List<string> codes)
        {
            var items = await _context.DanhMucHangHoaTonCuonRepository
                .Where(x => codes.Contains(x.MaHangHoa) && x.DeletedDate == null)
                //.Where(p => p.DMDonViSuDungId == _tenantProvider.TenantId)
                //.OrderByDescending(x => x.DMTinhGiaId)
                .FirstOrDefaultAsync();
            if (items == null)
            {
                return NotFound();
            }

            return new DanhMucHangHoaTonCuonModel(items);
        }

        // PUT: api/DMHangHoas/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("Update/{id}")]
        public async Task<ActionResult<DanhMucHangHoaTonCuonModel>> Put(int id, DanhMucHangHoaTonCuonModel model)
        {
            DanhMucHangHoaTonCuon entity = _context.DanhMucHangHoaTonCuonRepository.FirstOrDefault(item => item.Id == model.Id && item.DeletedDate == null);
            if (entity == null)
            {
                return new DanhMucHangHoaTonCuonModel();
            }

            entity.MaHangHoa = model.MaHangHoa;
            entity.TenHangHoa = model.TenHangHoa;
            entity.DonViTinh = model.DonViTinh;
            entity.GiaNhap = (decimal)model.GiaNhap;
            entity.GiaXuat = (decimal)model.GiaXuat;
            entity.DonGia = (decimal)model.DonGia;
            entity.TyTrong = model.TyTrong;
            entity.KhoRongTon = model.KhoRongTon;
            var nhomHang = await _context.DanhMucNhomHangRepository.FirstOrDefaultAsync(item => item.Id == model.DMNhomHangId && item.DeletedDate == null);
            var mauSac = await _context.DanhMucMauSacRepository.FirstOrDefaultAsync(item => item.Id == model.DMMauSacId && item.DeletedDate == null);
            var doDay = await _context.DanhMucDoDayRepository.FirstOrDefaultAsync(item => item.Id == model.DMDoDayId && item.DeletedDate == null);
            var loaiTon = await _context.DanhMucLoaiTonRepository.FirstOrDefaultAsync(item => item.Id == model.DMLoaiTonId && item.DeletedDate == null);
            var chungLoai = await _context.DanhMucChungLoaiRepository.FirstOrDefaultAsync(item => item.Id == model.DMChungLoaiId && item.DeletedDate == null);
            var kieuSong = await _context.DanhMucKieuSongRepository.FirstOrDefaultAsync(item => item.Id == model.DMKieuSongId && item.DeletedDate == null);
            entity.MaNhomHang = nhomHang?.MaNhom;
            entity.MaMauSac = mauSac?.MaSo;
            entity.MaDoDay = doDay?.MaSo;
            entity.MaLoaiTon = loaiTon?.MaSo;
            entity.MaChungLoai = chungLoai?.MaSo;
            entity.MaKieuSong = kieuSong?.MaSo;
            entity.TenNhomHang = nhomHang?.TenNhomHang;
            entity.MauSac = mauSac?.ChiTieu;
            entity.DoDay = doDay?.ChiTieu;
            entity.LoaiTon = loaiTon?.ChiTieu;
            entity.ChungLoai = chungLoai?.ChiTieu;
            entity.KieuSong = kieuSong?.ChiTieu;
            entity.UpdatedDate = DateTime.Now;

            try
            {
                await _context.SaveChangesAsync();

                // Log Nhat ky
                await _nhatKyService.LogUpdate("DMHangHoaTonCuon");
            }
            catch (DbUpdateConcurrencyException)
            {
                // Log Nhat ky
                await _nhatKyService.LogError("Update_DMHangHoaTonCuon", "id : " + id + ";\nitem : " + model.ToString());
                if (!Exists(id))
                {
                    return new DanhMucHangHoaTonCuonModel();
                }
                else
                {
                    return new DanhMucHangHoaTonCuonModel();
                }
            }

            return new DanhMucHangHoaTonCuonModel(entity);
        }

        // POST: api/DMHangHoas
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost("Create")]
        public async Task<ActionResult<DanhMucHangHoaTonCuonModel>> Post(DanhMucHangHoaTonCuonModel model)
        {
            try
            {
                DanhMucHangHoaTonCuon entity = new DanhMucHangHoaTonCuon();
                entity.Id = 0;
                entity.MaHangHoa = model.MaHangHoa;
                entity.TenHangHoa = model.TenHangHoa;
                entity.DonViTinh = model.DonViTinh;
                entity.GiaNhap = (decimal)model.GiaNhap;
                entity.GiaXuat = (decimal)model.GiaXuat;
                entity.DonGia = (decimal)model.DonGia;
                entity.TyTrong = model.TyTrong;
                entity.KhoRongTon = model.KhoRongTon;
                var nhomHang = await _context.DanhMucNhomHangRepository.FirstOrDefaultAsync(item => item.Id == model.DMNhomHangId && item.DeletedDate == null);
                var mauSac = await _context.DanhMucMauSacRepository.FirstOrDefaultAsync(item => item.Id == model.DMMauSacId && item.DeletedDate == null);
                var doDay = await _context.DanhMucDoDayRepository.FirstOrDefaultAsync(item => item.Id == model.DMDoDayId && item.DeletedDate == null);
                var loaiTon = await _context.DanhMucLoaiTonRepository.FirstOrDefaultAsync(item => item.Id == model.DMLoaiTonId && item.DeletedDate == null);
                var chungLoai = await _context.DanhMucChungLoaiRepository.FirstOrDefaultAsync(item => item.Id == model.DMChungLoaiId && item.DeletedDate == null);
                var kieuSong = await _context.DanhMucKieuSongRepository.FirstOrDefaultAsync(item => item.Id == model.DMKieuSongId && item.DeletedDate == null);
                entity.MaNhomHang = nhomHang?.MaNhom;
                entity.MaMauSac = mauSac?.MaSo;
                entity.MaDoDay = doDay?.MaSo;
                entity.MaLoaiTon = loaiTon?.MaSo;
                entity.MaChungLoai = chungLoai?.MaSo;
                entity.MaKieuSong = kieuSong?.MaSo;
                entity.TenNhomHang = nhomHang?.TenNhomHang;
                entity.MauSac = mauSac?.ChiTieu;
                entity.DoDay = doDay?.ChiTieu;
                entity.LoaiTon = loaiTon?.ChiTieu;
                entity.ChungLoai = chungLoai?.ChiTieu;
                entity.KieuSong = kieuSong?.ChiTieu;
                entity.CreatedDate = DateTime.Now;
                entity.UpdatedDate = DateTime.Now;
                //item.DMDonViSuDungId = _tenantProvider.TenantId;
                _context.DanhMucHangHoaTonCuonRepository.Add(entity);
                await _context.SaveChangesAsync();

                // Log Nhat ky
                await _nhatKyService.LogCreate("DMHangHoaTonCuon");
                _cache.Remove(Contants.CACHE_HANGHOA_TON_KEY);
                return new DanhMucHangHoaTonCuonModel(entity);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        // DELETE: api/DMHangHoas/delete/5
        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult<DanhMucHangHoaTonCuonModel>> Delete(int id)
        {
            var item = await _context.DanhMucHangHoaTonCuonRepository
                //.Where(p => p.DMDonViSuDungId == _tenantProvider.TenantId)
                .FirstOrDefaultAsync(p => p.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            item.DeletedDate = DateTime.Now;
            await _context.SaveChangesAsync();
            // Log Nhat ky
            await _nhatKyService.LogDelete("DMHangHoaTonCuon");
            _cache.Remove(Contants.CACHE_HANGHOA_TON_KEY);
            return new DanhMucHangHoaTonCuonModel(item);
        }
        private bool Exists(int id)
        {
            return _context.DanhMucHangHoaTonCuonRepository.Any(e => e.Id == id);
        }
        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<DanhMucHangHoaTonCuonModel>>> Get(SearchRequest request)
        {
            return await _context.DanhMucHangHoaTonCuonRepository
                // .Include(p => p.DMNhomHangs)
                //.Include(p => p.DMMauSacs)
                //.Include(p => p.DMDoDays)
                //.Include(p => p.DMChungLoais)
                //.Include(p => p.DMLoaiTons)
                //.Include(p => p.DMKieuSongs)
                //.Include(p => p.DMTinhGias)
                //.Where(p => p.DMDonViSuDungId == _tenantProvider.TenantId)
                .Where(x => x.DeletedDate == null)
                .Select(item => new DanhMucHangHoaTonCuonModel(item))
                .ToListAsync();
        }

        [HttpGet("ExportToExcel")]
        public async Task<ActionResult<GetAllResponse<DanhMucHangHoaTonCuonModel>>> ExportToExcel([FromBody] HangHoaTonCuonSearchRequest request)
        {
            GetAllResponse<DanhMucHangHoaTonCuonModel> outputs = await GetData(request, false);

            // Log Nhat ky
            await _nhatKyService.LogExportExcel("DMHangHoaTonCuon");
            return outputs;
        }

        [HttpGet("GetAllPaged")]
        public async Task<ActionResult<GetAllResponse<DanhMucHangHoaTonCuonModel>>> GetAllPaged([FromBody] HangHoaTonCuonSearchRequest request)
        {
            GetAllResponse<DanhMucHangHoaTonCuonModel> outputs = await GetData(request, true);
            return outputs;
        }

        private async Task<GetAllResponse<DanhMucHangHoaTonCuonModel>> GetData(HangHoaTonCuonSearchRequest request, bool isPaging)
        {
            GetAllResponse<DanhMucHangHoaTonCuonModel> outputs = new GetAllResponse<DanhMucHangHoaTonCuonModel>();
            Expression<Func<DanhMucHangHoaTonCuon, bool>> filter = m => 1 == 1;

            if (request.DMNhomHangId > 0)
            {
                var maNhomHang = _context.DanhMucNhomHangRepository.FirstOrDefault(item => item.Id == request.DMNhomHangId && item.DeletedDate == null)?.MaNhom ?? string.Empty;
                if (!string.IsNullOrEmpty(maNhomHang))
                {
                    filter = filter.And(x => x.MaNhomHang == maNhomHang);
                }
            }
            if (request.DMMauSacId > 0)
            {
                var maSo = _context.DanhMucMauSacRepository.FirstOrDefault(item => item.Id == request.DMMauSacId && item.DeletedDate == null)?.MaSo ?? string.Empty;
                if (!string.IsNullOrEmpty(maSo))
                {
                    filter = filter.And(x => x.MaMauSac == maSo);
                }
            }
            if (request.DMDoDayId > 0)
            {
                var maSo = _context.DanhMucDoDayRepository.FirstOrDefault(item => item.Id == request.DMDoDayId && item.DeletedDate == null)?.MaSo ?? string.Empty;
                if (!string.IsNullOrEmpty(maSo))
                {
                    filter = filter.And(x => x.MaDoDay == maSo);
                }
            }
            if (request.DMChungLoaiId > 0)
            {
                var maSo = _context.DanhMucChungLoaiRepository.FirstOrDefault(item => item.Id == request.DMChungLoaiId && item.DeletedDate == null)?.MaSo ?? string.Empty;
                if (!string.IsNullOrEmpty(maSo))
                {
                    filter = filter.And(x => x.MaChungLoai == maSo);
                }
            }
            if (request.DMLoaiTonId > 0)
            {
                var maSo = _context.DanhMucLoaiTonRepository.FirstOrDefault(item => item.Id == request.DMLoaiTonId && item.DeletedDate == null)?.MaSo ?? string.Empty;
                if (!string.IsNullOrEmpty(maSo))
                {
                    filter = filter.And(x => x.MaLoaiTon == maSo);
                }
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

            Func<IQueryable<DanhMucHangHoaTonCuon>, IOrderedQueryable<DanhMucHangHoaTonCuon>> order = null;
            if (request.SortDirection != QLSX.Shared.Enums.SortDirection.None)
            {
                order = await OrderBy(request.SortLable, request.SortDirection == QLSX.Shared.Enums.SortDirection.Ascending);
            }

            IQueryable<DanhMucHangHoaTonCuon> query = _context.DanhMucHangHoaTonCuonRepository.Where(item => item.DeletedDate == null);

            //ICollection<FilterDefinition<DanhMucHangHoaTonCuon>> filter1 = request.Filter;
            //FilterBuider<DanhMucHangHoaTonCuon> filterBuider;
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
            //            filterBuider = new FilterBuider<DanhMucHangHoaTonCuon>(f);
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

            var joinQuery = (from hangHoa in query
                             join nhomHang in _context.DanhMucNhomHangRepository.Where(item => item.DeletedDate == null)
                             on hangHoa.MaNhomHang equals nhomHang.MaNhom into nhomHangQueryLeft
                             from nhomHangLeft in nhomHangQueryLeft.DefaultIfEmpty()
                             join mauSac in _context.DanhMucMauSacRepository.Where(item => item.DeletedDate == null)
                             on hangHoa.MaMauSac equals mauSac.MaSo into mauSacQueryLeft
                             from mauSacLeft in mauSacQueryLeft.DefaultIfEmpty()
                             join doDay in _context.DanhMucDoDayRepository.Where(item => item.DeletedDate == null)
                             on hangHoa.MaDoDay equals doDay.MaSo into doDayQueryLeft
                             from doDayLeft in doDayQueryLeft.DefaultIfEmpty()
                             join loaiTon in _context.DanhMucLoaiTonRepository.Where(item => item.DeletedDate == null)
                             on hangHoa.MaLoaiTon equals loaiTon.MaSo into loaiTonQueryLeft
                             from loaiTonLeft in loaiTonQueryLeft.DefaultIfEmpty()
                             join chungLoai in _context.DanhMucChungLoaiRepository.Where(item => item.DeletedDate == null)
                             on hangHoa.MaChungLoai equals chungLoai.MaSo into chungLoaiQueryLeft
                             from chungLoaiLeft in chungLoaiQueryLeft.DefaultIfEmpty()
                             join kieuSong in _context.DanhMucKieuSongRepository.Where(item => item.DeletedDate == null)
                             on hangHoa.MaKieuSong equals kieuSong.MaSo into kieuSongQueryLeft
                             from kieuSongLeft in kieuSongQueryLeft.DefaultIfEmpty()
                             select new
                             {
                                 hangHoa,
                                 nhomHangLeft,
                                 mauSacLeft,
                                 doDayLeft,
                                 loaiTonLeft,
                                 chungLoaiLeft,
                                 kieuSongLeft
                             }).ToList();

            var resultData = joinQuery.GroupBy(item => new { item.hangHoa })
                                     .Select(item => new DanhMucHangHoaTonCuonModel(
                                            item.Key.hangHoa,
                                            item.Select(item => item.nhomHangLeft)?.FirstOrDefault(),
                                            item.Select(item => item.mauSacLeft)?.FirstOrDefault(),
                                            item.Select(item => item.doDayLeft)?.FirstOrDefault(),
                                            item.Select(item => item.loaiTonLeft)?.FirstOrDefault(),
                                            item.Select(item => item.chungLoaiLeft)?.FirstOrDefault(),
                                            item.Select(item => item.kieuSongLeft)?.FirstOrDefault()))
                                     .ToList();
            outputs.Items = resultData;
            return outputs;
        }

        private Expression<Func<DanhMucHangHoaTonCuonModel, bool>> GetFilterTenNhom(FilterDefinition<DanhMucHangHoaTonCuonModel> f)
        {
            Expression<Func<DanhMucHangHoaTonCuonModel, bool>> filter = m => (1 == 1);
            var Operator = f.Operator;
            return Operator switch
            {
                FilterOperator.String.Contains when f.Value != null =>
                   filter.And(x => x.DMNhomHangs.TenNhom.Contains(f.Value.ToString())),
                FilterOperator.String.NotContains when f.Value != null =>
                  filter.And(x => !x.DMNhomHangs.TenNhom.Contains(f.Value.ToString())),
                FilterOperator.String.NotContains when f.Value != null =>
                   filter.And(x => x.DMNhomHangs.TenNhom.Contains(f.Value.ToString())),
                FilterOperator.String.Equal when f.Value != null =>
                   filter.And(x => x.DMNhomHangs.TenNhom.Equals(f.Value.ToString())),
                FilterOperator.String.NotEqual when f.Value != null =>
                  filter.And(x => !x.DMNhomHangs.TenNhom.Equals(f.Value.ToString())),
                FilterOperator.String.StartsWith when f.Value != null =>
                   filter.And(x => x.DMNhomHangs.TenNhom.StartsWith(f.Value.ToString())),
                FilterOperator.String.EndsWith when f.Value != null =>
                   filter.And(x => x.DMNhomHangs.TenNhom.EndsWith(f.Value.ToString())),
                FilterOperator.String.Empty =>
                   filter.And(x => string.IsNullOrEmpty(x.DMNhomHangs.TenNhom)),
                _ => filter.And(x => 1 == 1),
            };
        }
        private Expression<Func<DanhMucHangHoaTonCuonModel, bool>> GetFilterTenChungLoai(FilterDefinition<DanhMucHangHoaTonCuonModel> f)
        {
            Expression<Func<DanhMucHangHoaTonCuonModel, bool>> filter = m => (1 == 1);
            var Operator = f.Operator;
            return Operator switch
            {
                FilterOperator.String.Contains when f.Value != null =>
                   filter.And(x => x.DMChungLoais.ChiTieu.Contains(f.Value.ToString())),
                FilterOperator.String.NotContains when f.Value != null =>
                  filter.And(x => !x.DMChungLoais.ChiTieu.Contains(f.Value.ToString())),
                FilterOperator.String.NotContains when f.Value != null =>
                   filter.And(x => x.DMChungLoais.ChiTieu.Contains(f.Value.ToString())),
                FilterOperator.String.Equal when f.Value != null =>
                   filter.And(x => x.DMChungLoais.ChiTieu.Equals(f.Value.ToString())),
                FilterOperator.String.NotEqual when f.Value != null =>
                  filter.And(x => !x.DMChungLoais.ChiTieu.Equals(f.Value.ToString())),
                FilterOperator.String.StartsWith when f.Value != null =>
                   filter.And(x => x.DMChungLoais.ChiTieu.StartsWith(f.Value.ToString())),
                FilterOperator.String.EndsWith when f.Value != null =>
                   filter.And(x => x.DMChungLoais.ChiTieu.EndsWith(f.Value.ToString())),
                FilterOperator.String.Empty =>
                   filter.And(x => string.IsNullOrEmpty(x.DMChungLoais.ChiTieu)),
                _ => filter.And(x => 1 == 1),
            };
        }

        private Expression<Func<DanhMucHangHoaTonCuonModel, bool>> GetFilterTenMauSac(FilterDefinition<DanhMucHangHoaTonCuonModel> f)
        {
            Expression<Func<DanhMucHangHoaTonCuonModel, bool>> filter = m => (1 == 1);
            var Operator = f.Operator;
            return Operator switch
            {
                FilterOperator.String.Contains when f.Value != null =>
                   filter.And(x => x.DMMauSacs.ChiTieu.Contains(f.Value.ToString())),
                FilterOperator.String.NotContains when f.Value != null =>
                  filter.And(x => !x.DMMauSacs.ChiTieu.Contains(f.Value.ToString())),
                FilterOperator.String.NotContains when f.Value != null =>
                   filter.And(x => x.DMMauSacs.ChiTieu.Contains(f.Value.ToString())),
                FilterOperator.String.Equal when f.Value != null =>
                   filter.And(x => x.DMMauSacs.ChiTieu.Equals(f.Value.ToString())),
                FilterOperator.String.NotEqual when f.Value != null =>
                  filter.And(x => !x.DMMauSacs.ChiTieu.Equals(f.Value.ToString())),
                FilterOperator.String.StartsWith when f.Value != null =>
                   filter.And(x => x.DMMauSacs.ChiTieu.StartsWith(f.Value.ToString())),
                FilterOperator.String.EndsWith when f.Value != null =>
                   filter.And(x => x.DMMauSacs.ChiTieu.EndsWith(f.Value.ToString())),
                FilterOperator.String.Empty =>
                   filter.And(x => string.IsNullOrEmpty(x.DMMauSacs.ChiTieu)),
                _ => filter.And(x => 1 == 1),
            };
        }
        private Expression<Func<DanhMucHangHoaTonCuonModel, bool>> GetFilterTenDoDay(FilterDefinition<DanhMucHangHoaTonCuonModel> f)
        {
            Expression<Func<DanhMucHangHoaTonCuonModel, bool>> filter = m => (1 == 1);
            var Operator = f.Operator;
            return Operator switch
            {
                FilterOperator.String.Contains when f.Value != null =>
                   filter.And(x => x.DMDoDays.ChiTieu.Contains(f.Value.ToString())),
                FilterOperator.String.NotContains when f.Value != null =>
                  filter.And(x => !x.DMDoDays.ChiTieu.Contains(f.Value.ToString())),
                FilterOperator.String.NotContains when f.Value != null =>
                   filter.And(x => x.DMDoDays.ChiTieu.Contains(f.Value.ToString())),
                FilterOperator.String.Equal when f.Value != null =>
                   filter.And(x => x.DMDoDays.ChiTieu.Equals(f.Value.ToString())),
                FilterOperator.String.NotEqual when f.Value != null =>
                  filter.And(x => !x.DMDoDays.ChiTieu.Equals(f.Value.ToString())),
                FilterOperator.String.StartsWith when f.Value != null =>
                   filter.And(x => x.DMDoDays.ChiTieu.StartsWith(f.Value.ToString())),
                FilterOperator.String.EndsWith when f.Value != null =>
                   filter.And(x => x.DMDoDays.ChiTieu.EndsWith(f.Value.ToString())),
                FilterOperator.String.Empty =>
                   filter.And(x => string.IsNullOrEmpty(x.DMDoDays.ChiTieu)),
                _ => filter.And(x => 1 == 1),
            };
        }
        private Expression<Func<DanhMucHangHoaTonCuonModel, bool>> GetFilterTenKieuSong(FilterDefinition<DanhMucHangHoaTonCuonModel> f)
        {
            Expression<Func<DanhMucHangHoaTonCuonModel, bool>> filter = m => (1 == 1);
            var Operator = f.Operator;
            return Operator switch
            {
                FilterOperator.String.Contains when f.Value != null =>
                   filter.And(x => x.DMKieuSongs.ChiTieu.Contains(f.Value.ToString())),
                FilterOperator.String.NotContains when f.Value != null =>
                  filter.And(x => !x.DMKieuSongs.ChiTieu.Contains(f.Value.ToString())),
                FilterOperator.String.NotContains when f.Value != null =>
                   filter.And(x => x.DMKieuSongs.ChiTieu.Contains(f.Value.ToString())),
                FilterOperator.String.Equal when f.Value != null =>
                   filter.And(x => x.DMKieuSongs.ChiTieu.Equals(f.Value.ToString())),
                FilterOperator.String.NotEqual when f.Value != null =>
                  filter.And(x => !x.DMKieuSongs.ChiTieu.Equals(f.Value.ToString())),
                FilterOperator.String.StartsWith when f.Value != null =>
                   filter.And(x => x.DMKieuSongs.ChiTieu.StartsWith(f.Value.ToString())),
                FilterOperator.String.EndsWith when f.Value != null =>
                   filter.And(x => x.DMKieuSongs.ChiTieu.EndsWith(f.Value.ToString())),
                FilterOperator.String.Empty =>
                   filter.And(x => string.IsNullOrEmpty(x.DMKieuSongs.ChiTieu)),
                _ => filter.And(x => 1 == 1),
            };
        }
        private Expression<Func<DanhMucHangHoaTonCuonModel, bool>> GetFilterTenLoaiTon(FilterDefinition<DanhMucHangHoaTonCuonModel> f)
        {
            Expression<Func<DanhMucHangHoaTonCuonModel, bool>> filter = m => (1 == 1);
            var Operator = f.Operator;
            return Operator switch
            {
                FilterOperator.String.Contains when f.Value != null =>
                   filter.And(x => x.DMLoaiTons.ChiTieu.Contains(f.Value.ToString())),
                FilterOperator.String.NotContains when f.Value != null =>
                  filter.And(x => !x.DMLoaiTons.ChiTieu.Contains(f.Value.ToString())),
                FilterOperator.String.NotContains when f.Value != null =>
                   filter.And(x => x.DMLoaiTons.ChiTieu.Contains(f.Value.ToString())),
                FilterOperator.String.Equal when f.Value != null =>
                   filter.And(x => x.DMLoaiTons.ChiTieu.Equals(f.Value.ToString())),
                FilterOperator.String.NotEqual when f.Value != null =>
                  filter.And(x => !x.DMLoaiTons.ChiTieu.Equals(f.Value.ToString())),
                FilterOperator.String.StartsWith when f.Value != null =>
                   filter.And(x => x.DMLoaiTons.ChiTieu.StartsWith(f.Value.ToString())),
                FilterOperator.String.EndsWith when f.Value != null =>
                   filter.And(x => x.DMLoaiTons.ChiTieu.EndsWith(f.Value.ToString())),
                FilterOperator.String.Empty =>
                   filter.And(x => string.IsNullOrEmpty(x.DMLoaiTons.ChiTieu)),
                _ => filter.And(x => 1 == 1),
            };
        }
        private async Task<Func<IQueryable<DanhMucHangHoaTonCuon>, IOrderedQueryable<DanhMucHangHoaTonCuon>>> OrderBy(string sortBy, bool sortType)
        {
            Func<IQueryable<DanhMucHangHoaTonCuon>, IOrderedQueryable<DanhMucHangHoaTonCuon>> myFunc;
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
        // GET: api/DMHangHoas
        [HttpGet("GetAllPagedDialog")]
        public async Task<ActionResult<GetAllResponse<DanhMucHangHoaTonCuonModel>>> GetAllPagedDialog([FromBody] HangHoaTonCuonSearchRequest request)
        {
            GetAllResponse<DanhMucHangHoaTonCuonModel> outputs = new GetAllResponse<DanhMucHangHoaTonCuonModel>();

            var query = (
               from cus in _context.DanhMucHangHoaTonCuonRepository
                   //.Include(p => p.DMNhomHangs)
                   //.Include(p => p.DMMauSacs)
                   //.Include(p => p.DMDoDays)
                   //.Include(p => p.DMChungLoais)
                   //.Include(p => p.DMLoaiTons)
                   //.Include(p => p.DMKieuSongs)
                   //.Include(p => p.DMTinhGias)
               where /*cus.DMDonViSuDungId == _tenantProvider.TenantId &&*/ cus.DeletedDate == null
               select cus);

            if (!string.IsNullOrEmpty(request.SearchText)) query = query.Where(x => x.MaHangHoa.ToLower().Contains(request.SearchText.ToLower())
            || x.TenHangHoa.ToLower().Contains(request.SearchText.ToLower())
            || x.DonViTinh.ToLower().Contains(request.SearchText.ToLower())
            );
            Console.WriteLine(query.ToQueryString());

            outputs.TotalRecords = await query.CountAsync();
            outputs.TotalPages = (int)Math.Ceiling(outputs.TotalRecords / (double)request.PageSize);
            outputs.Page = request.Page;
            outputs.PageSize = request.PageSize;
            //var item = await query.ToListAsync();
            query = query
                .Skip(request.Page * request.PageSize).Take(request.PageSize);
            outputs.Items = await query.Select(item => new DanhMucHangHoaTonCuonModel(item)).ToListAsync();
            return outputs;
        }
    }
}
