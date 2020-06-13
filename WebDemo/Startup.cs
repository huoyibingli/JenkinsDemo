using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace WebDemo
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public static IConfiguration Configuration { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMemoryCache();
            services.AddControllersWithViews();
            services.AddResponseCompression();//�����Ӧѹ��
            services.AddResponseCaching();//��Ӧ����
            services.AddMvc(options => { options.EnableEndpointRouting = false; })
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
            services.Configure<FormOptions>(x =>
            {
                x.ValueLengthLimit = int.MaxValue; //2147483647 //2G
                x.MultipartBodyLengthLimit = int.MaxValue; //2147483647;//2G
            });
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
                app.UseHsts();
            }
            //����
            //app.UseCors(corsPolicyBuilder => corsPolicyBuilder.AllowAnyMethod().AllowAnyOrigin());
            app.UseCors(builder => builder.WithMethods("POST", "GET", "HEAD", "OPTIONS").AllowAnyOrigin()
                              .WithHeaders("Authorization", "accept", "content-type", "origin"));
            //DefaultFilesOptions defaultFilesOptions = new DefaultFilesOptions();
            //defaultFilesOptions.DefaultFileNames.Clear();
            //defaultFilesOptions.DefaultFileNames.Add("Home.html");
            //app.UseDefaultFiles(defaultFilesOptions);
            app.UseStaticFiles();
            app.UseAuthorization();
            app.UseResponseCompression();//����ѹ��
            //app.UseSession();
            //app.UseMQ(); 
            app.UseMvc();
            app.Run(ctx =>
            {
                ctx.Response.Redirect("/Home/Index"); //����֧������·������index.html������ʼҳ.
                return Task.FromResult(0);
            });
        }
    }
}
