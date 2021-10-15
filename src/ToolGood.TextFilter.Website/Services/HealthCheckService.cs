/* Copyright (c) 2021 温岭城东知君软件开发工作室 All rights reserved. */
/* GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html   */
#if GRPC
using System.Threading.Tasks;
using Grpc.Core;
using Grpc.Health.V1;

namespace ToolGood.TextFilter.Website.Services
{
    public class HealthCheckService : Health.HealthBase
    {
        public override Task<HealthCheckResponse> Check(HealthCheckRequest request, ServerCallContext context)
        {
            return Task.FromResult(new HealthCheckResponse() { Status = HealthCheckResponse.Types.ServingStatus.Serving });
        }

        public override async Task Watch(HealthCheckRequest request, IServerStreamWriter<HealthCheckResponse> responseStream, ServerCallContext context)
        {
            await responseStream.WriteAsync(new HealthCheckResponse() { Status = HealthCheckResponse.Types.ServingStatus.Serving });
        }
    }
}

#endif