using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using XKANBAN.Contxet;
using XKANBAN.Services;
using XKANBAN.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http.Features;
using System;

namespace XKANBAN
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            #region Connect Database

                services.AddDbContext<KanBanContext>(option => {
                    option.UseSqlServer(Configuration.GetConnectionString("DBConnection"));
        });
                
            #endregion

            #region IoC

                services.AddTransient<IUserService, UserService>();
                services.AddTransient<IProjectService, ProjectService>();

            #endregion

            #region Authentication

                services.AddAuthentication(option => {
                    option.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    option.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    option.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                }).AddCookie(option => {
                    option.LoginPath = "/User/Login";
                    option.LogoutPath = "/User/Logout";
                    option.ExpireTimeSpan = System.TimeSpan.FromDays(15);
                });

            #endregion
                services.Configure<FormOptions>(x => {
                    x.ValueLengthLimit = int.MaxValue;
                    x.MultipartBodyLengthLimit = int.MaxValue; // Настройка для размера загружаемых файлов
                    x.MemoryBufferThreshold = Int32.MaxValue;
                });
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
