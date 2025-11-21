using AspNetCore.Reporting;
using AutoMapper;
using QLSX.Shared.Constants;
using QLSX.Shared.Data.Requests.BaoCao;
using QLSX.Shared.Data.Responses;
using QLSX.Shared.Data.Responses.BaoCao;
using QLSX.Shared.Models;
using QLSX.Shared.Ultils;
using SaleAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using SaleAPI.Interfaces;
using QLSX.Shared.Entities;
using User = QLSX.Shared.Entities.User;

namespace SaleAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("_myAllowSpecificOrigins")]
    public class ReportsController : ControllerBase
    {
        private readonly CRMDBContext _context;
        private readonly QLSX.Shared.Models.JWTSettings _jwtsettings;
        private readonly IMapper _mapper;
        private readonly ITenantProvider _tenantProvider;
        public IConfiguration _configuration { get; }
        private readonly IWebHostEnvironment _webHostEnvironment;
        public Microsoft.Extensions.Configuration.IConfiguration Configuration { get; }
        public ReportsController(IWebHostEnvironment webHostEnvironment, IConfiguration configuration,
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

        /// <summary>
        /// Get products whose price is greater than equal to 1000 store procedure method.
        /// </summary>
        /// <returns>Returns - List of products whose price is greater than equal to 1000</returns>
        [HttpGet("DanhMucKhachHangRepository/pdf")]
        public async Task<IActionResult> GetPDFReport(string request)
        {
            User user = await GetUserFromAccessToken(request);
            if (user == null)
            {
                return NotFound();
            }
            // Initialization.
            try
            {
                // Processing.
                DataTable dt = new DataTable();
                DataSet ds = new DataSet();
                string StoredProc = "exec ThongHopSoLieu " +
                    "@id = 1";
                string connectionString = Configuration.GetConnectionString("CRMConnectStrings");

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    adapter.SelectCommand = new SqlCommand(StoredProc, connection);
                    adapter.Fill(ds);
                }


                var path = this._webHostEnvironment.WebRootPath + "\\Reports\\Report1.rdlc";
                string mimeType = "";
                int extentsion = 1;
                //Dictionary<string, string> params = new Dictionary<string, string>();
                LocalReport localReport = new LocalReport(path);
                localReport.AddDataSource("SampleDataSet1", ds.Tables[0]);
                localReport.AddDataSource("DataSet1", ds.Tables[1]);

                var rs = localReport.Execute(RenderType.Pdf, extentsion, null, mimeType);
                return File(rs.MainStream, "application/pdf");

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Get products whose price is greater than equal to 1000 store procedure method.
        /// </summary>
        /// <returns>Returns - List of products whose price is greater than equal to 1000</returns>
        [HttpGet("DanhMucKhachHangRepository/Excel")]
        public async Task<IActionResult> rptPhieuThuExcel(string request, int id)
        {
            User user = await GetUserFromAccessToken(request);
            if (user == null)
            {
                return NotFound();
            }
            // Initialization.
            try
            {
                // Processing.
                DataSet ds = new DataSet();
                string StoredProc = "exec InPhieuThuChi " +
                     "@hien=0, @id = " + id.ToString() + ", @tmptblOK ='ZZTEMP'";
                string connectionString = Configuration.GetConnectionString("CRMConnectStrings");

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    adapter.SelectCommand = new SqlCommand(StoredProc, connection);
                    adapter.Fill(ds);
                }

                var path = this._webHostEnvironment.WebRootPath + "\\Reports\\rptPhieuThu.rdlc";
                string mimeType = "";
                int extentsion = 1;
                //Dictionary<string, string> params = new Dictionary<string, string>();
                LocalReport localReport = new LocalReport(path);
                localReport.AddDataSource("dsData", ds.Tables[0]);
                localReport.AddDataSource("dsHeader", ds.Tables[1]);
                var rs = localReport.Execute(RenderType.Excel, extentsion, null, mimeType);
                return File(rs.MainStream, "application/vnd.ms-excel");

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Get products whose price is greater than equal to 1000 store procedure method.
        /// </summary>
        /// <returns>Returns - List of products whose price is greater than equal to 1000</returns>
        [HttpGet("rptPhieuThu/pdf")]
        public async Task<IActionResult> rptPhieuThuPdf(string request, int id)
        {
            User user = await GetUserFromAccessToken(request);
            if (user == null)
            {
                return NotFound();
            }
            // Initialization.
            try
            {
                // Processing.
                DataSet ds = new DataSet();
                string StoredProc = "exec InPhieuThuChi " +
                    "@hien=0, @id = " + id.ToString() + ", @tmptblOK ='ZZTEMP'";
                string connectionString = Configuration.GetConnectionString("CRMConnectStrings");

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    adapter.SelectCommand = new SqlCommand(StoredProc, connection);
                    adapter.Fill(ds);
                }

                var path = this._webHostEnvironment.WebRootPath + "\\Reports\\rptPhieuThu.rdlc";
                string mimeType = "";
                int extentsion = 1;
                //Dictionary<string, string> params = new Dictionary<string, string>();
                LocalReport localReport = new LocalReport(path);

                localReport.AddDataSource("dsData", ds.Tables[0]);
                localReport.AddDataSource("dsHeader", ds.Tables[1]);
                var rs = localReport.Execute(RenderType.Pdf, extentsion, null, mimeType);
                return File(rs.MainStream, "application/pdf");

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpGet("rptPhieuChi/pdf")]
        public async Task<IActionResult> rptPhieuChiPdf(string request, int id)
        {
            User user = await GetUserFromAccessToken(request);
            if (user == null)
            {
                return NotFound();
            }
            // Initialization.
            try
            {
                // Processing.
                DataSet ds = new DataSet();
                string StoredProc = "exec InPhieuThuChi " +
                    "@hien=0, @id = " + id.ToString() + ", @tmptblOK ='ZZTEMP'";
                string connectionString = Configuration.GetConnectionString("CRMConnectStrings");

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    adapter.SelectCommand = new SqlCommand(StoredProc, connection);
                    adapter.Fill(ds);
                }

                var path = this._webHostEnvironment.WebRootPath + "\\Reports\\rptPhieuChi.rdlc";
                string mimeType = "";
                int extentsion = 1;
                //Dictionary<string, string> params = new Dictionary<string, string>();
                LocalReport localReport = new LocalReport(path);

                localReport.AddDataSource("dsData", ds.Tables[0]);
                localReport.AddDataSource("dsHeader", ds.Tables[1]);
                var rs = localReport.Execute(RenderType.Pdf, extentsion, null, mimeType);
                return File(rs.MainStream, "application/pdf");

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpGet("rptPhieuXuat/pdf")]
        public async Task<IActionResult> rptPhieuXuatPdf(string request, int id)
        {
            User user = await GetUserFromAccessToken(request);
            if (user == null)
            {
                return NotFound();
            }
            // Initialization.
            try
            {
                // Processing.
                DataSet ds = new DataSet();
                string StoredProc = "exec InPhieuXuat " +
                    "@id = " + id.ToString() + "";
                string connectionString = Configuration.GetConnectionString("CRMConnectStrings");

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    adapter.SelectCommand = new SqlCommand(StoredProc, connection);
                    adapter.Fill(ds);
                }

                var path = this._webHostEnvironment.WebRootPath + "\\Reports\\rptPhieuXuat.rdlc";
                string mimeType = "";
                int extentsion = 1;
                //Dictionary<string, string> params = new Dictionary<string, string>();
                LocalReport localReport = new LocalReport(path);

                localReport.AddDataSource("dsData", ds.Tables[0]);
                localReport.AddDataSource("dsHeader", ds.Tables[1]);
                var rs = localReport.Execute(RenderType.Pdf, extentsion, null, mimeType);
                return File(rs.MainStream, "application/pdf");

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private async Task<User> GetUserFromAccessToken(string accessToken)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_jwtsettings.SecretKey);

                var tokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };

                SecurityToken securityToken;
                var principle = tokenHandler.ValidateToken(accessToken, tokenValidationParameters, out securityToken);

                JwtSecurityToken jwtSecurityToken = securityToken as JwtSecurityToken;

                //if (jwtSecurityToken != null && jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                //{
                //    var userId = principle.FindFirst(ClaimTypes.Name)?.Value;
                //    var user = await _context.Users.Include(u => u.Role)
                //                        .Where(u => u.UserId == Convert.ToInt32(userId)).FirstOrDefaultAsync();
                //    user.RoleName = user.Role.RoleDesc;
                //    return user;
                //}
            }
            catch (Exception ex)
            {
                return new User();
            }

            return new User();
        }

        
        [HttpPost(ApiBaoCaoPath.SoQuyTienMat)]
        public async Task<ActionResult<ReportResponseBase<SoQuyTienMatResponse>>> SoQuyTienMat(SoQuyTienMatRequest request)
        {
            string StoredProc = "EXEC SoQuyTienMat @date1 = '" + request.TuNgay?.ToString("MM/dd/yyy") + "',@date2 = '" + request.DenNgay?.ToString("MM/dd/yyy") + "', @mdvsd = " + _tenantProvider.TenantId.ToString();
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
            var lst = ConvertDatatableToList.ConvertToList<SoQuyTienMatResponse>(ds.Tables[0]);
            var lstApi = new ReportResponseBase<SoQuyTienMatResponse>()
            {
                StatusCode = ApiResponseCodes.Success,
                Message = ApiResponseMessages.Success,
                ListData = lst,
                ThongTin = ttres
            };
            return Ok(lstApi);
        }

        [HttpPost(ApiBaoCaoPath.ChiTietKhoanChi)]
        public async Task<ActionResult<ReportResponseBase<ChiTietKhoanChiResponse>>> ChiTietKhoanChi(ChiTietKhoanChiRequest request)
        {
            string StoredProc = "EXEC ChiTietKhoanChi  @date1 = '" + request.TuNgay?.ToString("MM/dd/yyy") + "',@date2 = '" + request.DenNgay?.ToString("MM/dd/yyy") + "', @mdvsd = " + _tenantProvider.TenantId.ToString();
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
            var lst = ConvertDatatableToList.ConvertToList<ChiTietKhoanChiResponse>(ds.Tables[0]);
            var lstApi = new ReportResponseBase<ChiTietKhoanChiResponse>()
            {
                StatusCode = ApiResponseCodes.Success,
                Message = ApiResponseMessages.Success,
                ListData = lst,
                ThongTin = ttres
            };
            return Ok(lstApi);
        }
        [HttpPost(ApiBaoCaoPath.ChiTietKhoanThu)]
        public async Task<ActionResult<ReportResponseBase<BangCanDoiThuChiResponse>>> ChiTietKhoanThu(ChiTietKhoanThuRequest request)
        {
            string StoredProc = "EXEC ChiTietKhoanThu  @date1 = '" + request.TuNgay?.ToString("MM/dd/yyy") + "',@date2 = '" + request.DenNgay?.ToString("MM/dd/yyy") + "', @mdvsd = " + _tenantProvider.TenantId.ToString();
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
            var lst = ConvertDatatableToList.ConvertToList<ChiTietKhoanThuResponse>(ds.Tables[0]);
            var lstApi = new ReportResponseBase<ChiTietKhoanThuResponse>()
            {
                StatusCode = ApiResponseCodes.Success,
                Message = ApiResponseMessages.Success,
                ListData = lst,
                ThongTin = ttres
            };
            return Ok(lstApi);
        }


        [HttpPost(ApiBaoCaoPath.BangCanDoiThuChi)]
        public async Task<ActionResult<ReportResponseBase<BangCanDoiThuChiResponse>>> BangCanDoiThuChi(BangCanDoiThuChiRequest request)
        {
            string StoredProc = "EXEC BangCanDoiThuChi  @date1 = '" + request.TuNgay?.ToString("MM/dd/yyy") + "',@date2 = '" + request.DenNgay?.ToString("MM/dd/yyy") + "', @mdvsd = " + _tenantProvider.TenantId.ToString();
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
            var lst = ConvertDatatableToList.ConvertToList<BangCanDoiThuChiResponse>(ds.Tables[0]);
            var lstApi = new ReportResponseBase<BangCanDoiThuChiResponse>()
            {
                StatusCode = ApiResponseCodes.Success,
                Message = ApiResponseMessages.Success,
                ListData = lst,
                ThongTin = ttres
            };
            return Ok(lstApi);
        }

        [HttpPost(ApiBaoCaoPath.SoPhaiThu)]
        public async Task<ActionResult<ReportResponseBase<SoPhaiThuResponse>>> SoPhaiThu(SoPhaiThuRequest request)
        {
            string StoredProc = "EXEC SoPhaiThu  @date1 = '" + request.TuNgay?.ToString("MM/dd/yyy") + "',@date2 = '" + request.DenNgay?.ToString("MM/dd/yyy") + "', @mdvsd = " + _tenantProvider.TenantId.ToString();
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
            var lst = ConvertDatatableToList.ConvertToList<SoPhaiThuResponse>(ds.Tables[0]);
            var lstApi = new ReportResponseBase<SoPhaiThuResponse>()
            {
                StatusCode = ApiResponseCodes.Success,
                Message = ApiResponseMessages.Success,
                ListData = lst,
                ThongTin = ttres
            };
            return Ok(lstApi);
        }

        [HttpPost(ApiBaoCaoPath.SoPhaiTra)]
        public async Task<ActionResult<ReportResponseBase<SoPhaiTraResponse>>> SoPhaiTra(SoPhaiTraRequest request)
        {
            string StoredProc = "EXEC SoPhaiTra  @date1 = '" + request.TuNgay?.ToString("MM/dd/yyy") + "',@date2 = '" + request.DenNgay?.ToString("MM/dd/yyy") + "', @mdvsd = " + _tenantProvider.TenantId.ToString();
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
            var lst = ConvertDatatableToList.ConvertToList<SoPhaiTraResponse>(ds.Tables[0]);
            var lstApi = new ReportResponseBase<SoPhaiTraResponse>()
            {
                StatusCode = ApiResponseCodes.Success,
                Message = ApiResponseMessages.Success,
                ListData = lst,
                ThongTin = ttres
            };
            return Ok(lstApi);
        }


        [HttpPost(ApiBaoCaoPath.SoPhaiThuTongHop)]
        public async Task<ActionResult<ReportResponseBase<SoPhaiThuTongHopResponse>>> SoPhaiThuTongHop(SoPhaiThuTongHopRequest request)
        {
            string StoredProc = "EXEC SoPhaiThuTongHop  @date1 = '" + request.TuNgay?.ToString("MM/dd/yyy") + "',@date2 = '" + request.DenNgay?.ToString("MM/dd/yyy") + "', @mdvsd = " + _tenantProvider.TenantId.ToString();
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
            var lst = ConvertDatatableToList.ConvertToList<SoPhaiThuTongHopResponse>(ds.Tables[0]);
            var lstApi = new ReportResponseBase<SoPhaiThuTongHopResponse>()
            {
                StatusCode = ApiResponseCodes.Success,
                Message = ApiResponseMessages.Success,
                ListData = lst,
                ThongTin = ttres
            };
            return Ok(lstApi);
        }
        [HttpPost(ApiBaoCaoPath.SoPhaiTraTongHop)]
        public async Task<ActionResult<ReportResponseBase<SoPhaiTraTongHopResponse>>> SoPhaiTraTongHop(SoPhaiThuTongHopRequest request)
        {
            string StoredProc = "EXEC SoPhaiTraTongHop  @date1 = '" + request.TuNgay?.ToString("MM/dd/yyy") + "',@date2 = '" + request.DenNgay?.ToString("MM/dd/yyy") + "', @mdvsd = " + _tenantProvider.TenantId.ToString();
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
            var lst = ConvertDatatableToList.ConvertToList<SoPhaiTraTongHopResponse>(ds.Tables[0]);
            var lstApi = new ReportResponseBase<SoPhaiTraTongHopResponse>()
            {
                StatusCode = ApiResponseCodes.Success,
                Message = ApiResponseMessages.Success,
                ListData = lst,
                ThongTin = ttres
            };
            return Ok(lstApi);
        }
        [HttpPost(ApiBaoCaoPath.SoChiTietHangHoa)]
        public async Task<ActionResult<ReportResponseBase<SoChiTietHangHoaResponse>>> SoChiTietHangHoa(SoChiTietHangHoaRequest request)
        {
            string StoredProc = "EXEC SoChiTietHangHoa  @date1 = '" + request.TuNgay?.ToString("MM/dd/yyy") + "',@date2 = '" + request.DenNgay?.ToString("MM/dd/yyy") + "', @mdvsd = " + _tenantProvider.TenantId.ToString();
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
            var lst = ConvertDatatableToList.ConvertToList<SoChiTietHangHoaResponse>(ds.Tables[0]);
            var lstApi = new ReportResponseBase<SoChiTietHangHoaResponse>()
            {
                StatusCode = ApiResponseCodes.Success,
                Message = ApiResponseMessages.Success,
                ListData = lst,
                ThongTin = ttres
            };
            return Ok(lstApi);
        }
        [HttpPost(ApiBaoCaoPath.SoTongHopHangHoa)]
        public async Task<ActionResult<ReportResponseBase<SoTongHopHangHoaResponse>>> SoTongHopHangHoa(SoTongHopHangHoaRequest request)
        {
            string StoredProc = "EXEC SoTongHopHangHoa  @date1 = '" + request.TuNgay?.ToString("MM/dd/yyy") + "',@date2 = '" + request.DenNgay?.ToString("MM/dd/yyy") + "', @mdvsd = " + _tenantProvider.TenantId.ToString();
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
            var lst = ConvertDatatableToList.ConvertToList<SoTongHopHangHoaResponse>(ds.Tables[0]);
            var lstApi = new ReportResponseBase<SoTongHopHangHoaResponse>()
            {
                StatusCode = ApiResponseCodes.Success,
                Message = ApiResponseMessages.Success,
                ListData = lst,
                ThongTin = ttres
            };
            return Ok(lstApi);
        }
    }
}
