using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using PointOfSale.Server.Data;
using PointOfSale.Server.Models;
using PointOfSale.BLL.BaseObjects;
using PointOfSale.BLL.Contexts;
using Radzen;
using System.IO;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Mvc.Authorization;
using Newtonsoft.Json;
using PointOfSale.DAL.Domains;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.Cookies;
using System;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

namespace PointOfSale.Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<POSContext>(options =>
          options.UseSqlServer(
              Configuration.GetConnectionString("DefaultConnection")), ServiceLifetime.Transient);
            //  services.AddDbContext<ApplicationDbContext>(options =>
            //options.UseLazyLoadingProxies().UseSqlServer(
            //    Configuration.GetConnectionString("DefaultConnection")), ServiceLifetime.Transient);
            services.AddUnitOfWork<POSContext>();
            services.AddBusinessServices();
            services.AddScoped<DialogService>();
            services.AddScoped<NotificationService>();
            services.AddScoped<TooltipService>();
            services.AddBlazoredLocalStorage();
            services.AddDatabaseDeveloperPageExceptionFilter();
            services.AddHttpContextAccessor();
            services.AddLocalization();

            services.AddIdentity<SahlUserIdentity, IdentityRole>(options =>
            {
                options.User.RequireUniqueEmail = false;
                options.Password.RequireDigit = false;
                options.Password.RequiredUniqueChars = 0;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;

            }).AddEntityFrameworkStores<POSContext>().AddDefaultTokenProviders();
            services.Configure<CookieAuthenticationOptions>(opt =>
            {
                opt.LoginPath = new PathString("/login");
            });
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = true,
                            ValidateAudience = true,
                            ValidateLifetime = true,
                            ValidateIssuerSigningKey = true,
                            ValidIssuer = Configuration["JwtIssuer"],
                            ValidAudience = Configuration["JwtAudience"],
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JwtSecurityKey"]))
                        };
                        options.Events = new JwtBearerEvents
                        {
                            OnAuthenticationFailed = context =>
                            {
                                if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                                {
                                    context.Response.Headers.Add("Token-Expired", "true");
                                }
                                return Task.CompletedTask;
                            }
                        };
                    });
            //services.AddIdentityServer()
            //    .AddApiAuthorization<SahlUserIdentity, POSContext>();

            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                Formatting = Formatting.Indented
            };
            services.AddAutoMapper();
            services.AddAuthentication()
                .AddIdentityServerJwt();
            var jwt_key = Configuration.GetSection("JwtSecurityKey").Value;
            var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwt_key));
            var tokenValidationParameters = new TokenValidationParameters
            {
                // The signing key must match!
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = signingKey,

                // Validate the JWT Issuer (iss) claim
                ValidateIssuer = true,
                ValidIssuer = Configuration.GetSection("JwtIssuer").Value,

                // Validate the JWT Audience (aud) claim
                ValidateAudience = true,
                ValidAudience = Configuration.GetSection("JwtAudience").Value,

                // Validate the token expiry
                ValidateLifetime = true,

                // If you want to allow a certain amount of clock drift, set that here:
                ClockSkew = TimeSpan.Zero
            };
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
            {
                int minute = 60;
                int hour = minute * 60;
                int day = hour * 24;
                int week = day * 7;
                int year = 365 * day;

                options.LoginPath = "/login";
                options.Cookie.IsEssential = true;
                options.SlidingExpiration = true;
                options.ExpireTimeSpan = TimeSpan.FromSeconds(day / 2);

                options.Cookie.Name = "access_token";

                options.TicketDataFormat = new CustomJwtDataFormat(
                    SecurityAlgorithms.HmacSha256,
                    tokenValidationParameters);
            });
            services.AddControllersWithViews(options =>
            {
                options.Filters.Add(new AuthorizeFilter(new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build()));
            }).AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                options.SerializerSettings.Formatting = Formatting.Indented;
            }

        );
            services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
                app.UseWebAssemblyDebugging();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"StaticFiles")),
                RequestPath = new PathString("/StaticFiles")
            });
            app.UseRouting();

            //    app.UseIdentityServer();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
                endpoints.MapFallbackToFile("index.html");
            });
        }
    }
}
