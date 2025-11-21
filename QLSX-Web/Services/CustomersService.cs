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
using QLSX.Shared.Models.Request;

namespace QLSX.Web.Services
{
    interface ICustomersService<T>
    {
        Task<GetAllResponsePaged<CustomerVM>> GetAllByUserIdAsync(CustomerSearchRequest request);
        Task<List<CustomerVM>> GetAllByUserIdForAdminAsync();
        Task<GetAllResponsePaged<CustomerVM>> GetAllByUserIdForAdminPagedAsync(CustomerSearchRequest request);
        Task<List<T>> GetSaleByCustomerIdAsync(int customerId);
        Task<List<T>> GetSaleByCustomerCodeAsync(string customerCode);
        Task<List<T>> GetAllAsync();
        Task<T> GetByIdAsync(int Id);
        Task<T> CreateAsync(T obj);
        Task<T> UpdateAsync(int Id, T obj);
        Task<bool> DeleteAsync(int Id);

        Task<List<T>> GetCongNoAdminAsync();
        Task<List<T>> GetCongNoAsync();
        Task<List<TongHopThongKe>> ThongKeTongHopAsync(TongHopThongKeRequest request);

        Task<List<TongHopDongTien>> SoQuyTongHopAsync(TongHopDongTienRequest request);
        Task<BangLuong> BangLuongAsync(BangLuongRequest request);

    }

    public class CustomersService<T> : ICustomersService<T>
    {
        private string requestUri = "customers";
        public HttpClient _httpClient { get; }
        public AppSettings _appSettings { get; }
        public ILocalStorageService _localStorageService { get; }
        public AppService _appService;
        public CustomersService(HttpClient httpClient
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
            _httpClient = httpClient;
            _appService = appService;
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
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, requestUri);

            var token = await _localStorageService.GetItemAsync<string>("accessToken");
            requestMessage.Headers.Authorization
                = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

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

        public async Task<List<T>> GetSaleByCustomerCodeAsync(string customerCode)
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, requestUri + "/GetDonHangTheoKhachHangByCode/" + customerCode);

            var token = await _localStorageService.GetItemAsync<string>("accessToken");
            requestMessage.Headers.Authorization
                = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

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
        public async Task<List<T>> GetSaleByCustomerIdAsync(int customerId)
        {


            var requestMessage = new HttpRequestMessage(HttpMethod.Get, requestUri + "/GetDonHangTheoKhachHang/" + customerId);

            var token = await _localStorageService.GetItemAsync<string>("accessToken");
            requestMessage.Headers.Authorization
                = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

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
        public async Task<GetAllResponsePaged<CustomerVM>> GetAllByUserIdAsync(CustomerSearchRequest request)
        {
            string serializedUser = JsonConvert.SerializeObject(request);
            UserModel user = await _localStorageService.GetItemAsync<UserModel>("UserLogin");

            var requestMessage = new HttpRequestMessage(HttpMethod.Get, requestUri + "/GetAllByUserId/" + user.Id);

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
                return await Task.FromResult(JsonConvert.DeserializeObject<GetAllResponsePaged<CustomerVM>>(responseBody));
            }
            else
                return new();
        }
        public async Task<List<CustomerVM>> GetAllByUserIdForAdminAsync()
        {
            UserModel user = await _localStorageService.GetItemAsync<UserModel>("UserLogin");

            var requestMessage = new HttpRequestMessage(HttpMethod.Get, requestUri + "/GetAllByUserIdForAdmin/" + user.Id);

            var token = await _localStorageService.GetItemAsync<string>("accessToken");
            requestMessage.Headers.Authorization
                = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.SendAsync(requestMessage);

            var responseStatusCode = response.StatusCode;

            if (responseStatusCode.ToString() == "OK")
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                return await Task.FromResult(JsonConvert.DeserializeObject<List<CustomerVM>>(responseBody));
            }
            else
                return new();
        }
        public async Task<GetAllResponsePaged<CustomerVM>> GetAllByUserIdForAdminPagedAsync(CustomerSearchRequest request)
        {
            string serializedUser = JsonConvert.SerializeObject(request);
            UserModel user = await _localStorageService.GetItemAsync<UserModel>("UserLogin");

            var requestMessage = new HttpRequestMessage(HttpMethod.Get, requestUri + "/GetAllByUserIdForAdminPaged/" + user.Id);

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
                return await Task.FromResult(JsonConvert.DeserializeObject<GetAllResponsePaged<CustomerVM>>(responseBody));
            }
            else
                return new();
        }

        public async Task<List<T>> GetCongNoAsync()
        {
            UserModel user = await _localStorageService.GetItemAsync<UserModel>("UserLogin");

            TongHopCongNoRequest obj = new TongHopCongNoRequest();
            obj.hien = 0;
            obj.date1 = "01/01/2021";
            obj.date2 = DateTime.Now.ToString("MM/dd/yyyy");
            obj.mdvsd = "01";
            obj.MNhom = user.Id.ToString();
            //obj.RoleId = user.RoleId;
            obj.tmptblOK = "ZZTEMPS";

            string serializedUser = JsonConvert.SerializeObject(obj);
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, requestUri + "/TongHopCongNo");

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
                return await Task.FromResult(JsonConvert.DeserializeObject<List<T>>(responseBody));
            }
            else
                return new();
        }

        public async Task<List<T>> GetCongNoAdminAsync()
        {
            UserModel user = await _localStorageService.GetItemAsync<UserModel>("UserLogin");

            TongHopCongNoRequest obj = new TongHopCongNoRequest();
            obj.hien = 0;
            obj.date1 = "01/01/2021";
            obj.date2 = DateTime.Now.ToString("MM/dd/yyyy");
            obj.mdvsd = "01";
            obj.MNhom = user.Id.ToString();
            //obj.RoleId = user.RoleId;
            obj.tmptblOK = "ZZTEMPS";

            string serializedUser = JsonConvert.SerializeObject(obj);
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, requestUri + "/TongHopCongNoAdmin");

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

        public async Task<List<TongHopThongKe>> ThongKeTongHopAsync(TongHopThongKeRequest request)
        {
            UserModel user = await _localStorageService.GetItemAsync<UserModel>("UserLogin");
            if (user == null) return new();
            int userId = 0;
            if (user.IsAdmin)
                userId = 0;
            userId = user.Id;

            string serializedUser = JsonConvert.SerializeObject(request);

            var requestMessage = new HttpRequestMessage(HttpMethod.Get, requestUri + "/GetTongHopThongKe/" + userId);

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
                return await Task.FromResult(JsonConvert.DeserializeObject<List<TongHopThongKe>>(responseBody));
            }
            else
                return new();
        }


        public async Task<List<TongHopDongTien>> SoQuyTongHopAsync(TongHopDongTienRequest request)
        {
            UserModel user = await _localStorageService.GetItemAsync<UserModel>("UserLogin");
            if (user == null) return new();
            
            string serializedUser = JsonConvert.SerializeObject(request);

            var requestMessage = new HttpRequestMessage(HttpMethod.Get, requestUri + "/SoQuyTongHop");

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
                return await Task.FromResult(JsonConvert.DeserializeObject<List<TongHopDongTien>>(responseBody));
            }
            else
                return new();
        }

        public async Task<BangLuong> BangLuongAsync(BangLuongRequest request)
        {
            UserModel user = await _localStorageService.GetItemAsync<UserModel>("UserLogin");
            if (user == null) return new();

            string serializedUser = JsonConvert.SerializeObject(request);

            var requestMessage = new HttpRequestMessage(HttpMethod.Get, requestUri + "/BangLuong");

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
                return await Task.FromResult(JsonConvert.DeserializeObject<BangLuong>(responseBody));
            }
            else
                return new();
        }

        public void DownloadReport(string id)
        {
            var records = new List<dynamic>();
            dynamic record = new ExpandoObject();
            record.Id = 1;
            record.Name = "one";
            records.Add(record);

            using (var writer = new StringWriter())
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(records);

                 
            }
        }
        public void WriteCSVFile(string path, List<Customer> students)
        {
            using (StreamWriter sw = new StreamWriter(path, false, new UTF8Encoding(true)))
            using (CsvWriter cw = new CsvWriter(sw, CultureInfo.InvariantCulture))
            {
                cw.WriteHeader<Customer>();
                cw.NextRecord();
                foreach (Customer stu in students)
                {
                    cw.WriteRecord<Customer>(stu);
                    cw.NextRecord();
                }
            }
        }
    }
}
