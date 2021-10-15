package toolgood.textfilter.api.Tests;

import static org.junit.Assert.assertEquals;

import java.util.List;

import org.junit.Test;

import toolgood.textfilter.api.ServiceUrlType;
import toolgood.textfilter.api.TextFilterConfig;
import toolgood.textfilter.api.Datas.Requests.TextFilterRequest;
import toolgood.textfilter.api.Datas.Texts.TextFilterResult;
import toolgood.textfilter.api.Interfaces.ITextFilterProvider;

public class ConsulTest {

    @Test
    public void GetServerName() {
        TextFilterConfig config = TextFilterConfig.Instance();

        List<String> urls = config.GetServiceUrls(ServiceUrlType.Http);
        assertEquals(1, urls.size());
        assertEquals("http://127.0.0.1:9191", urls.get(0));
    }

    @Test
    public void Test_Http() {
        ITextFilterProvider provider = TextFilterConfig.Instance().CreateTextFilterProvider();
        TextFilterResult result = provider.TextFilter(new TextFilterRequest("abb", false, false));
        assertEquals(result.code, 0);
    }

    @Test
    public void Test_Grpc() {
        ITextFilterProvider provider = TextFilterConfig.Instance().CreateTextFilterGrpcClient();
        TextFilterResult result = provider.TextFilter(new TextFilterRequest("abb", false, false));
        assertEquals(result.code, 0);
    }

}
