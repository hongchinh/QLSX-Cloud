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
    interface INhapXuatTonCuonsService<T>
    {
        Task<List<T>> GetAllToListAsync(NhapXuatTonCuonSearchRequest request);
        Task<GetAllResponsePaged<T>> GetAllAsync();
        Task<NhapXuatTonCuonNavigatorResponse> GetByIdAsync(int Id);
        Task<NhapXuatTonCuonNavigatorResponse> GetNhapByIndexAsync(NhapXuatTonCuonSearchRequest request);
        Task<NhapXuatTonCuonNavigatorResponse> GetXuatByIndexAsync(NhapXuatTonCuonSearchRequest request);
        Task<NhapXuatTonCuonNavigatorResponse> GetNhapXuatByIDAsync(NhapXuatTonCuonSearchRequest request);
        Task<NhapXuatTonCuonModel> CreateAsync(T obj);
        Task<T> UpdateAsync(int Id, T obj);
        Task<bool> DeleteAsync(int Id);

        Task<GetAllResponsePaged<T>> GetAllPagedAsync(NhapXuatTonCuonSearchRequest request);
        Task<GetAllResponsePaged<T>> ExportToExcelAsync(NhapXuatTonCuonSearchRequest request);
        Task<GetAllResponsePaged<TraCuuNhapXuatAll>> GetAllPagedOnTraCuuAllAsync(NhapXuatTonCuonSearchRequest request);
        Task<GetAllResponsePaged<QLSX.Web.Data.Responses.NavigatorResponse>> TimKiemNhanhAsync(NhapXuatTonCuonSearchRequest request);

        Task<List<int>> GetAllNhapXuatIDs(NhapXuatTonCuonSearchRequest request);
        Task<int> GetIdLastest(string loai);
    }

    public class NhapXuatTonCuonsService<T> : INhapXuatTonCuonsService<T>
    {
        public HttpClient _httpClient { get; }
        public AppSettings _appSettings { get; }
        public AppService _appService { get; }
        public ILocalStorageService _localStorageService { get; }
        private string requestUri = "nhapxuattoncuons";
        public NhapXuatTonCuonsService(HttpClient httpClient
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

            return await Task.FromResult(true);
        }
        public async Task<List<T>> GetAllToListAsync(NhapXuatTonCuonSearchRequest request)
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

        public async Task<NhapXuatTonCuonNavigatorResponse> GetByIdAsync(int Id)
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, requestUri + "/" + Id);

            var token = await _localStorageService.GetItemAsync<string>("accessToken");
            requestMessage.Headers.Authorization
                = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.SendAsync(requestMessage);

            var responseStatusCode = response.StatusCode;
            var responseBody = await response.Content.ReadAsStringAsync();

            return await Task.FromResult(JsonConvert.DeserializeObject<NhapXuatTonCuonNavigatorResponse>(responseBody));
        }
        public async Task<NhapXuatTonCuonNavigatorResponse> GetNhapByIndexAsync(NhapXuatTonCuonSearchRequest request)
        {
            string serializedString = JsonConvert.SerializeObject(request);

            var requestMessage = new HttpRequestMessage(HttpMethod.Get, requestUri + "/index/nhapton/" + request.Index);

            var token = await _localStorageService.GetItemAsync<string>("accessToken");
            requestMessage.Headers.Authorization
                = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            requestMessage.Content = new StringContent(serializedString);
            requestMessage.Content.Headers.ContentType
             = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var response = await _httpClient.SendAsync(requestMessage);

            var responseStatusCode = response.StatusCode;
            var responseBody = await response.Content.ReadAsStringAsync();

            return await Task.FromResult(JsonConvert.DeserializeObject<NhapXuatTonCuonNavigatorResponse>(responseBody));
        }
        public async Task<NhapXuatTonCuonNavigatorResponse> GetXuatByIndexAsync(NhapXuatTonCuonSearchRequest request)
        {
            string serializedString = JsonConvert.SerializeObject(request);

            var requestMessage = new HttpRequestMessage(HttpMethod.Get, requestUri + "/index/xuatton/" + request.Index);

            var token = await _localStorageService.GetItemAsync<string>("accessToken");
            requestMessage.Headers.Authorization
                = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            requestMessage.Content = new StringContent(serializedString);
            requestMessage.Content.Headers.ContentType
             = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var response = await _httpClient.SendAsync(requestMessage);

            var responseStatusCode = response.StatusCode;
            var responseBody = await response.Content.ReadAsStringAsync();

            return await Task.FromResult(JsonConvert.DeserializeObject<NhapXuatTonCuonNavigatorResponse>(responseBody));
        }
        public async Task<NhapXuatTonCuonNavigatorResponse> GetNhapXuatByIDAsync(NhapXuatTonCuonSearchRequest request)
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

            return await Task.FromResult(JsonConvert.DeserializeObject<NhapXuatTonCuonNavigatorResponse>(responseBody));
        }
        public async Task<NhapXuatTonCuonModel> CreateAsync(T obj)
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

            try
            {
                var returnedObj = JsonConvert.DeserializeObject<NhapXuatTonCuonModel>(responseBody);
                return await Task.FromResult(returnedObj);
            }
            catch (Exception ex)
            {
                return new();
            }
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

        public async Task<GetAllResponsePaged<T>> GetAllPagedAsync(NhapXuatTonCuonSearchRequest request)
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

        public async Task<GetAllResponsePaged<TraCuuNhapXuatAll>> GetAllPagedOnTraCuuAllAsync(NhapXuatTonCuonSearchRequest request)
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
        public async Task<GetAllResponsePaged<QLSX.Web.Data.Responses.NavigatorResponse>> TimKiemNhanhAsync(NhapXuatTonCuonSearchRequest request)
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
        public async Task<List<int>> GetAllNhapIDs(NhapXuatTonCuonSearchRequest request)
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
        public async Task<List<int>> GetAllNhapXuatIDs(NhapXuatTonCuonSearchRequest request)
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

        public async Task<GetAllResponsePaged<T>> ExportToExcelAsync(NhapXuatTonCuonSearchRequest request)
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
    }
}
