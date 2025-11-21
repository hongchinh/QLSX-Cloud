using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using QLSX.Shared.Models;
using QLSX.Shared.Models.Request;

namespace QLSX.Web.Services
{
    interface INhatKyService<T>
    {
        Task<List<T>> GetAllAsync();
        Task<T> GetByIdAsync(int Id);
        Task<GetAllResponsePaged<NhatKyModel>> GetAllPagedAsync(QLSX.Shared.Models.NhatKySearchRequest request);
        Task<GetAllResponsePaged<NhatKyModel>> ExportToExcelAsync(QLSX.Shared.Models.NhatKySearchRequest request);

        Task<GetAllResponsePaged<NhatKyModel>> GetByIdPhieuAsync(QLSX.Shared.Models.NhatKySearchRequest request);

    }

    public class NhatKyService<T> : INhatKyService<T>
    {
        public HttpClient _httpClient { get; }
        public AppSettings _appSettings { get; }
        public AppService _appService { get; }
        public ILocalStorageService _localStorageService { get; }
        private string requestUri = "NhatKy";
        private readonly HttpInterceptorService _interceptor;
        public NhatKyService(HttpClient httpClient
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


        public async Task<GetAllResponsePaged<NhatKyModel>> GetAllPagedAsync( NhatKySearchRequest request)
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
                return await Task.FromResult(JsonConvert.DeserializeObject<GetAllResponsePaged<NhatKyModel>>(responseBody));
            }
            else
                return new();
        }
        public async Task<GetAllResponsePaged<NhatKyModel>> GetByIdPhieuAsync(QLSX.Shared.Models.NhatKySearchRequest request)
        {
            string serializedUser = JsonConvert.SerializeObject(request);

            var requestMessage = new HttpRequestMessage(HttpMethod.Get, requestUri + "/GetByIdPhieu");

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
                return await Task.FromResult(JsonConvert.DeserializeObject<GetAllResponsePaged<NhatKyModel>>(responseBody));
            }
            else
                return new();
        }
        public async Task<GetAllResponsePaged<NhatKyModel>> ExportToExcelAsync(QLSX.Shared.Models.NhatKySearchRequest request)
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
            //_interceptor.MonitorEvent();
            var response = await _httpClient.SendAsync(requestMessage);

            var responseStatusCode = response.StatusCode;

            if (responseStatusCode.ToString() == "OK")
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                return await Task.FromResult(JsonConvert.DeserializeObject<GetAllResponsePaged<NhatKyModel>>(responseBody));
            }
            else
                return new();
        }
    }
}
