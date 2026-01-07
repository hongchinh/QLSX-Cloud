using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using QLSX.Web.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using CsvHelper;
using System.Dynamic;
using System.IO;
using System.Globalization;
using System.Text;
using QLSX.Shared.Models;
using QLSX.Shared.Data.Responses;
using System.Net;

namespace QLSX.Web.Services
{
    interface ITraCuuService
    {
     
        Task<GetAllResponsePaged<SoTongHopHangHoa>> TraCuuTonKhoHangHoaAsync(TraCuuTonKhoRequest request);
        Task<GetAllResponsePaged<ViewNhapXuat>> ViewNhapXuatAsync(ViewNhapXuatRequest request);

        Task<GetAllResponsePaged<SoPhaiThuTongHop>> SoPhaiThuTongHopAsync(TraCuuCongNoRequest request);

        Task<GetAllResponsePaged<ViewNhapXuat>> ViewCongNoAsync(ViewCongNoRequest request);
        Task<GetAllResponsePaged<TraCuuNhapXuatAll>> TraCuuToanBoAsync(NhapXuatSearchRequest request);

    }

    public class TraCuuService  : ITraCuuService
    {
        private string requestUri = "tracuu";
        public HttpClient _httpClient { get; }
        public AppSettings _appSettings { get; }
        public ILocalStorageService _localStorageService { get; }
        public AppService _appService;
        public TraCuuService(HttpClient httpClient
            , IOptions<AppSettings> appSettings
            , ILocalStorageService localStorageService,
            AppService appService)
        {
            _appSettings = appSettings.Value;
            _localStorageService = localStorageService;

            httpClient.BaseAddress = new Uri(_appSettings.ApiBase);
            httpClient.DefaultRequestHeaders.Add(QLSX.Shared.Contansts.RequestHeaders.XTenantId, appService.DMDonViSuDungId.ToString());
            httpClient.DefaultRequestHeaders.Add(QLSX.Shared.Contansts.RequestHeaders.XUserId, appService.UserId.ToString());
            _httpClient = httpClient;
            _appService = appService;
        }

        

        public async Task<GetAllResponsePaged<SoTongHopHangHoa>> TraCuuTonKhoHangHoaAsync(TraCuuTonKhoRequest request)
        {
            try
            {
                string serializedString = JsonConvert.SerializeObject(request);

                var requestMessage = new HttpRequestMessage(HttpMethod.Get, requestUri + "/hanghoas");

                var token = await _localStorageService.GetItemAsync<string>("accessToken");
                requestMessage.Headers.Authorization
                    = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                requestMessage.Content = new StringContent(serializedString);
                requestMessage.Content.Headers.ContentType
                 = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

                var response = await _httpClient.SendAsync(requestMessage);

                var responseBody = await response.Content.ReadAsStringAsync();
                var results = await Task.FromResult(JsonConvert.DeserializeObject<GetAllResponsePaged<SoTongHopHangHoa>>(responseBody));
                return results;

            }
            catch (Exception ex)
            {

                return new GetAllResponsePaged<SoTongHopHangHoa>
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError,
                    Message = ex.Message
                };
            }
        }
        public async Task<GetAllResponsePaged<ViewNhapXuat>> ViewNhapXuatAsync(ViewNhapXuatRequest request)
        {
            try
            {
                string serializedString = JsonConvert.SerializeObject(request);

                var requestMessage = new HttpRequestMessage(HttpMethod.Get, requestUri + "/GetViewDanhSachNhapXuat");

                var token = await _localStorageService.GetItemAsync<string>("accessToken");
                requestMessage.Headers.Authorization
                    = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                requestMessage.Content = new StringContent(serializedString);
                requestMessage.Content.Headers.ContentType
                 = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

                var response = await _httpClient.SendAsync(requestMessage);

                var responseBody = await response.Content.ReadAsStringAsync();
                var results = await Task.FromResult(JsonConvert.DeserializeObject<GetAllResponsePaged<ViewNhapXuat>>(responseBody));
                return results;

            }
            catch (Exception ex)
            {

                return new GetAllResponsePaged<ViewNhapXuat>
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError,
                    Message = ex.Message
                };
            }
        }

        public async Task<GetAllResponsePaged<SoPhaiThuTongHop>> SoPhaiThuTongHopAsync(TraCuuCongNoRequest request)
        {
            try
            {
                string serializedString = JsonConvert.SerializeObject(request);

                var requestMessage = new HttpRequestMessage(HttpMethod.Get, requestUri + "/congno");

                var token = await _localStorageService.GetItemAsync<string>("accessToken");
                requestMessage.Headers.Authorization
                    = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                requestMessage.Content = new StringContent(serializedString);
                requestMessage.Content.Headers.ContentType
                 = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

                var response = await _httpClient.SendAsync(requestMessage);

                var responseBody = await response.Content.ReadAsStringAsync();
                var results = await Task.FromResult(JsonConvert.DeserializeObject<GetAllResponsePaged<SoPhaiThuTongHop>>(responseBody));
                return results;

            }
            catch (Exception ex)
            {

                return new GetAllResponsePaged<SoPhaiThuTongHop>
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError,
                    Message = ex.Message
                };
            }
        }
        public async Task<GetAllResponsePaged<ViewNhapXuat>> ViewCongNoAsync(ViewCongNoRequest request)
        {
            try
            {
                string serializedString = JsonConvert.SerializeObject(request);

                var requestMessage = new HttpRequestMessage(HttpMethod.Get, requestUri + "/GetViewDanhSachCongNo");

                var token = await _localStorageService.GetItemAsync<string>("accessToken");
                requestMessage.Headers.Authorization
                    = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                requestMessage.Content = new StringContent(serializedString);
                requestMessage.Content.Headers.ContentType
                 = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

                var response = await _httpClient.SendAsync(requestMessage);

                var responseBody = await response.Content.ReadAsStringAsync();
                var results = await Task.FromResult(JsonConvert.DeserializeObject<GetAllResponsePaged<ViewNhapXuat>>(responseBody));
                return results;

            }
            catch (Exception ex)
            {

                return new GetAllResponsePaged<ViewNhapXuat>
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError,
                    Message = ex.Message
                };
            }
        }
        public async Task<GetAllResponsePaged<TraCuuNhapXuatAll>> TraCuuToanBoAsync(NhapXuatSearchRequest request)
        {
            try
            {
                string serializedString = JsonConvert.SerializeObject(request);

                var requestMessage = new HttpRequestMessage(HttpMethod.Get, requestUri + "/TraCuuToanBo");

                var token = await _localStorageService.GetItemAsync<string>("accessToken");
                requestMessage.Headers.Authorization
                    = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                requestMessage.Content = new StringContent(serializedString);
                requestMessage.Content.Headers.ContentType
                 = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

                var response = await _httpClient.SendAsync(requestMessage);

                var responseBody = await response.Content.ReadAsStringAsync();
                var results = await Task.FromResult(JsonConvert.DeserializeObject<GetAllResponsePaged<TraCuuNhapXuatAll>>(responseBody));
                return results;

            }
            catch (Exception ex)
            {

                return new GetAllResponsePaged<TraCuuNhapXuatAll>
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError,
                    Message = ex.Message
                };
            }
        }
    }
}
