using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetaTest;
using ToolGood.TextFilter.Api;
using ToolGood.TextFilter.Api.Datas.Requests;

namespace ToolGood.TextFilter.Test.GrpcTests
{
    [TestFixture]
    public class TextFilterGrpcTest
    {

        [Test]
        public void TextFilter_link()
        {
            var provider = TextFilterConfig.Instance.CreateTextFilterGrpcClient();
            var t = provider.TextFilter(new TextFilterRequest( "abb"));
            Assert.AreEqual(0, t.Code);
            t = provider.HtmlFilter(new TextFilterRequest("abb"));
            Assert.AreEqual(0, t.Code);
            t = provider.JsonFilter(new TextFilterRequest("abb"));
            Assert.AreEqual(0, t.Code);
            t = provider.MarkdownFilter(new TextFilterRequest("abb"));
            Assert.AreEqual(0, t.Code);

            t = provider.MarkdownFilter(new TextFilterRequest("习近平"));
            Assert.AreEqual(0, t.Code);


            //var t = provider.TextFilter(new TextFindAllGrpcRequest() { Txt = "abb" });
            //Assert.AreEqual(0, t.Code);
            //t = provider.HtmlFilter(new TextFindAllGrpcRequest() { Txt = "abb" });
            //Assert.AreEqual(0, t.Code);
            //t = provider.JsonFilter(new TextFindAllGrpcRequest() { Txt = "abb" });
            //Assert.AreEqual(0, t.Code);
            //t = provider.MarkdownFilter(new TextFindAllGrpcRequest() { Txt = "abb" });
            //Assert.AreEqual(0, t.Code);

            //t = provider.TextFilter(new TextFindAllGrpcRequest() { Txt = "习近平" });
            Assert.AreEqual(0, t.Code);
            Assert.AreEqual("REJECT", t.RiskLevel);

        }
        [Test]
        public void TextReplace_link()
        {
            var provider = TextFilterConfig.Instance.CreateTextFilterGrpcClient();

            var t = provider.TextReplace(new TextReplaceRequest( "abb"));
            Assert.AreEqual(0, t.Code);
            t = provider.HtmlReplace(new TextReplaceRequest("abb"));
            Assert.AreEqual(0, t.Code);
            t = provider.JsonReplace(new TextReplaceRequest("abb"));
            Assert.AreEqual(0, t.Code);
            t = provider.MarkdownReplace(new TextReplaceRequest("abb"));
            Assert.AreEqual(0, t.Code);




           
        }

    }
}
