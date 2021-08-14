using System;
using System.Buffers;
using System.Linq;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.ObjectPool;
using Microsoft.Extensions.Options;
using NetCoreWebGoat.Config;
using NetCoreWebGoat.Data;
using NetCoreWebGoat.Repositories;

namespace NetCoreWebGoat
{
    public class Startup
    {
        public AppConfig Config { get; }
        public Startup(IConfiguration configuration)
        {
            Config = new AppConfig();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews()
                .AddNewtonsoftJson()
                .AddRazorRuntimeCompilation();

            services.AddOptions<MvcOptions>()
                .PostConfigure<IOptions<JsonOptions>, IOptions<MvcNewtonsoftJsonOptions>, ArrayPool<char>, ObjectPoolProvider, ILoggerFactory>(
            (mvcOptions, jsonOpts, newtonJsonOpts, charPool, objectPoolProvider, loggerFactory) =>
            {
                var formatter = mvcOptions.InputFormatters.OfType<NewtonsoftJsonInputFormatter>().First(i => i.SupportedMediaTypes.Contains("application/json"));
                formatter.SupportedMediaTypes.Add(Microsoft.Net.Http.Headers.MediaTypeHeaderValue.Parse("application/csp-report"));
                mvcOptions.InputFormatters.RemoveType<NewtonsoftJsonInputFormatter>();
                mvcOptions.InputFormatters.Add(formatter);
            });

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.Cookie.SecurePolicy = Microsoft.AspNetCore.Http.CookieSecurePolicy.None;
                    options.Cookie.HttpOnly = false;
                    options.Cookie.SameSite = Microsoft.AspNetCore.Http.SameSiteMode.None;
                    options.Cookie.Name = ".NetCoreWebGoatCookie";
                    options.ExpireTimeSpan = TimeSpan.FromMinutes(Config.CookieExpiresInMinutes);
                    options.LoginPath = "/Account/Login";
                });

            services.AddLogging(options => options.AddSimpleConsole(c => c.TimestampFormat = "[yyyy-MM-dd HH:mm:ss] "));

            services.AddSingleton(Config);

            new Database(Config).Initialize();

            services.AddScoped<UserRepository>();
            services.AddScoped<PostRepository>();
            services.AddScoped<CspRepository>();
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
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            }

            app.UseHsts();
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.Use(async (ctx, next) =>
            {
                if (!string.IsNullOrEmpty(Config.CspHttpHeader))
                    ctx.Response.Headers.Add("Content-Security-Policy", Config.CspHttpHeader);
                await next();
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}