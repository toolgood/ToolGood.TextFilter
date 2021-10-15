/* Copyright (c) 2021 温岭城东知君软件开发工作室 All rights reserved. */
/* GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html   */
using System;
using System.Threading;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ToolGood.TextFilter.Windows
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ThreadPool.SetMinThreads(1000, 1000);
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            CreateHostBuilder(args).Build().Run();
        }
 

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Microsoft.Extensions.Hosting.Host.CreateDefaultBuilder(args)
            .ConfigureLogging(config => {
                config.SetMinimumLevel(LogLevel.None);
                //config.ClearProviders();//去掉默认添加的日志提供程序
            })
            .ConfigureWebHostDefaults(webBuilder => {
                if (HasKestrelSettings("appsettings.json") == false) {
                    var port = GetAppSettings<int>("appsettings.json", "Ports:Http");
                    var grpcPort = GetAppSettings<int>("appsettings.json", "Ports:Grpc");
                    if (port == 0) { port = 9191; }
                    if (grpcPort == 0) { grpcPort = 9192; }
                    if (grpcPort == port) { grpcPort = port + 1; }

                    webBuilder.ConfigureKestrel(options => {
                        //options.ListenLocalhost(port, o => o.Protocols = HttpProtocols.Http1);
                        //options.ListenLocalhost(grpcPort, o => o.Protocols = HttpProtocols.Http2);
                        options.ListenAnyIP(port, o => o.Protocols = HttpProtocols.Http1);
                        options.ListenAnyIP(grpcPort, o => o.Protocols = HttpProtocols.Http2);
                    });
                }
                webBuilder.UseKestrel(options => {
                    options.Limits.MaxRequestBodySize = null;
                    //options.Limits.MaxRequestLineSize = 302768;
                    options.Limits.MaxRequestBufferSize = null;
                });
                webBuilder.UseStartup<Startup>();
            });


        private static bool HasKestrelSettings(string fileName)
        {
            var directory = AppContext.BaseDirectory.Replace("\\", "/");

            var filePath = $"{directory}/{fileName}";
            if (!System.IO.File.Exists(filePath)) {
                var length = directory.IndexOf("/bin");
                filePath = $"{directory.Substring(0, length)}/{fileName}";
            }

            IConfiguration configuration;
            var builder = new ConfigurationBuilder();

            builder.AddJsonFile(filePath, false, false);

            configuration = builder.Build();
            var t = configuration.GetValue<object>("Kestrel");
            return t != null;
        }

        private static T GetAppSettings<T>(string fileName, string key)
        {
            var directory = AppContext.BaseDirectory.Replace("\\", "/");

            var filePath = $"{directory}/{fileName}";
            if (!System.IO.File.Exists(filePath)) {
                var length = directory.IndexOf("/bin");
                filePath = $"{directory.Substring(0, length)}/{fileName}";
            }

            IConfiguration configuration;
            var builder = new ConfigurationBuilder();

            builder.AddJsonFile(filePath, false, false);
            configuration = builder.Build();
            return configuration.GetValue<T>(key);
        }
    }
}
