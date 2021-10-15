using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetaTest;
using ToolGood.TextFilter.Api;
using ToolGood.TextFilter.Api.Datas.Requests;
using ToolGood.TextFilter.Api.Interfaces;

namespace ToolGood.TextFilter.Test.Tests
{
    [TestFixture]
    public class TextFilterProviderTest
    {
        [Test]
        public void TextFilter_link()
        {
            ITextFilterProvider provider = TextFilterConfig.Instance.CreateTextFilterProvider();
            var t = provider.TextFilter(new TextFilterRequest("abb", false));
            Assert.AreEqual(0, t.Code);
            t = provider.HtmlFilter(new TextFilterRequest("abb", false));
            Assert.AreEqual(0, t.Code);
            t = provider.JsonFilter(new TextFilterRequest("abb", false));
            Assert.AreEqual(0, t.Code);
            t = provider.MarkdownFilter(new TextFilterRequest("abb", false));
            Assert.AreEqual(0, t.Code);

            t = provider.TextFilter(new TextFilterRequest("习近平", false));
            Assert.AreEqual(0, t.Code);
            Assert.AreEqual("REJECT", t.RiskLevel);
        }

        [Test]
        public void TextReplace_link()
        {
            ITextFilterProvider provider = TextFilterConfig.Instance.CreateTextFilterProvider();
            var t = provider.TextReplace(new  TextReplaceRequest("abb", '*', false, false));
            Assert.AreEqual(0, t.Code);
            t = provider.HtmlReplace(new TextReplaceRequest("abb", '*', false, false));
            Assert.AreEqual(0, t.Code);
            t = provider.JsonReplace(new TextReplaceRequest("abb", '*', false, false));
            Assert.AreEqual(0, t.Code);
            t = provider.MarkdownReplace(new TextReplaceRequest("abb", '*', false, false));
            Assert.AreEqual(0, t.Code);
        }





    }
}
