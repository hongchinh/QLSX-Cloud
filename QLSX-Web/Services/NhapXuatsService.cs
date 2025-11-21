using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using QLSX.Web.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using QLSX.Shared.DTOs;
using QLSX.Shared.Models;
using QLSX.Shared.Models.Request;

namespace QLSX.Web.Services
{
    interface INhapXuatsService<T>
    {
        Task<List<T>> GetAllToListAsync(NhapXuatSearchRequest request);
        Task<GetAllResponsePaged<T>> GetAllAsync();
        Task<NhapXuatNavigatorResponse> GetByIdAsync(int Id);
        Task<NhapXuatNavigatorResponse> GetNhapByIndexAsync(NhapXuatSearchRequest request);
        Task<NhapXuatNavigatorResponse> GetXuatByIndexAsync(NhapXuatSearchRequest request);
        Task<NhapXuatNavigatorResponse> GetNhapXuatByIDAsync(NhapXuatSearchRequest request);
        Task<T> CreateAsync(T obj);
        Task<T> UpdateAsync(int Id, T obj);
        Task<bool> DeleteAsync(int Id);

        Task<GetAllResponsePaged<T>> GetAllPagedAsync(NhapXuatSearchRequest request);
        Task<GetAllResponsePaged<T>> ExportToExcelAsync(NhapXuatSearchRequest request);
        Task<GetAllResponsePaged<TraCuuNhapXuatAll>> GetAllPagedOnTraCuuAllAsync(NhapXuatSearchRequest request);
        Task<GetAllResponsePaged<QLSX.Web.Data.Responses.NavigatorResponse>> TimKiemNhanhAsync(NhapXuatSearchRequest request);

        Task<List<int>> GetAllNhapXuatIDs(NhapXuatSearchRequest request);
        Task<int> GetIdLastest(string loai);
        Task<NhapXuatNavigatorResponse> GetBySoChungTuAsync(NhapXuatSearchRequest request);

        Task<List<NoiDungNhapXuatModel>> GetHangHoaByLoaiPhieuAsync(string loaiPhieu);

        Task<List<NhapXuatModel>> GetDonHangByMaDonViAsync(string maDoiTuong);
    }

    public class NhapXuatsService<T> : INhapXuatsService<T>
    {
        public HttpClient _httpClient { get; }
        public AppSettings _appSettings { get; }
        public AppService _appService { get; }
        public ILocalStorageService _localStorageService { get; }
        private string requestUri = "nhapxuats";
        public NhapXuatsService(HttpClient httpClient
            , IOptions<AppSettings> appSettings
            , ILocalStorageService localStorageService,
             AppService appService)
        {
            _appSettings = appSettings.Value;
            _localStorageService = localStorageService;
            httpClient.BaseAddress = new Uri(_appSettings.ApiBase);
            httpClient.DefaultRequestHeaders.Add("User-Agent", "BlazorServer");
            httpClient.DefaultRequestHeaders.Add(QLSX.Shared.Contansts.RequestHeaders.XTenantId, appService.DMDonViSuDungId.ToString());
            httpClient.DefaultRequestHeaders.Add(QLSX.Shared.Contansts.RequestHeaders.XUserId, appService.UserId.ToString());
            _appService = appService;
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

            return await Task.FromResult(responseStatusCode.ToString() == "OK");
        }
        public async Task<List<T>> GetAllToListAsync(NhapXuatSearchRequest request)
        {
            string serializedString = JsonConvert.SerializeObject(request);

            var requestMessage = new HttpRequestMessage(HttpMethod.Get, requestUri + "/GetList");

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
        public async Task<GetAllResponsePaged<T>> GetAllAsync()
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
                return await Task.FromResult(JsonConvert.DeserializeObject<GetAllResponsePaged<T>>(responseBody));
            }
            else
                return new();
        }

        public async Task<NhapXuatNavigatorResponse> GetByIdAsync(int Id)
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, requestUri + "/" + Id);

            var token = await _localStorageService.GetItemAsync<string>("accessToken");
            requestMessage.Headers.Authorization
                = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.SendAsync(requestMessage);

            var responseStatusCode = response.StatusCode;
            var responseBody = await response.Content.ReadAsStringAsync();

            return await Task.FromResult(JsonConvert.DeserializeObject<NhapXuatNavigatorResponse>(responseBody));
        }
        public async Task<NhapXuatNavigatorResponse> GetNhapByIndexAsync(NhapXuatSearchRequest request)
        {
            string serializedString = JsonConvert.SerializeObject(request);

            var requestMessage = new HttpRequestMessage(HttpMethod.Get, requestUri + "/index/nhap/" + request.Index);

            var token = await _localStorageService.GetItemAsync<string>("accessToken");
            requestMessage.Headers.Authorization
                = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            requestMessage.Content = new StringContent(serializedString);
            requestMessage.Content.Headers.ContentType
             = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var response = await _httpClient.SendAsync(requestMessage);

            var responseStatusCode = response.StatusCode;
            var responseBody = await response.Content.ReadAsStringAsync();

            return await Task.FromResult(JsonConvert.DeserializeObject<NhapXuatNavigatorResponse>(responseBody));
        }
        public async Task<NhapXuatNavigatorResponse> GetXuatByIndexAsync(NhapXuatSearchRequest request)
        {
            string serializedString = JsonConvert.SerializeObject(request);

            var requestMessage = new HttpRequestMessage(HttpMethod.Get, requestUri + "/index/xuat/" + request.Index);

            var token = await _localStorageService.GetItemAsync<string>("accessToken");
            requestMessage.Headers.Authorization
                = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            requestMessage.Content = new StringContent(serializedString);
            requestMessage.Content.Headers.ContentType
             = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var response = await _httpClient.SendAsync(requestMessage);

            var responseStatusCode = response.StatusCode;
            var responseBody = await response.Content.ReadAsStringAsync();

            return await Task.FromResult(JsonConvert.DeserializeObject<NhapXuatNavigatorResponse>(responseBody));
        }
        public async Task<NhapXuatNavigatorResponse> GetBySoChungTuAsync(NhapXuatSearchRequest request)
        {
            string serializedString = JsonConvert.SerializeObject(request);

            var requestMessage = new HttpRequestMessage(HttpMethod.Get, requestUri + "/GetBySoChungTu");

            var token = await _localStorageService.GetItemAsync<string>("accessToken");
            requestMessage.Headers.Authorization
                = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            requestMessage.Content = new StringContent(serializedString);
            requestMessage.Content.Headers.ContentType
             = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var response = await _httpClient.SendAsync(requestMessage);

            var responseStatusCode = response.StatusCode;
            var responseBody = await response.Content.ReadAsStringAsync();

            return await Task.FromResult(JsonConvert.DeserializeObject<NhapXuatNavigatorResponse>(responseBody));
        }

        public async Task<NhapXuatNavigatorResponse> GetNhapXuatByIDAsync(NhapXuatSearchRequest request)
        {
            string serializedString = JsonConvert.SerializeObject(request);

            var requestMessage = new HttpRequestMessage(HttpMethod.Get, requestUri + @"/" + request.Id);

            var token = await _localStorageService.GetItemAsync<string>("accessToken");
            requestMessage.Headers.Authorization
                = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            requestMessage.Content = new StringContent(serializedString);
            requestMessage.Content.Headers.ContentType
             = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var response = await _httpClient.SendAsync(requestMessage);

            var responseStatusCode = response.StatusCode;
            var responseBody = await response.Content.ReadAsStringAsync();
            try
            {
                var result = await Task.FromResult(JsonConvert.DeserializeObject<NhapXuatNavigatorResponse>(responseBody));
                return result;
            }
            catch (Exception ex)
            {
                return new();
            }
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

        public async Task<GetAllResponsePaged<T>> GetAllPagedAsync(NhapXuatSearchRequest request)
        {
            string serializedUser = JsonConvert.SerializeObject(request);

            var requestMessage = new HttpRequestMessage(HttpMethod.Get, requestUri + "/GetAllPaged");

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
                return await Task.FromResult(JsonConvert.DeserializeObject<GetAllResponsePaged<T>>(responseBody));
            }
            else
                return new();
        }

        public async Task<GetAllResponsePaged<TraCuuNhapXuatAll>> GetAllPagedOnTraCuuAllAsync(NhapXuatSearchRequest request)
        {
            string serializedUser = JsonConvert.SerializeObject(request);

            var requestMessage = new HttpRequestMessage(HttpMethod.Get, requestUri + "/GetAllPagedOnTraCuuAll");

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
                return await Task.FromResult(JsonConvert.DeserializeObject<GetAllResponsePaged<TraCuuNhapXuatAll>>(responseBody));
            }
            else
                return new();
        }
        public async Task<GetAllResponsePaged<QLSX.Web.Data.Responses.NavigatorResponse>> TimKiemNhanhAsync(NhapXuatSearchRequest request)
        {
            string serializedUser = JsonConvert.SerializeObject(request);

            var requestMessage = new HttpRequestMessage(HttpMethod.Get, requestUri + "/TimKiemNhanh/");

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
                return await Task.FromResult(JsonConvert.DeserializeObject<GetAllResponsePaged<QLSX.Web.Data.Responses.NavigatorResponse>>(responseBody));
            }
            else
                return new();
        }
        public async Task<List<int>> GetAllNhapIDs(NhapXuatSearchRequest request)
        {
            string serializedUser = JsonConvert.SerializeObject(request);

            var requestMessage = new HttpRequestMessage(HttpMethod.Get, requestUri + "/GetAllNhapIDs");

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
                return await Task.FromResult(JsonConvert.DeserializeObject<List<int>>(responseBody));
            }
            else
                return new();
        }
        public async Task<List<int>> GetAllNhapXuatIDs(NhapXuatSearchRequest request)
        {
            string serializedUser = JsonConvert.SerializeObject(request);

            var requestMessage = new HttpRequestMessage(HttpMethod.Get, requestUri + "/GetAllNhapXuatIDs");

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
                return await Task.FromResult(JsonConvert.DeserializeObject<List<int>>(responseBody));
            }
            else
                return new();
        }

        public async Task<GetAllResponsePaged<T>> ExportToExcelAsync(NhapXuatSearchRequest request)
        {
            string serializedUser = JsonConvert.SerializeObject(request);

            var requestMessage = new HttpRequestMessage(HttpMethod.Get, requestUri + "/ExportToExcel");

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
                return await Task.FromResult(JsonConvert.DeserializeObject<GetAllResponsePaged<T>>(responseBody));
            }
            else
                return new();
        }

        public async Task<int> GetIdLastest(string loai)
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, requestUri + "/GetIdLastest/" + loai);

            var token = await _localStorageService.GetItemAsync<string>("accessToken");
            requestMessage.Headers.Authorization
                = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.SendAsync(requestMessage);

            var responseStatusCode = response.StatusCode;

            if (responseStatusCode.ToString() == "OK")
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                return await Task.FromResult(JsonConvert.DeserializeObject<int>(responseBody));
            }
            else
                return 0;
        }

        public async Task<List<NoiDungNhapXuatModel>> GetHangHoaByLoaiPhieuAsync(string loaiPhieu)
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, requestUri + $"/GetHangHoaByLoaiPhieu?loaiPhieu={loaiPhieu}");

            var token = await _localStorageService.GetItemAsync<string>("accessToken");
            requestMessage.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.SendAsync(requestMessage);

            if (response.IsSuccessStatusCode)
            {
                string jsonResponse = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<NoiDungNhapXuatModel>>(jsonResponse);
            }
            return null;
        }

        public async Task<List<NhapXuatModel>> GetDonHangByMaDonViAsync(string maDoiTuong)
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, requestUri + $"/GetDonHangByMaDonVi?maDoiTuong={maDoiTuong}");

            var token = await _localStorageService.GetItemAsync<string>("accessToken");
            requestMessage.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.SendAsync(requestMessage);

            if (response.IsSuccessStatusCode)
            {
                string jsonResponse = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<NhapXuatModel>>(jsonResponse);
            }
            return null;
        }
    }
}
