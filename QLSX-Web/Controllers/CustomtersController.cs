using QLSX.Web.Data;
using QLSX.Web.Services;
using QLSX.Shared.Data.Requests.DMHangHoa;
using QLSX.Shared.Data.Requests.NhapXuat;
using QLSX.Shared.Data.Responses;
using QLSX.Shared.Data.Responses.DMHangHoa;
using QLSX.Shared.Data.Responses.NhapXuat;
using QLSX.Shared.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using QLSX.Shared.Models;

namespace QLSX.Web.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CustomtersController : Controller
    {
        private ICustomersService<Customer> _customersService;
        private readonly IMemoryCache _memoryCache;
        private readonly IApiWrapperServices _apiServices;
        public IConfiguration _configuration { get; }

        public CustomtersController(IMemoryCache memoryCache, IConfiguration configuration, IApiWrapperServices apiServices)
        {
            _configuration = configuration;
            _memoryCache = memoryCache;
            _apiServices = apiServices;
        }
        //[HttpGet]
        //[Route("inphieu/{id}")]
        //public async Task<IActionResult> InPhieuNhapXuat(int id)
        //{
        //    var request = new InPhieuNhapRequest
        //    {
        //        DMDonViSuDungId = 1,
        //        Id = id,
        //    };
        //    string token = (string)_memoryCache.Get("_Key_Token");
        //    if (!ValidateToken(token))
        //    {
        //        return Unauthorized();
        //    }
        //    var results = await _apiServices.SendReportPostAsync<InPhieuNhapRequest, InPhieuNhapXuatResponse>(request);
        //    if (results.StatusCode == 401 || string.IsNullOrEmpty(token))
        //    {
        //        return Unauthorized();
        //    }

        //    var host = _configuration.GetSection("AppSettings:WebBase").Value.ToString();
        //    var options = new ConvertOptions
        //    {
        //        HeaderHtml = host + "/header.html",
        //        FooterHtml = host + "/footer.html",
        //    };
        //    _generatePdf.SetConvertOptions(options);
        //    results.Host = host;
        //    var pdf = await _generatePdf.GetByteArray("Reports/InPhieuNhap.cshtml", results);
        //    var pdfStream = new System.IO.MemoryStream();
        //    pdfStream.Write(pdf, 0, pdf.Length);
        //    pdfStream.Position = 0;
            
        //    return new FileStreamResult(pdfStream, "application/pdf");
        //}
        [HttpPost]
        public JsonResult IsExitsCustomer(string Mobile1)
        {
            return Json(CheckExist(Mobile1));
        }

        public bool CheckExist(string Mobile1)
        {
            return true;
        }

        public bool ValidateToken(string token)
        {
            if (token == null)
                return false;
            var jwtSection = _configuration.GetSection("JWTSettings");
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("SecretKey@SecretKey");
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


    public class TestData
    {
        public string Text { get; set; }
        public int Number { get; set; }
    }
}
