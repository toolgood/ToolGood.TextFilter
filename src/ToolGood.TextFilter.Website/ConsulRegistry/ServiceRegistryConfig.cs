/* Copyright (c) 2021 温岭城东知君软件开发工作室 All rights reserved. */
/* GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html   */
#if Consul

namespace ToolGood.TextFilter.Website.ConsulRegistry
{
    /// <summary>
    /// 服务注册节点
    /// </summary>
    public class ServiceRegistryConfig
    {
        public string RegistryAddress { get; set; }

        public string Address { set; get; }

        // 服务ID
        public string Id { get; set; }
        public string Name { get; set; }
        public int Port { set; get; }
        public string HealthCheckAddress { get; set; }


        public string GrpcId { get; set; }
        public string GrpcName { get; set; }
        public int GrpcPort { set; get; }

    }
}

#endif