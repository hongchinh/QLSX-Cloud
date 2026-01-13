using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using QLSX.Web.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using QLSX.Shared.Models;
using QLSX.Shared.Interfaces;
using System.IO;
using System.Linq.Expressions;
using System.Drawing;
using System.Drawing.Imaging;
using QLSX.Shared.Models.Request;

namespace QLSX.Web.Services
{
    interface IDMHangHoaService<T>
    {
        Task<List<T>> GetAllAsync();
        Task<T> GetByIdAsync(int Id);
        Task<DanhMucHangHoaModel> GetByCodeAsync(string code);
        Task<T> CreateAsync(T obj);
        Task<T> UpdateAsync(int Id, T obj);
        Task<bool> DeleteAsync(int Id);
        Task<T> GetLoaiGiaByCodeAsync(List<string> codes);

        Task<GetAllResponsePaged<DanhMucHangHoaModel>> ExportToExcelAsync(QLSX.Shared.Models.HangHoaSearchRequest request);
        Task<GetAllResponsePaged<DanhMucHangHoaModel>> GetAllPagedAsync(QLSX.Shared.Models.HangHoaSearchRequest request);
        Task<GetAllResponsePaged<DanhMucHangHoaModel>> GetAllPagedDialogAsync(QLSX.Shared.Models.HangHoaSearchRequest request);
        Task<bool> UpdateListPrintAsync(List<string> request);
        Task<Stream> GetImage();

        Task<double> GetSoDuHangHoaByCodeAsync(QLSX.Shared.Models.GetSoDuHangHoaRequest request);
        Task<Dictionary<string, double>> GetSoDuHangHoaBatchAsync(QLSX.Shared.Models.GetSoDuHangHoaBatchRequest request);

    }

    public class DMHangHoaService<T> : IDMHangHoaService<T>
    {
        public HttpClient _httpClient { get; }
        private readonly IApiWrapperServices _apiServices;

        public AppService _appService { get; }
        private readonly HttpInterceptorService _interceptor;
        public AppSettings _appSettings { get; }
        public ILocalStorageService _localStorageService { get; }
        string requestUri = "DmHangHoas";
        public DMHangHoaService(HttpClient httpClient
            , IOptions<AppSettings> appSettings
            , ILocalStorageService localStorageService
            , HttpInterceptorService interceptor,
            IApiWrapperServices apiServices,
            AppService appService)
        {
            _appSettings = appSettings.Value;
            _localStorageService = localStorageService;

            httpClient.BaseAddress = new Uri(_appSettings.ApiBase);
            httpClient.DefaultRequestHeaders.Add("User-Agent", "BlazorServer");
            httpClient.DefaultRequestHeaders.Add(QLSX.Shared.Contansts.RequestHeaders.XTenantId, appService.DMDonViSuDungId.ToString());
            httpClient.DefaultRequestHeaders.Add(QLSX.Shared.Contansts.RequestHeaders.XUserId, appService.UserId.ToString());
            _httpClient = httpClient;
            _appService = appService;
            _interceptor = interceptor;
            _apiServices = apiServices;
        }

        public async Task<bool> DeleteAsync(int Id)
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Delete, requestUri + "/delete/" + Id);

            var token = await _localStorageService.GetItemAsync<string>("accessToken");
            requestMessage.Headers.Authorization
                = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            _interceptor.MonitorEvent();
            var response = await _httpClient.SendAsync(requestMessage);

            var responseStatusCode = response.StatusCode;

            return await Task.FromResult(true);
        }

        public async Task<List<T>> GetAllAsync()
        {
            SearchRequest request = new SearchRequest() { DMDonViSuDungId = _appService.DMDonViSuDungId };

            string serializedString = JsonConvert.SerializeObject(request);

            var requestMessage = new HttpRequestMessage(HttpMethod.Get, requestUri);

            var token = await _localStorageService.GetItemAsync<string>("accessToken");
            requestMessage.Headers.Authorization
                = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            requestMessage.Content = new StringContent(serializedString);
            requestMessage.Content.Headers.ContentType
             = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            _interceptor.MonitorEvent();

            var response = await _httpClient.SendAsync(requestMessage);

            var responseStatusCode = response.StatusCode;

            if (responseStatusCode.ToString() == "OK")
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                return await Task.FromResult(JsonConvert.DeserializeObject<List<T>>(responseBody));
            }
            else
                return new();
        }

        public async Task<T> GetByIdAsync(int Id)
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, requestUri + "/" + Id);

            var token = await _localStorageService.GetItemAsync<string>("accessToken");
            requestMessage.Headers.Authorization
                = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            _interceptor.MonitorEvent();

            var response = await _httpClient.SendAsync(requestMessage);

            var responseStatusCode = response.StatusCode;
            var responseBody = await response.Content.ReadAsStringAsync();

            return await Task.FromResult(JsonConvert.DeserializeObject<T>(responseBody));
        }
        public async Task<DanhMucHangHoaModel> GetByCodeAsync(string code)
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, requestUri + "/getCode/" + code);

            var token = await _localStorageService.GetItemAsync<string>("accessToken");
            requestMessage.Headers.Authorization
                = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            _interceptor.MonitorEvent();

            var response = await _httpClient.SendAsync(requestMessage);

            var responseStatusCode = response.StatusCode;
            var responseBody = await response.Content.ReadAsStringAsync();
            if (responseStatusCode.ToString() == "OK")
            {
                return await Task.FromResult(JsonConvert.DeserializeObject<DanhMucHangHoaModel>(responseBody));
            }
            else
                return new();
        }
        public async Task<T> GetLoaiGiaByCodeAsync(List<string> codes)
        {
            string serializedUser = JsonConvert.SerializeObject(codes);

            var requestMessage = new HttpRequestMessage(HttpMethod.Get, requestUri + "/getLoaiGiaByCodes");

            var token = await _localStorageService.GetItemAsync<string>("accessToken");
            requestMessage.Headers.Authorization
                = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            requestMessage.Content = new StringContent(serializedUser);
            requestMessage.Content.Headers.ContentType
               = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            _interceptor.MonitorEvent();

            var response = await _httpClient.SendAsync(requestMessage);

            var responseStatusCode = response.StatusCode;
            var responseBody = await response.Content.ReadAsStringAsync();

            return await Task.FromResult(JsonConvert.DeserializeObject<T>(responseBody));
        }
        public async Task<T> CreateAsync(T obj)
        {
            string serializedUser = JsonConvert.SerializeObject(obj);

            var requestMessage = new HttpRequestMessage(HttpMethod.Post, requestUri + "/create");

            var token = await _localStorageService.GetItemAsync<string>("accessToken");
            requestMessage.Headers.Authorization
                = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            requestMessage.Content = new StringContent(serializedUser);

            requestMessage.Content.Headers.ContentType
                = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            _interceptor.MonitorEvent();
            var response = await _httpClient.SendAsync(requestMessage);

            var responseStatusCode = response.StatusCode;
            var responseBody = await response.Content.ReadAsStringAsync();

            var returnedObj = JsonConvert.DeserializeObject<T>(responseBody);

            return await Task.FromResult(returnedObj);
        }

        public async Task<T> UpdateAsync(int Id, T obj)
        {
            string serializedUser = JsonConvert.SerializeObject(obj);

            var requestMessage = new HttpRequestMessage(HttpMethod.Put, requestUri + "/update/" + Id);
            var token = await _localStorageService.GetItemAsync<string>("accessToken");
            requestMessage.Headers.Authorization
                = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            requestMessage.Content = new StringContent(serializedUser);

            requestMessage.Content.Headers.ContentType
                = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            _interceptor.MonitorEvent();
            var response = await _httpClient.SendAsync(requestMessage);

            var responseStatusCode = response.StatusCode;
            var responseBody = await response.Content.ReadAsStringAsync();

            var returnedObj = JsonConvert.DeserializeObject<T>(responseBody);

            return await Task.FromResult(returnedObj);
        }

        public async Task<GetAllResponsePaged<DanhMucHangHoaModel>> GetAllPagedAsync(QLSX.Shared.Models.HangHoaSearchRequest request)
        {
            request.DMDonViSuDungId = _appService.DMDonViSuDungId;
            string serializedUser = JsonConvert.SerializeObject(request);

            var requestMessage = new HttpRequestMessage(HttpMethod.Get, requestUri + "/GetAllPaged/");

            var token = await _localStorageService.GetItemAsync<string>("accessToken");

            requestMessage.Headers.Authorization
                = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            requestMessage.Content = new StringContent(serializedUser);
            requestMessage.Content.Headers.ContentType
               = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            _interceptor.MonitorEvent();
            var response = await _httpClient.SendAsync(requestMessage);

            var responseStatusCode = response.StatusCode;

            if (responseStatusCode.ToString() == "OK")
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                return await Task.FromResult(JsonConvert.DeserializeObject<GetAllResponsePaged<DanhMucHangHoaModel>>(responseBody));
            }
            else
                return new();
        }
        public async Task<GetAllResponsePaged<DanhMucHangHoaModel>> ExportToExcelAsync(QLSX.Shared.Models.HangHoaSearchRequest request)
        {
            request.DMDonViSuDungId = _appService.DMDonViSuDungId;
            string serializedUser = JsonConvert.SerializeObject(request);

            var requestMessage = new HttpRequestMessage(HttpMethod.Get, requestUri + "/ExportToExcel/");

            var token = await _localStorageService.GetItemAsync<string>("accessToken");

            requestMessage.Headers.Authorization
                = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            requestMessage.Content = new StringContent(serializedUser);
            requestMessage.Content.Headers.ContentType
               = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            _interceptor.MonitorEvent();
            var response = await _httpClient.SendAsync(requestMessage);

            var responseStatusCode = response.StatusCode;

            if (responseStatusCode.ToString() == "OK")
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                return await Task.FromResult(JsonConvert.DeserializeObject<GetAllResponsePaged<DanhMucHangHoaModel>>(responseBody));
            }
            else
                return new();
        }

        public async Task<GetAllResponsePaged<DanhMucHangHoaModel>> GetAllPagedDialogAsync(QLSX.Shared.Models.HangHoaSearchRequest request)
        {
            request.DMDonViSuDungId = _appService.DMDonViSuDungId;
            string serializedUser = JsonConvert.SerializeObject(request);

            var requestMessage = new HttpRequestMessage(HttpMethod.Get, requestUri + "/GetAllPagedDialog/");

            var token = await _localStorageService.GetItemAsync<string>("accessToken");

            requestMessage.Headers.Authorization
                = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            requestMessage.Content = new StringContent(serializedUser);
            requestMessage.Content.Headers.ContentType
               = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            _interceptor.MonitorEvent();
            var response = await _httpClient.SendAsync(requestMessage);

            var responseStatusCode = response.StatusCode;

            if (responseStatusCode.ToString() == "OK")
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                return await Task.FromResult(JsonConvert.DeserializeObject<GetAllResponsePaged<DanhMucHangHoaModel>>(responseBody));
            }
            else
                return new();
        }

        public async Task<bool> UpdateListPrintAsync(List<string> request)
        {

            string serializedUser = JsonConvert.SerializeObject(request);

            var requestMessage = new HttpRequestMessage(HttpMethod.Post, "BaoCaos/UpdateListPrint/HangHoa");

            var token = await _localStorageService.GetItemAsync<string>("accessToken");

            requestMessage.Headers.Authorization
                = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            requestMessage.Content = new StringContent(serializedUser);
            requestMessage.Content.Headers.ContentType
               = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            _interceptor.MonitorEvent();
            var response = await _httpClient.SendAsync(requestMessage);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return true;
            }
            else
                return false;
        }
        public async Task<Stream> GetImage()
        {
            try
            {
                var requestMessage = new HttpRequestMessage(HttpMethod.Get, "https://img.vietqr.io/image/vietinbank-666666666666-qr_only.png?amount=20000000&addInfo=AX1001&accountName=QUY%20VAC%20XIN%20PHONG%20CHONG%20COVID%2019");

                var response = await _httpClient.SendAsync(requestMessage);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var responseBody = response.Content.ReadAsStream();
                    Image image = System.Drawing.Image.FromStream(responseBody);
                    image.Save("D:\\Projects\\Sale\\sale-web\\QLSX.Web\\wwwroot\\Image_QRCode.jpg", ImageFormat.Jpeg);
                    return responseBody;

                }
                else
                    return null;
            }
            catch (Exception)
            {

                throw;
            }


        }

        public async Task<double> GetSoDuHangHoaByCodeAsync(GetSoDuHangHoaRequest request)
        {
            request.DMDonViSuDungId = _appService.DMDonViSuDungId;
            string serializedUser = JsonConvert.SerializeObject(request);

            var requestMessage = new HttpRequestMessage(HttpMethod.Get, requestUri + "/GetSoDuHangHoaByCode");

            var token = await _localStorageService.GetItemAsync<string>("accessToken");

            requestMessage.Headers.Authorization
                = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            requestMessage.Content = new StringContent(serializedUser);
            requestMessage.Content.Headers.ContentType
               = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            _interceptor.MonitorEvent();
            var response = await _httpClient.SendAsync(requestMessage);

            var responseStatusCode = response.StatusCode;

            if (responseStatusCode.ToString() == "OK")
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                return await Task.FromResult(JsonConvert.DeserializeObject<double>(responseBody));
            }
            return 0;

        }

        public async Task<Dictionary<string, double>> GetSoDuHangHoaBatchAsync(GetSoDuHangHoaBatchRequest request)
        {
            request.DMDonViSuDungId = _appService.DMDonViSuDungId;
            string serializedUser = JsonConvert.SerializeObject(request);

            var requestMessage = new HttpRequestMessage(HttpMethod.Post, requestUri + "/GetSoDuHangHoaBatch");

            var token = await _localStorageService.GetItemAsync<string>("accessToken");

            requestMessage.Headers.Authorization
                = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            requestMessage.Content = new StringContent(serializedUser);
            requestMessage.Content.Headers.ContentType
               = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            _interceptor.MonitorEvent();
            var response = await _httpClient.SendAsync(requestMessage);

            var responseStatusCode = response.StatusCode;

            if (responseStatusCode.ToString() == "OK")
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<Dictionary<string, double>>(responseBody);
                return await Task.FromResult(result ?? new Dictionary<string, double>());
            }
            return new Dictionary<string, double>();

        }
    }
}
