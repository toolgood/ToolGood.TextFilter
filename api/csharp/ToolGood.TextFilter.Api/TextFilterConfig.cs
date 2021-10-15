using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using ToolGood.TextFilter.Api.Impl;
using ToolGood.TextFilter.Api.Interfaces;
#if Async
using Grpc.Net.Client;
using ToolGood.TextFilter.Api.Grpcs;
#endif
#if Consul
using ToolGood.TextFilter.Api.ConsulDiscovery;
#endif



namespace ToolGood.TextFilter.Api
{
    public class TextFilterConfig
    {

        private const string HttpServiceName = "ToolGood.TextFilter";
        private const string GrpcServiceName = "ToolGood.TextFilter.Grpc";
        public static int OvertimeSeconds = 10;

        private static TextFilterConfig _textFilterConfig;
        public static TextFilterConfig Instance {
            get {
                if (_textFilterConfig == null) {
                    _textFilterConfig = new TextFilterConfig();
                }
                return _textFilterConfig;
            }
        }



        #region TextFilterHost
        private string _textFilterHost;
        private string _tempTextFilterHost;
        private DateTime _maxTemp_Text;

        /// <summary>
        /// 不设置  取Consul
        /// </summary>
        public string TextFilterHost {
            set {
                _textFilterHost = value;
            }
            get {
#if Consul
                if (_textFilterHost == null) {
                    if (string.IsNullOrEmpty(_tempTextFilterHost) || DateTime.Now > _maxTemp_Text) {
                        var task = ConsulServiceDiscovery.DiscoveryOne(ConsulAddress, HttpServiceName);
                        task.Wait();
                        _tempTextFilterHost = task.Result;
                        _maxTemp_Text = DateTime.Now.AddSeconds(OvertimeSeconds);
                    }
                    return _tempTextFilterHost;
                }
#endif
                return _textFilterHost;
            }
        }
        #endregion

        public Func<HttpClient> SetHttpClientFunc { private get; set; }
        public IHttpClientFactory HttpClientFactory { private get; set; }
        public string FactoryName { private get; set; }
        internal HttpClient CreateHttpClient()
        {
            if (HttpClientFactory != null) {
                return HttpClientFactory.CreateClient(FactoryName ?? "");
            }
            if (SetHttpClientFunc != null) {
                return SetHttpClientFunc();
            }
            return new HttpClient();
        }


#if GRPC

        #region GrpcHost
        private string _grpcHost;
        private string _tempGrpcHost;
        private DateTime _maxTemp_Grpc;
        public string GrpcHost {
            set {
                _grpcHost = value;
            }
            get {
#if Consul
                if (_grpcHost == null) {

                    if (string.IsNullOrEmpty(_tempGrpcHost) || DateTime.Now > _maxTemp_Grpc) {
                        var task = ConsulServiceDiscovery.DiscoveryOne(ConsulAddress, HttpServiceName);
                        task.Wait();
                        _tempGrpcHost = task.Result;
                        _maxTemp_Grpc = DateTime.Now.AddSeconds(OvertimeSeconds);
                    }
                    return _tempGrpcHost;
                }
#endif
                return _grpcHost;
            }
        }
        #endregion

        private bool isCreate;
        public TextFilterGrpcProvider CreateTextFilterGrpcClient()
        {
            if (isCreate == false) {
                AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
                isCreate = true;
            }
            return new TextFilterGrpcProvider(GrpcHost);
        }
        public TextFilterGrpcAsyncProvider CreateTextFilterGrpcAnsycClient()
        {
            if (isCreate == false) {
                AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
                isCreate = true;
            }
            return new TextFilterGrpcAsyncProvider(GrpcHost);
        }
 
#endif


        #region CreateKeywordProvider  CreateKeywordTypeProvider CreateSysProvider
        public IKeywordProvider CreateKeywordProvider()
        {
            return new KeywordProvider(this);
        }

        public IKeywordTypeProvider CreateKeywordTypeProvider()
        {
            return new KeywordTypeProvider(this);
        }

        public ISysProvider CreateSysProvider()
        {
            return new SysProvider(this);
        }
        #endregion

        #region TextFilterProvider

        public ITextFilterProvider CreateTextFilterProvider()
        {
            return new TextFilterProvider(this);
        }

        #endregion

#if Async
        public ITextFilterAsyncProvider CreateTextFilterAsyncProvider()
        {
            return new TextFilterAsyncProvider(this);
        }
#endif


#if Consul

        public string ConsulAddress { private get; set; } = "http://localhost:8500";


        public Task<List<string>> GetServiceUrls(ServiceUrlType urlType)
        {
            if (urlType == ServiceUrlType.Grpc) {
                return ConsulServiceDiscovery.Discovery(ConsulAddress, GrpcServiceName);
            }
            return ConsulServiceDiscovery.Discovery(ConsulAddress, HttpServiceName);
        }

#endif
    }

}
