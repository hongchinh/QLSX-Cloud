using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using QLSX.Web.Data;
using System.Net.Http;
using Microsoft.AspNetCore.Components.Authorization;
using Blazored.LocalStorage;
using QLSX.Web.Services;
using QLSX.Web.Handlers;
using FoolProof.Core;
using Toolbelt.Blazor.Extensions.DependencyInjection;
using QLSX.Shared;
using Blazored.Toast;
using QLSX.Shared.Interfaces;
using QLSX.Shared.Services;
using Microsoft.AspNetCore.Http;
using QLSX.Shared.Mapper;
using System.Text;
using MudBlazor.Services;
using Serilog;
using MudBlazor;
using QLSX.Shared.Models;
using Blazored.Modal;
using QLSX.Web.Bots;
using MudBlazor.Extensions;
//using IgniteUI.Blazor.Controls;


namespace QLSX.Web
{
    public class Startup
    {
        public static string WebRootPath { get; private set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
          

            var baseUrlConfig = new BaseUrlConfiguration();
            Configuration.Bind(BaseUrlConfiguration.CONFIG_NAME, baseUrlConfig);

                    
            services.AddScoped<BaseUrlConfiguration>(sp => baseUrlConfig);
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddDistributedMemoryCache();

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromSeconds(10);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddMvc();
             
            services.AddMemoryCache();
            services.AddControllersWithViews();
 
            
            var appSettingSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingSection);

            services.AddAuthorizationCore();

            services.AddTransient<ValidateHeaderHandler>();
            services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
            
            var jwtSection = Configuration.GetSection("JWTSettings");
            services.Configure<QLSX.Shared.Models.JWTSettings>(jwtSection);

            //to validate the token which has been sent by clients
            var appSettings = jwtSection.Get<QLSX.Shared.Models.JWTSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.SecretKey);
            
            services.AddScoped<HttpClient>();
            services.AddScoped<IApiWrapperServices, ApiWrapperServices>();

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
            });

            services.AddBlazoredLocalStorage();
            services.AddHttpClient<IUserService, UserService>();

            services.AddHttpClient<ICustomersService<Customer>, CustomersService<Customer>>()
                   .AddHttpMessageHandler<ValidateHeaderHandler>();

            services.AddHttpClient<ICustomersService<CustomerUpdate>, CustomersService<CustomerUpdate>>()
                 .AddHttpMessageHandler<ValidateHeaderHandler>();
            services.AddHttpClient<ICustomersService<TongHopCongNo>, CustomersService<TongHopCongNo>>()
               .AddHttpMessageHandler<ValidateHeaderHandler>();

            services.AddHttpClient<ICustomersService<TongHopDongTien>, CustomersService<TongHopDongTien>>()
               .AddHttpMessageHandler<ValidateHeaderHandler>();

            services.AddHttpClient<ICustomersService<BangLuong>, CustomersService<BangLuong>>()
             .AddHttpMessageHandler<ValidateHeaderHandler>();
        
            services.AddHttpClient<IRoleService<Role>, RoleService<Role>>();


            services.AddHttpClient<INotificationService<Noti>, NotificationService<Noti>>();


            services.AddHttpClient<IDMDonViSuDungUserService<QLSX.Shared.Models.DMDonViSuDungUser>, DMDonViSuDungUserService<QLSX.Shared.Models.DMDonViSuDungUser>>();
            services.AddHttpClient<IDMDonViSuDungUserService<UserVM>, DMDonViSuDungUserService<UserVM>>();


            services.AddHttpClient<IPermissionRegionsService<PermissionRegion>, PermissionRegionsService<PermissionRegion>>();
            services.AddHttpClient<IPermissionDepartmentsService<PermissionDepartment>, PermissionDepartmentsService<PermissionDepartment>>();
            services.AddHttpClient<IDMPhongBanService<QLSX.Shared.Models.DMPhongBan>, DMPhongBanService<QLSX.Shared.Models.DMPhongBan>>();
            services.AddHttpClient<IRegionSalesService<RegionSale>, RegionSalesService<RegionSale>>();

            services.AddHttpClient<IEmloyeeService<UserModel>, EmloyeeService<UserModel>>();
            services.AddHttpClient<IEmloyeeService<UserUpdateRequest>, EmloyeeService<UserUpdateRequest>>();

            services.AddHttpClient<ICapNhatTonKhoService, CapNhatTonKhoService>();

            //services.AddBlazoredLocalStorageAsSingleton();
            services.AddHttpClient<INhapXuatsService<TraCuuNhapXuatAll>, NhapXuatsService<TraCuuNhapXuatAll>>();
            services.AddHttpClient<INhapXuatsService<NhapXuatModel>, NhapXuatsService<NhapXuatModel>>().AddHttpMessageHandler<ValidateHeaderHandler>();
            services.AddHttpClient<INhapXuatsService<PhieuNhapXuatAllModel>, NhapXuatsService<PhieuNhapXuatAllModel>>().AddHttpMessageHandler<ValidateHeaderHandler>();
            services.AddHttpClient<INhapXuatTonCuonsService<NhapXuatTonCuonModel>, NhapXuatTonCuonsService<NhapXuatTonCuonModel>>();
            services.AddHttpClient<IDieuChuyensService<DieuChuyen>, DieuChuyensService<DieuChuyen>>().AddHttpMessageHandler<ValidateHeaderHandler>();
            services.AddHttpClient<IDieuChuyensService<NhapXuatModel>, DieuChuyensService<NhapXuatModel>>().AddHttpMessageHandler<ValidateHeaderHandler>();
            services.AddHttpClient<IDMTinhThanhService<QLSX.Shared.Models.DanhMucTinhThanhModel>, DMTinhThanhService<QLSX.Shared.Models.DanhMucTinhThanhModel>>();
            services.AddHttpClient<IDMHangHoaService<DanhMucHangHoaModel>, DMHangHoaService<DanhMucHangHoaModel>>();
            services.AddHttpClient<IDMHangHoaTonCuonService<DanhMucHangHoaTonCuonModel>, DMHangHoaTonCuonService<DanhMucHangHoaTonCuonModel>>();
            services.AddHttpClient<IDMKhachHangService<DanhMucKhachHangModel>, DMKhachHangService<DanhMucKhachHangModel>>();
            services.AddHttpClient<IDMHinhThucTTService<DanhMucHinhThucTTModel>, DMHinhThucTTService<DanhMucHinhThucTTModel>>();
            services.AddHttpClient<IDMTinhTrangService<QLSX.Shared.Models.DMTinhTrangModel>, DMTinhTrangService<QLSX.Shared.Models.DMTinhTrangModel>>();
            services.AddHttpClient<INhatKyService<NhatKyModel>, NhatKyService<NhatKyModel>>();

            services.AddHttpClient<IDonDatHangsService<DonDatHangModel>, DonDatHangsService<DonDatHangModel>>().AddHttpMessageHandler<ValidateHeaderHandler>();
            services.AddHttpClient<IDonDatHangsService<PhieuNhapXuatAllModel>, DonDatHangsService<PhieuNhapXuatAllModel>>().AddHttpMessageHandler<ValidateHeaderHandler>();


            //tra cuuu
            services.AddHttpClient<ITraCuuService, TraCuuService>();

            // thongke
            services.AddHttpClient<IThongKeService, ThongKeService>();

            services.AddHttpClient<IDMNhomHangService<DanhMucNhomHangModel>, DMNhomHangService<DanhMucNhomHangModel>>();
            services.AddHttpClient<IDMChungLoaiService<DanhMucChungLoaiModel>, DMChungLoaiService<DanhMucChungLoaiModel>>();
            services.AddHttpClient<IDMDoDayService<DanhMucDoDayModel>, DMDoDayService<DanhMucDoDayModel>>();
            services.AddHttpClient<IDMKieuSongService<DanhMucKieuSongModel>, DMKieuSongService<DanhMucKieuSongModel>>();
            services.AddHttpClient<IDMLoaiTonService<DanhMucLoaiTonModel>, DMLoaiTonService<DanhMucLoaiTonModel>>();
            services.AddHttpClient<IDMMauSacService<DanhMucMauSacModel>, DMMauSacService<DanhMucMauSacModel>>();
            services.AddHttpClient<IDMTinhGiaService<QLSX.Shared.Models.DMTinhGia>, DMTinhGiaService<QLSX.Shared.Models.DMTinhGia>>();
            services.AddHttpClient<IDMNhomKhachHangService<DanhMucNhomKhachHangModel>, DMNhomKhachHangService<DanhMucNhomKhachHangModel>>();
            services.AddHttpClient<IDMKhoHangService<DanhMucKhoHangModel>, DMKhoHangService<DanhMucKhoHangModel>>();
            services.AddHttpClient<IDMKhoanThuService<DanhMucKhoanThuModel>, DMKhoanThuService<DanhMucKhoanThuModel>>();
            services.AddHttpClient<IDMKhoanChiService<DanhMucKhoanChiModel>, DMKhoanChiService<DanhMucKhoanChiModel>>();
            services.AddHttpClient<IDMLoaiTienService<DanhMucLoaiTienModel>, DMLoaiTienService<DanhMucLoaiTienModel>>();
            services.AddHttpClient<IThuChiService<ThuChiModel>, ThuChiService<ThuChiModel>>().AddHttpMessageHandler<ValidateHeaderHandler>();
           
            services.AddHttpClient<ISoDuHangHoaService<QLSX.Shared.Models.SoDuHangHoa>, SoDuHangHoaService<QLSX.Shared.Models.SoDuHangHoa>>();
            services.AddHttpClient<ISoDuCongNoService<QLSX.Shared.Models.SoDuCongNo>, SoDuCongNoService<QLSX.Shared.Models.SoDuCongNo>>();
            services.AddHttpClient<ISoDuLoaiTienService<QLSX.Shared.Models.SoDuLoaiTien>, SoDuLoaiTienService<QLSX.Shared.Models.SoDuLoaiTien>>();

            services.AddHttpClient<IDMSoCTService<QLSX.Shared.Models.DanhMucSoChungTuModel>, DMSoCTService<QLSX.Shared.Models.DanhMucSoChungTuModel>>();
          
            
            services.AddHttpClient<IBaoCaoService<QLSX.Shared.Models.BaoCao>, BaoCaoService<QLSX.Shared.Models.BaoCao>>();

            services.AddHttpClient<IReportService, ReportService>();
            services.AddHttpClient<ISettingService<QLSX.Shared.Models.SettingModel>, SettingService<QLSX.Shared.Models.SettingModel>>();
            services.AddHttpClient<IPhanQuyenSuDungService<QLSX.Shared.Models.QuyenSuDungModel>, PhanQuyenSuDungService<QLSX.Shared.Models.QuyenSuDungModel>>();


            services.AddHttpClient<IImportDataService<QLSX.Shared.Models.InformationClumns>, ImportDataService<QLSX.Shared.Models.InformationClumns>>();

            services.AddScoped<AppService>();

            services.AddHttpClientInterceptor();
            services.AddScoped<HttpInterceptorService>();

            services.AddAuthorization(options =>
            {
                options.AddPolicy("SeniorEmployee", policy =>
                    policy.RequireClaim("IsUserEmployedBefore1990", "true"));
            });

            services.AddFoolProof();

            services.AddBlazoredToast();

            services.AddAutoMapper(typeof(MappingProfile).Assembly);

            services.AddMudServices(config =>
            {
                config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.BottomLeft;

                config.SnackbarConfiguration.PreventDuplicates = false;
                config.SnackbarConfiguration.NewestOnTop = false;
                config.SnackbarConfiguration.ShowCloseIcon = true;
                config.SnackbarConfiguration.VisibleStateDuration = 10000;
                config.SnackbarConfiguration.HideTransitionDuration = 500;
                config.SnackbarConfiguration.ShowTransitionDuration = 500;
                config.SnackbarConfiguration.SnackbarVariant = Variant.Filled;
            });

            services.ConfigureApplicationCookie(options => {
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(5);
                options.LoginPath = "login";
                options.LogoutPath = "logout";
                options.AccessDeniedPath = "404";
            });

            services.AddBlazoredModal();

            services.AddLocalization();

            services.AddMudServicesWithExtensions();


            //services.AddIgniteUIBlazor(
            //    typeof(IgbInputModule),
            //    typeof(IgbPropertyEditorPanelModule),
            //    typeof(IgbGridModule),
            //     typeof(IgbDataGridModule),
            //     typeof(IgbGridColumnOptionsModule)
            //);
           
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            //var supportedCultures = new[]
            //{
            //   new CultureInfo("vi-VN"),

            //};

            //app.UseRequestLocalization(new RequestLocalizationOptions
            //{
            //    DefaultRequestCulture = new RequestCulture("vi-VN"),
            //    SupportedCultures = supportedCultures,
            //    SupportedUICultures = supportedCultures
            //});

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            var appSettingSection = Configuration.GetSection("AppSettings");
            app.UseAuthentication();

            app.UseCors("CorsPolicy");

            app.UseAuthorization();

            app.UseSession();

            app.UseSerilogRequestLogging(); // <-- Add this line

            app.UseRequestLocalization("vi-VN");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
                //endpoints.MapHub<ChatHub>("/chatHub");
                //endpoints.MapHub<ClockHub>("/ClockHub");
                // endpoints.MapHub<WeatherHub>("/hubs/weather");
                endpoints.MapHub<SaleWebChatHub>(SaleWebChatHub.HubUrl);
                endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            });

            

           // app.UseRequestLocalization("vi-VN");


            WebRootPath = env.WebRootPath;
        }
    }
}
