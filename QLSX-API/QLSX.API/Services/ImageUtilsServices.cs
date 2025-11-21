
using Blazored.LocalStorage;
using FoolProof.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using QLSX.Shared.Models;
using SaleAPI.Interfaces;
using SaleAPI.Models;
using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace SaleAPI.Services
{
    public class ImageUtilsServices : IImageUtilsServices
    {
        private readonly CRMDBContext _context;
        private readonly INhatKyService _nhatKyService;
        private readonly ITenantProvider _tenantProvider;
        public HttpClient _httpClient { get; }
        public ImageUtilsServices(CRMDBContext context, INhatKyService nhatKyService, HttpClient httpClient, ITenantProvider tenantProvider)
        {
            _context = context;
            _nhatKyService = nhatKyService;
            _httpClient = httpClient;
            _tenantProvider = tenantProvider;
        }

        public async Task<ImageQRCode> CreateImageBarcode(int mdvid, int userid, string loai, int id, double sotien, string ghichu)
        {
            //compact2    540x640 Bao gồm: Mã QR, các logo , thông tin chuyển khoản
            //compact 540x540 QR kèm logo VietQR, Napas, ngân hàng
            //qr_only 480x480 Trả về ảnh QR đơn giản, chỉ bao gồm QR
            //print   600x776 Bao gồm: Mã QR, các logo và đầy đủ thông tin chuyển khoản
            var setting = await _context.SettingRepository.FindAsync(userid);
           
            var item = await _context.ImageQRCodes.Where(x => x.Loai== loai).Where(x => x.IdPhieu== id).Where(x => x.DMDonViSuDungId== mdvid).FirstOrDefaultAsync();
            if(item == null)
            {
                item = new ImageQRCode();
                item.CreatedDate = DateTime.Now;
                item.UpdatedDate = DateTime.Now;
                item.Loai = loai;
                item.IdPhieu = id;
                item.SoTien = sotien;
                item.GhiChu = ghichu;
                item.Width = 540;
                item.Hieght = 640;
                item.DMDonViSuDungId = mdvid;
                var strtmp = "{0}\\{1}-{2}-{3}.jpg?amount={4}&addInfo={5}&accountName={6}";
                string requestUri = string.Format(strtmp, setting.UrlQR ?? @"https://img.vietqr.io/image", setting.BankQR, setting.SoTaiKhoan, setting.TemplateQR, sotien, ghichu, setting.TenDonVi);
                // configuration.GetSection("AppSettings:QRCodeLink").Value;
                // "https://img.vietqr.io/image/vietinbank-113366668888-compact2.jpg?amount=790000&addInfo=dong%20gop%20quy%20vac%20xin&accountName=Quy%20Vac%20Xin%20Covid";
                var requestMessage = new HttpRequestMessage(HttpMethod.Get, requestUri);

                var response = await _httpClient.SendAsync(requestMessage);

                var responseStatusCode = response.StatusCode;
                if (responseStatusCode.ToString() == "OK")
                {
                    var responseBody = await response.Content.ReadAsByteArrayAsync();
                    item.Bytes = responseBody;
                }

                _context.ImageQRCodes.Add(item);
                await _context.SaveChangesAsync();
            }
            else
            {
               
                item.UpdatedDate = DateTime.Now;
                item.Loai = loai;
                item.IdPhieu = id;
                item.SoTien = sotien;
                item.GhiChu = ghichu;
                item.Width = 540;
                item.Hieght = 640;
                item.DMDonViSuDungId = mdvid;
                var strtmp = "{0}\\{1}-{2}-{3}.jpg?amount={4}&addInfo={5}&accountName={6}";
                string requestUri = string.Format(strtmp, setting.UrlQR ?? @"https://img.vietqr.io/image", setting.BankQR, setting.SoTaiKhoan, setting.TemplateQR, sotien, ghichu, setting.TenDonVi);
                // configuration.GetSection("AppSettings:QRCodeLink").Value;
                // "https://img.vietqr.io/image/vietinbank-113366668888-compact2.jpg?amount=790000&addInfo=dong%20gop%20quy%20vac%20xin&accountName=Quy%20Vac%20Xin%20Covid";
                var requestMessage = new HttpRequestMessage(HttpMethod.Get, requestUri);

                var response = await _httpClient.SendAsync(requestMessage);

                var responseStatusCode = response.StatusCode;
                if (responseStatusCode.ToString() == "OK")
                {
                    var responseBody = await response.Content.ReadAsByteArrayAsync();
                    item.Bytes = responseBody;
                }

                _context.Entry(item).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
           

            // Log Nhat ky
            await _nhatKyService.LogCreate("ImageQRCodes");
            return item;
        }
        public async Task<ImageQRCode> SaveImageBarcode(ImageQRCode item)
        {

            return item;
        }
        public async Task<ImageQRCode> GetImageBarcode(string loai, int idPhieu)
        {
            var item = await _context.ImageQRCodes.Where(x => x.Loai == loai && x.IdPhieu == idPhieu).FirstOrDefaultAsync();
            return item;
        }


    }
}
