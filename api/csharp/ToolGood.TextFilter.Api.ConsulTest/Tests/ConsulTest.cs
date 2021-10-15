using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetaTest;
using ToolGood.TextFilter.Api;

namespace ToolGood.TextFilter.Test.Tests
{
    [TestFixture]
    public class ConsulTest
    {

        [Test]
        public void GetServerName()
        {
            var urlsTask = TextFilterConfig.Instance.GetServiceUrls( ServiceUrlType.Http);
            urlsTask.Wait();
            var urls = urlsTask.Result;
            Assert.Greater(urls.Count, 0);
            Assert.AreEqual("http://127.0.0.1:9191", urls[0]);
        }

        [Test]
        public void Test_Http()
        {
            var provider = TextFilterConfig.Instance.CreateTextFilterProvider();
            var result = provider.TextFilter(new Api.Datas.Requests.TextFilterRequest("abb"));
            Assert.AreEqual(result.Code, 0);
        }

        [Test]
        public void Test_Grpc()
        {
            var provider = TextFilterConfig.Instance.CreateTextFilterGrpcClient();
            var result = provider.TextFilter(new Api.Datas.Requests.TextFilterRequest("abb"));
            Assert.AreEqual(result.Code, 0);
        }



    }
}
