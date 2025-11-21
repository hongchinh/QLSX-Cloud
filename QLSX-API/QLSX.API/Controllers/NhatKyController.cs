using QLSX.Shared.Models.Request;
using SaleAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QLSX.Shared.Entities;
using QLSX.Shared.Models;
using NhatKy = QLSX.Shared.Entities.NhatKy;

namespace SaleAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class NhatKyController : ControllerBase
    {
        private readonly CRMDBContext _context;
        public NhatKyController(CRMDBContext context)
        {
            _context = context;
        }
        [HttpGet("GetAllPaged")]
        public async Task<ActionResult<GetAllResponsePaged<NhatKyModel>>> Get(NhatKySearchRequest request)
        {
            GetAllResponsePaged<NhatKyModel> outputs = new GetAllResponsePaged<NhatKyModel>();

            var query =
        from res in _context.NhatKyRepository
            //.Include(x => x.User)
        select res;

            if (!string.IsNullOrEmpty(request.ChucNang))
            {
                query = query.Where(x => x.ChucNang.ToLower().Contains(request.ChucNang.ToLower()));
            };
            if (!string.IsNullOrEmpty(request.HoTen))
            {
                query = query.Where(x => x.HoTen.Contains(request.HoTen));
            };
            if (!string.IsNullOrEmpty(request.SoChungTu))
            {
                query = query.Where(x => x.SoChungTu.Contains(request.SoChungTu));
            };
            if (!string.IsNullOrEmpty(request.IdPhieu))
            {
                query = query.Where(x => x.IdPhieu.Contains(request.IdPhieu));
            };
            Func<IQueryable<NhatKy>, IOrderedQueryable<NhatKy>> order = null;
            if (request.SortDirection != QLSX.Shared.Enums.SortDirection.None)
            {
                order = await OrderBy(request.SortLable, request.SortDirection == QLSX.Shared.Enums.SortDirection.Ascending);
            }
            if (order != null) query = order(query);


            outputs.TotalRecords = await query.CountAsync();
            outputs.TotalPages = (int)Math.Ceiling(outputs.TotalRecords / (double)request.PageSize);
            outputs.Page = request.Page;
            outputs.PageSize = request.PageSize;

            query = query.Skip(request.Page * request.PageSize).Take(request.PageSize);
            outputs.Items = await query.Select(x => new NhatKyModel(x)).ToListAsync();
            return outputs;
        }

        [HttpGet("ExportToExcel")]
        public async Task<ActionResult<GetAllResponse<NhatKyModel>>> exportExcel(SearchNhatKisRequest request)
        {
            GetAllResponse<NhatKyModel> outputs = new GetAllResponse<NhatKyModel>();

            var query =
             from res in _context.NhatKyRepository
                 //.Include(x => x.User)
             select res;

            if (!string.IsNullOrEmpty(request.ChucNang))
            {
                query = query.Where(x => x.ChucNang.ToLower().Contains(request.ChucNang.ToLower()));
            };
            //if (!string.IsNullOrEmpty(request.Error))
            //{
            //    query = query.Where(x => x.Error != null && x.Error.Contains(request.Error));
            //};
            //int? userId = request?.UserId;
            //if (userId != null)
            //{
            //    query = query.Where(x => x.UserId.Equals(request.UserId));
            //};
            //int? tenantId = request?.TenantId;
            //if (tenantId != null)
            //{
            //    query = query.Where(x => x.UserId.Equals(request.UserId));
            //};
            //DateTime? thoiGian_From = request?.ThoiGian_From;
            //if (thoiGian_From != null)
            //{
            //    query = query.Where(x => x.ThoiGian >= request.ThoiGian_From);
            //};
            //DateTime? thoiGian_To = request?.ThoiGian_To;
            //if (thoiGian_To != null)
            //{
            //    query = query.Where(x => x.ThoiGian <= request.ThoiGian_To);
            //};


            outputs.TotalRecords = await query.CountAsync();
            outputs.TotalPages = (int)Math.Ceiling(outputs.TotalRecords / (double)request.PageSize);
            outputs.Page = request.Page;
            outputs.PageSize = request.PageSize;

            //query = query.Skip(request.Page * request.PageSize).Take(request.PageSize);
            outputs.Items = await query.Select(x => new NhatKyModel(x)).ToListAsync();
            return outputs;
        }

        [HttpGet("GetByIdPhieu")]
        public async Task<ActionResult<GetAllResponse<NhatKyModel>>> GetByIdPhieu(SearchNhatKisRequest request)
        {
            GetAllResponse<NhatKyModel> outputs = new GetAllResponse<NhatKyModel>();

            var query =
        from res in _context.NhatKyRepository
            //.Include(x => x.User)
        select res;

            //if ( request.IdPhieu.HasValue)
            //{
            //    query = query.Where(x => x.IdPhieu == request .IdPhieu);

            //    Func<IQueryable<NhatKy>, IOrderedQueryable<NhatKy>> order = null;
            //    if (request.SortDirection != QLSX.Shared.Enums.SortDirection.None)
            //    {
            //        order = await OrderBy(request.SortLable, request.SortDirection == QLSX.Shared.Enums.SortDirection.Ascending);
            //    }
            //    if (order != null) query = order(query);
            //    outputs.Items = await query.ToListAsync();
            //    return outputs;
            //}
            //else
            //{
            //    outputs.Items = new List<NhatKy>();
            //    return outputs;
            //}

            return outputs;
        }


        private async Task<Func<IQueryable<NhatKy>, IOrderedQueryable<NhatKy>>> OrderBy(string sortBy, bool sortType)
        {
            Func<IQueryable<NhatKy>, IOrderedQueryable<NhatKy>> myFunc;
            if (sortBy == "ThoiGian")
            {
                //if (sortType) myFunc = source => source.OrderBy(x => x.ThoiGian);
                //else myFunc = source => source.OrderByDescending(x => x.ThoiGian);
                return null;
            }
            if (sortBy == "ChucNang")
            {
                if (sortType) myFunc = source => source.OrderBy(x => x.ChucNang);
                else myFunc = source => source.OrderByDescending(x => x.ChucNang);
                return myFunc;
            }
            //if (sortBy == "NoiDungCu")
            //{
            //    if (sortType) myFunc = source => source.OrderBy(x => x.NoiDungCu);
            //    else myFunc = source => source.OrderByDescending(x => x.NoiDungCu);
            //    return myFunc;
            //}
            //if (sortBy == "NoiDungMoi")
            //{
            //    if (sortType) myFunc = source => source.OrderBy(x => x.NoiDungMoi);
            //    else myFunc = source => source.OrderByDescending(x => x.NoiDungMoi);
            //    return myFunc;
            //}
            //if (sortBy == "Error")
            //{
            //    if (sortType) myFunc = source => source.OrderBy(x => x.Error);
            //    else myFunc = source => source.OrderByDescending(x => x.Error);
            //    return myFunc;
            //}

            return null;

        }

    }
}
