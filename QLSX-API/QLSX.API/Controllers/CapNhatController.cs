using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SaleAPI.Models;
using QLSX.Shared.Models;
using System.Linq.Expressions;
using SaleAPI.Extensions;
using QLSX.Shared.Data.Responses;
using SaleAPI.Interfaces;
using SaleAPI.Services;
using AspNetCore.ReportingServices.ReportProcessing.ReportObjectModel;

namespace SaleAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CapNhatController : ControllerBase
    {
        private readonly CRMDBContext _context;
        private readonly ITenantProvider _tenantProvider;
        private readonly INhatKyService _nhatKyService;
        public CapNhatController(CRMDBContext context, ITenantProvider tenantProvider, INhatKyService nhatKyService)
        {
            _context = context;
            _tenantProvider = tenantProvider;
            _nhatKyService = nhatKyService;
        }
        // GET: api/Department
        [HttpPost("CapNhatGiaVon")]
        public async Task CapNhatGiaVon(CapNhatRequest request)
        {
            DateTime ngay = request.DenNgay ?? DateTime.Now;

            ngay = ngay.AddMonths(1).AddDays(-1);

            var sql = $"exec CapNhatDonGiaVon '{string.Format("{0:yyyy/MM/dd}", request.TuNgay)}','{string.Format("{0:yyyy/MM/dd}", ngay)}',{request.DMKhoHangId},'{_tenantProvider.TenantId}','{_tenantProvider.UserId}'";
            _context.Database.ExecuteSqlRaw(sql);


        }
    }
}
