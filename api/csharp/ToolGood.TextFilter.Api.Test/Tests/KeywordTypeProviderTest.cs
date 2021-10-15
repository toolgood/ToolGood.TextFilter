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
    public class KeywordTypeProviderTest
    {
        [Test]
        public void GetKeywordList()
        {
            var provider = TextFilterConfig.Instance.CreateKeywordTypeProvider();

            var list = provider.GetList();

            Assert.AreEqual(0, list.Code);
            Assert.Greater(list.Data.Count, 0);

        }
        [Test]
        public void SetKeywordType()
        {
            var provider = TextFilterConfig.Instance.CreateKeywordTypeProvider();

            var result = provider.SetKeywordType(3,0,0,0);

            Assert.AreEqual(0, result.Code);
        }




    }
}
