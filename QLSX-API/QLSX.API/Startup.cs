using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QLSX.Shared.Mapper;
using SaleAPI.Extensions;
using SaleAPI.Interfaces;
using SaleAPI.Models;
using SaleAPI.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Serilog;
using System.Net.Http;
using Microsoft.AspNetCore.SignalR;
using Sale.API.SignalR;

namespace SaleAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        readonly string OriginHost = "_myAllowSpecificOrigins";
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var baseUrlConfig = new BaseUrlConfiguration();
            Configuration.Bind(BaseUrlConfiguration.CONFIG_NAME, baseUrlConfig);
            
            services.AddControllers();
            services.AddHttpContextAccessor();
            services.AddScoped<HttpClient>();
            services.AddMvc(option => option.EnableEndpointRouting = false)
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
                .AddNewtonsoftJson(opt => opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);

            services.AddDbContext<CRMDBContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("CRMConnectStrings")));

            services.AddDbContext<MHDBContext>(options =>
                   options.UseSqlServer(Configuration.GetConnectionString("Ketoan")));

            var appSettingSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingSection);

            var jwtSection = Configuration.GetSection("JWTSettings");
            services.Configure<JWTSettings>(jwtSection);

            //to validate the token which has been sent by clients
            var appSettings = jwtSection.Get<JWTSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.SecretKey);

           
            services.AddScoped<IFileSystem, WebFileSystem>(x => new WebFileSystem($"{baseUrlConfig.WebBase}File"));
            services.AddScoped<ITenantProvider, TenantProvider>();
            services.AddScoped<IQuyenSuDungSerVice, QuyenSuDungService>();
            services.AddScoped<INhatKyService, NhatKyService>();
            services.AddScoped<IImageUtilsServices, ImageUtilsServices>();

            services.AddCors(options =>
            {
                options.AddPolicy(OriginHost, builder =>
                {
                    builder.WithOrigins(Configuration["AllowedHosts"]).AllowAnyHeader().AllowAnyMethod();
                });
            });

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false ,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.FromDays(1)
                };
            });

            services.AddSwaggerGen(gen =>
            {
                gen.SwaggerDoc("v1.0", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "API Quản lý sản xuất", Version = "v1.0" });
                gen.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header using the Bearer scheme \r\n\r\n
                    Enter 'Bearer' [space]  and then your token in the text input below 
                       \r\n\r Example: 'Bearer 1234356werty'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
                gen.AddSecurityRequirement(new OpenApiSecurityRequirement {
                    { new OpenApiSecurityScheme
                        {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme ,
                            Id= "Bearer"
                        },
                        Scheme= "oauth2",
                        Name = "Bearer",
                        In= ParameterLocation.Header
                        }, new List<string>()
                    }

                });
                gen.OperationFilter<TenantIdHeaderFilter>();
            });

            services.AddMemoryCache();

            services.AddAutoMapper(typeof(MappingProfile).Assembly);

            services.AddSignalR();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseSwagger();

            app.UseCors(builder => builder
                // .WithOrigins("https://localhost:5005")
                 //.AllowAnyOrigin()
                 .SetIsOriginAllowed(c => true)
                 .AllowAnyMethod()
                 .AllowAnyHeader()
                 .AllowCredentials());
            app.UseCors(OriginHost);

            app.UseSwaggerUI(ui =>
            {
                ui.SwaggerEndpoint("/swagger/v1.0/swagger.json", "API Quản lý sản xuất");


            });
            app.UseSerilogRequestLogging(); // <-- Add this line
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<AppSignalR>("/myHub");
            });
        }
    }
}
