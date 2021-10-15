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
    public class SysProviderTest
    {
        [Test]
        public void SysProvider()
        {
            var provider = TextFilterConfig.Instance.CreateSysProvider();
            var result = provider.GCCollect();
            Assert.AreEqual(0, result.Code);

            var info = provider.Info();
            Assert.AreEqual(0, info.Code);

            var r = provider.Refresh();
            Assert.AreEqual(0, r.Code);

        }


    }
}
