using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using QLSX.Shared.Models;
using QLSX.Web.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace QLSX.Web.Services
{
    public interface INotiService<T>
    {
        Task<int> GetCountByUserIdAsync(int Id, string token);
              
    }

    public class NotiService<T> : INotiService<T>
    {
        public HttpClient _httpClient { get; }
        public AppSettings _appSettings { get; }
        public AppService _appService;
        string requestUri = "notifications/GetCountUser";
        public NotiService(HttpClient httpClient
            , IOptions<AppSettings> appSettings,
            AppService appService)
        {
            _appSettings = appSettings.Value;
            httpClient.BaseAddress = new Uri(_appSettings.ApiBase);
            httpClient.DefaultRequestHeaders.Add("User-Agent", "BlazorServer");
            httpClient.DefaultRequestHeaders.Add(QLSX.Shared.Contansts.RequestHeaders.XTenantId, appService.DMDonViSuDungId.ToString());
            httpClient.DefaultRequestHeaders.Add(QLSX.Shared.Contansts.RequestHeaders.XUserId, appService.UserId.ToString());
            _httpClient = httpClient;
            _appService = appService;
        }
        public async Task<int> GetCountByUserIdAsync( int userId, string token)
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, requestUri + "/" + userId);
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
