using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using QLSX.Shared.Models;
using System.Data;
using QLSX.Shared.Models.Request;

namespace QLSX.Web.Services
{
    interface IThongKeService
    {
        Task<List<ThongKeDoanhThu>> ThongKeDoanhThuAsync(int nam);
        Task<List<ThongKeDoanhThu>> ThongKeThuTienAsync(int nam);
        Task<List<ThongKeDoanhThuTheoNV>> ThongKeDoanhThuTheoNhanVienAsync(int nam);
        Task<List<ThongKeLoaiTien>> ThongKeLoaiTienAsync(DateTime toDate);
        Task<List<TongHopDongTienModel>> TongHopDongTienAsync(DateTime fromDate, DateTime toDate);

    }

    public class ThongKeService : IThongKeService
    {
        public HttpClient _httpClient { get; }
        public AppSettings _appSettings { get; }
        public ILocalStorageService _localStorageService { get; }
        private string requestUri = "thongke";
        public AppService _appService { get; }
        public ThongKeService(HttpClient httpClient
            , IOptions<AppSettings> appSettings
            , ILocalStorageService localStorageService,
            AppService appService)
        {
            _appSettings = appSettings.Value;
            _localStorageService = localStorageService;

            httpClient.BaseAddress = new Uri(_appSettings.ApiBase);
            httpClient.DefaultRequestHeaders.Add(QLSX.Shared.Contansts.RequestHeaders.XTenantId, appService.DMDonViSuDungId.ToString());
            httpClient.DefaultRequestHeaders.Add(QLSX.Shared.Contansts.RequestHeaders.XUserId, appService.UserId.ToString());
            _appService = appService;
            _httpClient = httpClient;
        }

        public async Task<List<ThongKeDoanhThu>> ThongKeDoanhThuAsync(int nam)
        {
            SearchRequest request = new SearchRequest() { DMDonViSuDungId = _appService.DMDonViSuDungId, Id = nam };
            string serializedString = JsonConvert.SerializeObject(request);

            var requestMessage = new HttpRequestMessage(HttpMethod.Get, requestUri + "/thongkedoanhthuthang/" + nam);

            var token = await _localStorageService.GetItemAsync<string>("accessToken");
            requestMessage.Headers.Authorization
                = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            requestMessage.Content = new StringContent(serializedString);
            requestMessage.Content.Headers.ContentType
             = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var response = await _httpClient.SendAsync(requestMessage);

            var responseStatusCode = response.StatusCode;

            if (responseStatusCode.ToString() == "OK")
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                return await Task.FromResult(JsonConvert.DeserializeObject<List<ThongKeDoanhThu>>(responseBody));
            }
            else
                return new();
        }
        public async Task<List<ThongKeDoanhThu>> ThongKeThuTienAsync(int nam)
        {
            SearchRequest request = new SearchRequest() { DMDonViSuDungId = _appService.DMDonViSuDungId, Id = nam };
            string serializedString = JsonConvert.SerializeObject(request);

            var requestMessage = new HttpRequestMessage(HttpMethod.Get, requestUri + "/thongkethutien/" + nam);

            var token = await _localStorageService.GetItemAsync<string>("accessToken");
            requestMessage.Headers.Authorization
                = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            requestMessage.Content = new StringContent(serializedString);
            requestMessage.Content.Headers.ContentType
             = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var response = await _httpClient.SendAsync(requestMessage);

            var responseStatusCode = response.StatusCode;

            if (responseStatusCode.ToString() == "OK")
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                return await Task.FromResult(JsonConvert.DeserializeObject<List<ThongKeDoanhThu>>(responseBody));
            }
            else
                return new();
        }
        public async Task<List<ThongKeDoanhThuTheoNV>> ThongKeDoanhThuTheoNhanVienAsync(int nam)
        {
            SearchRequest request = new SearchRequest() { DMDonViSuDungId = _appService.DMDonViSuDungId, Id = nam };
            string serializedString = JsonConvert.SerializeObject(request);

            var requestMessage = new HttpRequestMessage(HttpMethod.Get, requestUri + "/ThongKeDoanhThuTheoNV/" + nam);

            var token = await _localStorageService.GetItemAsync<string>("accessToken");
            requestMessage.Headers.Authorization
                = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            requestMessage.Content = new StringContent(serializedString);
            requestMessage.Content.Headers.ContentType
             = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var response = await _httpClient.SendAsync(requestMessage);

            var responseStatusCode = response.StatusCode;

            if (responseStatusCode.ToString() == "OK")
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                return await Task.FromResult(JsonConvert.DeserializeObject<List<ThongKeDoanhThuTheoNV>>(responseBody));
            }
            else
                return new();
        }
        public async Task<List<ThongKeLoaiTien>> ThongKeLoaiTienAsync(DateTime toNgay)
        {
            SearchRequest request = new SearchRequest() { DMDonViSuDungId = _appService.DMDonViSuDungId };
            string serializedString = JsonConvert.SerializeObject(request);

            var requestMessage = new HttpRequestMessage(HttpMethod.Get, requestUri + "/Thongkeloaitien/" + String.Format("{0:yyyyMMdd}", toNgay));

            var token = await _localStorageService.GetItemAsync<string>("accessToken");
            requestMessage.Headers.Authorization
                = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            requestMessage.Content = new StringContent(serializedString);
            requestMessage.Content.Headers.ContentType
             = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var response = await _httpClient.SendAsync(requestMessage);

            var responseStatusCode = response.StatusCode;

            if (responseStatusCode.ToString() == "OK")
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                return await Task.FromResult(JsonConvert.DeserializeObject<List<ThongKeLoaiTien>>(responseBody));
            }
            else
                return new();
        }
        public async Task<List<TongHopDongTienModel>> TongHopDongTienAsync(DateTime fromDate, DateTime toDate)
        {
            TongHopDongTienRequest request = new TongHopDongTienRequest() 
            {
                date1 = String.Format("{0:MM/dd/yyyy}", fromDate),
                date2 = String.Format("{0:MM/dd/yyyy}", toDate),
            };
            string serializedString = JsonConvert.SerializeObject(request);

            var requestMessage = new HttpRequestMessage(HttpMethod.Get, requestUri + "/tonghopdongtien");

            var token = await _localStorageService.GetItemAsync<string>("accessToken");
            requestMessage.Headers.Authorization
                = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            requestMessage.Content = new StringContent(serializedString);
            requestMessage.Content.Headers.ContentType
             = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var response = await _httpClient.SendAsync(requestMessage);

            var responseStatusCode = response.StatusCode;

            if (responseStatusCode.ToString() == "OK")
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                return await Task.FromResult(JsonConvert.DeserializeObject<List<TongHopDongTienModel>>(responseBody));
            }
            else
                return new();
        }
    }
}
