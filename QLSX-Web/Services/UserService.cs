using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Blazored.LocalStorage;
using QLSX.Shared.Models;

namespace QLSX.Web.Services;

public interface IUserService
{
    public Task<UserModel> LoginAsync(LoginRequest user);
    public Task<UserModel> GetUser();
    public Task<UserModel> RegisterUserAsync(UserModel user);
    public Task SetUser(UserModel user);
    public Task<UserModel> GetUserByAccessTokenAsync(string accessToken);
    public Task<UserModel> RefreshTokenAsync(RefreshRequest refreshRequest);
}

public class UserService : IUserService
{
    private NavigationManager _navigationManager;
    private ILocalStorageService _localStorageService;

    public HttpClient _httpClient { get; }
    public AppSettings _appSettings { get; }
    AppService _appService;
    public async Task<UserModel> GetUser()
    {
        return await _localStorageService.GetItemAsync<UserModel>("UserLogin");
    }

    public UserService(HttpClient httpClient, IOptions<AppSettings> appSettings,
         NavigationManager navigationManager,
       ILocalStorageService localStorageService, AppService appService)
    {
        _appSettings = appSettings.Value;

        httpClient.BaseAddress = new Uri(_appSettings.ApiBase);
        httpClient.DefaultRequestHeaders.Add("User-Agent", "BlazorServer");
        httpClient.DefaultRequestHeaders.Add(QLSX.Shared.Contansts.RequestHeaders.XTenantId, appService.DMDonViSuDungId.ToString());
        httpClient.DefaultRequestHeaders.Add(QLSX.Shared.Contansts.RequestHeaders.XUserId, appService.UserId.ToString());

        _httpClient = httpClient;
        _navigationManager = navigationManager;
        _localStorageService = localStorageService;
        _appService = appService;
    }
    public async Task SetUser(UserModel user)
    {
        await _localStorageService.SetItemAsync("UserLogin", user);
    }

    public async Task<UserModel> LoginAsync(LoginRequest user)
    {
        user.Password = EncryptionHelper.Encrypt(user.Password);
        string serializedUser = JsonConvert.SerializeObject(user);

        var requestMessage = new HttpRequestMessage(HttpMethod.Post, "Users/Login");
        requestMessage.Content = new StringContent(serializedUser);

        requestMessage.Content.Headers.ContentType
            = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

        var response = await _httpClient.SendAsync(requestMessage);

        var responseStatusCode = response.StatusCode;
        var responseBody = await response.Content.ReadAsStringAsync();

        var returnedUser = JsonConvert.DeserializeObject<UserModel>(responseBody);

        return await Task.FromResult(returnedUser);

    }

    public async Task<UserModel> RegisterUserAsync(QLSX.Shared.Models.UserModel user)
    {
        user.MatKhau = EncryptionHelper.Encrypt(user.MatKhau);
        string serializedUser = JsonConvert.SerializeObject(user);

        var requestMessage = new HttpRequestMessage(HttpMethod.Post, "Users/RegisterUser");
        requestMessage.Content = new StringContent(serializedUser);

        requestMessage.Content.Headers.ContentType
            = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

        var response = await _httpClient.SendAsync(requestMessage);

        var responseStatusCode = response.StatusCode;
        var responseBody = await response.Content.ReadAsStringAsync();

        var returnedUser = JsonConvert.DeserializeObject<UserModel>(responseBody);

        return await Task.FromResult(returnedUser);
    }

    public async Task<UserModel> RefreshTokenAsync(RefreshRequest refreshRequest)
    {
        string serializedUser = JsonConvert.SerializeObject(refreshRequest);

        var requestMessage = new HttpRequestMessage(HttpMethod.Post, "Users/RefreshToken");
        requestMessage.Content = new StringContent(serializedUser);

        requestMessage.Content.Headers.ContentType
            = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

        var response = await _httpClient.SendAsync(requestMessage);

        var responseStatusCode = response.StatusCode;
        var responseBody = await response.Content.ReadAsStringAsync();

        var returnedUser = JsonConvert.DeserializeObject<UserModel>(responseBody);

        return await Task.FromResult(returnedUser);
    }

    public async Task<UserModel> GetUserByAccessTokenAsync(string accessToken)
    {
        string serializedRefreshRequest = JsonConvert.SerializeObject(accessToken);

        var requestMessage = new HttpRequestMessage(HttpMethod.Post, "Users/GetUserByAccessToken");
        requestMessage.Content = new StringContent(serializedRefreshRequest);

        requestMessage.Content.Headers.ContentType
            = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

        var response = await _httpClient.SendAsync(requestMessage);

        var responseStatusCode = response.StatusCode;
        var responseBody = await response.Content.ReadAsStringAsync();

        var returnedUser = JsonConvert.DeserializeObject<UserModel>(responseBody);

        return await Task.FromResult(returnedUser);
    }
}
