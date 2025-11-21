using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using System.Threading.Tasks;
using QLSX.Web.Services;
using System.Net.Http;
using Microsoft.Extensions.Caching.Memory;
using QLSX.Shared.Entities;
using System.Net.Http.Headers;
using QLSX.Shared.Models;

namespace QLSX.Web.Data
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        public ILocalStorageService _localStorageService { get; }
        public IUserService _userService { get; set; }
        public AppService _appService { get; set; }
        private readonly HttpClient _httpClient;
        private readonly IMemoryCache _memoryCache;
        public CustomAuthenticationStateProvider(ILocalStorageService localStorageService,
            IUserService userService,
            HttpClient httpClient, IMemoryCache memoryCache, AppService appService)
        {
            //throw new Exception("CustomAuthenticationStateProviderException");
            _localStorageService = localStorageService;
            _userService = userService;
            _httpClient = httpClient;
            _memoryCache = memoryCache;
            _appService = appService;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var accessToken = await _localStorageService.GetItemAsync<string>("accessToken");
            _memoryCache.Set("_Key_Token", accessToken);

            ClaimsIdentity identity;

            if (!string.IsNullOrEmpty(accessToken))
            {
                try
                {
                    UserModel user = await _userService.GetUserByAccessTokenAsync(accessToken);
                    _appService.DMDonViSuDungId = user.DMDonViSuDungId ?? 0;
                    _appService.UserId = user.Id;
                    await _localStorageService.SetItemAsync("UserLogin", user);
                    identity = GetClaimsIdentity(user);
                    _memoryCache.Set("_User_Login", user);
                    //_memoryCache.Set("_DonVi_Id", user.DMDonViSuDungId.ToString());
                    _memoryCache.Set("_User_Id", user.Id.ToString());
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", accessToken);
                }
                catch (System.Exception ex)
                {

                    identity = new ClaimsIdentity();
                }

            }
            else
            {
                identity = new ClaimsIdentity();
            }

            var claimsPrincipal = new ClaimsPrincipal(identity);
            return await Task.FromResult(new AuthenticationState(claimsPrincipal));
        }

        public async Task MarkUserAsAuthenticated(UserModel user)
        {
            await _localStorageService.SetItemAsync("accessToken", user.AccessToken);
            await _localStorageService.SetItemAsync("refreshToken", user.RefreshToken);
            await _localStorageService.SetItemAsync("UserLogin", user);

            var identity = GetClaimsIdentity(user);
            //_appService.DMDonViSuDungId = user.DMDonViSuDungId;
            _appService.UserId = user.Id;
            var claimsPrincipal = new ClaimsPrincipal(identity);
            _memoryCache.Set("_Key_Token", user.AccessToken);
            _memoryCache.Set("_User_Login", user);
            _memoryCache.Set("_DonVi_Id", user.DMDonViSuDungId.ToString());
            _memoryCache.Set("_User_Id", user.Id.ToString());
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", user.AccessToken);

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
        }

        public async Task MarkUserAsLoggedOut()
        {
            await _localStorageService.RemoveItemAsync("refreshToken");
            await _localStorageService.RemoveItemAsync("accessToken");
            await _localStorageService.RemoveItemAsync("UserLogin");

            _httpClient.DefaultRequestHeaders.Authorization = null;

            var identity = new ClaimsIdentity();
            var user = new ClaimsPrincipal(identity);
            _memoryCache.Set("_Key_Token", "");
            _memoryCache.Set("_User_Login", new User());
            _memoryCache.Set("_DonVi_Id", "");
            _memoryCache.Set("_User_Id", "");
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
        }

        private ClaimsIdentity GetClaimsIdentity(UserModel user)
        {
            var claimsIdentity = new ClaimsIdentity();

            if (user.EmailAddress != null)
            {
                if (IsAdmin(user) == "true")
                {

                }

                claimsIdentity = new ClaimsIdentity(new[]
                                {
                                    new Claim(ClaimTypes.Name, user.EmailAddress),
                                    new Claim(ClaimTypes.Role, GetRoles(user)),
                                    new Claim("IsUserEmployedBefore1990", IsUserEmployedBefore1990(user)),
                                    new Claim("IsAdmin", IsAdmin(user)),
                                    new Claim("IsTP", IsTP(user)),
                                    new Claim("IsNV", IsNV(user)),
                                    new Claim("UserId", user.Id.ToString() ),
                                    new Claim("TenantId", user.DMDonViSuDungId.ToString() ),
                                }, "apiauth_type");
            }

            return claimsIdentity;
        }

        private string IsUserEmployedBefore1990(UserModel user)
        {
            if (user.HireDate.Value.Year < 1990)
                return "true";
            else
                return "false";
        }
        private string GetRoles(UserModel user)
        {
            if (user.Quyen == 100)
                return "Admin";
            else if (user.Quyen == 99)
                return "TP";
            else return "NV";

        }
        private string IsAdmin(UserModel user)
        {
            if (user.Quyen == 100)
                return "true";
            else
                return "false";
        }
        private string IsTP(UserModel user)
        {
            if (user.Quyen == 99)
                return "true";
            else
                return "false";
        }
        private string IsNV(UserModel user)
        {
            if (user.Quyen != 100 || user.Quyen != 99)
                return "true";
            else
                return "false";
        }
    }
}
