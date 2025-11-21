using Microsoft.Extensions.Options;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using QLSX.Shared.Models;

namespace QLSX.Web.Services
{
    interface IReportService
    {
        public Task<string> GetPathReportAsync(string reportname, string type, string id);
        public Task<string> GetPathXMLAsync(string reportname, string type, string id);
    }

    public class ReportService : IReportService
    {
        public HttpClient _httpClient { get; }
        public AppSettings _appSettings { get; }
        public ILocalStorageService _localStorageService { get; }
        public AppService _appService { get; }
        public ReportService(HttpClient httpClient
            , IOptions<AppSettings> appSettings
            , ILocalStorageService localStorageService,
             AppService appService)
        {
            _appSettings = appSettings.Value;
            _localStorageService = localStorageService;
            httpClient.BaseAddress = new Uri(_appSettings.ReportUrl);
            httpClient.DefaultRequestHeaders.Add("User-Agent", "BlazorServer");
            httpClient.DefaultRequestHeaders.Add(QLSX.Shared.Contansts.RequestHeaders.XTenantId, appService.DMDonViSuDungId.ToString());
            httpClient.DefaultRequestHeaders.Add(QLSX.Shared.Contansts.RequestHeaders.XUserId, appService.UserId.ToString());
            _httpClient = httpClient;
            _appService = appService;
        }

        public async Task<string> GetPathReportAsync(string reportname, string type, string id )
        {
            var token = await _localStorageService.GetItemAsync<string>("accessToken");
            var mdvsd = _appService.DMDonViSuDungId;
             return _appSettings.ReportUrl + "Reports/Viewer?ReportFileName=" + reportname + "&type="+ type + "&token=" + token  + "&id=" +id + "&mdvsd=" + mdvsd;
        }
        public async Task<string> GetPathXMLAsync(string reportname, string type, string id)
        {
            var token = await _localStorageService.GetItemAsync<string>("accessToken");
            var mdvsd = _appService.DMDonViSuDungId;
            return _appSettings.ReportUrl + "Reports/XMLViewer?ReportFileName=" + reportname + "&type=" + type + "&token=" + token + "&id=" + id + "&mdvsd=" + mdvsd;
        }
    }
}
