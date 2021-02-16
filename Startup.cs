using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using QuizWebApplication.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuizWebApplication.EntityFramework;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace QuizWebApplication
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //https://developers.onelogin.com/blog/how-to-use-openid-connect-authentication-with-dotnet-core
            //https://manage.auth0.com/dashboard/eu/jjdevine1984/applications/LJQNyfrrxd4xfefDckiMIai1P7eyvjT5/settings

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
            })
            .AddCookie()
            .AddOpenIdConnect(options =>
            {
                //       options.ClientId = "LJQNyfrrxd4xfefDckiMIai1P7eyvjT5";
                //     options.ClientSecret = "yLJ_tr_Efg1k42itXp871HnhqZsjh1OZJDw9TfAeT_Dns51Sqi2B6TOC1iHhrG01";
                //      options.Authority = "https://jjdevine1984.eu.auth0.com/";
                options.ClientId = Configuration.GetValue<String>("IdP_ClientId");
                options.ClientSecret = Configuration.GetValue<String>("IdP_ClientSecret");
                options.Authority = Configuration.GetValue<String>("IdP_Authority");
                options.ResponseType = OpenIdConnectResponseType.Code;
                options.GetClaimsFromUserInfoEndpoint = true;
            });

            services.AddRazorPages();
            services.AddSession();

            services.AddTransient<DatabaseService>();
            services.AddTransient<IQuizRepository, QuizRepository>();

            services.AddControllers();

            services.AddMvc().AddRazorPagesOptions(options => {
                options.Conventions.AddPageRoute("/QuizPages/QuizSelect", "");
            }).SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            services.AddDbContext<QuizContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("QuizWebApplicationContext")));
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
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

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseSession();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
        //        endpoints.MapGet("/api", (context) =>
        //        {
        //            var generatedValue = app.ApplicationServices.GetService<JDService>().getJDServiceValue();
        //            return context.Response.WriteAsync(generatedValue);
        //        });
            });
        }
    }
}
