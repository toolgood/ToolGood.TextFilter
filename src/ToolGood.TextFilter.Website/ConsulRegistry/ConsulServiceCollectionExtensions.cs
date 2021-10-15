/* Copyright (c) 2021 温岭城东知君软件开发工作室 All rights reserved. */
/* GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html   */
#if Consul
using System;
using System.Linq;
using System.Timers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ToolGood.TextFilter.Application;

namespace ToolGood.TextFilter.Website.ConsulRegistry
{
    public static class ConsulServiceCollectionExtensions
    {
        private static Timer _timer;

        // consul服务注册
        public static IServiceCollection AddConsulRegistry(this IServiceCollection services, IConfiguration configuration, string consulRegistryName = "ConsulRegistry")
        {
            ServiceRegistryConfig config = new ServiceRegistryConfig();
            config.RegistryAddress = configuration.GetValue<string>("ConsulRegistry:RegistryAddress");
            config.Address = configuration.GetValue<string>("ConsulRegistry:Address");

            config.Port = configuration.GetValue<int>("Ports:Http");
            config.Name = "ToolGood.TextFilter";
            config.HealthCheckAddress = $"http://{config.Address}:{config.Port}/HealthCheck";
            config.Id = $"Http:{config.Address}:{config.Port}";

            config.GrpcPort = configuration.GetValue<int>("Ports:Grpc");
            config.GrpcName = "ToolGood.TextFilter.Grpc";
            config.GrpcId = $"Grpc:{config.Address}:{config.GrpcPort}";

            _timer = new Timer(15 * 1000) { AutoReset = true };
            _timer.Elapsed += (object sender, ElapsedEventArgs e) => {
                if (SysApplication.HasGrpcLicence() == false) {
                    _timer.Interval = 90 * 1000;
                    return;
                }
                if (CheckConsul(config)) {
                    _timer.Interval = 90 * 1000;
                } else {
                    try {
                        new NConsulBuilder(new NConsul.ConsulClient(x => x.Address = new Uri(config.RegistryAddress)))
                             .AddHttpHealthCheck($"http://{config.Address}:{config.Port}/HealthCheck")
                             .RegisterService(config.Id, config.Name, config.Address, config.Port, new[] { "ToolGood.TextFilter" });
#if GRPC
                        new NConsulBuilder(new NConsul.ConsulClient(x => x.Address = new Uri(config.RegistryAddress)))
                             .AddGRPCHealthCheck($"{config.Address}:{config.GrpcPort}")
                            .RegisterService(config.GrpcId, config.GrpcName, config.Address, config.GrpcPort, new[] { "ToolGood.TextFilter.Grpc" });
#endif
                    } catch { }
                    _timer.Interval = 10 * 1000;
                }
            };
            _timer.Start();

            AppDomain.CurrentDomain.ProcessExit += (sender, e) => {
                try {
                    var _client = new NConsul.ConsulClient(x => x.Address = new Uri(config.RegistryAddress));
                    _client.Agent.ServiceDeregister(config.Id).Wait();
#if GRPC
                    _client.Agent.ServiceDeregister(config.GrpcId).Wait();
#endif
                } catch { }
            };
            return services;
        }
        private static bool CheckConsul(ServiceRegistryConfig serviceNode)
        {
            var consulClient = new NConsul.ConsulClient(x => x.Address = new Uri(serviceNode.RegistryAddress));
            try {
                var query = consulClient.Catalog.Service(serviceNode.Name);
                query.Wait();
                var queryResult = query.Result;
                return queryResult.Response.Any(q => q.ServiceID == serviceNode.Id);
            } catch {
            } finally {
                consulClient.Dispose();
            }
            return false;
        }

    }
}

#endif