using Blazored.LocalStorage;
using QLSX.Shared.Data.Responses;
using QLSX.Shared.Interfaces;
using QLSX.Shared.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace QLSX.Shared.Services
{
    public class ApiWrapperServices : IApiWrapperServices
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _apiDomain;
        private readonly IHttpContextAccessor _accessor;
        private readonly IMemoryCache _memoryCache;
        public ILocalStorageService _localStorageService { get; }
        public ApiWrapperServices(IHttpClientFactory httpClientFactory, IHttpContextAccessor accessor, IConfiguration configuration, IMemoryCache memoryCache, ILocalStorageService localStorageService)
        {
            _httpClientFactory = httpClientFactory;
            _accessor = accessor;
            _apiDomain = configuration["AppSettings:ApiBase"];
            _memoryCache = memoryCache;
            _localStorageService = localStorageService;
        }

        public async Task<ApiResponseBase<TResponse>> SendApiPostAsync<TRequest, TResponse>(TRequest request)
             where TRequest : class, IApiWrapperRequest
            where TResponse : class, IApiWrapperResponse
        {
            return await PostAsyn<TRequest, TResponse>(request, _apiDomain, request.RequestPath);
        }

        public async Task<ReportResponseBase<TResponse>> SendReportPostAsync<TRequest, TResponse>(TRequest request)
           where TRequest : class, IApiWrapperRequest
          where TResponse : class, IApiWrapperResponse
        {
            return await PostReportAsyn<TRequest, TResponse>(request, _apiDomain, request.RequestPath);
        }

        public async Task<ApiResponseBase<TResponse>> SendApiGetAsync<TRequest, TResponse>(TRequest request)
            where TRequest : class, IApiWrapperRequest
           where TResponse : class, IApiWrapperResponse
        {
            return await GetAsyn<TRequest, TResponse>(request, _apiDomain, request.RequestPath);
        }

        private async Task<ApiResponseBase<TResponse>> PostAsyn<TRequest, TResponse>(TRequest request, string domain, string requestPath)
        {
            using (var httpClient = _httpClientFactory.CreateClient())
            {
                string token = (string)_memoryCache.Get("_Key_Token");
                var donviId = (string)_memoryCache.Get("_DonVi_Id");
                var userId = (string)_memoryCache.Get("_User_Id");
                httpClient.DefaultRequestHeaders.Add(QLSX.Shared.Contansts.RequestHeaders.XTenantId, donviId);
                httpClient.DefaultRequestHeaders.Add(QLSX.Shared.Contansts.RequestHeaders.XUserId, userId);
                httpClient.BaseAddress = new Uri(Path.Combine(domain, requestPath));
                httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
                httpClient.DefaultRequestHeaders.Add("Accept", "*/*");
                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/plain"));//ACCEPT header
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));//ACCEPT header


                JsonSerializerOptions options = new JsonSerializerOptions()
                {
                    IgnoreNullValues = true
                };

                var requestContent = new StringContent(
                                        JsonSerializer.Serialize(request, options),
                                        Encoding.UTF8,
                                        "application/json");

                var response = await httpClient.PostAsync("", requestContent);

                if (response.IsSuccessStatusCode && response.Content is object && response.Content.Headers.ContentType?.MediaType == "application/json")
                {
                    var test = await response.Content.ReadAsStringAsync();

                    using var responseStream = await response.Content.ReadAsStreamAsync();

                    return await JsonSerializer.DeserializeAsync<ApiResponseBase<TResponse>>(responseStream);
                }
                else
                {
                    return new ApiResponseBase<TResponse>()
                    {
                        StatusCode = response.StatusCode
                    };
                }
            }
        }

        private async Task<ReportResponseBase<TResponse>> PostReportAsyn<TRequest, TResponse>(TRequest request, string domain, string requestPath)
        {
            using (var httpClient = _httpClientFactory.CreateClient())
            {
                string token = (string)_memoryCache.Get("_Key_Token");
                var donviId = (string)_memoryCache.Get("_DonVi_Id");
                var userId = (string)_memoryCache.Get("_User_Id");
                httpClient.DefaultRequestHeaders.Add(QLSX.Shared.Contansts.RequestHeaders.XTenantId, donviId);
                httpClient.DefaultRequestHeaders.Add(QLSX.Shared.Contansts.RequestHeaders.XUserId, userId);

                httpClient.BaseAddress = new Uri(Path.Combine(domain, requestPath));
                httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
                httpClient.DefaultRequestHeaders.Add("Accept", "*/*");
                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/plain"));//ACCEPT header
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));//ACCEPT header


                JsonSerializerOptions options = new JsonSerializerOptions()
                {
                    IgnoreNullValues = true
                };

                var requestContent = new StringContent(
                                        JsonSerializer.Serialize(request, options),
                                        Encoding.UTF8,
                                        "application/json");

                var response = await httpClient.PostAsync("", requestContent);

                if (response.IsSuccessStatusCode && response.Content is object && response.Content.Headers.ContentType?.MediaType == "application/json")
                {
                    var test = await response.Content.ReadAsStringAsync();

                    using var responseStream = await response.Content.ReadAsStreamAsync();

                    return await JsonSerializer.DeserializeAsync<ReportResponseBase<TResponse>>(responseStream);
                }
                else
                {
                    return new ReportResponseBase<TResponse>()
                    {
                        StatusCode = (int)response.StatusCode
                    };
                }
            }
        }

        private async Task<ApiResponseBase<TResponse>> GetAsyn<TRequest, TResponse>(TRequest request, string domain, string requestPath)
        {
            using (var httpClient = _httpClientFactory.CreateClient())
            {
                string token = (string)_memoryCache.Get("_Key_Token");
                var donviId = (string)_memoryCache.Get("_DonVi_Id");
                var userId = (string)_memoryCache.Get("_User_Id");
                httpClient.DefaultRequestHeaders.Add(QLSX.Shared.Contansts.RequestHeaders.XTenantId, donviId);
                httpClient.DefaultRequestHeaders.Add(QLSX.Shared.Contansts.RequestHeaders.XUserId, userId);
                HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Get, Path.Combine(domain, requestPath));
                message.Headers.Add("Accept", "application/json");
                message.Headers.Add("Accept", "*/*");
                message.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token); 
                var response = await httpClient.SendAsync(message);

                var responseStatusCode = response.StatusCode;

                if (response.IsSuccessStatusCode && response.Content is object && response.Content.Headers.ContentType?.MediaType == "application/json")
                {
                    using var responseStream = await response.Content.ReadAsStreamAsync();
                    return await JsonSerializer.DeserializeAsync<ApiResponseBase<TResponse>>(responseStream);
                }
                else
                {
                    return new ApiResponseBase<TResponse>()
                    {
                        StatusCode = response.StatusCode
                    };
                }
            }
        }
    }
}
