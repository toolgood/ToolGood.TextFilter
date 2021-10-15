/* Copyright (c) 2021 温岭城东知君软件开发工作室 All rights reserved. */
/* GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html   */
#if Consul
using System;
using System.Collections.Generic;
using NConsul;

namespace ToolGood.TextFilter.Website.ConsulRegistry
{
    public class NConsulBuilder
    {
        private readonly ConsulClient _client;
        private readonly List<AgentServiceCheck> _checks = new List<AgentServiceCheck>();

        public NConsulBuilder(ConsulClient client)
        {
            _client = client;
        }
        public NConsulBuilder AddHealthCheck(AgentServiceCheck check)
        {
            _checks.Add(check);
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="timeout">unit: second</param>
        /// <param name="interval">check interval. unit: second</param>
        /// <returns></returns>
        public NConsulBuilder AddHttpHealthCheck(string url, int timeout = 10, int interval = 10)
        {
            _checks.Add(new AgentServiceCheck() {
                DeregisterCriticalServiceAfter = TimeSpan.FromSeconds(20),
                Interval = TimeSpan.FromSeconds(interval),
                HTTP = url,
                Timeout = TimeSpan.FromSeconds(timeout)
            });
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="endpoint">GPRC service address.</param>
        /// <param name="grpcUseTls"></param>
        /// <param name="timeout">unit: second</param>
        /// <param name="interval">check interval. unit: second</param>
        /// <returns></returns>
        public NConsulBuilder AddGRPCHealthCheck(string endpoint, bool grpcUseTls = false, int timeout = 10, int interval = 10)
        {
            _checks.Add(new AgentServiceCheck() {
                DeregisterCriticalServiceAfter = TimeSpan.FromSeconds(20),
                Interval = TimeSpan.FromSeconds(interval),
                GRPC = endpoint,
                GRPCUseTLS = grpcUseTls,
                Timeout = TimeSpan.FromSeconds(timeout)
            });
            return this;
        }

        public void RegisterService(string name, string host, int port, string[] tags)
        {
            var registration = new AgentServiceRegistration() {
                Checks = _checks.ToArray(),
                ID = Guid.NewGuid().ToString(),
                Name = name,
                Address = host,
                Port = port,
                Tags = tags
            };

            _client.Agent.ServiceRegister(registration).Wait();

            AppDomain.CurrentDomain.ProcessExit += (sender, e) => {
                _client.Agent.ServiceDeregister(registration.ID).Wait();
            };
        }
        public void RegisterService(string id, string name, string host, int port, string[] tags)
        {
            var registration = new AgentServiceRegistration() {
                Checks = _checks.ToArray(),
                ID = id,
                Name = name,
                Address = host,
                Port = port,
                Tags = tags
            };

            _client.Agent.ServiceRegister(registration).Wait();
        }


        /// <summary>
        /// 移除服务
        /// </summary>
        /// <param name="serviceId"></param>
        public void Deregister(string serviceId)
        {
            _client?.Agent?.ServiceDeregister(serviceId).GetAwaiter().GetResult();
        }

    }

}

#endif