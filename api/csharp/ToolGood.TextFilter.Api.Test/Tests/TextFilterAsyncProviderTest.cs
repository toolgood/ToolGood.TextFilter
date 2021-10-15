using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetaTest;
using ToolGood.TextFilter.Api;
using ToolGood.TextFilter.Api.Datas.Requests;

namespace ToolGood.TextFilter.Test.Tests
{
    [TestFixture]
    public class TextFilterAsyncProviderTest
    {
        [Test]
        public void TextFilter_link()
        {
            var provider = TextFilterConfig.Instance.CreateTextFilterAsyncProvider();
            var t = provider.TextFilter(new TextFilterAsyncRequest("abb", false, url: "http://localhost:5000/api/sys-Debug"));
            Assert.AreEqual(0, t.Code);
            t = provider.HtmlFilter(new TextFilterAsyncRequest("abb", false, url: "http://localhost:5000/api/sys-Debug"));
            Assert.AreEqual(0, t.Code);
            t = provider.JsonFilter(new TextFilterAsyncRequest("abb", false, url: "http://localhost:5000/api/sys-Debug"));
            Assert.AreEqual(0, t.Code);
            t = provider.MarkdownFilter(new TextFilterAsyncRequest("abb", false, url: "http://localhost:5000/api/sys-Debug"));
            Assert.AreEqual(0, t.Code);
        }

        [Test]
        public void TextReplace_link()
        {
            var provider = TextFilterConfig.Instance.CreateTextFilterAsyncProvider();
            var t = provider.TextReplace(new TextReplaceAsyncRequest("abb", '*', false, false, url: "http://localhost:5000/api/sys-Debug"));
            Assert.AreEqual(0, t.Code);
            t = provider.HtmlReplace(new TextReplaceAsyncRequest("abb", '*', false, false, url: "http://localhost:5000/api/sys-Debug"));
            Assert.AreEqual(0, t.Code);
            t = provider.JsonReplace(new TextReplaceAsyncRequest("abb", '*', false, false, url: "http://localhost:5000/api/sys-Debug"));
            Assert.AreEqual(0, t.Code);
            t = provider.MarkdownReplace(new TextReplaceAsyncRequest("abb", '*', false, false, url: "http://localhost:5000/api/sys-Debug"));
            Assert.AreEqual(0, t.Code);
        }

    }
}
