package toolgood.textfilter.api.Tests;

import static org.junit.Assert.assertEquals;

import org.junit.Test;

import toolgood.textfilter.api.TextFilterConfig;
import toolgood.textfilter.api.Datas.Requests.TextFilterRequest;
import toolgood.textfilter.api.Datas.Requests.TextReplaceRequest;
import toolgood.textfilter.api.Datas.Texts.TextFilterResult;
import toolgood.textfilter.api.Datas.Texts.TextReplaceResult;
import toolgood.textfilter.api.Interfaces.ITextFilterProvider;

public class TextFilterProviderTest {

    @Test
    public void TextFilter_link() {
        ITextFilterProvider provider = TextFilterConfig.Instance().CreateTextFilterProvider();
        TextFilterResult t = provider.TextFilter(new TextFilterRequest("abb", false, false));
        assertEquals(0, t.code);
        t = provider.HtmlFilter(new TextFilterRequest("abb", false, false));
        assertEquals(0, t.code);
        t = provider.JsonFilter(new TextFilterRequest("abb", false, false));
        assertEquals(0, t.code);
        t = provider.MarkdownFilter(new TextFilterRequest("abb", false, false));
        assertEquals(0, t.code);

        t = provider.TextFilter(new TextFilterRequest("习近平", false, false));
        assertEquals(0, t.code);
        assertEquals("REJECT", t.riskLevel);
    }

    @Test
    public void TextReplace_link() {
        ITextFilterProvider provider = TextFilterConfig.Instance().CreateTextFilterProvider();
        TextReplaceResult t = provider.TextReplace(new TextReplaceRequest("abb", '*', false, false, false, false));
        assertEquals(0, t.code);
        t = provider.HtmlReplace(new TextReplaceRequest("abb", '*', false, false, false, false));
        assertEquals(0, t.code);
        t = provider.JsonReplace(new TextReplaceRequest("abb", '*', false, false, false, false));
        assertEquals(0, t.code);
        t = provider.MarkdownReplace(new TextReplaceRequest("abb", '*', false, false, false, false));
        assertEquals(0, t.code);
    }

}
