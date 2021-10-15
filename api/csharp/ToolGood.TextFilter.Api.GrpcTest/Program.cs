using System;

namespace ToolGood.TextFilter.Api.GrpcTest
{
    class Program
    {
        static void Main(string[] args)
        {
            TextFilterConfig.Instance.FactoryName = "my";
            //TextFilterConfig.Instance.SetHttpClientFunc
            TextFilterConfig.Instance.TextFilterHost = "http://localhost:9191";
            TextFilterConfig.Instance.GrpcHost = "http://localhost:9192";

            PetaTest.Runner.RunMain(args);
        }
    }
}
