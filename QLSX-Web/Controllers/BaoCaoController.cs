using QLSX.Shared.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;

namespace QLSX.Web.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BaoCaoController : Controller
    {

        private readonly IMemoryCache _memoryCache;
        private readonly IApiWrapperServices _apiServices;
        public IConfiguration _configuration { get; }
        private readonly IWebHostEnvironment _webHostEnvironment;
        public BaoCaoController(IMemoryCache memoryCache,
            IConfiguration configuration, IApiWrapperServices apiServices,
            IWebHostEnvironment webHostEnvironment)
        {
            _configuration = configuration;
            _memoryCache = memoryCache;
            _apiServices = apiServices;
            _webHostEnvironment = webHostEnvironment;
        }

       
        //[HttpGet]
        //[Route(ApiBaoCaoPath.SoQuyTienMat)]
        //public async Task<IActionResult> SoQuyTienMat(string tungay, string denngay, string donvi)
        //{
        //    DateTime td1 = DateTime.Now;
        //    DateTime td2 = DateTime.Now;
        //    DateTime.TryParse(tungay, out td1);
        //    DateTime.TryParse(denngay, out td2);
        //    int dv = string.IsNullOrEmpty(donvi) ? ((User)_memoryCache.Get("_User_Login")).DMDonViSuDungId : int.Parse(donvi);
        //    var request = new SoQuyTienMatRequest
        //    {
        //        DMDonViSuDungId = dv,
        //        TuNgay = new DateTime(td1.Year, td1.Month, td1.Day),
        //        DenNgay = new DateTime(td2.Year, td2.Month, td2.Day),
        //    };
        //    string token = (string)_memoryCache.Get("_Key_Token");
        //    if (!ValidateToken(token))
        //    {
        //        return Unauthorized();
        //    }
        //    var results = await _apiServices.SendReportPostAsync<SoQuyTienMatRequest, SoQuyTienMatResponse>(request);
        //    if (results.StatusCode == 401 || string.IsNullOrEmpty(token))
        //    {
        //        return Unauthorized();
        //    }
        //    if (results.ListData == null || results.ListData.Count == 0)
        //    {

        //    }
        //    var host = _configuration.GetSection("AppSettings:WebBase").Value.ToString();
        //    var options = new ConvertOptions { };
        //    // options.set
        //    _generatePdf.SetConvertOptions(options);

        //    results.Host = host;

        //    var pdf = await _generatePdf.GetByteArray("/Reports/SoQuyTienMat.cshtml", results);
        //    var pdfStream = new System.IO.MemoryStream();
        //    pdfStream.Write(pdf, 0, pdf.Length);
        //    pdfStream.Position = 0;

        //    return new FileStreamResult(pdfStream, "application/pdf");
        //}
        //[HttpGet]
        //[Route(ApiBaoCaoPath.ChiTietKhoanChi)]
        //public async Task<IActionResult> ChiTietKhoanChi(string tungay, string denngay, string donvi)
        //{
        //    DateTime td1 = DateTime.Now;
        //    DateTime td2 = DateTime.Now;
        //    DateTime.TryParse(tungay, out td1);
        //    DateTime.TryParse(denngay, out td2);
        //    int dv = string.IsNullOrEmpty(donvi) ? ((User)_memoryCache.Get("_User_Login")).DMDonViSuDungId : int.Parse(donvi);
        //    var request = new ChiTietKhoanChiRequest
        //    {
        //        DMDonViSuDungId = dv,
        //        TuNgay = new DateTime(td1.Year, td1.Month, td1.Day),
        //        DenNgay = new DateTime(td2.Year, td2.Month, td2.Day),
        //    };
        //    string token = (string)_memoryCache.Get("_Key_Token");
        //    if (!ValidateToken(token))
        //    {
        //        return Unauthorized();
        //    }
        //    var results = await _apiServices.SendReportPostAsync<ChiTietKhoanChiRequest, ChiTietKhoanChiResponse>(request);
        //    if (results.StatusCode == 401 || string.IsNullOrEmpty(token))
        //    {
        //        return Unauthorized();
        //    }
        //    if (results.ListData == null || results.ListData.Count == 0)
        //    {

        //    }
        //    var host = _configuration.GetSection("AppSettings:WebBase").Value.ToString();
        //    var options = new ConvertOptions { };
        //    // options.set
        //    _generatePdf.SetConvertOptions(options);

        //    results.Host = host;
        //    //string path = HttpContext.Current.Server("");
        //    var pdf = await _generatePdf.GetByteArray("/Reports/ChiTietKhoanChi.cshtml", results);
        //    var pdfStream = new System.IO.MemoryStream();
        //    pdfStream.Write(pdf, 0, pdf.Length);
        //    pdfStream.Position = 0;

        //    return new FileStreamResult(pdfStream, "application/pdf");
        //}

        //[HttpGet]
        //[Route(ApiBaoCaoPath.ChiTietKhoanThu)]
        //public async Task<IActionResult> ChiTietKhoanThu(string tungay, string denngay, string donvi)
        //{
        //    DateTime td1 = DateTime.Now;
        //    DateTime td2 = DateTime.Now;
        //    DateTime.TryParse(tungay, out td1);
        //    DateTime.TryParse(denngay, out td2);
        //    int dv = string.IsNullOrEmpty(donvi) ? ((User)_memoryCache.Get("_User_Login")).DMDonViSuDungId : int.Parse(donvi);
        //    var request = new ChiTietKhoanThuRequest
        //    {
        //        DMDonViSuDungId = dv,
        //        TuNgay = new DateTime(td1.Year, td1.Month, td1.Day),
        //        DenNgay = new DateTime(td2.Year, td2.Month, td2.Day),
        //    };
        //    string token = (string)_memoryCache.Get("_Key_Token");
        //    if (!ValidateToken(token))
        //    {
        //        return Unauthorized();
        //    }
        //    var results = await _apiServices.SendReportPostAsync<ChiTietKhoanThuRequest, ChiTietKhoanThuResponse>(request);
        //    if (results.StatusCode == 401 || string.IsNullOrEmpty(token))
        //    {
        //        return Unauthorized();
        //    }

        //    var host = _configuration.GetSection("AppSettings:WebBase").Value.ToString();
        //    var options = new ConvertOptions { };
        //    // options.set
        //    _generatePdf.SetConvertOptions(options);

        //    results.Host = host;

        //    var pdf = await _generatePdf.GetByteArray("/Reports/ChiTietKhoanThu.cshtml", results);
        //    var pdfStream = new System.IO.MemoryStream();
        //    pdfStream.Write(pdf, 0, pdf.Length);
        //    pdfStream.Position = 0;

        //    return new FileStreamResult(pdfStream, "application/pdf");
        //}

        //[MiddlewareFilter(typeof(JsReportPipeline))]
        //[Route(ApiBaoCaoPath.SoQuyTienMatNew)]
        //public async Task<IActionResult> SoQuyTienMatNew()
        //{

        //    var request = new SoQuyTienMatRequest
        //    {
        //        DMDonViSuDungId = ((User)_memoryCache.Get("_User_Login")).DMDonViSuDungId,
        //        TuNgay = new DateTime(2010, 1, 1),
        //        DenNgay = new DateTime(2022, 1, 1),
        //    };
        //    string token = (string)_memoryCache.Get("_Key_Token");
        //    if (!ValidateToken(token))
        //    {
        //        return Unauthorized();
        //    }
        //    var results = await _apiServices.SendReportPostAsync<SoQuyTienMatRequest, SoQuyTienMatResponse>(request);
        //    if (results.StatusCode == 401 || string.IsNullOrEmpty(token))
        //    {
        //        return Unauthorized();
        //    }

        //    var header = await JsReportMVCService.RenderViewToStringAsync(HttpContext, RouteData, "Header", new { });
        //    var footer = await JsReportMVCService.RenderViewToStringAsync(HttpContext, RouteData, "Footer", new { });

        //    HttpContext.JsReportFeature()
        //        .Recipe(Recipe.ChromePdf)
        //        .Configure((r) => r.Template.Chrome = new Chrome
        //        {
        //            HeaderTemplate = header,
        //            DisplayHeaderFooter = true,
        //            FooterTemplate = footer,
        //            MarginTop = "1cm",
        //            MarginLeft = "1cm",
        //            MarginBottom = "1cm",
        //            MarginRight = "1cm",
        //        });

        //    return View("SoQuyTienMat", results);
        //}


        //[HttpGet]
        //[Route(ApiBaoCaoPath.BangCanDoiThuChi)]
        //public async Task<IActionResult> BangCanDoiThuChi(string tungay, string denngay, string donvi)
        //{
        //    DateTime td1 = DateTime.Now;
        //    DateTime td2 = DateTime.Now;
        //    DateTime.TryParse(tungay, out td1);
        //    DateTime.TryParse(denngay, out td2);
        //    int dv = string.IsNullOrEmpty(donvi) ? ((User)_memoryCache.Get("_User_Login")).DMDonViSuDungId : int.Parse(donvi);
        //    var request = new BangCanDoiThuChiRequest
        //    {
        //        DMDonViSuDungId = dv,
        //        TuNgay = new DateTime(td1.Year, td1.Month, td1.Day),
        //        DenNgay = new DateTime(td2.Year, td2.Month, td2.Day),
        //    };
        //    string token = (string)_memoryCache.Get("_Key_Token");
        //    if (!ValidateToken(token))
        //    {
        //        return Unauthorized();
        //    }
        //    var results = await _apiServices.SendReportPostAsync<BangCanDoiThuChiRequest, BangCanDoiThuChiResponse>(request);
        //    if (results.StatusCode == 401 || string.IsNullOrEmpty(token))
        //    {
        //        return Unauthorized();
        //    }

        //    var host = _configuration.GetSection("AppSettings:WebBase").Value.ToString();
        //    var options = new ConvertOptions { };
        //    // options.set
        //    _generatePdf.SetConvertOptions(options);

        //    results.Host = host;

        //    var pdf = await _generatePdf.GetByteArray("/Reports/BangCanDoiThuChi.cshtml", results);
        //    var pdfStream = new System.IO.MemoryStream();
        //    pdfStream.Write(pdf, 0, pdf.Length);
        //    pdfStream.Position = 0;

        //    return new FileStreamResult(pdfStream, "application/pdf");
        //}


        //[HttpGet]
        //[Route(ApiBaoCaoPath.SoPhaiThu)]
        //public async Task<IActionResult> SoPhaiThu(string tungay, string denngay, string donvi)
        //{
        //    DateTime td1 = DateTime.Now;
        //    DateTime td2 = DateTime.Now;
        //    DateTime.TryParse(tungay, out td1);
        //    DateTime.TryParse(denngay, out td2);
        //    int dv = string.IsNullOrEmpty(donvi) ? ((User)_memoryCache.Get("_User_Login")).DMDonViSuDungId : int.Parse(donvi);
        //    var request = new SoPhaiThuRequest
        //    {
        //        DMDonViSuDungId = dv,
        //        TuNgay = new DateTime(td1.Year, td1.Month, td1.Day),
        //        DenNgay = new DateTime(td2.Year, td2.Month, td2.Day),
        //    };
        //    string token = (string)_memoryCache.Get("_Key_Token");
        //    if (!ValidateToken(token))
        //    {
        //        return Unauthorized();
        //    }
        //    var results = await _apiServices.SendReportPostAsync<SoPhaiThuRequest, SoPhaiThuResponse>(request);
        //    if (results.StatusCode == 401 || string.IsNullOrEmpty(token))
        //    {
        //        return Unauthorized();
        //    }

        //    var host = _configuration.GetSection("AppSettings:WebBase").Value.ToString();
        //    var options = new ConvertOptions { };
        //    // options.set
        //    _generatePdf.SetConvertOptions(options);

        //    results.Host = host;

        //    var pdf = await _generatePdf.GetByteArray("/Reports/SoPhaiThu.cshtml", results);
        //    var pdfStream = new System.IO.MemoryStream();
        //    pdfStream.Write(pdf, 0, pdf.Length);
        //    pdfStream.Position = 0;

        //    return new FileStreamResult(pdfStream, "application/pdf");
        //}

        //[HttpGet]
        //[Route(ApiBaoCaoPath.SoPhaiTra)]
        //public async Task<IActionResult> SoPhaiTra(string tungay, string denngay, string donvi)
        //{
        //    DateTime td1 = DateTime.Now;
        //    DateTime td2 = DateTime.Now;
        //    DateTime.TryParse(tungay, out td1);
        //    DateTime.TryParse(denngay, out td2);
        //    int dv = string.IsNullOrEmpty(donvi) ? ((User)_memoryCache.Get("_User_Login")).DMDonViSuDungId : int.Parse(donvi);
        //    var request = new SoPhaiTraRequest
        //    {
        //        DMDonViSuDungId = dv,
        //        TuNgay = new DateTime(td1.Year, td1.Month, td1.Day),
        //        DenNgay = new DateTime(td2.Year, td2.Month, td2.Day),
        //    };
        //    string token = (string)_memoryCache.Get("_Key_Token");
        //    if (!ValidateToken(token))
        //    {
        //        return Unauthorized();
        //    }
        //    var results = await _apiServices.SendReportPostAsync<SoPhaiTraRequest, SoPhaiTraResponse>(request);
        //    if (results.StatusCode == 401 || string.IsNullOrEmpty(token))
        //    {
        //        return Unauthorized();
        //    }

        //    var host = _configuration.GetSection("AppSettings:WebBase").Value.ToString();
        //    var options = new ConvertOptions { };
        //    // options.set
        //    _generatePdf.SetConvertOptions(options);

        //    results.Host = host;

        //    var pdf = await _generatePdf.GetByteArray("/Reports/SoPhaiTra.cshtml", results);
        //    var pdfStream = new System.IO.MemoryStream();
        //    pdfStream.Write(pdf, 0, pdf.Length);
        //    pdfStream.Position = 0;

        //    return new FileStreamResult(pdfStream, "application/pdf");
        //}

        //[HttpGet]
        //[Route(ApiBaoCaoPath.SoPhaiThuTongHop)]
        //public async Task<IActionResult> SoPhaiThuTongHop(string tungay, string denngay, string donvi)
        //{
        //    DateTime td1 = DateTime.Now;
        //    DateTime td2 = DateTime.Now;
        //    DateTime.TryParse(tungay, out td1);
        //    DateTime.TryParse(denngay, out td2);
        //    int dv = string.IsNullOrEmpty(donvi) ? ((User)_memoryCache.Get("_User_Login")).DMDonViSuDungId : int.Parse(donvi);
        //    var request = new SoPhaiThuTongHopRequest
        //    {
        //        DMDonViSuDungId = dv,
        //        TuNgay = new DateTime(td1.Year, td1.Month, td1.Day),
        //        DenNgay = new DateTime(td2.Year, td2.Month, td2.Day),
        //    };
        //    string token = (string)_memoryCache.Get("_Key_Token");
        //    if (!ValidateToken(token))
        //    {
        //        return Unauthorized();
        //    }
        //    var results = await _apiServices.SendReportPostAsync<SoPhaiThuTongHopRequest, SoPhaiThuTongHopResponse>(request);
        //    if (results.StatusCode == 401 || string.IsNullOrEmpty(token))
        //    {
        //        return Unauthorized();
        //    }

        //    var host = _configuration.GetSection("AppSettings:WebBase").Value.ToString();
        //    var options = new ConvertOptions { };
        //    // options.set
        //    _generatePdf.SetConvertOptions(options);

        //    results.Host = host;

        //    var pdf = await _generatePdf.GetByteArray("/Reports/SoPhaiThuTongHop.cshtml", results);
        //    var pdfStream = new System.IO.MemoryStream();
        //    pdfStream.Write(pdf, 0, pdf.Length);
        //    pdfStream.Position = 0;

        //    return new FileStreamResult(pdfStream, "application/pdf");
        //}
        //[HttpGet]
        //[Route(ApiBaoCaoPath.SoPhaiTraTongHop)]
        //public async Task<IActionResult> SoPhaiTraTongHop(string tungay, string denngay, string donvi)
        //{
        //    DateTime td1 = DateTime.Now;
        //    DateTime td2 = DateTime.Now;
        //    DateTime.TryParse(tungay, out td1);
        //    DateTime.TryParse(denngay, out td2);
        //    int dv = string.IsNullOrEmpty(donvi) ? ((User)_memoryCache.Get("_User_Login")).DMDonViSuDungId : int.Parse(donvi);
        //    var request = new SoPhaiTraTongHopRequest
        //    {
        //        DMDonViSuDungId = dv,
        //        TuNgay = new DateTime(td1.Year, td1.Month, td1.Day),
        //        DenNgay = new DateTime(td2.Year, td2.Month, td2.Day),
        //    };
        //    string token = (string)_memoryCache.Get("_Key_Token");
        //    if (!ValidateToken(token))
        //    {
        //        return Unauthorized();
        //    }
        //    var results = await _apiServices.SendReportPostAsync<SoPhaiTraTongHopRequest, SoPhaiTraTongHopResponse>(request);
        //    if (results.StatusCode == 401 || string.IsNullOrEmpty(token))
        //    {
        //        return Unauthorized();
        //    }

        //    var host = _configuration.GetSection("AppSettings:WebBase").Value.ToString();
        //    var options = new ConvertOptions { };
        //    // options.set
        //    _generatePdf.SetConvertOptions(options);

        //    results.Host = host;

        //    var pdf = await _generatePdf.GetByteArray("/Reports/SoPhaiTraTongHop.cshtml", results);
        //    var pdfStream = new System.IO.MemoryStream();
        //    pdfStream.Write(pdf, 0, pdf.Length);
        //    pdfStream.Position = 0;

        //    return new FileStreamResult(pdfStream, "application/pdf");
        //}

        //[HttpGet]
        //[Route(ApiBaoCaoPath.SoChiTietHangHoa)]
        //public async Task<IActionResult> SoChiTietHangHoa(string tungay, string denngay, string donvi)
        //{
        //    DateTime td1 = DateTime.Now;
        //    DateTime td2 = DateTime.Now;
        //    DateTime.TryParse(tungay, out td1);
        //    DateTime.TryParse(denngay, out td2);
        //    int dv = string.IsNullOrEmpty(donvi) ? ((User)_memoryCache.Get("_User_Login")).DMDonViSuDungId : int.Parse(donvi);
        //    var request = new SoChiTietHangHoaRequest
        //    {
        //        DMDonViSuDungId = dv,
        //        TuNgay = new DateTime(td1.Year, td1.Month, td1.Day),
        //        DenNgay = new DateTime(td2.Year, td2.Month, td2.Day),
        //    };
        //    string token = (string)_memoryCache.Get("_Key_Token");
        //    if (!ValidateToken(token))
        //    {
        //        return Unauthorized();
        //    }
        //    var results = await _apiServices.SendReportPostAsync<SoChiTietHangHoaRequest, SoChiTietHangHoaResponse>(request);
        //    if (results.StatusCode == 401 || string.IsNullOrEmpty(token))
        //    {
        //        return Unauthorized();
        //    }

        //    var host = _configuration.GetSection("AppSettings:WebBase").Value.ToString();
        //    var options = new ConvertOptions
        //    {
        //        PageOrientation = Wkhtmltopdf.NetCore.Options.Orientation.Landscape
        //    };
        //    // options.set
        //    _generatePdf.SetConvertOptions(options);

        //    results.Host = host;

        //    var pdf = await _generatePdf.GetByteArray("/Reports/SoChiTietHangHoa.cshtml", results);
        //    var pdfStream = new System.IO.MemoryStream();
        //    pdfStream.Write(pdf, 0, pdf.Length);
        //    pdfStream.Position = 0;

        //    return new FileStreamResult(pdfStream, "application/pdf");
        //}
        //[HttpGet]
        //[Route(ApiBaoCaoPath.SoTongHopHangHoa)]
        //public async Task<IActionResult> SoTongHopHangHoa(string tungay, string denngay, string donvi)
        //{
        //    DateTime td1 = DateTime.Now;
        //    DateTime td2 = DateTime.Now;
        //    DateTime.TryParse(tungay, out td1);
        //    DateTime.TryParse(denngay, out td2);
        //    int dv = string.IsNullOrEmpty(donvi) ? ((User)_memoryCache.Get("_User_Login")).DMDonViSuDungId : int.Parse(donvi);
        //    var request = new SoTongHopHangHoaRequest
        //    {
        //        DMDonViSuDungId = dv,
        //        TuNgay = new DateTime(td1.Year, td1.Month, td1.Day),
        //        DenNgay = new DateTime(td2.Year, td2.Month, td2.Day),
        //    };
        //    string token = (string)_memoryCache.Get("_Key_Token");
        //    if (!ValidateToken(token))
        //    {
        //        return Unauthorized();
        //    }
        //    var results = await _apiServices.SendReportPostAsync<SoTongHopHangHoaRequest, SoTongHopHangHoaResponse>(request);
        //    if (results.StatusCode == 401 || string.IsNullOrEmpty(token))
        //    {
        //        return Unauthorized();
        //    }

        //    var host = _configuration.GetSection("AppSettings:WebBase").Value.ToString();
        //    var options = new ConvertOptions { };
        //    // options.set
        //    _generatePdf.SetConvertOptions(options);

        //    results.Host = host;

        //    var pdf = await _generatePdf.GetByteArray("/Reports/SoTongHopHangHoa.cshtml", results);
        //    var pdfStream = new System.IO.MemoryStream();
        //    pdfStream.Write(pdf, 0, pdf.Length);
        //    pdfStream.Position = 0;

        //    return new FileStreamResult(pdfStream, "application/pdf");
        //}
        //[HttpGet]
        //[Route(ApiBaoCaoPath.TongHopNhapXuatTon)]
        //public async Task<IActionResult> TongHopNhapXuatTon(string tungay, string denngay, string donvi)
        //{
        //    DateTime td1 = DateTime.Now;
        //    DateTime td2 = DateTime.Now;
        //    DateTime.TryParse(tungay, out td1);
        //    DateTime.TryParse(denngay, out td2);
        //    int dv = string.IsNullOrEmpty(donvi) ? ((User)_memoryCache.Get("_User_Login")).DMDonViSuDungId : int.Parse(donvi);
        //    var request = new SoTongHopHangHoaRequest
        //    {
        //        DMDonViSuDungId = dv,
        //        TuNgay = new DateTime(td1.Year, td1.Month, td1.Day),
        //        DenNgay = new DateTime(td2.Year, td2.Month, td2.Day),
        //    };
        //    string token = (string)_memoryCache.Get("_Key_Token");
        //    if (!ValidateToken(token))
        //    {
        //        return Unauthorized();
        //    }
        //    var results = await _apiServices.SendReportPostAsync<SoTongHopHangHoaRequest, SoTongHopHangHoaResponse>(request);
        //    if (results.StatusCode == 401 || string.IsNullOrEmpty(token))
        //    {
        //        return Unauthorized();
        //    }

        //    var host = _configuration.GetSection("AppSettings:WebBase").Value.ToString();
        //    var options = new ConvertOptions { };
        //    // options.set
        //    _generatePdf.SetConvertOptions(options);

        //    results.Host = host;

        //    var pdf = await _generatePdf.GetByteArray("/Reports/TongHopNhapXuatTon.cshtml", results);
        //    var pdfStream = new System.IO.MemoryStream();
        //    pdfStream.Write(pdf, 0, pdf.Length);
        //    pdfStream.Position = 0;

        //    return new FileStreamResult(pdfStream, "application/pdf");
        //}
        public bool ValidateToken(string token)
        {
            if (token == null)
                return false;
            var jwtKey = _configuration.GetSection("JWTSettings:SecretKey");
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(jwtKey.Value.ToString());
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.FromDays(1)
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var userId = int.Parse(jwtToken.Claims.First(x => x.Type == "UserId")?.Value);

                // return user id from JWT token if validation successful
                return userId == null ? false : (userId > 0 ? true : false);
            }
            catch
            {
                // return null if validation fails
                return false;
            }
        }

    }


}

