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
using System.IO;
using QLSX.Shared;
using QLSX.Shared.Models.Request;

namespace QLSX.Web.Services
{
    interface IDMKhachHangService<T>
    {
        public Task<List<T>> GetAllAsync();
        public Task<T> GetByIdAsync(int Id);
        public Task<DanhMucKhachHangModel> GetByCodeAsync(string code);
        public Task<T> CreateAsync(T obj);
        public Task<T> UpdateAsync(int Id, T obj);
        public Task<bool> DeleteAsync(int Id);
        public Task<T> GetLoaiGiaByCodeAsync(List<string> codes);
        public Task<byte[]> GetDataReportAsync();
        public Task<string> GetPathPDFReportAsync();
        public Task<string> GetPathExcelReportAsync();
        public Task<GetAllResponsePaged<DanhMucKhachHangModel>> ExportToExcelAsync(QLSX.Shared.Models.KhachHangSearchRequest request);
        public Task<GetAllResponsePaged<DanhMucKhachHangModel>> GetAllPagedAsync(QLSX.Shared.Models.KhachHangSearchRequest request);
        public Task<GetAllResponsePaged<DanhMucKhachHangModel>> GetAllPagedDialogAsync(QLSX.Shared.Models.KhachHangSearchRequest request);

        public Task<double> GetSoDuCongNoById(GetSoDuCongNoRequest request);
        Task<bool> UpdateListPrintAsync(List<int> request);
    }

    public class DMKhachHangService<T> : IDMKhachHangService<T>
    {

        private string _apiUrl;

        public HttpClient _httpClient { get; }
        public AppSettings _appSettings { get; }
        public AppService _appService { get; }
        public ILocalStorageService _localStorageService { get; }
        string requestUri = "DMKhachHangs";
        public DMKhachHangService(HttpClient httpClient
            , IOptions<AppSettings> appSettings
            , ILocalStorageService localStorageService,
             BaseUrlConfiguration baseUrlConfiguration,
             AppService appService)
        {
            _appSettings = appSettings.Value;
            _localStorageService = localStorageService;
            _appService = appService;
            httpClient.BaseAddress = new Uri(_appSettings.ApiBase);
            httpClient.DefaultRequestHeaders.Add("User-Agent", "BlazorServer");
            httpClient.DefaultRequestHeaders.Add(QLSX.Shared.Contansts.RequestHeaders.XTenantId, appService.DMDonViSuDungId.ToString());
            httpClient.DefaultRequestHeaders.Add(QLSX.Shared.Contansts.RequestHeaders.XUserId, appService.UserId.ToString());
            _apiUrl = baseUrlConfiguration.ApiBase;
            _httpClient = httpClient;
        }

        public async Task<bool> DeleteAsync(int Id)
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Delete, requestUri + "/delete/" + Id);

            var token = await _localStorageService.GetItemAsync<string>("accessToken");
            requestMessage.Headers.Authorization
                = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

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

            var response = await _httpClient.SendAsync(requestMessage);

            var responseStatusCode = response.StatusCode;
            var responseBody = await response.Content.ReadAsStringAsync();

            return await Task.FromResult(JsonConvert.DeserializeObject<T>(responseBody));
        }

        public async Task<DanhMucKhachHangModel> GetByCodeAsync(string code)
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, requestUri + "/getCode/" + code);

            var token = await _localStorageService.GetItemAsync<string>("accessToken");
            requestMessage.Headers.Authorization
                = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.SendAsync(requestMessage);

            var responseStatusCode = response.StatusCode;
            var responseBody = await response.Content.ReadAsStringAsync();
            try
            {
                return await Task.FromResult(JsonConvert.DeserializeObject<DanhMucKhachHangModel>(responseBody));
            }
            catch (Exception)
            {
                return new();
            }
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

            var response = await _httpClient.SendAsync(requestMessage);

            var responseStatusCode = response.StatusCode;
            var responseBody = await response.Content.ReadAsStringAsync();

            var returnedObj = JsonConvert.DeserializeObject<T>(responseBody);

            return await Task.FromResult(returnedObj);
        }

        public async Task<GetAllResponsePaged<DanhMucKhachHangModel>> GetAllPagedAsync(QLSX.Shared.Models.KhachHangSearchRequest request)
        {
            string serializedUser = JsonConvert.SerializeObject(request);

            var requestMessage = new HttpRequestMessage(HttpMethod.Get, requestUri + "/GetAllPaged/");

            var token = await _localStorageService.GetItemAsync<string>("accessToken");

            requestMessage.Headers.Authorization
                = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            requestMessage.Content = new StringContent(serializedUser);
            requestMessage.Content.Headers.ContentType
               = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var response = await _httpClient.SendAsync(requestMessage);

            var responseStatusCode = response.StatusCode;

            if (responseStatusCode.ToString() == "OK")
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                return await Task.FromResult(JsonConvert.DeserializeObject<GetAllResponsePaged<DanhMucKhachHangModel>>(responseBody));
            }
            else
                return new();
        }
        public async Task<GetAllResponsePaged<DanhMucKhachHangModel>> ExportToExcelAsync(QLSX.Shared.Models.KhachHangSearchRequest request)
        {
            string serializedUser = JsonConvert.SerializeObject(request);

            var requestMessage = new HttpRequestMessage(HttpMethod.Get, requestUri + "/ExportToExcel/");

            var token = await _localStorageService.GetItemAsync<string>("accessToken");

            requestMessage.Headers.Authorization
                = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            requestMessage.Content = new StringContent(serializedUser);
            requestMessage.Content.Headers.ContentType
               = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var response = await _httpClient.SendAsync(requestMessage);

            var responseStatusCode = response.StatusCode;

            if (responseStatusCode.ToString() == "OK")
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                return await Task.FromResult(JsonConvert.DeserializeObject<GetAllResponsePaged<DanhMucKhachHangModel>>(responseBody));
            }
            else
                return new();
        }
        public async Task<GetAllResponsePaged<DanhMucKhachHangModel>> GetAllPagedDialogAsync(QLSX.Shared.Models.KhachHangSearchRequest request)
        {
            string serializedUser = JsonConvert.SerializeObject(request);

            var requestMessage = new HttpRequestMessage(HttpMethod.Get, requestUri + "/GetAllPagedDialog/");

            var token = await _localStorageService.GetItemAsync<string>("accessToken");

            requestMessage.Headers.Authorization
                = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            requestMessage.Content = new StringContent(serializedUser);
            requestMessage.Content.Headers.ContentType
               = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var response = await _httpClient.SendAsync(requestMessage);

            var responseStatusCode = response.StatusCode;

            if (responseStatusCode.ToString() == "OK")
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                return await Task.FromResult(JsonConvert.DeserializeObject<GetAllResponsePaged<DanhMucKhachHangModel>>(responseBody));
            }
            else
                return new();
        }
        public async Task<byte[]> GetDataReportAsync()
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, "reports/DMKhachHang");

            var token = await _localStorageService.GetItemAsync<string>("accessToken");
            requestMessage.Headers.Authorization
                = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.SendAsync(requestMessage);

            var responseStatusCode = response.StatusCode;
            return await response.Content.ReadAsByteArrayAsync();
        }
        public async Task<string> GetPathPDFReportAsync()
        {
            var token = await _localStorageService.GetItemAsync<string>("accessToken");
            return _appSettings.ApiBase + "reports/" + requestUri + "/pdf?token=" + token;
        }
        public async Task<string> GetPathExcelReportAsync()
        {
            var token = await _localStorageService.GetItemAsync<string>("accessToken");
            return _appSettings.ApiBase + "reports/" + requestUri + "/Excel?token=" + token;
        }


        public async Task<double> GetSoDuCongNoById(QLSX.Shared.Models.GetSoDuCongNoRequest request)
        {
            string serializedUser = JsonConvert.SerializeObject(request);

            var requestMessage = new HttpRequestMessage(HttpMethod.Get, requestUri + "/GetSoDuCongNoById");

            var token = await _localStorageService.GetItemAsync<string>("accessToken");

            requestMessage.Headers.Authorization
                = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            requestMessage.Content = new StringContent(serializedUser);
            requestMessage.Content.Headers.ContentType
               = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var response = await _httpClient.SendAsync(requestMessage);

            var responseStatusCode = response.StatusCode;

            if (responseStatusCode.ToString() == "OK")
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                return await Task.FromResult(JsonConvert.DeserializeObject<double>(responseBody));
            }
            else
                return 0;
        }

        public async Task<bool> UpdateListPrintAsync(List<int> request)
        {

            string serializedUser = JsonConvert.SerializeObject(request);

            var requestMessage = new HttpRequestMessage(HttpMethod.Post, "BaoCaos/UpdateListPrint/KhachHang");

            var token = await _localStorageService.GetItemAsync<string>("accessToken");

            requestMessage.Headers.Authorization
                = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            requestMessage.Content = new StringContent(serializedUser);
            requestMessage.Content.Headers.ContentType
               = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            var response = await _httpClient.SendAsync(requestMessage);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return true;
            }
            else
                return false;
        }
    }
}
