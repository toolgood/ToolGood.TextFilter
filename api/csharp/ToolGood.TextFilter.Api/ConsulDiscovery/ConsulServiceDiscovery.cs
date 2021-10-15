#if Consul
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Consul;

namespace ToolGood.TextFilter.Api.ConsulDiscovery
{
    /// <summary>
    /// consul服务发现实现
    /// </summary>
    public static class ConsulServiceDiscovery
    {
        private static Random random = new Random();

        public static async Task<List<string>> Discovery(string registryAddress, string serviceName)
        {
            try {
                // 1、创建consul客户端连接
                var consulClient = new ConsulClient(configuration => {
                    configuration.Address = new Uri(registryAddress);
                });

                // 2、consul查询服务,根据具体的服务名称查询
                var queryResult = await consulClient.Catalog.Service(serviceName);
                consulClient.Dispose();

                // 3、将服务进行拼接
                var list = new List<string>();
                foreach (var service in queryResult.Response) {
                    list.Add($"http://{service.ServiceAddress}:{service.ServicePort}");
                }
                return list;
            } catch { }
            return new List<string>();
        }

        /// <summary>
        /// 随机负载均衡
        /// </summary>
        /// <param name="serviceName"></param>
        /// <returns></returns>
        public async static Task<string> DiscoveryOne(string registryAddress, string serviceName)
        {
            var consulClient = new ConsulClient(configuration => { configuration.Address = new Uri(registryAddress); });
            var queryResult = await consulClient.Catalog.Service(serviceName);
            consulClient.Dispose();

            if (queryResult.Response == null || queryResult.Response.Length == 0) { return null; }
            if (queryResult.Response.Length == 1) {
                return $"http://{queryResult.Response[0].ServiceAddress}:{queryResult.Response[0].ServicePort}";
            }
            var index = random.Next(queryResult.Response.Length);
            return $"http://{queryResult.Response[index].ServiceAddress}:{queryResult.Response[index].ServicePort}";
        }


    }
}

#endif