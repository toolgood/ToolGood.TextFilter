package toolgood.textfilter.api;

import toolgood.textfilter.api.Datas.Requests.TextFilterRequest;
import toolgood.textfilter.api.Interfaces.ITextFilterProvider;

/**
 * Unit test for simple App.
 */
public class AppTest {

    public static void main(String[] args) {
        TextFilterConfig config = TextFilterConfig.Instance();
        config.SetTextFilterHost("http://localhost:5000");

        ITextFilterProvider textFilterProvider = config.CreateTextFilterProvider();
        textFilterProvider.TextFilter(new TextFilterRequest("123", false, false));
    }

}
