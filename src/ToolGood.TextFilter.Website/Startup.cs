/* Copyright (c) 2021 温岭城东知君软件开发工作室 All rights reserved. */
/* GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html   */
using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ToolGood.TextFilter.Application;
using ToolGood.TextFilter.Website.Commons;
#if Consul
using ToolGood.TextFilter.Website.ConsulRegistry;
#endif
#if Async
using Polly;
#endif
#if GRPC
using ToolGood.TextFilter.Services;
using ToolGood.TextFilter.Website.Services;
#endif



namespace ToolGood.TextFilter.Windows
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
#if Async
            services.AddHttpClient();
            services.AddHttpClient("Polly").AddTransientHttpErrorPolicy(builder => builder.WaitAndRetryAsync(new[] {
                                    TimeSpan.FromSeconds(1),
                                    TimeSpan.FromSeconds(5),
                                    TimeSpan.FromSeconds(10),
            }));
#endif
#if GRPC
            services.AddGrpc();
#endif
#if Consul
            services.AddConsulRegistry(Configuration);
            services.AddHealthChecks();
#endif
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IConfiguration configuration,IWebHostEnvironment env)
        {
            Bootstrap.Init(app.ApplicationServices);

            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }
            app.UseHsts();
#if DEBUG
            app.UseStaticFiles();
#endif
            app.UseRouting();
#if Consul
            app.UseHealthChecks("/HealthCheck");
#endif
            app.UseEndpoints(endpoints => {
#if GRPC
                if (SysApplication.HasGrpcLicence()) {
                    endpoints.MapGrpcService<HealthCheckService>();
                    endpoints.MapGrpcService<TextFilterService>();
                }
#endif
                endpoints.MapControllers();
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
